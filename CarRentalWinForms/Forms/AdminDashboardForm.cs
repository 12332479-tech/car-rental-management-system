using CarRentalWinForms.Models;
using CarRentalWinForms.Services;

namespace CarRentalWinForms.Forms;

public class AdminDashboardForm : Form
{
    private readonly ApiClient _api = new();
    private DataGridView gridCars = new() { Left = 15, Top = 190, Width = 600, Height = 280, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private DataGridView gridReservations = new() { Left = 630, Top = 55, Width = 520, Height = 415, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private TextBox txtId = new() { Left = 90, Top = 55, Width = 70, ReadOnly = true };
    private TextBox txtBrand = new() { Left = 90, Top = 85, Width = 150 };
    private TextBox txtModel = new() { Left = 90, Top = 115, Width = 150 };
    private TextBox txtColor = new() { Left = 315, Top = 55, Width = 150 };
    private TextBox txtClass = new() { Left = 315, Top = 85, Width = 150 };
    private NumericUpDown numPrice = new() { Left = 315, Top = 115, Width = 150, Maximum = 10000, DecimalPlaces = 2 };
    private CheckBox chkAvailable = new() { Left = 485, Top = 85, Text = "Available", Checked = true };

    public AdminDashboardForm(UserDto user)
    {
        Text = "Admin Dashboard - Car Rental"; Width = 1180; Height = 540; StartPosition = FormStartPosition.CenterScreen;
        Controls.Add(new Label { Text = "Admin Panel: Manage Cars and Reservations", Left = 15, Top = 15, AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold) });
        AddLabel("ID", 20, 58); AddLabel("Brand", 20, 88); AddLabel("Model", 20, 118);
        AddLabel("Color", 250, 58); AddLabel("Class", 250, 88); AddLabel("Price/Day", 250, 118);
        var btnAdd = new Button { Text = "Add", Left = 15, Top = 150, Width = 90 };
        var btnUpdate = new Button { Text = "Update", Left = 115, Top = 150, Width = 90 };
        var btnDelete = new Button { Text = "Delete", Left = 215, Top = 150, Width = 90 };
        var btnClear = new Button { Text = "Clear", Left = 315, Top = 150, Width = 90 };
        var btnRefresh = new Button { Text = "Refresh", Left = 415, Top = 150, Width = 90 };
        btnAdd.Click += async (_, _) => await AddCarAsync();
        btnUpdate.Click += async (_, _) => await UpdateCarAsync();
        btnDelete.Click += async (_, _) => await DeleteCarAsync();
        btnClear.Click += (_, _) => ClearFields();
        btnRefresh.Click += async (_, _) => await LoadAllAsync();
        gridCars.SelectionChanged += (_, _) => FillFields();
        Controls.AddRange([txtId, txtBrand, txtModel, txtColor, txtClass, numPrice, chkAvailable, btnAdd, btnUpdate, btnDelete, btnClear, btnRefresh, gridCars, gridReservations]);
        Controls.Add(new Label { Text = "Customer Booking Information", Left = 630, Top = 25, AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold) });
        Shown += async (_, _) => await LoadAllAsync();
    }

    private void AddLabel(string text, int x, int y) => Controls.Add(new Label { Text = text, Left = x, Top = y, Width = 70 });
    private async Task LoadAllAsync() { gridCars.DataSource = await _api.GetCarsAsync(); gridReservations.DataSource = await _api.GetReservationsAsync(); }
    private Car CurrentCarFromFields() => new()
    {
        CarId = int.TryParse(txtId.Text, out var id) ? id : 0,
        Brand = txtBrand.Text,
        Model = txtModel.Text,
        Color = txtColor.Text,
        CarClass = txtClass.Text,
        PricePerDay = numPrice.Value,
        IsAvailable = chkAvailable.Checked
    };
    private void FillFields()
    {
        if (gridCars.CurrentRow?.DataBoundItem is not Car car) return;
        txtId.Text = car.CarId.ToString(); txtBrand.Text = car.Brand; txtModel.Text = car.Model; txtColor.Text = car.Color; txtClass.Text = car.CarClass; numPrice.Value = car.PricePerDay; chkAvailable.Checked = car.IsAvailable;
    }
    private void ClearFields() { txtId.Clear(); txtBrand.Clear(); txtModel.Clear(); txtColor.Clear(); txtClass.Clear(); numPrice.Value = 0; chkAvailable.Checked = true; }
    private async Task AddCarAsync() { bool ok = await _api.AddCarAsync(CurrentCarFromFields()); MessageBox.Show(ok ? "Car added." : "Add failed."); await LoadAllAsync(); }
    private async Task UpdateCarAsync() { bool ok = await _api.UpdateCarAsync(CurrentCarFromFields()); MessageBox.Show(ok ? "Car updated." : "Update failed."); await LoadAllAsync(); }
    private async Task DeleteCarAsync() { if (!int.TryParse(txtId.Text, out int id)) return; bool ok = await _api.DeleteCarAsync(id); MessageBox.Show(ok ? "Car deleted." : "Delete failed."); await LoadAllAsync(); ClearFields(); }
}
