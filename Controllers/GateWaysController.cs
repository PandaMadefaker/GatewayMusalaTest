using System.Linq;
using System.Threading.Tasks;
using GatewayMusalaTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

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
            this._context = context;
        }
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
    }
}