using AppSchool.API_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppSchool.API_User.Controller
{
    public class UserController
    {
        ApiConsumer _apiConsumer;
        public UserController()
        {
            _apiConsumer = new ApiConsumer("https://localhost:8090/api/v1/");
        }
        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                var users = await _apiConsumer.GetUsers("users");
                return users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error");
                return null;
            }
        }
        public async Task<User> GetUserByNameAsync(String name)
        {
            Console.WriteLine("Buscando usuario por nombre..."+name);
            try
            {
                var users = await _apiConsumer.GetUsers("users");
                return users.Find(u => u.FamilyName == name);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error");
                return null;
            }
        }
        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                // Llamar al método de actualización del consumidor de la API
                bool updateSuccessful = await _apiConsumer.UpdateUsers(user);

                if (updateSuccessful)
                {
                    // La actualización fue exitosa
                    return true;
                }
                else
                {
                    // La actualización falló (podrías querer mostrar un mensaje de error en este caso)
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la actualización
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error");
                return false;
            }
        }

    }
}
