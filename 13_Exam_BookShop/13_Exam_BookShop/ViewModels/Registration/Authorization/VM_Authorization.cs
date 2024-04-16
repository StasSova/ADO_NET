using _13_Exam_BookShop.Service;
using _13_Exam_BookShop.ViewModels.DbViewModels;
using _13_Exam_BookShop.ViewModels.Pages.ViewModel;
using BookStore_DbContext.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _13_Exam_BookShop.ViewModels.Registration.Authorization;

public partial class VM_Authorization : VM_Page
{
    IDataBaseAPI API;
    [ObservableProperty] string username;
    [ObservableProperty] string password;

    public event EventHandler RegistrationSuccess;

    public event EventHandler AuthorizationSuccess;

    private void OnUserActionSuccess(object sender, EventArgs e)
    {
        try
        {
            NavigationPageService.Instance.NavigateTo<VM_BookPage>();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public VM_Authorization()
    {
#if DEBUG
        Username = "Stas2";
        Password = "Stas2";
#endif 
        API = StorageCollection.API;
        RegistrationSuccess += OnUserActionSuccess;
        AuthorizationSuccess += OnUserActionSuccess;
    }

    // Метод для регистрации пользователя
    [RelayCommand]
    async Task Registration()
    {
        // Проверяем, что поля username и password не пустые
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            // Выполняем запрос к API для проверки существования пользователя с таким именем
            bool userExists = await API.CheckUserExists(username);

            if (userExists)
            {
                MessageBox.Show("User with this username already exists.");
            }
            else
            {
                // Выполняем регистрацию пользователя
                var user = await API.RegisterUser(username, password);
                VM_User.SetInstance(user);
                if (user != null)
                {
                    // Вызываем событие успешной регистрации
                    RegistrationSuccess?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show("Registration successful.");
                }
                else
                {
                    MessageBox.Show("Failed to register user.");
                }
            }
        }
        else
        {
            MessageBox.Show("Please enter both username and password.");
        }
    }

    // Метод для авторизации пользователя
    [RelayCommand]
    async Task Authorization()
    {
        // Проверяем, что поля username и password не пустые
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            // Выполняем запрос к API для авторизации пользователя
            var user = await API.AuthorizeUser(username, password);

            if (user != null)
            {
                VM_User.SetInstance(user);
                // Вызываем событие успешной авторизации
                AuthorizationSuccess?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Authorization successful.");
            }
            else
            {
                MessageBox.Show("Failed to authorize user. Please check your credentials.");
            }
        }
        else
        {
            MessageBox.Show("Please enter both username and password.");
        }
    }
}