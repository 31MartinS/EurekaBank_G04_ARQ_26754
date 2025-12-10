# GUÍA: Ejecutar Cliente Móvil Eurabank en tu Teléfono

## ✅ Cliente Móvil Creado Exitosamente

El cliente móvil está listo con todas las funcionalidades:
- ✅ Login (MONSTER/monster9)
- ✅ Lista de Clientes
- ✅ Realizar Depósito
- ✅ Realizar Retiro
- ✅ Realizar Transferencia
- ✅ Ver Movimientos

---

## 📱 OPCIÓN 1: Ejecutar en Android (Recomendado)

### Requisitos Previos:
1. **Visual Studio 2022** con la carga de trabajo ".NET Multi-platform App UI development"
2. **Android SDK** (se instala con Visual Studio)
3. **Cable USB** para conectar tu teléfono
4. **Teléfono Android** con depuración USB activada

### Pasos para Habilitar Depuración USB en Android:

1. **Activar Modo Desarrollador:**
   - Ve a `Ajustes` → `Acerca del teléfono`
   - Toca 7 veces en "Número de compilación"
   - Aparecerá un mensaje: "Ahora eres desarrollador"

2. **Activar Depuración USB:**
   - Ve a `Ajustes` → `Sistema` → `Opciones para desarrolladores`
   - Activa `Depuración USB`
   - Activa `Instalar vía USB` (opcional, pero recomendado)

3. **Conectar el Teléfono:**
   - Conecta tu teléfono a la PC con el cable USB
   - En el teléfono aparecerá: "¿Permitir depuración USB?"
   - Marca "Permitir siempre desde esta computadora"
   - Toca `Permitir`

### Ejecutar desde Visual Studio:

1. **Abrir el proyecto:**
   ```
   Abre: C:\src\Eurabank_Restfull_SOAP_G04\EurabankMobileClient\EurabankMobileClient.csproj
   ```

2. **Seleccionar tu dispositivo:**
   - En la barra de herramientas, busca el menú desplegable de dispositivos
   - Debe aparecer tu teléfono (ej: "Samsung Galaxy S23")
   - Si no aparece, verifica que la depuración USB esté activada

3. **Configurar la URL del servidor:**
   - Tu teléfono NO puede conectarse a `localhost`
   - Necesitas usar la IP de tu PC en la red local
   
   **Obtener tu IP:**
   ```powershell
   ipconfig
   ```
   Busca "Dirección IPv4" (ej: 192.168.1.100)

4. **Actualizar la URL en el código:**
   - Abre: `Services\EurabankApiService.cs`
   - Cambia la línea 15:
   ```csharp
   // Cambiar esto:
   BaseAddress = new Uri("http://10.0.2.2:5199")
   
   // Por tu IP real (ejemplo):
   BaseAddress = new Uri("http://192.168.1.100:5199")
   ```

5. **Ejecutar el servidor REST API:**
   ```powershell
   cd C:\src\Eurabank_Restfull_SOAP_G04\Eurabank_Restfull_SOAP_G04
   dotnet run
   ```
   Verifica que está escuchando en: `http://localhost:5199`

6. **Ejecutar la app móvil:**
   - En Visual Studio, presiona `F5` o haz clic en el botón ▶ (Run)
   - La app se instalará automáticamente en tu teléfono
   - Se abrirá automáticamente
   - ¡No necesitas generar APK!

### Solución de Problemas Android:

**El dispositivo no aparece:**
- Asegúrate de que la depuración USB esté activada
- Prueba con otro cable USB (algunos cables solo cargan)
- Reinicia Visual Studio
- Ejecuta: `adb devices` en CMD para verificar conexión

**Error de conexión a la API:**
- Verifica que usaste tu IP real (no localhost ni 10.0.2.2)
- Asegúrate de que el servidor esté corriendo
- Verifica que el firewall de Windows permita conexiones al puerto 5199
- Ambos dispositivos deben estar en la misma red WiFi

**Permitir conexiones en el Firewall:**
```powershell
netsh advfirewall firewall add rule name="DotNet Port 5199" dir=in action=allow protocol=TCP localport=5199
```

---

## 📱 OPCIÓN 2: Emulador de Android

Si no tienes un teléfono Android a mano:

1. **Instalar Android Emulator desde Visual Studio:**
   - `Herramientas` → `Administrador de dispositivos Android`
   - Crear un nuevo dispositivo virtual (ej: Pixel 5 API 34)

2. **Usar la IP especial para emulador:**
   - En `EurabankApiService.cs`, usa: `http://10.0.2.2:5199`
   - Esta IP especial conecta al localhost de tu PC desde el emulador

3. **Ejecutar:**
   - Selecciona el emulador en Visual Studio
   - Presiona F5
   - La app se ejecutará en el emulador

---

## 🍎 OPCIÓN 3: iOS (Si tienes Mac)

