using Microsoft.EntityFrameworkCore;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo;

namespace Eurabank_Restfull_SOAP_G04.Data
{
    public class CalculatorDbContext : DbContext
    {
        public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options)
            : base(options)
        {
        }

        // DbSets - Representan las tablas en la base de datos
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Modena> Modenas { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<TipoMovimiento> TiposMovimientos { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<CargoMantenimiento> CargosMantenimientos { get; set; }
        public DbSet<CostoMovimiento> CostosMovimientos { get; set; }
        public DbSet<InteresMensual> InteresesMensuales { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Asignado> Asignados { get; set; }
        public DbSet<LogSession> LogSessions { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Contador> Contadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar claves compuestas
            modelBuilder.Entity<Movimiento>()
                .HasKey(m => new { m.ChrCuencodigo, m.IntMovinumero });

            modelBuilder.Entity<Permiso>()
                .HasKey(p => new { p.ChrEmplcodigo, p.IntModucodigo });
        }
    }
}
