using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using RegistrationLetters.Domain.Models;
using System.Net.Http.Headers;

namespace RegistrationLetters.Client.API
{
    public class MailAPI
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<bool> CreateNewMail(MailModel mailModel)
        {
            bool requestStatus = false;
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:44331/api/mail/create", mailModel);
            if (response.IsSuccessStatusCode)
            {
                requestStatus = true;
                return requestStatus;
            }

            return requestStatus;
        }

        public async Task<IEnumerable<MailModel>> GetMail(EmployeeModel employeeModel)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44331/api/mail/getMail?" +
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
                var mailModels = JsonSerializer.Deserialize<IEnumerable<MailModel>>(result, options);

                return mailModels;
            }
            else
            {
                return [];
            }
        }
    }
}
