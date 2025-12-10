using Microsoft.AspNetCore.Mvc;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.service;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.controlador
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;

        public CuentasController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var cuentas = await _cuentaService.ObtenerTodasLasCuentasAsync();
            return Ok(cuentas);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> ObtenerPorCodigo(string codigo)
        {
            var cuenta = await _cuentaService.ObtenerCuentaPorCodigoAsync(codigo);
            if (cuenta == null)
                return NotFound(new { mensaje = "Cuenta no encontrada" });
            
            return Ok(cuenta);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearCuentaDto dto)
        {
            try
            {
                var cuenta = await _cuentaService.CrearCuentaAsync(dto);
                return CreatedAtAction(nameof(ObtenerPorCodigo), new { codigo = cuenta.Codigo }, cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{codigo}/estado")]
        public async Task<IActionResult> CambiarEstado(string codigo, [FromBody] string estado)
        {
            try
            {
                var resultado = await _cuentaService.ActualizarEstadoCuentaAsync(codigo, estado);
                if (!resultado)
                    return NotFound(new { mensaje = "Cuenta no encontrada" });
                
                return Ok(new { mensaje = "Estado de cuenta actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{codigo}/saldo")]
        public async Task<IActionResult> ObtenerSaldo(string codigo)
        {
            var saldo = await _cuentaService.ConsultarSaldoAsync(codigo);
            return Ok(new { codigoCuenta = codigo, saldo });
        }
    }
}
