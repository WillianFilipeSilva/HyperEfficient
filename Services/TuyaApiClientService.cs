using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Equipamento;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace HyperEfficient.Services
{
    public class TuyaApiClientService : ITuyaApiClientService
    {
        private readonly string _baseUrl = "https://openapi.tuyaus.com";
        private readonly string _clientId = "y8yuqpy5ku98kpkvray4";
        private readonly string _clientSecret = "335ef4735e204e378f765721714cc21f";
        private readonly HttpClient _http = new();

        private string? _accessToken;
        private long _tokenExpireAt;

        public async Task<EquipamentoStatusDto> GetStatusAsync(string deviceId)
        {
            await EnsureTokenAsync();
            var path = $"/v1.0/iot-03/devices/{deviceId}/status";
            var t = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            var body = string.Empty;
            var contentHash = Sha256Hex(body);
            var stringToSign = $"GET\n{contentHash}\n\n{path}";
            var signStr = _clientId + _accessToken + t + stringToSign;
            var sign = ComputeHmac(signStr, _clientSecret);

            using var req = new HttpRequestMessage(HttpMethod.Get, _baseUrl + path);
            req.Headers.Add("client_id", _clientId);
            req.Headers.Add("access_token", _accessToken!);
            req.Headers.Add("sign", sign);
            req.Headers.Add("sign_method", "HMAC-SHA256");
            req.Headers.Add("t", t);

            var resp = await _http.SendAsync(req);
            resp.EnsureSuccessStatusCode();
            using var stream = await resp.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);
            var data = doc.RootElement.GetProperty("result"); // array
            double potencia = 0;
            double total = 0;
            var ligado = false;
            foreach (var item in data.EnumerateArray())
            {
                var code = item.GetProperty("code").GetString();
                switch (code)
                {
                    case "cur_power":
                        potencia = item.GetProperty("value").GetDouble();
                        break;
                    case "add_ele":
                        total = item.GetProperty("value").GetDouble();
                        break;
                    case "switch_1":
                        ligado = item.GetProperty("value").GetBoolean();
                        break;
                }
            }

            return new EquipamentoStatusDto { PotenciaKwh = potencia, TotalKwh = total, Ligado = ligado };
        }

        public Task LigarAsync(string deviceId)
        {
            return SendSwitchCommandAsync(true, deviceId);
        }

        public Task DesligarAsync(string deviceId)
        {
            return SendSwitchCommandAsync(false, deviceId);
        }

        private async Task EnsureTokenAsync()
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            if (!string.IsNullOrEmpty(_accessToken) && now < _tokenExpireAt - 60_000)
            {
                return;
            }

            await GenerateTokenAsync();
        }

        private async Task GenerateTokenAsync()
        {
            var t = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            var sign = ComputeHmac(_clientId + t, _clientSecret);

            using var req = new HttpRequestMessage(HttpMethod.Get, _baseUrl + "/v1.0/token?grant_type=1");
            req.Headers.Add("client_id", _clientId);
            req.Headers.Add("sign", sign);
            req.Headers.Add("sign_method", "HMAC-SHA256");
            req.Headers.Add("t", t);

            var resp = await _http.SendAsync(req);
            resp.EnsureSuccessStatusCode();
            using var stream = await resp.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);
            var result = doc.RootElement.GetProperty("result");
            _accessToken = result.GetProperty("access_token").GetString();
            var expire = result.GetProperty("expire_time").GetInt64();
            _tokenExpireAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + expire * 1000;
        }

        private static string ComputeHmac(string data, string secret)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            var sb = new StringBuilder();
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString().ToUpperInvariant();
        }

        private static string Sha256Hex(string data)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(data));
            var sb = new StringBuilder();
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private async Task SendSwitchCommandAsync(bool value, string deviceId)
        {
            await EnsureTokenAsync();
            var path = $"/v1.0/devices/{deviceId}/commands";
            var t = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            var bodyObj = new { commands = new[] { new { code = "switch_1", value } } };
            var bodyJson = JsonSerializer.Serialize(bodyObj);
            var contentHash = Sha256Hex(bodyJson);
            var stringToSign = $"POST\n{contentHash}\n\n{path}";
            var signStr = _clientId + _accessToken + t + stringToSign;
            var sign = ComputeHmac(signStr, _clientSecret);

            using var req = new HttpRequestMessage(HttpMethod.Post, _baseUrl + path);
            req.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");
            req.Headers.Add("client_id", _clientId);
            req.Headers.Add("access_token", _accessToken!);
            req.Headers.Add("sign", sign);
            req.Headers.Add("sign_method", "HMAC-SHA256");
            req.Headers.Add("t", t);

            var resp = await _http.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }
    }
}