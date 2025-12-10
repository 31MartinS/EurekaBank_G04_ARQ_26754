using Microsoft.AspNetCore.Mvc;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.service;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.controlador
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var clientes = await _clienteService.ObtenerTodosLosClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> ObtenerPorCodigo(string codigo)
        {
            var cliente = await _clienteService.ObtenerClientePorIdAsync(codigo);
            if (cliente == null)
                return NotFound(new { mensaje = "Cliente no encontrado" });
            
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] CrearClienteDto dto)
        {
            try
            {
                var cliente = await _clienteService.CrearClienteAsync(dto);
                return CreatedAtAction(nameof(ObtenerPorCodigo), new { codigo = cliente.Codigo }, cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> ActualizarCliente(string codigo, [FromBody] ActualizarClienteDto dto)
        {
            try
            {
                var actualizado = await _clienteService.ActualizarClienteAsync(codigo, dto);
                if (!actualizado)
                    return NotFound(new { mensaje = "Cliente no encontrado" });
                
                return Ok(new { mensaje = "Cliente actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> EliminarCliente(string codigo)
        {
            try
            {
                var eliminado = await _clienteService.EliminarClienteAsync(codigo);
                if (!eliminado)
                    return NotFound(new { mensaje = "Cliente no encontrado" });
                
                return Ok(new { mensaje = "Cliente eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}