using AppSchool.API_User.Controller;
using AppSchool.API_User.Models;
using Ship_Shooting_Game.API.Password;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppSchool
{
    public partial class Login : Form
    {
        private UserController _userController;
        public Login()
        {
            InitializeComponent();
            _userController = new UserController();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Iniciando sesión...", textUserName);
            User user = await _userController.GetUserByNameAsync(textUserName.Text);
            //_user = user;

            if (user == null)
            {
                MessageBox.Show("El nombre de usuario no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Console.WriteLine("Usuario encontrado: " + user.Password);
            if (PasswordManager.VerifyPassword(textPassword.Text, user.Password))
            {
                MessageBox.Show("Inicio de sesión exitoso", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form formRegiter = new Registro();
                formRegiter.Size = new Size(1500, 1100);
                formRegiter.StartPosition = FormStartPosition.CenterScreen;
                formRegiter.BackColor = Color.Black;
                formRegiter.Show();
                this.Hide(); // Cerrar el formulario de inicio de sesión
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
