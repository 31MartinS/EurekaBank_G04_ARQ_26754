using EurabankWebClient.Models;
using System.Text;
using System.Text.Json;

namespace EurabankWebClient.Services
{
    public class EurabankApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EurabankApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // Clientes
        public async Task<List<ClienteDto>> ObtenerTodosLosClientesAsync()
        {
            var response = await _httpClient.GetAsync("/api/Clientes");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ClienteDto>>(content, _jsonOptions) ?? new List<ClienteDto>();
        }

        public async Task<ClienteDto?> ObtenerClientePorCodigoAsync(string codigo)
        {
            var response = await _httpClient.GetAsync($"/api/Clientes/{codigo}");
            if (!response.IsSuccessStatusCode) return null;
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ClienteDto>(content, _jsonOptions);
        }

        // Cuentas
        public async Task<List<CuentaDto>> ObtenerTodasLasCuentasAsync()
        {
            var response = await _httpClient.GetAsync("/api/Cuentas");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CuentaDto>>(content, _jsonOptions) ?? new List<CuentaDto>();
        }

        public async Task<CuentaDto?> ObtenerCuentaPorCodigoAsync(string codigo)
        {
            var response = await _httpClient.GetAsync($"/api/Cuentas/{codigo}");
            if (!response.IsSuccessStatusCode) return null;
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CuentaDto>(content, _jsonOptions);
        }

        public async Task<decimal> ObtenerSaldoCuentaAsync(string codigoCuenta)
        {
            var response = await _httpClient.GetAsync($"/api/Cuentas/{codigoCuenta}/saldo");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var resultado = JsonSerializer.Deserialize<SaldoResponse>(content, _jsonOptions);
            return resultado?.Saldo ?? 0;
        }

        // Movimientos
        public async Task<List<MovimientoDto>> ObtenerMovimientosPorCuentaAsync(string codigoCuenta)
        {
            var response = await _httpClient.GetAsync($"/api/Movimientos/cuenta/{codigoCuenta}");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<MovimientoDto>>(content, _jsonOptions) ?? new List<MovimientoDto>();
        }

        public async Task<MovimientoDto> RealizarDepositoAsync(RealizarMovimientoDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("/api/Movimientos/deposito", stringContent);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MovimientoDto>(content, _jsonOptions) 
                ?? throw new Exception("Error al deserializar respuesta");
        }

        public async Task<MovimientoDto> RealizarRetiroAsync(RealizarMovimientoDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("/api/Movimientos/retiro", stringContent);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MovimientoDto>(content, _jsonOptions) 
                ?? throw new Exception("Error al deserializar respuesta");
        }

        public async Task<MovimientoDto> RealizarTransferenciaAsync(RealizarTransferenciaDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("/api/Movimientos/transferencia", stringContent);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MovimientoDto>(content, _jsonOptions) 
                ?? throw new Exception("Error al deserializar respuesta");
        }

        private class SaldoResponse
        {
            public string CodigoCuenta { get; set; } = string.Empty;
            public decimal Saldo { get; set; }
        }
    }
}
