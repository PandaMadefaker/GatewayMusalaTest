using System.Linq;
using System.Threading.Tasks;
using GatewayMusalaTest.Data;
using Microsoft.AspNetCore.Mvc;
using GatewayMusalaTest.Models;
using Microsoft.EntityFrameworkCore;

namespace GatewayMusalaTest.Controller
{
     // http://localhost:5000/peripherals
    [ApiController]
    [Route("[controller]")]
    public class PeripheralsController: ControllerBase
    {        
        private readonly DataContext _context;
        public PeripheralsController(DataContext context)
        {
            this._context = context;
        }
        
        //GET /peripherals
        [HttpGet]
        public async Task<IActionResult> GetPeripherals()
        {
            var peripherals = await _context.Peripheral.ToListAsync();
            return Ok(peripherals);
        }
        
        //GET /peripheral
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeripheral(int id)
        {
            var peripheral = await _context.Peripheral.FirstOrDefaultAsync(x => x.id == id);
            return Ok(peripheral);
        }

        [HttpPost("createPeripheral")]
        public async Task<IActionResult> CreatePeripheral(Peripheral peripheral)
        {
            if( await _context.Peripheral.AnyAsync(x => x.uId == peripheral.uId)){
                 return BadRequest("Peripheral already Exist");
            }

            var PeripheralToCreate = new Peripheral{
                uId = peripheral.uId,
                vendor = peripheral.vendor,
                isOnline = peripheral.isOnline,
                dateCreated = peripheral.dateCreated
            };

            _context.Peripheral.Add(PeripheralToCreate);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePeripheral(int id, Peripheral peripheral)
        {
            if (id != peripheral.id)
            {
                return BadRequest();
            }

            var foundedPeripheral = await _context.Peripheral.FindAsync(id);
            if (foundedPeripheral == null)
            {
                return NotFound();
            }

            foundedPeripheral.uId = peripheral.uId;
            foundedPeripheral.vendor = peripheral.vendor;
            foundedPeripheral.dateCreated = peripheral.dateCreated;
            foundedPeripheral.IdGateway = peripheral.IdGateway;
            foundedPeripheral.isOnline = peripheral.isOnline;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PeripheralExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeripheral(int id)
        {
            var peripheral = await _context.Peripheral.FindAsync(id);

            if (peripheral == null)
            {
                return NotFound();
            }

            _context.Peripheral.Remove(peripheral);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeripheralExists(int id) => _context.Peripheral.Any(e => e.id == id);     
    
    }
}