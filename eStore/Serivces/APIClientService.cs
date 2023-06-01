using Common;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace eStore.Serivces
{
    public class APIClientService
    {
        private static readonly string _logPrefix = "[APIClientService]";

        private readonly HttpClient _client;
        private readonly HttpContext _httpContext;

        public APIClientService(HttpContext httpContext, HttpClient client)
        {
            _httpContext = httpContext;
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:5006");
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContext.Session.GetString("accessToken"));
        }

        public async Task<T?> Get<T>(string relativeUrl)
        {
            try
            {
                var res = await _client.GetAsync(relativeUrl);
                if ((int)res.StatusCode == 401) await _httpContext.SignOutAsync("CookieAuthentication");
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when processing Get request with response type {nameof(T)}. Error: {ex}");
                return default;
            }
        }

        public async Task<T?> Post<T>(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PostAsync(relativeUrl, GetBody(data));
                if ((int)res.StatusCode == 401) await _httpContext.SignOutAsync("CookieAuthentication");
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when processing Post request with response type {nameof(T)}. Error: {ex}");
                return default;
            }
        }

        public async Task<T?> Put<T>(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PutAsync(relativeUrl, GetBody(data));
                if ((int)res.StatusCode == 401) await _httpContext.SignOutAsync("CookieAuthentication");
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when processing Put request with response type {nameof(T)}. Error: {ex}");
                return default;
            }
        }

        public async Task<T?> Delete<T>(string relativeUrl)
        {
            try
            {
                var res = await _client.DeleteAsync(relativeUrl);
                if ((int)res.StatusCode == 401) await _httpContext.SignOutAsync("CookieAuthentication");
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when processing Delete request with response type {nameof(T)}. Error: {ex}");
                return default;
            }
        }

        public async Task<string?> Get(string relativeUrl)
        {
            try
            {
                var res = await _client.GetAsync(relativeUrl);
                if ((int)res.StatusCode == 401) await _httpContext.SignOutAsync("CookieAuthentication");
                return await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when processing Get request. Error: {ex}");
                return null;
            }
        }

        public async Task<string?> Post(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PostAsync(relativeUrl, GetBody(data));
                if ((int)res.StatusCode == 401) await _httpContext.SignOutAsync("CookieAuthentication");
                return await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when processing Post request. Error: {ex}");
                return null;
            }
        }

        public async Task<string?> Put(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PutAsync(relativeUrl, GetBody(data));
                if ((int)res.StatusCode == 401) await _httpContext.SignOutAsync("CookieAuthentication");
                return await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when processing Put request. Error: {ex}");
                return null;
            }
        }

        public async Task<string?> Delete(string relativeUrl)
        {
            try
            {
                var res = await _client.DeleteAsync(relativeUrl);
                if ((int)res.StatusCode == 401) await _httpContext.SignOutAsync("CookieAuthentication");
                return await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when processing Delete request. Error: {ex}");
                return null;
            }
        }

        private StringContent? GetBody(object? data)
        {
            if (data == null) return null;
            var body = JsonConvert.SerializeObject(data);
            return new StringContent(body, Encoding.UTF8, "application/json");
        }
    }
}
