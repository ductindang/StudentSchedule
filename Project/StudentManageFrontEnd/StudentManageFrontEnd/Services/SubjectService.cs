using Newtonsoft.Json;
using StudentManageFrontEnd.Models;
using StudentManageFrontEnd.Services.IServices;
using System.Net.Http.Headers;
using System.Text;

namespace StudentManageFrontEnd.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly HttpClient _httpClient;

        public SubjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IEnumerable<Subject>> GetAllSubject(string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync("api/subject");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<IEnumerable<Subject>>(jsonString);
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

        public async Task<Subject> GetSubjectById(int id, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"api/subject/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Subject>(jsonString);
                    return result;
                }
                else { return null; }
            }catch(Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                throw new ApplicationException("An error occured while logging in.", ex);
            }
        }

        public async Task<Subject> InsertSubject(Subject subject, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonContent = JsonConvert.SerializeObject(subject);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Subject", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Subject>(jsonString);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new ApplicationException("An error occurred while inserting schedule.", ex);
            }
        }

        public async Task<Subject> UpdateSubject(Subject subject, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonContent = JsonConvert.SerializeObject(subject);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/Subject", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Subject>(jsonString);
                }
                return null;
            }catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new ApplicationException("An error occurred while updating subject.", ex);
            }
        }


        public async Task<Subject> DeleteSubject(int id, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.DeleteAsync($"api/subject/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var subject = JsonConvert.DeserializeObject<Subject>(jsonString);
                    return subject;
                }
                return null;
            }catch(Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                throw new ApplicationException("An error occured while logging in.", ex);
            }
        }
    }
}
