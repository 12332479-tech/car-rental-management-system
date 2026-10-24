using CarRentalWinForms.Services;

namespace CarRentalWinForms.Forms;

public class RegisterForm : Form
{
    private readonly ApiClient _api = new();
    private TextBox txtName = new() { Width = 220 };
    private TextBox txtEmail = new() { Width = 220 };
    private TextBox txtPhone = new() { Width = 220 };

    public RegisterForm()
    {
        Text = "Create Account"; Width = 420; Height = 330; StartPosition = FormStartPosition.CenterParent;
        var y = 30;
        Controls.Add(new Label { Text = "Full Name", Left = 50, Top = y, Width = 90 }); txtName.Left = 150; txtName.Top = y; Controls.Add(txtName); y += 40;
        Controls.Add(new Label { Text = "Email", Left = 50, Top = y, Width = 90 }); txtEmail.Left = 150; txtEmail.Top = y; Controls.Add(txtEmail); y += 40;
        Controls.Add(new Label { Text = "Phone", Left = 50, Top = y, Width = 90 }); txtPhone.Left = 150; txtPhone.Top = y; Controls.Add(txtPhone); y += 50;
        var btn = new Button { Text = "Register", Left = 150, Top = y, Width = 120 };
        btn.Click += async (_, _) =>
        {
            var user = await _api.RegisterAsync(txtName.Text, txtEmail.Text, txtPhone.Text);
            MessageBox.Show(user == null ? "Registration failed." : "Account created successfully.");
            if (user != null) Close();
        };
        Controls.Add(btn);
    }
}
