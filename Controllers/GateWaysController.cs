using System;
using System.Linq;
using System.Threading.Tasks;
using GatewayMusalaTest.Data;
using Microsoft.AspNetCore.Mvc;
using GatewayMusalaTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GatewayMusalaTest.Controllers
{
    // http://localhost:5000/gateways
    [ApiController]
    [Route("[controller]")]
    public class GateWaysController: ControllerBase
    {
        private readonly DataContext _context;
        
        public GateWaysController(DataContext context)
        {
        
        this._context = context;        }

        //GET /gateways
        [HttpGet]
        public async Task<IActionResult> GetGateways()
        {
            var gateways = await _context.Gateway.ToListAsync();
            return Ok(gateways);
        }
        
        //GET /gateway
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGateway(int id)
        {
            var gateway = await _context.Gateway.FirstOrDefaultAsync(x => x.id == id);
            return Ok(gateway);
        }
      
        [HttpPost("createGateway")]
        public async Task<IActionResult> CreateGateway(GateWay gateway)
        {
            if( await _context.Gateway.AnyAsync(x => x.serialNumber == gateway.serialNumber)){
                 return BadRequest("Gateway already Exist");
            }
            var GateWayToCreate = new GateWay{
                name = gateway.name,
                iPv4Address = gateway.iPv4Address,
                serialNumber = gateway.serialNumber
            };

            _context.Gateway.Add(GateWayToCreate);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGateway(int id, GateWay gateway)
        {
            if (id != gateway.id)
            {
                return BadRequest();
            }

            var foundedGateway = await _context.Gateway.FindAsync(id);
            if (foundedGateway == null)
            {
                return NotFound();
            }

            foundedGateway.name = gateway.name;
            foundedGateway.serialNumber = gateway.serialNumber;
            foundedGateway.iPv4Address = gateway.iPv4Address;

            var countPeripherals = CountPeripheralsByGateway(id);
            if (countPeripherals > 10){
                return StatusCode(501);
            }
            Console.WriteLine(countPeripherals);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!GateWayExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }  

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGateway(int id)
        {
            var gateway = await _context.Gateway.FindAsync(id);

            if (gateway == null)
            {
                return NotFound();
            }

            _context.Gateway.Remove(gateway);
            await _context.SaveChangesAsync();

            return NoContent();
        }
            
        private bool GateWayExists(int id) => _context.Gateway.Any(e => e.id == id);  

        private int CountPeripheralsByGateway( int gatewayId) =>_context.Peripheral.Count(x => x.IdGateway == gatewayId);

        private List<Peripheral> peripheralsByGateway(int gatewayId) => _context.Peripheral.Where(x => x.IdGateway == gatewayId).ToList();
          
    }
}