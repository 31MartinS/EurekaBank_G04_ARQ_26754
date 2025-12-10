# 📱 Ejecutar en tu Xiaomi 23129RA5FL (Android 15.0 - API 35)

## ✅ Configuración Completada

**IP Configurada:** `10.40.17.162:5199`
**Dispositivo:** Xiaomi 23129RA5FL (Android 15.0 - API 35)
**Estado:** ✅ Listo para ejecutar

---

## 🚀 Pasos para Ejecutar

### 1️⃣ Habilitar Depuración USB en tu Xiaomi

En tu teléfono Xiaomi:

```
1. Ajustes → Acerca del teléfono
2. Toca 7 veces en "Versión de MIUI" (no "Número de compilación")
3. Aparecerá: "Ahora eres desarrollador"

4. Vuelve a Ajustes → Ajustes adicionales → Opciones para desarrolladores
5. Activa "Depuración USB"
6. Activa "Instalar vía USB"
7. Activa "Depuración de USB (Configuración de seguridad)" (opcional pero recomendado)
```

**IMPORTANTE para Xiaomi:**
- Los Xiaomi tienen protecciones adicionales de MIUI
- Asegúrate de desactivar "Optimización MIUI" si te da problemas
- Si no aparece el dispositivo, ve a: Ajustes → Permisos → Autostart → Activa para VS/Android Studio

### 2️⃣ Configurar Firewall de Windows

**Abre PowerShell como Administrador** (clic derecho → Ejecutar como administrador)

```powershell
netsh advfirewall firewall add rule name="Eurabank API Port 5199" dir=in action=allow protocol=TCP localport=5199
```

Deberías ver: "Correcto."

### 3️⃣ Iniciar el Servidor REST API

En una terminal PowerShell normal:

```powershell
cd C:\src\Eurabank_Restfull_SOAP_G04\Eurabank_Restfull_SOAP_G04
dotnet run
```

Verifica que muestre:
```
Now listening on: http://localhost:5199
```

**MANTÉN ESTA TERMINAL ABIERTA** mientras usas la app móvil.

### 4️⃣ Conectar tu Xiaomi

1. **Conecta el cable USB** de tu Xiaomi a la PC

2. **En tu Xiaomi aparecerá:**
   ```
   ¿Permitir depuración USB?
   Huella digital de la clave RSA:
   XX:XX:XX:XX...
   
   [Cancelar] [Permitir]
   ```

3. **Marca:** "Permitir siempre desde esta computadora"

4. **Toca:** "Permitir"

### 5️⃣ Ejecutar desde Visual Studio 2022

**Opción A: Visual Studio 2022 (Recomendado)**

1. Abre Visual Studio 2022

2. Abre el proyecto:
   ```
   Archivo → Abrir → Proyecto/Solución
   C:\src\Eurabank_Restfull_SOAP_G04\EurabankMobileClient\EurabankMobileClient.csproj
   ```

3. En la barra de herramientas, busca el selector de dispositivos:
   ```
   [Xiaomi 23129RA5FL (Android 15.0 - API 35)] ▼
   ```

4. Si no aparece tu Xiaomi:
   - Verifica que la depuración USB esté activada
   - Desconecta y reconecta el cable
   - Reinicia Visual Studio

5. Presiona **F5** o haz clic en el botón **▶ (Start)**

6. La app se instalará automáticamente en tu Xiaomi y se ejecutará

**Opción B: Desde línea de comandos**

```powershell
cd C:\src\Eurabank_Restfull_SOAP_G04\EurabankMobileClient
dotnet build -t:Run -f net9.0-android
```

### 6️⃣ Probar la App

1. **Login:**
   - Usuario: `MONSTER`
   - Contraseña: `monster9`

2. **Verás la lista de clientes** con sus saldos

3. **Toca cualquier cliente** para ver opciones:
   - Realizar Depósito
   - Realizar Retiro
   - Realizar Transferencia
   - Ver Movimientos

---

## 🔧 Solución de Problemas

### ❌ "Xiaomi 23129RA5FL no aparece en Visual Studio"

**Solución 1: Verificar ADB**
```powershell
adb devices
```
Debería mostrar:
```
List of devices attached
XXXXXX  device
```

Si muestra "unauthorized":
- Desconecta el USB
- Revoca autorizaciones USB en el teléfono (Opciones desarrollador)
- Reconecta y autoriza de nuevo

**Solución 2: Instalar drivers Xiaomi**
- Los drivers genéricos de Windows suelen funcionar
- Si no, descarga "Xiaomi USB Drivers" desde el sitio oficial de Xiaomi

**Solución 3: Cambiar modo USB**
- En tu Xiaomi, al conectar el cable, toca la notificación USB
- Cambia a "Transferencia de archivos (MTP)" o "PTP"
- Intenta nuevamente

### ❌ "Unable to connect to 10.40.17.162:5199"

