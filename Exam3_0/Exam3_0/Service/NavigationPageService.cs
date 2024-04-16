using Exam3_0.Models;
using Exam3_0.View;
using Exam3_0.View.Book;
using Exam3_0.View.Books;
using Exam3_0.View.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Exam3_0.Service
{
    public class NavigationPageService
    {
        private readonly Dictionary<(string, Type), (Type, Type)> _mappings;
        private static NavigationPageService? _instance;
        private Type? CurrentViewModel;

        private NavigationPageService()
        {
            _mappings = new Dictionary<(string, Type), (Type, Type)>();
            CreatePageViewModelMappings();
        }
        private void CreatePageViewModelMappings()
        {
            _mappings.Add(("Registration", typeof(VM_Registration)), (typeof(VM_Registration), typeof(V_Registration)));
            _mappings.Add(("Details", typeof(VM_Book_Page)), (typeof(VM_Book_Page), typeof(V_Book_Details)));
            _mappings.Add(("Edit", typeof(VM_Book_Page)), (typeof(VM_Book_Page), typeof(V_Book_Edit)));
            _mappings.Add(("Create", typeof(VM_Book_Page)), (typeof(VM_Book_Page), typeof(V_Book_Add)));


            _mappings.Add(("Index", typeof(VM_Books_Page)), (typeof(VM_Books_Page), typeof(V_Books_Index)));

            // Add more mappings here...
        }

        public static NavigationPageService Instance => _instance ??= new NavigationPageService();

        public void NavigateTo<TViewModel>(string action) where TViewModel : VM_Page
        {
            if (!IsCurrentPage<TViewModel>())
            {
                var (viewModelType, pageType) = GetTypesForAction(action, typeof(TViewModel));
                InternalNavigateTo(viewModelType, pageType, null);
            }
        }

        public void NavigateTo<TViewModel>(string action, object parameter) where TViewModel : VM_Page
        {
            if (!IsCurrentPage<TViewModel>())
            {
                var (viewModelType, pageType) = GetTypesForAction(action, typeof(TViewModel));
                InternalNavigateTo(viewModelType, pageType, parameter);
            }
        }

        protected virtual void InternalNavigateTo(Type viewModelType, Type pageType, object parameter)
        {
            Page page = (Page)Activator.CreateInstance(pageType)!;
            VM_Page viewModel = (VM_Page)Activator.CreateInstance(viewModelType)!;
            page.DataContext = viewModel;

            viewModel?.Initialize(parameter);

            if (Application.Current.MainWindow?.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(page);
            }

            CurrentViewModel = viewModelType;
        }

        protected (Type, Type) GetTypesForAction(string action, Type viewModelType)
        {
            var key = (action, viewModelType);
            if (!_mappings.ContainsKey(key))
            {
                throw new KeyNotFoundException($"No map for {key} was found on navigation mappings");
            }
            return _mappings[key];
        }



        public bool IsCurrentPage<TViewModel>() where TViewModel : VM_Page
        {
            return typeof(TViewModel) == CurrentViewModel;
        }
    }
}
