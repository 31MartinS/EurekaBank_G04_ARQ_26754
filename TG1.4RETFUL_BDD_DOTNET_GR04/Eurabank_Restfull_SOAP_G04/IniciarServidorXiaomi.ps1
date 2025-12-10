# Script para iniciar el servidor Eurabank con configuración para Xiaomi
# IP configurada: 10.40.17.162

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "   EURABANK - Servidor REST API" -ForegroundColor Cyan
Write-Host "   Para Xiaomi 23129RA5FL" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Verificar IP actual
Write-Host "Verificando configuración de red..." -ForegroundColor Yellow
$ipAddress = (Get-NetIPAddress -AddressFamily IPv4 | Where-Object { $_.IPAddress -like "10.40.*" }).IPAddress

if ($ipAddress -eq "10.40.17.162") {
    Write-Host "✅ IP correcta: $ipAddress" -ForegroundColor Green
} else {
    Write-Host "⚠️  IP actual: $ipAddress" -ForegroundColor Yellow
    Write-Host "⚠️  IP esperada: 10.40.17.162" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Si la IP cambió, actualiza Services\EurabankApiService.cs en el proyecto móvil" -ForegroundColor Yellow
    Write-Host ""
}

# Verificar si el firewall está configurado
Write-Host ""
Write-Host "Verificando configuración del firewall..." -ForegroundColor Yellow
$firewallRule = netsh advfirewall firewall show rule name="Eurabank API Port 5199" 2>&1

if ($firewallRule -like "*No se encontró ninguna regla*" -or $firewallRule -like "*No rules*") {
    Write-Host "⚠️  Firewall NO configurado" -ForegroundColor Red
    Write-Host ""
    Write-Host "Ejecuta este comando como ADMINISTRADOR:" -ForegroundColor Yellow
    Write-Host "netsh advfirewall firewall add rule name=`"Eurabank API Port 5199`" dir=in action=allow protocol=TCP localport=5199" -ForegroundColor White
    Write-Host ""
    $continuar = Read-Host "¿Deseas continuar de todos modos? (S/N)"
    if ($continuar -ne "S" -and $continuar -ne "s") {
        exit
    }
} else {
    Write-Host "✅ Firewall configurado correctamente" -ForegroundColor Green
}

# Verificar que la base de datos esté disponible
Write-Host ""
Write-Host "Verificando SQL Server..." -ForegroundColor Yellow
try {
    $sqlTest = sqlcmd -S localhost -U sa -P comand -Q "SELECT name FROM sys.databases WHERE name = 'CalculatorDb'" -h -1 2>&1
    if ($sqlTest -like "*CalculatorDb*") {
        Write-Host "✅ Base de datos CalculatorDb encontrada" -ForegroundColor Green
    } else {
        Write-Host "⚠️  Base de datos CalculatorDb no encontrada" -ForegroundColor Yellow
    }
} catch {
    Write-Host "⚠️  No se pudo verificar SQL Server (puede estar bien si usa otra configuración)" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Iniciando servidor en puerto 5199..." -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "URL Local: http://localhost:5199" -ForegroundColor White
Write-Host "URL Red:   http://$ipAddress:5199" -ForegroundColor White
Write-Host ""
Write-Host "Presiona Ctrl+C para detener el servidor" -ForegroundColor Yellow
Write-Host ""

# Cambiar al directorio del servidor
Set-Location $PSScriptRoot

# Iniciar el servidor
dotnet run
