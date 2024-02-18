using _04_StationeryCompany.Base;
using _04_StationeryCompany.Interface;
using _04_StationeryCompany.MVVM.ViewModel;
using _04_StationeryCompany.MVVM.ViewModelResponseDB;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _04_StationeryCompany.MVVM
{
    public class VM_Main : VM_Base
    {
        private static string _connectionString;
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        public VM_Main()
        {

        }
        public override async void Initialize(object parameter)
        {
            base.Initialize(parameter);
            DataBaseCrud = (IDataBaseCrud)parameter;
            StorageCollection.DataBaseCrud = DataBaseCrud;
            await StorageCollection.FetchAllData();
        }

        public static IDataBaseCrud DataBaseCrud { get; set; }

        private DelegateCommand<Frame>? moveToItemsPage;
        private Type CurrentTypePage;
        private void MoveToItemsPage(Frame frame)
        {
            try
            {
                NavigationPageService.Instance.NavigateFrameTo<VM_ItemPage>(frame,this);
                CurrentTypePage = typeof(VM_ItemPage);
            }
            catch { }
        }
        private bool CanMoveToItemsPage() { return CurrentTypePage != typeof(VM_ItemPage); }
        public ICommand MoveToItemsPageCommand
        {
            get
            {
                if (moveToItemsPage == null)
                    moveToItemsPage = new DelegateCommand<Frame>(frame => MoveToItemsPage(frame), frame => CanMoveToItemsPage());
                return moveToItemsPage;
            }
        }


        private DelegateCommand<Frame>? moveToTypePage;
        private void MoveToTypePage(Frame frame)
        {
            try
            {
                NavigationPageService.Instance.NavigateFrameTo<VM_TypePage>(frame);
                CurrentTypePage = typeof(VM_TypePage);
            }
            catch { }
        }
        private bool CanMoveToTypePage() { return CurrentTypePage != typeof(VM_TypePage); }
        public ICommand MoveToTypePageCommand
        {
            get
            {
                if (moveToTypePage == null)
                    moveToTypePage = new DelegateCommand<Frame>(frame => MoveToTypePage(frame), frame => CanMoveToTypePage());
                return moveToTypePage;
            }
        }


        private DelegateCommand<Frame>? moveToManagerPage;
        private void MoveToManagerPage(Frame frame)
        {
            try
            {
                NavigationPageService.Instance.NavigateFrameTo<VM_ManagerPage>(frame);
                CurrentTypePage = typeof(VM_ManagerPage);
            }
            catch { }
        }
        private bool CanMoveToManagerPage() { return CurrentTypePage != typeof(VM_ManagerPage); }
        public ICommand MoveToManagerPageCommand
        {
            get
            {
                if (moveToManagerPage == null)
                    moveToManagerPage = new DelegateCommand<Frame>(frame => MoveToManagerPage(frame), frame => CanMoveToManagerPage());
                return moveToManagerPage;
            }
        }

        
        private DelegateCommand<Frame>? moveToCompanyPage;
        private void MoveToCompanyPage(Frame frame)
        {
            try
            {
                NavigationPageService.Instance.NavigateFrameTo<VM_CompanyPage>(frame);
                CurrentTypePage = typeof(VM_CompanyPage);
            }
            catch { }
        }
        private bool CanMoveToCompanyPage() { return CurrentTypePage != typeof(VM_CompanyPage); }
        public ICommand MoveToCompanyPageCommand
        {
            get
            {
                if (moveToCompanyPage == null)
                    moveToCompanyPage = new DelegateCommand<Frame>(frame => MoveToCompanyPage(frame), frame => CanMoveToCompanyPage());
                return moveToCompanyPage;
            }
        }

        
        private DelegateCommand<Frame>? moveToHystoryPage;
        private void MoveToHystoryPage(Frame frame)
        {
            try
            {
                NavigationPageService.Instance.NavigateFrameTo<VM_HystoryPage>(frame);
                CurrentTypePage = typeof(VM_HystoryPage);
            }
            catch { }
        }
        private bool CanMoveToHystoryPage() { return CurrentTypePage != typeof(VM_HystoryPage); }
        public ICommand MoveToHystoryPageCommand
        {
            get
            {
                if (moveToHystoryPage == null)
                    moveToHystoryPage = new DelegateCommand<Frame>(frame => MoveToHystoryPage(frame), frame => CanMoveToHystoryPage());
                return moveToHystoryPage;
            }
        }
    }
}
