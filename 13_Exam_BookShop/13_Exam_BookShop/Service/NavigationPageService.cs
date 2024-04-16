using _13_Exam_BookShop.ViewModels;
using _13_Exam_BookShop.ViewModels.Pages.View;
using _13_Exam_BookShop.ViewModels.Pages.ViewModel;
using _13_Exam_BookShop.ViewModels.Registration.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _13_Exam_BookShop.Service;

public class NavigationPageService
{
    private readonly Dictionary<Type, Type> _mappings;
    private static NavigationPageService? _instance;
    private Type? CurrentViewModel;
    private NavigationPageService()
    {
        _mappings = new Dictionary<Type, Type>();
        CreatePageViewModelMappings();
    }
    public static NavigationPageService Instance
    {
        get
        {
            if (_instance == null)
                _instance = new NavigationPageService();

            return _instance;
        }
    }

    public void NavigateTo<TViewModel>() where TViewModel : VM_Page
    {
        if (!IsCurrentPage<TViewModel>())
        {
            InternalNavigateTo(typeof(TViewModel), null!);
        }
    }

    public void NavigateTo<TViewModel>(object parameter) where TViewModel : VM_Page
    {
        if (!IsCurrentPage<TViewModel>())
        {
            InternalNavigateTo(typeof(TViewModel), parameter);
        }
    }

    protected virtual void InternalNavigateTo(Type viewModelType, object parameter)
    {
        Type pageType = GetPageTypeForViewModel(viewModelType);

        if (pageType == null)
        {
            throw new Exception($"Mapping type for {viewModelType} is not a page");
        }

        Page page = (Page)Activator.CreateInstance(pageType)!;
        VM_Page viewModel = (VM_Page)Activator.CreateInstance(viewModelType)!;
        page.DataContext = viewModel;
        page.BeginInit();

        if (viewModel != null)
        {
            viewModel.Initialize(parameter);
        }

        if (Application.Current.MainWindow != null)
        {
            // Находим Frame по его имени "MainFrame"
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                // Выполняем навигацию на страницу
                frame.Navigate(page);
            }
        }

        CurrentViewModel = viewModelType;
    }


    protected Type GetPageTypeForViewModel(Type viewModelType)
    {
        if (!_mappings.ContainsKey(viewModelType))
        {
            throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
        }
        return _mappings[viewModelType];
    }

    private void CreatePageViewModelMappings()
    {
        _mappings.Add(typeof(VM_BookPage), typeof(V_BookPage));
        _mappings.Add(typeof(VM_Authorization), typeof(V_Authorization));
        _mappings.Add(typeof(VM_UserPage), typeof(V_UserPage));
        _mappings.Add(typeof(VM_Add_Edit_Book), typeof(V_Add_Edit_Book));
    }

    public bool IsCurrentPage<TViewModel>() where TViewModel : VM_Page
    {
        return false;
        try
        {
            return typeof(TViewModel) == CurrentViewModel;
        }
        catch { return false; }
    }
}