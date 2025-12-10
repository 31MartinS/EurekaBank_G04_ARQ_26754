using Microsoft.AspNetCore.Mvc;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.service;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.controlador
{
    [ApiController]
    [Route("api/[controller]")]
    public class SucursalesController : ControllerBase
    {
        private readonly ISucursalService _sucursalService;

        public SucursalesController(ISucursalService sucursalService)
        {
            _sucursalService = sucursalService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var sucursales = await _sucursalService.ObtenerTodasLasSucursalesAsync();
            return Ok(sucursales);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> ObtenerPorCodigo(string codigo)
        {
            var sucursal = await _sucursalService.ObtenerSucursalPorCodigoAsync(codigo);
            if (sucursal == null)
                return NotFound(new { mensaje = "Sucursal no encontrada" });
            
            return Ok(sucursal);
        }
    }
}
