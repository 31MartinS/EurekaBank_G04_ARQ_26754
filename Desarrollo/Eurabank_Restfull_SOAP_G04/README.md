# 🏦 Sistema Bancario Eurabank - API RESTful

Sistema bancario completo con gestión de clientes, cuentas, movimientos, costos, usuarios y sucursales.

## 🎯 Funcionalidades Implementadas

### ✅ Gestión de Clientes
- **Clientes Persona Natural y Jurídica**
- Generación automática de código de cliente
- Registro de teléfonos
- Asignación a sucursales
- **Endpoints:**
  - `GET /api/clientes` - Obtener todos los clientes
  - `GET /api/clientes/{id}` - Obtener cliente por ID
  - `POST /api/clientes/natural` - Crear cliente natural
  - `POST /api/clientes/juridico` - Crear cliente jurídico

### ✅ Gestión de Cuentas
- Creación de cuentas con código automático
- Asociación de múltiples titulares
- Control de estado (ACTIVA/CERRADA)
- Gestión de saldo en tiempo real
- **Endpoints:**
  - `GET /api/cuentas` - Obtener todas las cuentas
  - `GET /api/cuentas/{id}` - Obtener cuenta por ID
  - `GET /api/cuentas/numero/{numero}` - Obtener cuenta por número
  - `POST /api/cuentas` - Crear nueva cuenta
  - `PUT /api/cuentas/{id}/cerrar` - Cerrar cuenta
  - `GET /api/cuentas/{id}/saldo` - Consultar saldo

### ✅ Gestión de Movimientos
- **Depósitos** - Ingresos a cuenta
- **Retiros** - Salidas con validación de saldo
- **Transferencias** - Entre cuentas con transacciones atómicas
- Generación automática de número de movimiento
- Histórico completo de transacciones
- **Endpoints:**
  - `GET /api/movimientos/cuenta/{idCuenta}` - Movimientos de una cuenta
  - `POST /api/movimientos/deposito` - Realizar depósito
  - `POST /api/movimientos/retiro` - Realizar retiro
  - `POST /api/movimientos/transferencia` - Realizar transferencia
  - `GET /api/movimientos/fecha?fechaInicio=...&fechaFin=...` - Movimientos por rango

### ✅ Gestión de Sucursales
- Consulta de sucursales
- Estadísticas de cuentas y clientes por sucursal
- **Endpoints:**
  - `GET /api/sucursales` - Obtener todas las sucursales
  - `GET /api/sucursales/{id}` - Obtener sucursal por ID

### ✅ Sistema de Generación de Códigos
- Generador automático de códigos únicos:
  - Códigos de cliente (CLIXXXXXXXXXXX)
  - Números de cuenta (CUEXXXXXXXXXXX)
  - Números de movimiento (MOVXXXXXXXXXXX)
- Longitud configurable
- Contador incremental persistente

## 📊 Modelo de Datos Completo

### Entidades Principales
- ✅ **Cliente** - Base para natural y jurídico
- ✅ **PersonaNatural** - Nombres, apellidos, identificación
- ✅ **PersonaJuridica** - Razón social, RUC
- ✅ **Telefono** - Números de contacto (móvil, fijo, trabajo)
- ✅ **Cuenta** - Número, moneda, saldo, estado, fecha apertura
- ✅ **Movimiento** - Número, tipo, fecha, importe, saldo
- ✅ **TipoMovimiento** - INGRESO/SALIDA
- ✅ **Moneda** - Soles, Dólares, etc.
- ✅ **Sucursal** - Sedes del banco
- ✅ **Usuario** - Personal del banco
- ✅ **Perfil** - Roles de usuario
- ✅ **Menu** - Opciones del sistema
- ✅ **CargoMantenimiento** - Costos de mantenimiento
- ✅ **CostoMovimiento** - Costos por transacción
- ✅ **CuentaCosto** - Costos aplicados
- ✅ **Contador** - Generación de códigos

### Relaciones
- Cliente ↔ PersonaNatural/PersonaJuridica (1:1)
- Cliente ↔ Cuenta (N:M via ClienteCuenta)
- Cliente ↔ Sucursal (N:M via ClienteSucursal)
- Cliente ↔ Telefono (1:N)
- Cuenta ↔ Movimiento (1:N)
- Cuenta ↔ Moneda (N:1)
- Cuenta ↔ Sucursal (N:1)
- Usuario ↔ Perfil ↔ Menu (N:M:M)

