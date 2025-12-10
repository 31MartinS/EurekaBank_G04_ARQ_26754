using System.Security.Cryptography;
using System.Text;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo;

namespace Eurabank_Restfull_SOAP_G04.Data
{
    public static class DbSeeder
    {
        public static void Seed(CalculatorDbContext context)
        {
            // Validar si ya existen datos
            if (!context.Modenas.Any())
            {
                SeedInitialData(context);
            }

            SeedNewClients(context);
        }

        private static void SeedInitialData(CalculatorDbContext context)
        {

            // 1. Monedas
            var soles = new Modena { ChrMonecodigo = "01", VchMonedescripcion = "Soles" };
            var dolares = new Modena { ChrMonecodigo = "02", VchMonedescripcion = "Dolares" };
            context.Modenas.AddRange(soles, dolares);

            // 2. Cargos Mantenimiento
            context.CargosMantenimientos.AddRange(
                new CargoMantenimiento { ChrMonecodigo = "01", DecCargMontoMaximo = 3500.00m, DecCargImporte = 7.00m },
                new CargoMantenimiento { ChrMonecodigo = "02", DecCargMontoMaximo = 1200.00m, DecCargImporte = 2.50m }
            );

            // 3. Costo Movimiento
            context.CostosMovimientos.AddRange(
                new CostoMovimiento { ChrMonecodigo = "01", DecCostimporte = 2.00m },
                new CostoMovimiento { ChrMonecodigo = "02", DecCostimporte = 0.60m }
            );

            // 4. Interes Mensual
            context.InteresesMensuales.AddRange(
                new InteresMensual { ChrMonecodigo = "01", DecInteimporte = 0.70m },
                new InteresMensual { ChrMonecodigo = "02", DecInteimporte = 0.60m }
            );

            // 5. Tipo Movimiento
            context.TiposMovimientos.AddRange(
               new TipoMovimiento { ChrTipocodigo = "001", VchTipodescripcion = "Apertura de cuenta", VchTipoaccion = "INGRESO", VchTipoestado = "ACTIVO" },
               new TipoMovimiento { ChrTipocodigo = "002", VchTipodescripcion = "Cancelar cuenta", VchTipoaccion = "SALIDA", VchTipoestado = "ACTIVO" },
               new TipoMovimiento { ChrTipocodigo = "003", VchTipodescripcion = "Deposito", VchTipoaccion = "INGRESO", VchTipoestado = "ACTIVO" },
               new TipoMovimiento { ChrTipocodigo = "004", VchTipodescripcion = "Retiro", VchTipoaccion = "SALIDA", VchTipoestado = "ACTIVO" },
               new TipoMovimiento { ChrTipocodigo = "005", VchTipodescripcion = "Interes", VchTipoaccion = "INGRESO", VchTipoestado = "ACTIVO" },
               new TipoMovimiento { ChrTipocodigo = "006", VchTipodescripcion = "Mantenimiento", VchTipoaccion = "SALIDA", VchTipoestado = "ACTIVO" },
               new TipoMovimiento { ChrTipocodigo = "007", VchTipodescripcion = "ITF", VchTipoaccion = "SALIDA", VchTipoestado = "ACTIVO" },
               new TipoMovimiento { ChrTipocodigo = "008", VchTipodescripcion = "Transferencia", VchTipoaccion = "INGRESO", VchTipoestado = "ACTIVO" },
               new TipoMovimiento { ChrTipocodigo = "009", VchTipodescripcion = "Transferencia", VchTipoaccion = "SALIDA", VchTipoestado = "ACTIVO" },
               new TipoMovimiento { ChrTipocodigo = "010", VchTipodescripcion = "Cargo por movimiento", VchTipoaccion = "SALIDA", VchTipoestado = "ACTIVO" }
            );

            // 6. Sucursal
            context.Sucursales.AddRange(
                new Sucursal { ChrSucucodigo = "001", VchSucunombre = "Sipan", VchSucuciudad = "Chiclayo", VchSucudireccion = "Av. Balta 1456", IntSucucontcuenta = 2 },
                new Sucursal { ChrSucucodigo = "002", VchSucunombre = "Chan Chan", VchSucuciudad = "Trujillo", VchSucudireccion = "Jr. Independencia 456", IntSucucontcuenta = 3 },
                new Sucursal { ChrSucucodigo = "003", VchSucunombre = "Los Olivos", VchSucuciudad = "Lima", VchSucudireccion = "Av. Central 1234", IntSucucontcuenta = 0 },
                new Sucursal { ChrSucucodigo = "004", VchSucunombre = "Pardo", VchSucuciudad = "Lima", VchSucudireccion = "Av. Pardo 345 - Miraflores", IntSucucontcuenta = 0 }
            );

            // 7. Parametros
            context.Parametros.AddRange(
                new Parametro { ChrParacodigo = "001", VchParadescripcion = "ITF - Impuesto a la Transacciones Financieras", VchParavalor = "0.08", VchParaestado = "ACTIVO" },
                new Parametro { ChrParacodigo = "002", VchParadescripcion = "Número de Operaciones Sin Costo", VchParavalor = "15", VchParaestado = "ACTIVO" }
            );

            // 8. Contadores
            context.Contadores.AddRange(
                new Contador { VchConttabla = "modena", IntContitem = 2, IntContlongitud = 2 },
                new Contador { VchConttabla = "tipomovimiento", IntContitem = 10, IntContlongitud = 3 },
                new Contador { VchConttabla = "sucursal", IntContitem = 7, IntContlongitud = 3 },
                new Contador { VchConttabla = "empleado", IntContitem = 14, IntContlongitud = 4 },
                new Contador { VchConttabla = "asignado", IntContitem = 11, IntContlongitud = 6 },
                new Contador { VchConttabla = "parametro", IntContitem = 2, IntContlongitud = 3 },
                new Contador { VchConttabla = "cliente", IntContitem = 20, IntContlongitud = 5 }
            );

            // 9. Empleados y Usuarios
            var empMonster = new Empleado { ChrEmplcodigo = "0012", VchEmplpaterno = "Mendoza", VchEmplmaterno = "Jara", VchEmplnombre = "Monica Valeria", VchEmplciudad = "Lima", VchEmpldireccion = "Calle Las Toronjas 450" };
            var empAdmin = new Empleado { ChrEmplcodigo = "0001", VchEmplpaterno = "Romero", VchEmplmaterno = "Castillo", VchEmplnombre = "Carlos Alberto", VchEmplciudad = "Trujillo", VchEmpldireccion = "Call1 1 Nro. 456" };
            
            context.Empleados.AddRange(empMonster, empAdmin);
            
            // Usuarios con Claves Hasheadas (SHA1 Hex)
            // MONSTER / MONSTER9
            // cromero / chicho
            
            string hashMonster = ComputeSha1Hex("MONSTER9");
            string hashAdmin = ComputeSha1Hex("chicho");

            context.Usuarios.AddRange(
                new Usuario { ChrEmplcodigo = "0012", VchEmplusuario = "MONSTER", VchEmplclave = hashMonster, VchEmplestado = "ACTIVO" },
                new Usuario { ChrEmplcodigo = "0001", VchEmplusuario = "cromero", VchEmplclave = hashAdmin, VchEmplestado = "ACTIVO" }
            );

            // 10. Clientes y Cuentas (Ejemplo básico)
            context.Clientes.AddRange(
                new Cliente { ChrCliecodigo = "00001", VchCliepaterno = "CORONEL", VchCliematerno = "CASTILLO", VchClienombre = "ERIC GUSTAVO", ChrCliedni = "06914897", VchClieciudad = "LIMA", VchCliedireccion = "LOS OLIVOS", VchClietelefono = "996-664-457", VchClieemail = "gcoronelc@gmail.com" },
                new Cliente { ChrCliecodigo = "00002", VchCliepaterno = "VALENCIA", VchCliematerno = "MORALES", VchClienombre = "PEDRO HUGO", ChrCliedni = "01576173", VchClieciudad = "LIMA", VchCliedireccion = "MAGDALENA", VchClietelefono = "924-7834", VchClieemail = "pvalencia@terra.com.pe" },
                new Cliente { ChrCliecodigo = "00003", VchCliepaterno = "MARCELO", VchCliematerno = "VILLALOBOS", VchClienombre = "RICARDO", ChrCliedni = "10762367", VchClieciudad = "LIMA", VchCliedireccion = "LINCE", VchClietelefono = "993-62966", VchClieemail = "ricardomarcelo@hotmail.com" },
                new Cliente { ChrCliecodigo = "00004", VchCliepaterno = "ROMERO", VchCliematerno = "CASTILLO", VchClienombre = "CARLOS ALBERTO", ChrCliedni = "06531983", VchClieciudad = "LIMA", VchCliedireccion = "LOS OLIVOS", VchClietelefono = "865-84762", VchClieemail = "c.romero@hotmail.com" },
                new Cliente { ChrCliecodigo = "00005", VchCliepaterno = "ARANDA", VchCliematerno = "LUNA", VchClienombre = "ALAN ALBERTO", ChrCliedni = "10875611", VchClieciudad = "LIMA", VchCliedireccion = "SAN ISIDRO", VchClietelefono = "834-67125", VchClieemail = "a.aranda@hotmail.com" },
                new Cliente { ChrCliecodigo = "00006", VchCliepaterno = "AYALA", VchCliematerno = "PAZ", VchClienombre = "JORGE LUIS", ChrCliedni = "10679245", VchClieciudad = "LIMA", VchCliedireccion = "SAN BORJA", VchClietelefono = "963-34769", VchClieemail = "j.ayala@yahoo.com" },
                new Cliente { ChrCliecodigo = "00007", VchCliepaterno = "CHAVEZ", VchCliematerno = "CANALES", VchClienombre = "EDGAR RAFAEL", ChrCliedni = "10145693", VchClieciudad = "LIMA", VchCliedireccion = "MIRAFLORES", VchClietelefono = "999-96673", VchClieemail = "e.chavez@gmail.com" },
                new Cliente { ChrCliecodigo = "00008", VchCliepaterno = "FLORES", VchCliematerno = "CHAFLOQUE", VchClienombre = "ROSA LIZET", ChrCliedni = "10773456", VchClieciudad = "LIMA", VchCliedireccion = "LA MOLINA", VchClietelefono = "966-87567", VchClieemail = "r.florez@hotmail.com" },
                new Cliente { ChrCliecodigo = "00009", VchCliepaterno = "FLORES", VchCliematerno = "CASTILLO", VchClienombre = "CRISTIAN RAFAEL", ChrCliedni = "10346723", VchClieciudad = "LIMA", VchCliedireccion = "LOS OLIVOS", VchClietelefono = "978-43768", VchClieemail = "c.flores@hotmail.com" },
                new Cliente { ChrCliecodigo = "00010", VchCliepaterno = "GONZALES", VchCliematerno = "GARCIA", VchClienombre = "GABRIEL ALEJANDRO", ChrCliedni = "10192376", VchClieciudad = "LIMA", VchCliedireccion = "SAN MIGUEL", VchClietelefono = "945-56782", VchClieemail = "g.gonzales@yahoo.es" },
                new Cliente { ChrCliecodigo = "00011", VchCliepaterno = "LAY", VchCliematerno = "VALLEJOS", VchClienombre = "JUAN CARLOS", ChrCliedni = "10942287", VchClieciudad = "LIMA", VchCliedireccion = "LINCE", VchClietelefono = "956-12657", VchClieemail = "j.lay@peru.com" },
                new Cliente { ChrCliecodigo = "00012", VchCliepaterno = "MONTALVO", VchCliematerno = "SOTO", VchClienombre = "DEYSI LIDIA", ChrCliedni = "10612376", VchClieciudad = "LIMA", VchCliedireccion = "SURCO", VchClietelefono = "965-67235", VchClieemail = "d.montalvo@hotmail.com" },
                new Cliente { ChrCliecodigo = "00013", VchCliepaterno = "RICALDE", VchCliematerno = "RAMIREZ", VchClienombre = "ROSARIO ESMERALDA", ChrCliedni = "10761324", VchClieciudad = "LIMA", VchCliedireccion = "MIRAFLORES", VchClietelefono = "991-23546", VchClieemail = "r.ricalde@gmail.com" },
                new Cliente { ChrCliecodigo = "00014", VchCliepaterno = "RODRIGUEZ", VchCliematerno = "FLORES", VchClienombre = "ENRIQUE MANUEL", ChrCliedni = "10773345", VchClieciudad = "LIMA", VchCliedireccion = "LINCE", VchClietelefono = "976-82838", VchClieemail = "e.rodriguez@gmail.com" },
                new Cliente { ChrCliecodigo = "00015", VchCliepaterno = "ROJAS", VchCliematerno = "OSCANOA", VchClienombre = "FELIX NINO", ChrCliedni = "10238943", VchClieciudad = "LIMA", VchCliedireccion = "LIMA", VchClietelefono = "962-32158", VchClieemail = "f.rojas@yahoo.com" },
                new Cliente { ChrCliecodigo = "00016", VchCliepaterno = "TEJADA", VchCliematerno = "DEL AGUILA", VchClienombre = "TANIA LORENA", ChrCliedni = "10446791", VchClieciudad = "LIMA", VchCliedireccion = "PUEBLO LIBRE", VchClietelefono = "966-23854", VchClieemail = "t.tejada@hotmail.com" },
                new Cliente { ChrCliecodigo = "00017", VchCliepaterno = "VALDEVIESO", VchCliematerno = "LEYVA", VchClienombre = "LIDIA ROXANA", ChrCliedni = "10452682", VchClieciudad = "LIMA", VchCliedireccion = "SURCO", VchClietelefono = "956-78951", VchClieemail = "r.valdivieso@terra.com.pe" },
                new Cliente { ChrCliecodigo = "00018", VchCliepaterno = "VALENTIN", VchCliematerno = "COTRINA", VchClienombre = "JUAN DIEGO", ChrCliedni = "10398247", VchClieciudad = "LIMA", VchCliedireccion = "LA MOLINA", VchClietelefono = "921-12456", VchClieemail = "j.valentin@terra.com.pe" },
                new Cliente { ChrCliecodigo = "00019", VchCliepaterno = "YAURICASA", VchCliematerno = "BAUTISTA", VchClienombre = "YESABETH", ChrCliedni = "10934584", VchClieciudad = "LIMA", VchCliedireccion = "MAGDALENA", VchClietelefono = "977-75777", VchClieemail = "y.yauricasa@terra.com.pe" },
                new Cliente { ChrCliecodigo = "00020", VchCliepaterno = "ZEGARRA", VchCliematerno = "GARCIA", VchClienombre = "FERNANDO MOISES", ChrCliedni = "10772365", VchClieciudad = "LIMA", VchCliedireccion = "SAN ISIDRO", VchClietelefono = "936-45876", VchClieemail = "f.zegarra@hotmail.com" }
            );

            context.Cuentas.Add(new Cuenta {
                ChrCuencodigo = "00100001",
                ChrMonecodigo = "01",
                ChrSucucodigo = "001",
                ChrEmplcreacuenta = "0001", // Asumiendo creado por admin
                ChrCliecodigo = "00001",
                DecCuensaldo = 6900.00m,
                DttCuenfechacreacion = DateTime.UtcNow.AddYears(-2),
                VchCuenestado = "ACTIVO",
                IntCuencontmov = 0,
                ChrCuenclave = "123456"
            });

            context.SaveChanges();
        }

