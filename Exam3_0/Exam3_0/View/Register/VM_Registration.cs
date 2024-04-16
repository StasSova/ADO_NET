using BookContext.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Exam3_0.Service;
using Exam3_0.View.Book;
using Exam3_0.View.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Exam3_0.View.Register;
public partial class VM_Registration : VM_Page
{
    [ObservableProperty] string password;
    [ObservableProperty] string user;
    private IDataBaseAPI API;
    public VM_Registration()
    {
        API = APIProvider.API;
#if DEBUG
        Password = "Stas";
        User = "Stas";
#endif
    }

    [RelayCommand]
    async Task Registration()
    {
        try
        {
            var result = await API.RegisterUser(User, Password);
            if (result != null)
            {
                NavigationPageService.Instance.NavigateTo<VM_Books_Page>("Index");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }

    [RelayCommand]
    async Task Authorization()
    {
        try
        {
            var result = await API.AuthorizeUser(User, Password);
            if (result != null)
            {
                NavigationPageService.Instance.NavigateTo<VM_Books_Page>("Index");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }
}
