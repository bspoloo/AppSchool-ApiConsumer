using AppSchool.API_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace AppSchool.API_User
{
    public class ApiConsumer
    {
        private readonly HttpClient client;
        public ApiConsumer(string baseAddress)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }
        public async Task<List<User>> GetUsers(String route)
        {
            try
            {
                var response = await client.GetAsync(route);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(content);

                if (jsonObject.TryGetValue("success", out JToken successToken) && successToken.Value<bool>())
                {
                    var usersJson = jsonObject["userOutDTOs"].ToString();
                    var users = JsonConvert.DeserializeObject<List<User>>(usersJson);
                    return users;
                }
                else
                {
                    // La solicitud fue exitosa pero no se encontraron usuarios
                    return new List<User>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> UpdateUsers(User updatedUser)
        {
            try
            {
                // Convertir el objeto User a JSON para enviarlo en la solicitud
                string jsonContent = JsonConvert.SerializeObject(updatedUser);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Realizar la solicitud PUT al servidor
                var response = await client.PutAsync($"users/{updatedUser.Id}", content);
                response.EnsureSuccessStatusCode();

                // Verificar el éxito de la operación en la respuesta
                var responseBody = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseBody);

                if (jsonObject.TryGetValue("success", out JToken successToken) && successToken.Value<bool>())
                {
                    // Operación de actualización exitosa
                    return true;
                }
                else
                {
                    // La solicitud fue exitosa, pero el servidor indicó un fallo en la actualización
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Capturar y manejar cualquier error que ocurra durante la solicitud
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
