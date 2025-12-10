# Script para crear y cargar la base de datos Eurabank

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  CREACION DE BASE DE DATOS EURABANK  " -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Verificar si existe sqlcmd
try {
    $sqlcmdVersion = sqlcmd -?
    Write-Host "✓ sqlcmd encontrado" -ForegroundColor Green
} catch {
    Write-Host "✗ ERROR: sqlcmd no está instalado o no está en el PATH" -ForegroundColor Red
    Write-Host "  Instala SQL Server Command Line Tools desde:" -ForegroundColor Yellow
    Write-Host "  https://learn.microsoft.com/en-us/sql/tools/sqlcmd/sqlcmd-utility" -ForegroundColor Yellow
    exit 1
}

$server = "localhost"
$username = "sa"
$password = "comand"

Write-Host "Servidor: $server" -ForegroundColor Yellow
Write-Host "Usuario: $username" -ForegroundColor Yellow
Write-Host ""

# Paso 1: Crear la base de datos y tablas
Write-Host "[1/2] Creando base de datos y tablas..." -ForegroundColor Cyan
try {
    sqlcmd -S $server -U $username -P $password -i ".\SQL\1_crear_bd.sql"
    Write-Host "✓ Base de datos y tablas creadas exitosamente" -ForegroundColor Green
} catch {
    Write-Host "✗ ERROR al crear la base de datos: $_" -ForegroundColor Red
    exit 1
}

Write-Host ""

# Paso 2: Cargar datos de prueba
Write-Host "[2/2] Cargando datos de prueba..." -ForegroundColor Cyan
try {
    sqlcmd -S $server -U $username -P $password -i ".\SQL\2_cargar_datos.sql"
    Write-Host "✓ Datos de prueba cargados exitosamente" -ForegroundColor Green
} catch {
    Write-Host "✗ ERROR al cargar datos: $_" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  ✓ PROCESO COMPLETADO EXITOSAMENTE   " -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Base de datos: EurabankDb" -ForegroundColor Yellow
Write-Host "Tablas creadas: 15" -ForegroundColor Yellow
Write-Host "Registros cargados:" -ForegroundColor Yellow
Write-Host "  - 2 monedas" -ForegroundColor White
Write-Host "  - 7 sucursales" -ForegroundColor White
Write-Host "  - 14 empleados" -ForegroundColor White
Write-Host "  - 13 usuarios" -ForegroundColor White
Write-Host "  - 20 clientes" -ForegroundColor White
Write-Host "  - 6 cuentas" -ForegroundColor White
Write-Host "  - 10 tipos de movimiento" -ForegroundColor White
Write-Host "  - 40+ movimientos" -ForegroundColor White
Write-Host ""
Write-Host "Ahora puedes ejecutar: dotnet run" -ForegroundColor Cyan
