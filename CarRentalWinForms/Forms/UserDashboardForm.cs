using CarRentalWinForms.Models;
using CarRentalWinForms.Services;

namespace CarRentalWinForms.Forms;

public class UserDashboardForm : Form
{
    private readonly ApiClient _api = new();
    private readonly UserDto _user;
    private DataGridView grid = new() { Dock = DockStyle.Bottom, Height = 300, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private TextBox txtSearch = new() { Left = 20, Top = 60, Width = 220 };
    private DateTimePicker dtStart = new() { Left = 20, Top = 105, Width = 180 };
    private DateTimePicker dtEnd = new() { Left = 220, Top = 105, Width = 180 };

    public UserDashboardForm(UserDto user)
    {
        _user = user;
        Text = "Customer Dashboard"; Width = 850; Height = 520; StartPosition = FormStartPosition.CenterScreen;
        Controls.Add(new Label { Text = $"Welcome {_user.FullName}", Left = 20, Top = 20, AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold) });
        var btnSearch = new Button { Text = "Search Cars", Left = 260, Top = 58, Width = 110 };
        var btnBook = new Button { Text = "Book Selected Car", Left = 420, Top = 103, Width = 140 };
        btnSearch.Click += async (_, _) => await LoadCarsAsync();
        btnBook.Click += async (_, _) => await BookSelectedAsync();
        Controls.AddRange([txtSearch, btnSearch, new Label{Text="Start Date",Left=20,Top=88}, new Label{Text="End Date",Left=220,Top=88}, dtStart, dtEnd, btnBook, grid]);
        Shown += async (_, _) => await LoadCarsAsync();
    }

    private async Task LoadCarsAsync() => grid.DataSource = await _api.GetCarsAsync(txtSearch.Text);

    private async Task BookSelectedAsync()
    {
        if (grid.CurrentRow?.DataBoundItem is not Car car) { MessageBox.Show("Select a car first."); return; }
        if (!car.IsAvailable) { MessageBox.Show("This car is not available."); return; }
        bool ok = await _api.BookAsync(_user.UserId, car.CarId, dtStart.Value, dtEnd.Value);
        MessageBox.Show(ok ? "Reservation booked successfully." : "Booking failed.");
        await LoadCarsAsync();
    }
}