        private static void SeedNewClients(CalculatorDbContext context)
        {
            // Adding 5 new active clients (Ecuadorian context)
            var newClients = new List<Cliente>
            {
                new Cliente { ChrCliecodigo = "90001", VchCliepaterno = "BENITEZ", VchCliematerno = "PAREDES", VchClienombre = "JUAN CARLOS", ChrCliedni = "17100001", VchClieciudad = "QUITO", VchCliedireccion = "AV AMAZONAS Y NACIONES UNIDAS", VchClietelefono = "0991234567", VchClieemail = "juan.benitez@email.ec" },
                new Cliente { ChrCliecodigo = "90002", VchCliepaterno = "VINTIMILLA", VchCliematerno = "CORDERO", VchClienombre = "MARIA AUGUSTA", ChrCliedni = "01020002", VchClieciudad = "CUENCA", VchCliedireccion = "CALLE LARGA Y BENIGNO MALO", VchClietelefono = "0987654321", VchClieemail = "m.vintimilla@email.ec" },
                new Cliente { ChrCliecodigo = "90003", VchCliepaterno = "ANDRADE", VchCliematerno = "LOPEZ", VchClienombre = "ROBERTO JAVIER", ChrCliedni = "09130003", VchClieciudad = "GUAYAQUIL", VchCliedireccion = "AV 9 DE OCTUBRE", VchClietelefono = "0998877665", VchClieemail = "roberto.andrade@email.ec" },
                new Cliente { ChrCliecodigo = "90004", VchCliepaterno = "PROAÑO", VchCliematerno = "SALAZAR", VchClienombre = "SOFIA ELENA", ChrCliedni = "18040004", VchClieciudad = "AMBATO", VchCliedireccion = "AV CEVALLOS", VchClietelefono = "0981122334", VchClieemail = "sofia.proano@email.ec" },
                new Cliente { ChrCliecodigo = "90005", VchCliepaterno = "VALENCIA", VchCliematerno = "ARROYO", VchClienombre = "LUIS ANTONIO", ChrCliedni = "08010005", VchClieciudad = "ESMERALDAS", VchCliedireccion = "LAS PALMAS", VchClietelefono = "0995544332", VchClieemail = "lvalencia@email.ec" }
            };

            foreach (var client in newClients)
            {
                var existingClient = context.Clientes.FirstOrDefault(c => c.ChrCliecodigo == client.ChrCliecodigo);
                if (existingClient == null)
                {
                    // INSERT new client ONLY if it doesn't exist
                    context.Clientes.Add(client);
                    
                    // Add active account for this client
                    var accountCode = "800" + client.ChrCliecodigo; 
                    context.Cuentas.Add(new Cuenta {
                        ChrCuencodigo = accountCode, 
                        ChrMonecodigo = "02", // DOLARES
                        ChrSucucodigo = "001",
                        ChrEmplcreacuenta = "0001",
                        ChrCliecodigo = client.ChrCliecodigo,
                        DecCuensaldo = 500.00m, // Initial balance in USD
                        DttCuenfechacreacion = DateTime.UtcNow,
                        VchCuenestado = "ACTIVO",
                        IntCuencontmov = 0,
                        ChrCuenclave = "123456"
                    });
                }
                // Si ya existe, NO HACEMOS NADA (Respetamos los datos actuales)
            }
            
            context.SaveChanges();

        }

        private static string ComputeSha1Hex(string input)
        {
            using (var sha1 = SHA1.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(bytes);
                return Convert.ToHexString(hashBytes).ToUpper();
            }
        }
    }
}
