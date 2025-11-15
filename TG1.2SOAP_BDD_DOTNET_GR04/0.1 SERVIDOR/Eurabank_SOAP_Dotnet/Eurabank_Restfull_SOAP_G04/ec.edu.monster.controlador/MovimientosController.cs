using Microsoft.AspNetCore.Mvc;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.service;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.controlador
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoService _movimientoService;

        public MovimientosController(IMovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        [HttpGet("cuenta/{codigoCuenta}")]
        public async Task<IActionResult> ObtenerPorCuenta(string codigoCuenta)
        {
            var movimientos = await _movimientoService.ObtenerMovimientosPorCuentaAsync(codigoCuenta);
            return Ok(movimientos);
        }

        [HttpPost("deposito")]
        public async Task<IActionResult> RealizarDeposito([FromBody] RealizarMovimientoDto dto)
        {
            try
            {
                // Log para debugging
                Console.WriteLine($"[DEPOSITO] Cuenta: '{dto?.CodigoCuenta}', Empleado: '{dto?.CodigoEmpleado}', Tipo: '{dto?.CodigoTipo}', Importe: {dto?.Importe}");
                
                // Validación adicional para debugging
                if (dto == null)
                    return BadRequest(new { mensaje = "Los datos del depósito son requeridos" });
                
                if (string.IsNullOrWhiteSpace(dto.CodigoCuenta))
                    return BadRequest(new { mensaje = $"El código de cuenta es requerido. Recibido: '{dto.CodigoCuenta}'" });
                
                if (dto.Importe <= 0)
                    return BadRequest(new { mensaje = $"El importe debe ser mayor a cero. Recibido: {dto.Importe}" });

                var movimiento = await _movimientoService.RealizarDepositoAsync(dto);
                return Ok(movimiento);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEPOSITO ERROR] {ex.Message}");
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("retiro")]
        public async Task<IActionResult> RealizarRetiro([FromBody] RealizarMovimientoDto dto)
        {
            try
            {
                // Validación adicional para debugging
                if (dto == null)
                    return BadRequest(new { mensaje = "Los datos del retiro son requeridos" });
                
                if (string.IsNullOrWhiteSpace(dto.CodigoCuenta))
                    return BadRequest(new { mensaje = "El código de cuenta es requerido" });
                
                if (dto.Importe <= 0)
                    return BadRequest(new { mensaje = "El importe debe ser mayor a cero" });

                var movimiento = await _movimientoService.RealizarRetiroAsync(dto);
                return Ok(movimiento);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("transferencia")]
        public async Task<IActionResult> RealizarTransferencia([FromBody] RealizarTransferenciaDto dto)
        {
            try
            {
                // Validar que el DTO no sea nulo
                if (dto == null)
                {
                    return BadRequest(new { mensaje = "Los datos de la transferencia son requeridos" });
                }

                // Validar campos requeridos
                if (string.IsNullOrWhiteSpace(dto.CodigoCuentaOrigen))
                {
                    return BadRequest(new { mensaje = "El código de cuenta origen es requerido" });
                }

                if (string.IsNullOrWhiteSpace(dto.CodigoCuentaDestino))
                {
                    return BadRequest(new { mensaje = "El código de cuenta destino es requerido" });
                }

                if (string.IsNullOrWhiteSpace(dto.CodigoEmpleado))
                {
                    return BadRequest(new { mensaje = "El código de empleado es requerido" });
                }

                if (string.IsNullOrWhiteSpace(dto.CodigoTipo))
                {
                    return BadRequest(new { mensaje = "El código de tipo es requerido" });
                }

                if (dto.Importe <= 0)
                {
                    return BadRequest(new { mensaje = "El importe debe ser mayor a cero" });
                }

                var movimiento = await _movimientoService.RealizarTransferenciaAsync(dto);
                return Ok(movimiento);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }

    public class TransferenciaDto
    {
        public int IdCuentaOrigen { get; set; }
        public int IdCuentaDestino { get; set; }
        public decimal Importe { get; set; }
        public int IdUsuario { get; set; }
        public string Observacion { get; set; } = string.Empty;
    }
}
