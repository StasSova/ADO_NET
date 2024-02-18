using _04_StationeryCompany.MVVM;
using _04_StationeryCompany.MVVM.View;
using _04_StationeryCompany.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _04_StationeryCompany.Base
{
    public class NavigationPageService
    {
        private readonly Dictionary<Type, Type> _mappings;
        private static NavigationPageService _instance;
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

        private Type CurrentViewModel;
        public void NavigateTo<TViewModel>() where TViewModel : VM_Base
        {
            InternalNavigateTo(typeof(TViewModel), null);
        }

        public void NavigateTo<TViewModel>(object parameter) where TViewModel : VM_Base
        {
            InternalNavigateTo(typeof(TViewModel), parameter);
        }
        public void NavigateFrameTo<TViewModel>(Frame frame, object parameter = null) where TViewModel : VM_Base
        {
            InternalNavigateFrameTo(frame, typeof(TViewModel), parameter);
        }
        protected virtual void InternalNavigateTo(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = (Page)Activator.CreateInstance(pageType);
            if (page.DataContext is VM_Base viewModel)
            {
                viewModel.Initialize(parameter);
            }

            if (Application.Current.MainWindow != null && Application.Current.MainWindow.Content is Frame frame)
            {
                frame.Navigate(page);
            }

            CurrentViewModel = viewModelType;
        }
        protected virtual void InternalNavigateFrameTo(Frame frame, Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = (Page)Activator.CreateInstance(pageType);
            if (page.DataContext is VM_Base viewModel)
            {
                viewModel.Initialize(parameter);
            }

            frame.Navigate(page);

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
            _mappings.Add(typeof(VM_Main), typeof(V_Main));

            _mappings.Add(typeof(VM_CompanyPage), typeof(V_CompanyPage));
            _mappings.Add(typeof(VM_ItemPage), typeof(V_ItemPage));
            _mappings.Add(typeof(VM_ManagerPage), typeof(V_ManagerPage));
            _mappings.Add(typeof(VM_TypePage), typeof(V_TypePage));
            _mappings.Add(typeof(VM_HystoryPage), typeof(V_HystoryPage));
        }

        public bool IsCurrentPage<TViewModel>() where TViewModel : VM_Base
        {
            try
            {
                return typeof(TViewModel) == CurrentViewModel;
            }
            catch { return false; }
        }
    }
}
