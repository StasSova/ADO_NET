using _13_Exam_BookShop.Service;
using _13_Exam_BookShop.ViewModels.DbViewModels;
using BookStore_DbContext.Interface;
using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_Exam_BookShop.ViewModels.Pages.ViewModel;

public partial class VM_UserPage : VM_Page
{
    IDataBaseAPI API;
    [ObservableProperty] VM_User user;
    [ObservableProperty] ObservableCollection<VM_BookForSale> books;
    public VM_UserPage()
    {
        API = StorageCollection.API;
        User = VM_User.GetInstance();
        _ = Initialize();
    }
    private async Task Initialize()
    {
        Books = User.CurrentShoppingCarts.Books;
    }
}
