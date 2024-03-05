using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using RegistrationLetters.Domain.Models;

namespace RegistrationLetters.Client.API
{
    public class EmployeeAPI
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<EmployeeModel> GetEmployee(EmployeeModel employeeModel)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44331/api/Employee/id?" +
                $"Id={employeeModel.Id}&" +
                $"FirstName={employeeModel.FirstName}&" +
                $"LastName={employeeModel.LastName}&" +
                $"JobPosition={employeeModel.JobPosition}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var employeeModels = JsonSerializer.Deserialize<EmployeeModel>(result, options);

                return employeeModels;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllEmployees()
        {
            var response = await _httpClient.GetAsync($"https://localhost:44331/api/Employee/all");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var employeeModels = JsonSerializer.Deserialize<IEnumerable<EmployeeModel>>(result, options);

                return employeeModels;
            }
            else
            {
                return [];
            }
        }

        public async Task<bool> CreateNewEmployee(EmployeeModel employeeModel)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:44331/api/Employee/create", employeeModel);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