Para ejecutar en iPhone necesitas:
- **Mac con Xcode**
- **Visual Studio for Mac** o **Visual Studio en Windows con Mac pareado**
- **Cable USB** para conectar el iPhone
- **Cuenta de desarrollador de Apple** (gratuita para desarrollo local)

Pasos:
1. Conecta tu iPhone al Mac
2. En Xcode, habilita el dispositivo para desarrollo
3. En Visual Studio, selecciona tu iPhone
4. Actualiza la URL a la IP de tu Mac
5. Presiona F5

---

## 🖥️ OPCIÓN 4: Windows (Probar en tu PC)

La forma más rápida para probar SIN teléfono:

```powershell
cd C:\src\Eurabank_Restfull_SOAP_G04\EurabankMobileClient
dotnet build -f net9.0-windows10.0.19041.0
dotnet run -f net9.0-windows10.0.19041.0
```

La app se abrirá como una aplicación de Windows.
Usa: `http://localhost:5199` en `EurabankApiService.cs`

---

## 🔧 Configuración de Red Recomendada

### Para Desarrollo en la Misma Máquina:
```csharp
// En EurabankApiService.cs línea 15:
BaseAddress = new Uri("http://localhost:5199")
```

### Para Desarrollo con Teléfono Android Real:
```csharp
// Reemplaza 192.168.1.100 con TU IP real:
BaseAddress = new Uri("http://192.168.1.100:5199")
```

### Para Desarrollo con Emulador Android:
```csharp
// IP especial del emulador:
BaseAddress = new Uri("http://10.0.2.2:5199")
```

---

## 📋 Flujo Completo Recomendado:

1. **Inicia el servidor:**
   ```powershell
   cd C:\src\Eurabank_Restfull_SOAP_G04\Eurabank_Restfull_SOAP_G04
   dotnet run
   ```

2. **Obtén tu IP:**
   ```powershell
   ipconfig
   ```
   Anota la "Dirección IPv4"

3. **Actualiza EurabankApiService.cs** con tu IP

4. **Conecta tu teléfono Android:**
   - Cable USB conectado
   - Depuración USB activada
   - Autoriza la conexión

5. **Abre Visual Studio 2022:**
   ```
   Abre: EurabankMobileClient.csproj
   ```

6. **Selecciona tu dispositivo** y presiona F5

7. **¡Listo!** La app se instalará y ejecutará automáticamente

---

## 📱 Características de la App Móvil:

### Pantalla de Login:
- Usuario: MONSTER
- Contraseña: monster9
- Validación de credenciales
- Mensaje de error si fallan

### Lista de Clientes:
- Muestra todos los clientes con sus saldos
- Actualizar con "Pull to Refresh"
- Tap en cualquier cliente para ver opciones

### Operaciones:
- **Depósito:** Agregar dinero a una cuenta
- **Retiro:** Retirar dinero (valida saldo)
- **Transferencia:** Entre dos clientes (con selector)
- **Movimientos:** Historial completo de transacciones

### Sincronización:
- Todas las operaciones se sincronizan con el servidor REST API
- Los cambios son visibles en todos los clientes (Web, Desktop, Consola, Móvil)

---

## 🎨 Interfaz Móvil:

- **Diseño Material:** Colores azul (#2196F3) y verde (#4CAF50)
- **Responsive:** Se adapta a diferentes tamaños de pantalla
- **Touch-Friendly:** Botones grandes y espaciados
- **Activity Indicators:** Muestra estado de carga
- **Alerts:** Mensajes de éxito/error claros

---

## ⚡ Hot Reload (Recarga en Caliente):

Visual Studio 2022 soporta **Hot Reload** para MAUI:
- Haz cambios en el código
- Guarda el archivo (Ctrl+S)
- La app se actualiza automáticamente en tu teléfono
- ¡Sin necesidad de recompilar!

---

## 📦 Si Deseas Crear APK (Opcional):

Para crear un APK instalable:

```powershell
cd C:\src\Eurabank_Restfull_SOAP_G04\EurabankMobileClient
dotnet publish -f net9.0-android -c Release
```

El APK estará en:
```
bin\Release\net9.0-android\publish\com.companyname.eurabankmobileclient-Signed.apk
```

Puedes instalarlo manualmente en cualquier Android.

---

## 🆘 Ayuda Adicional:

**No tengo Visual Studio 2022:**
- Descárgalo gratis: https://visualstudio.microsoft.com/vs/community/
- Durante la instalación, marca: ".NET Multi-platform App UI development"

**Mi teléfono no aparece:**
- Verifica drivers USB: Windows Update → Buscar actualizaciones
- Prueba modo de transferencia de archivos en el teléfono
- Ejecuta: `adb devices` para diagnosticar

**Error "Unable to connect to 10.0.2.2:5199":**
- Cambia a tu IP real (no uses 10.0.2.2 en teléfono físico)
- Verifica que el servidor esté corriendo
- Verifica el firewall

---

¡Todo listo para ejecutar en tu teléfono! 🎉
