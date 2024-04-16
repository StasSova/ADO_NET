using _13_Exam_BookShop.Service;
using _13_Exam_BookShop.ViewModels.DbViewModels;
using BookStore_DbContext.Interface;
using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _13_Exam_BookShop.ViewModels.Pages.ViewModel;

public partial class VM_Main : VM_Page
{
    public VM_Main()
    {

    }

    public override async void Initialize(object parameter)
    {

    }

    [RelayCommand]
    void MoveToUserAccount()
    {
        if (VM_User.GetInstance() != null)
        {
            NavigationPageService.Instance.NavigateTo<VM_UserPage>();
        }
        else
        {
            MessageBox.Show("Please log in to Account or Register if you don't have an account yet.");
        }
    }

}
