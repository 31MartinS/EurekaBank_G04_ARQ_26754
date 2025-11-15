using EurabankConsoleClient.Models;
using EurabankConsoleClient.Services;

namespace EurabankConsoleClient
{
    class Program
    {
        private static EurabankApiService _apiService = new EurabankApiService();
        private static List<CuentaDto> _cuentas = new List<CuentaDto>();
        private static string _usuarioActual = string.Empty;

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            if (!await Login())
            {
                Console.WriteLine("\nAcceso denegado. Presione cualquier tecla para salir...");
                Console.ReadKey();
                return;
            }

            await MenuPrincipal();
        }

        static async Task<bool> Login()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   SISTEMA BANCARIO EURABANK");
            Console.WriteLine("   Cliente de Consola");
            Console.WriteLine("========================================");
            Console.WriteLine();
            
            Console.Write("Usuario: ");
            string? usuario = Console.ReadLine();
            
            Console.Write("Contraseña: ");
            string? password = LeerPassword();
            
            Console.WriteLine();

            if (usuario == "MONSTER" && password == "monster9")
            {
                _usuarioActual = usuario;
                Console.WriteLine("\n¡Login exitoso! Bienvenido " + usuario);
                await Task.Delay(1000);
                return true;
            }

            Console.WriteLine("\nCredenciales incorrectas.");
            await Task.Delay(2000);
            return false;
        }

        static string LeerPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            return password;
        }

        static async Task MenuPrincipal()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("   MENU PRINCIPAL - EURABANK");
                Console.WriteLine("   Usuario: " + _usuarioActual);
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("1. Ver Lista de Clientes");
                Console.WriteLine("2. Realizar Depósito");
                Console.WriteLine("3. Realizar Retiro");
                Console.WriteLine("4. Realizar Transferencia");
                Console.WriteLine("5. Ver Movimientos");
                Console.WriteLine("6. Actualizar Datos");
                Console.WriteLine("0. Salir");
                Console.WriteLine();
                Console.Write("Seleccione una opción: ");

                string? opcion = Console.ReadLine();

                try
                {
                    switch (opcion)
                    {
                        case "1":
                            await MostrarClientes();
                            break;
                        case "2":
                            await RealizarDeposito();
                            break;
                        case "3":
                            await RealizarRetiro();
                            break;
                        case "4":
                            await RealizarTransferencia();
                            break;
                        case "5":
                            await VerMovimientos();
                            break;
                        case "6":
                            await ActualizarDatos();
                            break;
                        case "0":
                            salir = true;
                            Console.WriteLine("\nCerrando sesión...");
                            await Task.Delay(1000);
                            break;
                        default:
                            Console.WriteLine("\nOpción inválida. Presione cualquier tecla...");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static async Task MostrarClientes()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   LISTA DE CLIENTES");
            Console.WriteLine("========================================");
            Console.WriteLine();

            await CargarCuentas();

            var clientesAgrupados = _cuentas
                .GroupBy(c => c.CodigoCliente)
                .Select(g => new
                {
                    Codigo = g.Key,
                    Nombre = g.First().NombreCliente,
                    TotalCuentas = g.Count(),
                    SaldoTotal = g.Sum(c => c.Saldo)
                })
                .OrderBy(c => c.Codigo)
                .ToList();

            Console.WriteLine($"{"Código",-8} {"Nombre",-35} {"Cuentas",10} {"Saldo Total",15}");
            Console.WriteLine(new string('-', 70));

            foreach (var cliente in clientesAgrupados)
            {
                Console.WriteLine($"{cliente.Codigo,-8} {cliente.Nombre,-35} {cliente.TotalCuentas,10} {cliente.SaldoTotal,15:C}");
            }

            Console.WriteLine();
            Console.WriteLine($"Total de clientes: {clientesAgrupados.Count}");
            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }

        static async Task RealizarDeposito()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   REALIZAR DEPOSITO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            await CargarCuentas();

            var cliente = await SeleccionarCliente();
            if (cliente == null) return;

            var cuenta = _cuentas.FirstOrDefault(c => c.CodigoCliente == cliente.Value.Codigo);
            if (cuenta == null)
            {
                Console.WriteLine("\nNo se encontró cuenta para este cliente.");
                Console.WriteLine("Presione cualquier tecla...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nCliente: {cliente.Value.Nombre}");
            Console.WriteLine($"Cuenta: {cuenta.Codigo}");
            Console.WriteLine($"Saldo actual: {cuenta.Saldo:C}");
            Console.WriteLine();

            Console.Write("Ingrese el monto a depositar: $");
            if (!decimal.TryParse(Console.ReadLine(), out decimal monto) || monto <= 0)
            {
                Console.WriteLine("\nMonto inválido.");
                Console.WriteLine("Presione cualquier tecla...");
                Console.ReadKey();
                return;
            }

            var dto = new RealizarMovimientoDto
            {
                CodigoCuenta = cuenta.Codigo,
                CodigoEmpleado = "0001",
                CodigoTipo = "003",
                Importe = monto
            };

            var resultado = await _apiService.RealizarDepositoAsync(dto);

            Console.WriteLine($"\n¡Depósito realizado con éxito!");
            Console.WriteLine($"Nuevo saldo: {cuenta.Saldo + monto:C}");
            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }

        static async Task RealizarRetiro()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   REALIZAR RETIRO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            await CargarCuentas();

            var cliente = await SeleccionarCliente();
            if (cliente == null) return;

            var cuenta = _cuentas.FirstOrDefault(c => c.CodigoCliente == cliente.Value.Codigo);
            if (cuenta == null)
            {
                Console.WriteLine("\nNo se encontró cuenta para este cliente.");
                Console.WriteLine("Presione cualquier tecla...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nCliente: {cliente.Value.Nombre}");
            Console.WriteLine($"Cuenta: {cuenta.Codigo}");
            Console.WriteLine($"Saldo actual: {cuenta.Saldo:C}");
            Console.WriteLine();

            Console.Write("Ingrese el monto a retirar: $");
            if (!decimal.TryParse(Console.ReadLine(), out decimal monto) || monto <= 0)
            {
                Console.WriteLine("\nMonto inválido.");
                Console.WriteLine("Presione cualquier tecla...");
                Console.ReadKey();
                return;
            }

            if (monto > cuenta.Saldo)
            {
                Console.WriteLine("\nSaldo insuficiente.");
                Console.WriteLine("Presione cualquier tecla...");
                Console.ReadKey();
                return;
            }

            var dto = new RealizarMovimientoDto
            {
                CodigoCuenta = cuenta.Codigo,
                CodigoEmpleado = "0001",
                CodigoTipo = "004",
                Importe = monto
            };

            var resultado = await _apiService.RealizarRetiroAsync(dto);

            Console.WriteLine($"\n¡Retiro realizado con éxito!");
            Console.WriteLine($"Nuevo saldo: {cuenta.Saldo - monto:C}");
            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }

        static async Task RealizarTransferencia()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   REALIZAR TRANSFERENCIA");
            Console.WriteLine("========================================");
            Console.WriteLine();

            await CargarCuentas();

            Console.WriteLine("CLIENTE ORIGEN:");
            var clienteOrigen = await SeleccionarCliente();
            if (clienteOrigen == null) return;

            var cuentaOrigen = _cuentas.FirstOrDefault(c => c.CodigoCliente == clienteOrigen.Value.Codigo);
            if (cuentaOrigen == null)
            {
                Console.WriteLine("\nNo se encontró cuenta origen.");
                Console.WriteLine("Presione cualquier tecla...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nCliente origen: {clienteOrigen.Value.Nombre}");
            Console.WriteLine($"Saldo disponible: {cuentaOrigen.Saldo:C}");
            Console.WriteLine();

            Console.WriteLine("CLIENTE DESTINO:");
            var clienteDestino = await SeleccionarCliente(clienteOrigen.Value.Codigo);
            if (clienteDestino == null) return;

            var cuentaDestino = _cuentas.FirstOrDefault(c => c.CodigoCliente == clienteDestino.Value.Codigo);
            if (cuentaDestino == null)
            {
                Console.WriteLine("\nNo se encontró cuenta destino.");
                Console.WriteLine("Presione cualquier tecla...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nCliente destino: {clienteDestino.Value.Nombre}");
            Console.WriteLine();

            Console.Write("Ingrese el monto a transferir: $");
            if (!decimal.TryParse(Console.ReadLine(), out decimal monto) || monto <= 0)
            {
                Console.WriteLine("\nMonto inválido.");
                Console.WriteLine("Presione cualquier tecla...");
                Console.ReadKey();
                return;
            }

            if (monto > cuentaOrigen.Saldo)
            {
                Console.WriteLine("\nSaldo insuficiente.");
                Console.WriteLine("Presione cualquier tecla...");
                Console.ReadKey();
                return;
            }

            var dto = new RealizarTransferenciaDto
            {
                CodigoCuentaOrigen = cuentaOrigen.Codigo,
                CodigoCuentaDestino = cuentaDestino.Codigo,
                CodigoEmpleado = "0001",
                CodigoTipo = "009",
                Importe = monto
            };

            var resultado = await _apiService.RealizarTransferenciaAsync(dto);

            Console.WriteLine($"\n¡Transferencia realizada con éxito!");
            Console.WriteLine($"De: {clienteOrigen.Value.Nombre}");
            Console.WriteLine($"Para: {clienteDestino.Value.Nombre}");
            Console.WriteLine($"Monto: {monto:C}");
            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }

        static async Task VerMovimientos()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   VER MOVIMIENTOS DE CUENTA");
            Console.WriteLine("========================================");
            Console.WriteLine();

            await CargarCuentas();

            var cliente = await SeleccionarCliente();
            if (cliente == null) return;

            var cuenta = _cuentas.FirstOrDefault(c => c.CodigoCliente == cliente.Value.Codigo);
            if (cuenta == null)
            {
                Console.WriteLine("\nNo se encontró cuenta para este cliente.");
                Console.WriteLine("Presione cualquier tecla...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\n INFORMACIÓN DE LA CUENTA ");
            Console.WriteLine($" Cliente: {cliente.Value.Nombre,-40}");
            Console.WriteLine($" Cuenta:  {cuenta.Codigo,-40}");
            Console.WriteLine($" Saldo Actual: {cuenta.Saldo,30:C}  ");
            Console.WriteLine($"");
            Console.WriteLine();

            var movimientos = await _apiService.ObtenerMovimientosPorCuentaAsync(cuenta.Codigo);

            if (movimientos.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("");
                Console.WriteLine("  No hay movimientos registrados en esta cuenta.          ");
                Console.WriteLine("");
                Console.ResetColor();
            }
            else
            {
                // Ordenar del más reciente al más antiguo
                var movOrdenados = movimientos.OrderByDescending(m => m.Fecha).ToList();
                
                Console.WriteLine($"Total de movimientos: {movimientos.Count}");
                Console.WriteLine();
                Console.WriteLine("");
                Console.WriteLine("  #        FECHA Y HORA              TIPO             MONTO      SALDO   REFERENCIA   ");
                Console.WriteLine("");

                decimal saldoActual = cuenta.Saldo;
                int numMov = 1;
                
                foreach (var mov in movOrdenados)
                {
                    // Calcular el saldo después de este movimiento
                    decimal saldoDespues = saldoActual;
                    
                    // Determinar si es ingreso o egreso
                    string simbolo;
                    ConsoleColor colorMonto;
                    if (mov.CodigoTipo == "003" || mov.CodigoTipo == "008") // Depósito o Transferencia Entrante
                    {
                        simbolo = "+";
                        colorMonto = ConsoleColor.Green;
                        // Para retroceder, restamos
                        saldoActual -= mov.Importe;
                    }
                    else // Retiro o Transferencia Saliente
                    {
                        simbolo = "-";
                        colorMonto = ConsoleColor.Red;
                        // Para retroceder, sumamos
                        saldoActual += mov.Importe;
                    }

                    string referencia = string.IsNullOrEmpty(mov.CuentaReferencia) ? "---" : mov.CuentaReferencia;
                    string tipo = mov.NombreTipo.Length > 18 ? mov.NombreTipo.Substring(0, 18) : mov.NombreTipo;
                    
                    Console.Write($" {numMov,3}  {mov.Fecha:dd/MM/yyyy HH:mm:ss}  {tipo,-18}  ");
                    Console.ForegroundColor = colorMonto;
                    Console.Write($"{simbolo} {mov.Importe,11:C}");
                    Console.ResetColor();
                    Console.WriteLine($"  {saldoDespues,10:C}  {referencia,-13} ");
                    
                    numMov++;
                }

                Console.WriteLine("");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"ÚLTIMO MOVIMIENTO: {movOrdenados[0].NombreTipo} - {movOrdenados[0].Fecha:dd/MM/yyyy HH:mm:ss}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }

        static async Task<(string Codigo, string Nombre)?> SeleccionarCliente(string? excluirCodigo = null)
        {
            var clientesAgrupados = _cuentas
                .Where(c => excluirCodigo == null || c.CodigoCliente != excluirCodigo)
                .GroupBy(c => c.CodigoCliente)
                .Select(g => new
                {
                    Codigo = g.Key,
                    Nombre = g.First().NombreCliente,
                    Saldo = g.Sum(c => c.Saldo)
                })
                .OrderBy(c => c.Codigo)
                .ToList();

            Console.WriteLine("\nClientes disponibles:");
            for (int i = 0; i < clientesAgrupados.Count; i++)
            {
                Console.WriteLine($"{i + 1,3}. {clientesAgrupados[i].Nombre,-35} Saldo: {clientesAgrupados[i].Saldo,12:C}");
            }

            Console.WriteLine("  0. Cancelar");
            Console.WriteLine();
            Console.Write("Seleccione un cliente: ");

            if (!int.TryParse(Console.ReadLine(), out int opcion))
            {
                Console.WriteLine("\nOpción inválida.");
                await Task.Delay(1000);
                return null;
            }

            if (opcion == 0) return null;

            if (opcion < 1 || opcion > clientesAgrupados.Count)
            {
                Console.WriteLine("\nOpción inválida.");
                await Task.Delay(1000);
                return null;
            }

            var clienteSeleccionado = clientesAgrupados[opcion - 1];
            return (clienteSeleccionado.Codigo, clienteSeleccionado.Nombre);
        }

        static async Task CargarCuentas()
        {
            _cuentas = await _apiService.ObtenerTodasLasCuentasAsync();
        }

        static async Task ActualizarDatos()
        {
            Console.Clear();
            Console.WriteLine("Actualizando datos...");
            await CargarCuentas();
            Console.WriteLine("Datos actualizados correctamente.");
            await Task.Delay(1000);
        }
    }
}

