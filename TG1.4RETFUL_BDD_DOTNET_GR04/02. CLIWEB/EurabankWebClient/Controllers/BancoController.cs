using Microsoft.AspNetCore.Mvc;
using EurabankWebClient.Models;
using EurabankWebClient.Services;

namespace EurabankWebClient.Controllers
{
    public class BancoController : Controller
    {
        private readonly EurabankApiService _apiService;

        public BancoController(EurabankApiService apiService)
        {
            _apiService = apiService;
        }

        private bool EstaAutenticado()
        {
            return HttpContext.Session.GetString("Autenticado") == "true";
        }

        // GET: Banco/Index - Lista de Usuarios
        public async Task<IActionResult> Index()
        {
            if (!EstaAutenticado())
                return RedirectToAction("Login", "Auth");

            try
            {
                // Obtener todas las cuentas
                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                
                // Convertir a ClienteViewModel
                var clientes = cuentas
                    .GroupBy(c => c.CodigoCliente)
                    .Select(g => new ClienteViewModel
                    {
                        Codigo = g.First().CodigoCliente,
                        Nombre = g.First().NombreCliente,
                        Apellido = "", // Puedes ajustar según tu modelo
                        Saldo = g.Sum(c => c.Saldo) // Suma de saldos de todas las cuentas
                    })
                    .ToList();

                return View(clientes);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al cargar clientes: {ex.Message}";
                return View(new List<ClienteViewModel>());
            }
        }

        // GET: Banco/DepositoRetiro
        public IActionResult DepositoRetiro(string codigo, string nombre, string tipo)
        {
            if (!EstaAutenticado())
                return RedirectToAction("Login", "Auth");

            var model = new DepositoRetiroViewModel
            {
                CodigoCliente = codigo,
                NombreCliente = nombre,
                TipoOperacion = tipo.ToUpper()
            };

            return View(model);
        }

