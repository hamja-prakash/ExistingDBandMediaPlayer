using samplemaui.Helpers;
using samplemaui.Models;
using System.Collections.ObjectModel;
using System.Reflection;

namespace samplemaui;

public partial class ExistingDBDemo : ContentPage
{
    public ObservableCollection<Employee> Employees;
    public int EmployeeId = 0;
    public EmployeeRepository employeeRepository;

    public ExistingDBDemo()
    {
        InitializeComponent();
        employeeRepository = new EmployeeRepository();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindEmployeeData();
    }

    public void BindEmployeeData()
    {
        try
        {
            Employees = new ObservableCollection<Employee>();
            clvEmployees.ItemsSource = null;
            EmployeeRepository repository = new EmployeeRepository();
            foreach (var employee in repository.List())
            {
                Employees.Add(employee);
            }
            clvEmployees.ItemsSource = Employees.ToList();
        }
        catch (Exception ex)
        {
            var err = ex.Message;
        }
    }

    public bool ValidateControl()
    {
        bool isValid = false;
        if (string.IsNullOrEmpty(txtFirstName.Text))
        {
            App.Current.MainPage.DisplayAlert("Alert", "Please enter first name", "Ok");
            isValid = false;
        }
        else if (string.IsNullOrEmpty(txtLastName.Text))
        {
            App.Current.MainPage.DisplayAlert("Alert", "Please enter last name", "Ok");
            isValid = false;
        }
        else
            isValid = true;
        return isValid;
    }

    private void btnAddEmployee_Click(object sender, EventArgs e)
    {
        try
        {
            grdEmployees.IsVisible = false;
            grdAddEmployee.IsVisible = true;
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
        }
    }

    private async void FrmSave_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            await frmSave.ScaleTo(0.9, 100, Easing.Linear);
            await frmSave.ScaleTo(1.0, 100, Easing.Linear);
            if (ValidateControl())
            {
                Employee mEmployee = new Employee();
                mEmployee.FirstName = txtFirstName.Text;
                mEmployee.LastName = txtLastName.Text;
                if (EmployeeId > 0)
                {
                    mEmployee.EmployeeId = EmployeeId;
                    employeeRepository.UpdateEmployee(mEmployee);
                }
                else
                    employeeRepository.SaveEmployee(mEmployee);

                App.Current.MainPage.DisplayAlert("Alert", "Employee has been successfully save", "Ok");
                grdAddEmployee.IsVisible = false;
                grdEmployees.IsVisible = true;
                BindEmployeeData();
                //var employeeList = employeeRepository.List();
                //clvEmployees.ItemsSource= employeeList;
            }
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
        }
    }

    private void BtnEdit_Tapped(object sender, EventArgs e)
    {
        try
        {
            if (sender is ImageButton imgEdit && imgEdit.BindingContext is Employee mEmployee)
            {
                if (mEmployee.EmployeeId > 0)
                {
                    grdAddEmployee.IsVisible = true;
                    grdEmployees.IsVisible = false;
                    txtFirstName.Text = mEmployee.FirstName;
                    txtLastName.Text = mEmployee.LastName;
                    EmployeeId = mEmployee.EmployeeId;
                }
            }
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
        }
    }

    private async void BtnDelete_Tapped(object sender, EventArgs e)
    {
        try
        {
            if (sender is ImageButton imgEdit && imgEdit.BindingContext is Employee mEmployee)
            {
                if (mEmployee.EmployeeId > 0)
                {
                    var alertmsg = await App.Current.MainPage.DisplayAlert("Alert", "Are you sure you want to delete this record?", "Ok", "Cancel");
                    if (alertmsg)
                    {
                        employeeRepository.DeleteEmployee(mEmployee.EmployeeId);
                        BindEmployeeData();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
        }
    }
}