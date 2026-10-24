using CarRentalWinForms.Models;
using CarRentalWinForms.Services;

namespace CarRentalWinForms.Forms;

public class LoginForm : Form
{
    private readonly ApiClient _api = new();
    private TextBox txtEmail = new() { Width = 220 };

    public LoginForm()
    {
        Text = "Car Rental - Login";
        Width = 420; Height = 300; StartPosition = FormStartPosition.CenterScreen;
        var title = new Label { Text = "Car Rental Login", Font = new Font("Segoe UI", 18, FontStyle.Bold), AutoSize = true, Top = 25, Left = 90 };
        var lblEmail = new Label { Text = "Email", Top = 85, Left = 55, Width = 90 };
        txtEmail.Top = 82; txtEmail.Left = 145;
        var btnLogin = new Button { Text = "Login", Top = 130, Left = 145, Width = 100 };
        var btnRegister = new Button { Text = "Register", Top = 130, Left = 265, Width = 100 };
        btnLogin.Click += async (_, _) => await LoginAsync();
        btnRegister.Click += (_, _) => new RegisterForm().ShowDialog();
        Controls.AddRange([title, lblEmail, txtEmail, btnLogin, btnRegister]);
    }

    private async Task LoginAsync()
    {
        UserDto? user = await _api.LoginAsync(txtEmail.Text);
        if (user == null) { MessageBox.Show("Invalid email or API is not running."); return; }
        Hide();
        Form next = user.Role == "Admin" ? new AdminDashboardForm(user) : new UserDashboardForm(user);
        next.FormClosed += (_, _) => Close();
        next.Show();
    }
}
