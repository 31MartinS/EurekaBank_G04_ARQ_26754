using System.Net.Http.Json;
using System.Text.Json;
using EurabankMobileClient.Models;

namespace EurabankMobileClient.Services
{
    public class EurabankApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EurabankApiService()
        {
            // Configuración de HttpClient con timeout y ajustes para móvil
            var handler = new HttpClientHandler();
            
            _httpClient = new HttpClient(handler)
            {
                // IP de tu computadora: 10.40.17.162
                // Para tu Xiaomi 23129RA5FL (Android 15.0 - API 35)
                BaseAddress = new Uri("http://10.40.17.162:5199"),
                Timeout = TimeSpan.FromSeconds(30)
            };

            // Headers para mejor compatibilidad
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "EurabankMobileClient/1.0");

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // Permite cambiar la URL base si es necesario
        public void SetBaseAddress(string baseUrl)
        {
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<List<ClienteDto>> ObtenerTodosLosClientesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/clientes");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<ClienteDto>>(_jsonOptions) ?? new List<ClienteDto>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener clientes: {ex.Message}");
                throw;
            }
        }

        public async Task<List<CuentaDto>> ObtenerTodasLasCuentasAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"GET {_httpClient.BaseAddress}/api/cuentas");
                var response = await _httpClient.GetAsync("/api/cuentas");
                System.Diagnostics.Debug.WriteLine($"Status: {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<CuentaDto>>(_jsonOptions) ?? new List<CuentaDto>();
            }
            catch (HttpRequestException httpEx)
            {
                System.Diagnostics.Debug.WriteLine($"HttpRequestException al obtener cuentas: {httpEx}");
                System.Diagnostics.Debug.WriteLine($"InnerException: {httpEx.InnerException?.Message}");
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener cuentas: {ex}");
                throw;
            }
        }

        public async Task<List<MovimientoDto>> ObtenerMovimientosPorCuentaAsync(string codigoCuenta)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/movimientos/cuenta/{codigoCuenta}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<MovimientoDto>>(_jsonOptions) ?? new List<MovimientoDto>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener movimientos: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RealizarDepositoAsync(RealizarMovimientoDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/movimientos/deposito", dto);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al realizar depósito: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RealizarRetiroAsync(RealizarMovimientoDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/movimientos/retiro", dto);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al realizar retiro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RealizarTransferenciaAsync(RealizarTransferenciaDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/movimientos/transferencia", dto);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al realizar transferencia: {ex.Message}");
                throw;
            }
        }
    }
}