        // POST: Banco/DepositoRetiro
        [HttpPost]
        public async Task<IActionResult> DepositoRetiro(DepositoRetiroViewModel model)
        {
            if (!EstaAutenticado())
                return RedirectToAction("Login", "Auth");

            try
            {
                if (model.Monto <= 0)
                {
                    model.ErrorMessage = "El monto debe ser mayor a 0";
                    return View(model);
                }

                // Obtener la primera cuenta del cliente
                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                var cuenta = cuentas.FirstOrDefault(c => c.CodigoCliente == model.CodigoCliente);

                if (cuenta == null)
                {
                    model.ErrorMessage = "No se encontró cuenta para este cliente";
                    return View(model);
                }

                var movimientoDto = new RealizarMovimientoDto
                {
                    CodigoCuenta = cuenta.Codigo,
                    CodigoEmpleado = "0001", // Código válido de empleado
                    Importe = model.Monto,
                    CodigoTipo = model.TipoOperacion == "DEPOSITO" ? "003" : "004" // 003=Depósito, 004=Retiro
                };

                if (model.TipoOperacion == "DEPOSITO")
                {
                    await _apiService.RealizarDepositoAsync(movimientoDto);
                    TempData["Mensaje"] = $"Depósito de ${model.Monto:N2} realizado con éxito";
                }
                else
                {
                    await _apiService.RealizarRetiroAsync(movimientoDto);
                    TempData["Mensaje"] = $"Retiro de ${model.Monto:N2} realizado con éxito";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Error: {ex.Message}";
                return View(model);
            }
        }

        // GET: Banco/Transferencia
        public async Task<IActionResult> Transferencia(string codigoOrigen, string nombreOrigen)
        {
            if (!EstaAutenticado())
                return RedirectToAction("Login", "Auth");

            try
            {
                // Obtener todos los clientes para el destino
                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                var clientes = cuentas
                    .GroupBy(c => c.CodigoCliente)
                    .Select(g => new ClienteViewModel
                    {
                        Codigo = g.First().CodigoCliente,
                        Nombre = g.First().NombreCliente,
                        Apellido = "",
                        Saldo = g.Sum(c => c.Saldo)
                    })
                    .Where(c => c.Codigo != codigoOrigen) // Excluir el origen
                    .ToList();

                var model = new TransferenciaViewModel
                {
                    CodigoClienteOrigen = codigoOrigen,
                    NombreClienteOrigen = nombreOrigen,
                    ClientesDisponibles = clientes
                };

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error: {ex.Message}";
                return View(new TransferenciaViewModel());
            }
        }

        // POST: Banco/Transferencia
        [HttpPost]
        public async Task<IActionResult> Transferencia(TransferenciaViewModel model)
        {
            if (!EstaAutenticado())
                return RedirectToAction("Login", "Auth");

            try
            {
                if (model.Monto <= 0)
                {
                    model.ErrorMessage = "El monto debe ser mayor a 0";
                    // Recargar lista de clientes
                    var cuentasTemp = await _apiService.ObtenerTodasLasCuentasAsync();
                    model.ClientesDisponibles = cuentasTemp
                        .GroupBy(c => c.CodigoCliente)
                        .Select(g => new ClienteViewModel
                        {
                            Codigo = g.First().CodigoCliente,
                            Nombre = g.First().NombreCliente
                        })
                        .Where(c => c.Codigo != model.CodigoClienteOrigen)
                        .ToList();
                    return View(model);
                }

                if (string.IsNullOrEmpty(model.CodigoClienteDestino))
                {
                    model.ErrorMessage = "Seleccione un usuario destino";
                    var cuentasTemp = await _apiService.ObtenerTodasLasCuentasAsync();
                    model.ClientesDisponibles = cuentasTemp
                        .GroupBy(c => c.CodigoCliente)
                        .Select(g => new ClienteViewModel
                        {
                            Codigo = g.First().CodigoCliente,
                            Nombre = g.First().NombreCliente
                        })
                        .Where(c => c.Codigo != model.CodigoClienteOrigen)
                        .ToList();
                    return View(model);
                }

                // Obtener cuentas de origen y destino
                var cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                var cuentaOrigen = cuentas.FirstOrDefault(c => c.CodigoCliente == model.CodigoClienteOrigen);
                var cuentaDestino = cuentas.FirstOrDefault(c => c.CodigoCliente == model.CodigoClienteDestino);

                if (cuentaOrigen == null || cuentaDestino == null)
                {
                    model.ErrorMessage = "No se encontraron las cuentas";
                    return View(model);
                }

                var transferenciaDto = new RealizarTransferenciaDto
                {
                    CodigoCuentaOrigen = cuentaOrigen.Codigo,
                    CodigoCuentaDestino = cuentaDestino.Codigo,
                    CodigoEmpleado = "0001", // Código válido de empleado
                    CodigoTipo = "009", // 009=Transferencia (salida del origen)
                    Importe = model.Monto
                };

                await _apiService.RealizarTransferenciaAsync(transferenciaDto);
                TempData["Mensaje"] = $"Transferencia de ${model.Monto:N2} realizada con éxito";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Error: {ex.Message}";
                return View(model);
            }
        }

        // GET: Banco/Movimientos
        public async Task<IActionResult> Movimientos(string? codigoCliente, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            if (!EstaAutenticado())
                return RedirectToAction("Login", "Auth");

            try
            {
                var model = new ListadoMovimientosViewModel
                {
                    CodigoClienteFiltro = codigoCliente,
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta
                };

                // Obtener todas las cuentas
                var todasLasCuentas = await _apiService.ObtenerTodasLasCuentasAsync();
                
                // Poblar lista de clientes disponibles para filtro
                model.ClientesDisponibles = todasLasCuentas
                    .GroupBy(c => c.CodigoCliente)
                    .Select(g => new ClienteViewModel
                    {
                        Codigo = g.First().CodigoCliente,
                        Nombre = g.First().NombreCliente,
                        Apellido = "",
                        Saldo = g.Sum(c => c.Saldo)
                    })
                    .ToList();

                var cuentas = todasLasCuentas.ToList();

                // Filtrar por cliente si se especifica
                if (!string.IsNullOrEmpty(codigoCliente))
                {
                    cuentas = cuentas.Where(c => c.CodigoCliente == codigoCliente).ToList();
                    model.FiltroUsuario = codigoCliente;
                }

                // Obtener movimientos de cada cuenta
                var todosMovimientos = new List<MovimientoDto>();
                foreach (var cuenta in cuentas)
                {
                    try
                    {
                        var movimientos = await _apiService.ObtenerMovimientosPorCuentaAsync(cuenta.Codigo);
                        todosMovimientos.AddRange(movimientos);
                    }
                    catch
                    {
                        // Si no hay movimientos para esta cuenta, continuar
                        continue;
                    }
                }

                // Convertir a ViewModel
                model.Movimientos = todosMovimientos
                    .Select(m => new MovimientoViewModel
                    {
                        Fecha = m.Fecha,
                        Usuario = todasLasCuentas.FirstOrDefault(c => c.Codigo == m.CodigoCuenta)?.NombreCliente ?? "Desconocido",
                        NombreCliente = todasLasCuentas.FirstOrDefault(c => c.Codigo == m.CodigoCuenta)?.NombreCliente ?? "Desconocido",
                        TipoMovimiento = m.NombreTipo,
                        TipoAccion = m.CodigoTipo == "DEP" ? "INGRESO" : "SALIDA",
                        Importe = m.Importe,
                        Monto = m.Importe,
                        Saldo = 0 // El saldo lo obtendremos después
                    })
                    .OrderByDescending(m => m.Fecha)
                    .ToList();

                // Aplicar filtros de fecha si se especificaron
                if (fechaDesde.HasValue)
                {
                    model.Movimientos = model.Movimientos.Where(m => m.Fecha >= fechaDesde.Value).ToList();
                }
                if (fechaHasta.HasValue)
                {
                    model.Movimientos = model.Movimientos.Where(m => m.Fecha <= fechaHasta.Value.AddDays(1)).ToList();
                }

                // Calcular saldo acumulado
                decimal saldoAcumulado = 0;
                foreach (var mov in model.Movimientos.OrderBy(m => m.Fecha))
                {
                    if (mov.TipoAccion == "INGRESO")
                        saldoAcumulado += mov.Importe;
                    else
                        saldoAcumulado -= mov.Importe;
                    mov.Saldo = saldoAcumulado;
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error: {ex.Message}";
                return View(new ListadoMovimientosViewModel());
            }
        }
    }
}