## 🚀 Ejemplos de Uso

### 1. Crear Cliente Natural
```json
POST /api/clientes/natural
{
  "nombres": "Juan Carlos",
  "apellidos": "Pérez García",
  "identificacion": "1234567890",
  "idSucursal": 1,
  "telefonos": [
    {
      "numero": "0991234567",
      "tipo": "MOVIL"
    }
  ]
}
```

### 2. Crear Cuenta
```json
POST /api/cuentas
{
  "idCliente": 1,
  "idMoneda": 1,
  "idSucursal": 1,
  "depositoInicial": 1000.00
}
```

### 3. Realizar Depósito
```json
POST /api/movimientos/deposito
{
  "idCuenta": 1,
  "idTipoMovimiento": 1,
  "importe": 500.00,
  "idUsuario": 1,
  "observacion": "Depósito en efectivo"
}
```

### 4. Realizar Retiro
```json
POST /api/movimientos/retiro
{
  "idCuenta": 1,
  "idTipoMovimiento": 2,
  "importe": 200.00,
  "idUsuario": 1,
  "observacion": "Retiro cajero automático"
}
```

### 5. Realizar Transferencia
```json
POST /api/movimientos/transferencia
{
  "idCuentaOrigen": 1,
  "idCuentaDestino": 2,
  "importe": 300.00,
  "idUsuario": 1,
  "observacion": "Pago de servicios"
}
```

## ⚙️ Configuración

### Connection String
Actualizar en `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CalculatorDb;User Id=sa;Password=TU_PASSWORD;TrustServerCertificate=True;"
  }
}
```

### Ejecutar Migraciones
```bash
dotnet ef migrations add InicialBancario
dotnet ef database update
```

### Ejecutar Proyecto
```bash
dotnet run
```

## 📝 Estructura del Proyecto

```
├── ec.edu.monster.modelo/      # Entidades de base de datos
├── ec.edu.monster.ws/          # DTOs (Data Transfer Objects)
├── ec.edu.monster.service/     # Lógica de negocio
├── ec.edu.monster.controlador/ # Controladores API REST
├── Data/                       # DbContext
└── Program.cs                  # Configuración y startup
```

## 🔐 Características de Seguridad

- ✅ Validación de saldo antes de retiros
- ✅ Transacciones atómicas en transferencias
- ✅ Control de estados de cuentas
- ✅ Validaciones de negocio en servicios
- ✅ Sistema de roles y permisos preparado

## 📊 Lógica de Negocio Implementada

### Cuentas
- ✅ No se puede cerrar cuenta con saldo ≠ 0
- ✅ Generación automática de número de cuenta único
- ✅ Múltiples titulares por cuenta
- ✅ Control de moneda

### Movimientos
- ✅ Validación de saldo suficiente para retiros
- ✅ Actualización automática de saldo en cuenta
- ✅ Registro de saldo anterior y nuevo
- ✅ Transacciones con rollback en transferencias
- ✅ Registro de usuario que procesa
- ✅ Validación de tipo de movimiento (INGRESO/SALIDA)

### Clientes
- ✅ Generación automática de código único
- ✅ Registro de múltiples teléfonos
- ✅ Asignación automática a sucursal
- ✅ Soporte para natural y jurídico

## 🎨 Swagger UI

Acceder a la documentación interactiva en:
```
http://localhost:5199/swagger
```

## ✅ Estado del Proyecto

**TODO COMPLETADO** ✅

El sistema bancario está completamente funcional con:
- ✅ Todos los modelos creados
- ✅ Todas las relaciones configuradas
- ✅ Servicios con lógica de negocio completa
- ✅ Controladores REST completos
- ✅ Sistema de códigos automáticos
- ✅ Validaciones de negocio
- ✅ Transacciones seguras
- ✅ DTOs para todas las operaciones

## 🔄 Próximos Pasos Sugeridos

1. Crear script SQL para cargar datos de prueba
2. Implementar autenticación JWT
3. Agregar logs de auditoría
4. Implementar reportes financieros
5. Agregar validaciones de horarios bancarios
6. Implementar límites de retiro diario
7. Sistema de alertas por movimientos sospechosos

---
**Desarrollado con .NET 9.0 + Entity Framework Core + SQL Server**
