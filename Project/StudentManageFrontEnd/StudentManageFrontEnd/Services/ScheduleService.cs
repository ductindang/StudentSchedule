using Newtonsoft.Json;
using StudentManageFrontEnd.Models;
using StudentManageFrontEnd.Services.IServices;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace StudentManageFrontEnd.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly HttpClient _httpClient;

        public ScheduleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Schedule>> GetScheduleByUserId(string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync("api/Schedule/byUser");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<IEnumerable<Schedule>>(jsonString);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                throw new ApplicationException("An error occured while logging in.", ex);
            }
        }

        public async Task<Schedule> GetScheduleById(int id, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"api/schedule/{id}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Schedule>(jsonString);
                    return result;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                throw new ApplicationException("An error occured while logging in.", ex);
            }
        }

        public async Task<Schedule> InsertSchedule(Schedule schedule, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonContent = JsonConvert.SerializeObject(schedule);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Schedule", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Schedule>(jsonString);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new ApplicationException("An error occurred while inserting schedule.", ex);
            }
        }

        public async Task<Schedule> UpdateSchedule(Schedule schedule, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonContent = JsonConvert.SerializeObject(schedule);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/schedule", content);
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Schedule>(jsonString);
                }
                return null;
            }catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new ApplicationException("An error occurred while updating schedule.", ex);
            }
        }

        public async Task<Schedule> DeleteSchedule(int id, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.DeleteAsync($"api/schedule/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var schedule = JsonConvert.DeserializeObject<Schedule>(jsonString);
                    return schedule;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                throw new ApplicationException("An error occured while logging in.", ex);
            }
        }
    }
}