**Verifica que ambos estén en la misma red WiFi:**
```powershell
# En tu PC:
ipconfig

# Busca tu adaptador WiFi y verifica que la IP sea 10.40.17.162
```

**Verifica que el servidor esté corriendo:**
- Debe mostrar: `Now listening on: http://localhost:5199`

**Prueba la conexión desde tu PC:**
```powershell
curl http://10.40.17.162:5199/api/cuentas
```

Si funciona en tu PC pero no en el teléfono, el problema es el firewall.

**Verifica el firewall:**
```powershell
netsh advfirewall firewall show rule name="Eurabank API Port 5199"
```

### ❌ Error "INSTALL_FAILED_UPDATE_INCOMPATIBLE"

Si ya instalaste una versión anterior:

**En tu Xiaomi:**
```
Ajustes → Aplicaciones → Administrar aplicaciones
Busca: EurabankMobileClient
Desinstalar
```

Luego vuelve a ejecutar desde Visual Studio.

### ❌ App se instala pero no se conecta

**Verifica la configuración de red en MIUI:**
- Ajustes → WiFi → (i) junto a tu red
- Verifica que esté en la misma subred que tu PC (10.40.17.x)

**Desactiva optimizaciones de batería:**
- Ajustes → Aplicaciones → Administrar aplicaciones
- EurabankMobileClient → Ahorro de batería → Sin restricciones

### ❌ "Installation failed: FAILED_USER_RESTRICTED"

Específico de Xiaomi con MIUI:

```
1. Ajustes → Ajustes adicionales → Opciones para desarrolladores
2. Busca "Instalar vía USB" → Activar
3. Busca "Verificación de instalación vía USB" → Desactivar
```

---

## 📊 Verificación de Conectividad

**Desde tu Xiaomi, puedes probar la conexión:**

1. Instala una app de terminal en tu Xiaomi (ej: Termux)
2. Ejecuta:
   ```
   curl http://10.40.17.162:5199/api/cuentas
   ```
3. Debería devolver JSON con la lista de cuentas

O usa tu navegador en el Xiaomi:
```
http://10.40.17.162:5199/api/cuentas
```

---

## 🎯 Hot Reload (Recarga en Caliente)

Una vez que la app esté ejecutándose:

1. Deja la app corriendo en tu Xiaomi
2. En Visual Studio, edita cualquier archivo XAML o C#
3. Guarda (Ctrl+S)
4. La app se actualiza automáticamente en tu teléfono
5. ¡No necesitas recompilar ni reinstalar!

---

## 📱 Características Específicas de Android 15

Tu Xiaomi con Android 15.0 (API 35) soporta:

✅ **Material Design 3**
✅ **Modo Oscuro** (respeta configuración del sistema)
✅ **Gestos de navegación**
✅ **Notificaciones enriquecidas**
✅ **Permisos granulares**

La app está optimizada para Android 15 y aprovecha sus capacidades.

---

## 🔐 Permisos Necesarios

La app SOLO requiere:
- ✅ **Internet** (ya incluido en el manifiesto)

No necesita permisos adicionales. No se requiere autorización explícita del usuario.

---

## 📦 Si Prefieres Instalar APK Manualmente

Para crear un APK que puedas instalar sin Visual Studio:

```powershell
cd C:\src\Eurabank_Restfull_SOAP_G04\EurabankMobileClient
dotnet publish -f net9.0-android -c Release
```

El APK estará en:
```
bin\Release\net9.0-android\publish\com.companyname.eurabankmobileclient-Signed.apk
```

**Instalación manual:**
1. Copia el APK a tu Xiaomi
2. Abre el archivo en tu teléfono
3. Permitir instalación de fuentes desconocidas (MIUI te lo pedirá)
4. Instalar

---

## ✅ Checklist Final

Antes de ejecutar, verifica:

- [ ] Depuración USB activada en tu Xiaomi
- [ ] "Instalar vía USB" activada
- [ ] Cable USB conectado (preferiblemente USB 3.0 para mayor velocidad)
- [ ] Autorización USB permitida en el teléfono
- [ ] Firewall de Windows configurado (como administrador)
- [ ] Servidor REST API corriendo en puerto 5199
- [ ] Tu PC tiene IP 10.40.17.162
- [ ] Ambos dispositivos en la misma red WiFi
- [ ] Visual Studio 2022 muestra "Xiaomi 23129RA5FL" en el selector

---

## 🎉 ¡Listo!

Cuando todo esté configurado:

1. ✅ Servidor corriendo: `http://localhost:5199`
2. ✅ Xiaomi conectado y autorizado
3. ✅ Visual Studio con dispositivo seleccionado
4. ✅ Presiona **F5**

La app se instalará en segundos y se ejecutará automáticamente.

**Usuario:** MONSTER  
**Contraseña:** monster9

¡Disfruta tu app bancaria en tu Xiaomi! 📱💰
