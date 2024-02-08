using _02_ConnectionToDB_RequestToDB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using _02_ConnectionToDB_RequestToDB.Response;

namespace _02_ConnectionToDB_RequestToDB.MVVM
{
    public class VM_Main : VM_Base
    {
        private DB_Crud _dbCrud;
        public VM_Main()
        {
            _dbCrud = new DB_Crud();
        }

        private ObservableCollection<VM_Product> _product = new();
        public ObservableCollection<VM_Product> Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }
        public static Dictionary<int, string> type_product;
        private void FetchData(string request)
        {
            try
            {
                if (type_product == null) type_product = _dbCrud.GetTypesProduct();
                Product.Clear();
                List<VM_Product> products = Convert_M_ToVM(_dbCrud.ExecuteReader(request));
                foreach (var p in products)
                {
                    Product.Add(p);
                }
            }
            catch { }
        }
        private List<VM_Product> Convert_M_ToVM(IEnumerable<Product> products)
        {
            try
            {
                return products.Select(x => new VM_Product(x)).ToList();
            }
            catch
            {
                return new List<VM_Product>(); // Возвращаем пустой список в случае ошибки
            }
        }

        private DelegateCommand? connectCommand;
        private void Connect()
        {
            try
            {
                if (_dbCrud.Connection())
                    MessageBox.Show($"Вы подключились к ${_dbCrud.Database}", "Success");
                FetchData(RequestList.GetAllProducts);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
        private bool CanConnect()
        {
            return true;
        }
        public ICommand ConnectCommand
        {
            get
            {
                if (connectCommand == null)
                    connectCommand = new DelegateCommand(ex => Connect(), ce => CanConnect());
                return connectCommand;
            }
        }

        
        private DelegateCommand getAllName;
        private void GetAllName()
        {
            try
            {
                FetchData(RequestList.GetAllProducts);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
        private bool CanGetAllName() { return true; }
        public ICommand GetAllNameCommand
        {
            get
            {
                if (getAllName == null)
                    getAllName = new DelegateCommand(ex => GetAllName(), ce => CanGetAllName());
                return getAllName;
            }
        }

        
        
        
        private DelegateCommand getAllColors;
        private void GetAllColors()
        {
            try
            {
                Product.Clear();
                List<VM_Product> products = Convert_M_ToVM(_dbCrud.GetAllColor(RequestList.GetUniqueColors));
                foreach (var p in products)
                {
                    Product.Add(p);
                }
            }
            catch { }
        }
        private bool CanGetAllColors() { return true; }
        public ICommand GetAllColorsCommand
        {
            get
            {
                if (getAllColors == null)
                    getAllColors = new DelegateCommand(ex => GetAllColors(), ce => CanGetAllColors());
                return getAllColors;
            }
        }




        private DelegateCommand getMaxCallor;
        private void GetMaxCallor()
        {
            try
            {
                FetchData(RequestList.GetMaxCalories);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
        private bool CanGetMaxCallor() { return true; }
        public ICommand GetMaxCallorCommand
        {
            get
            {
                if (getMaxCallor == null)
                    getMaxCallor = new DelegateCommand(ex => GetMaxCallor(), ce => CanGetMaxCallor());
                return getMaxCallor;
            }
        }




        private DelegateCommand getMinCallor;
        private void GetMinCallor()
        {
            try
            {
                FetchData(RequestList.GetMinCalories);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
        private bool CanGetMinCallor() { return true; }
        public ICommand GetMinCallorCommand
        {
            get
            {
                if (getMinCallor == null)
                    getMinCallor = new DelegateCommand(ex => GetMinCallor(), ce => CanGetMinCallor());
                return getMinCallor;
            }
        }


        private DelegateCommand getAvgCallor;
        private void GetAvgCallor()
        {
            try
            {
                FetchData(RequestList.GetAvgCalories);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
        private bool CanGetAvgCallor() { return true; }
        public ICommand GetAvgCallorCommand
        {
            get
            {
                if (getAvgCallor == null)
                    getAvgCallor = new DelegateCommand(ex => GetAvgCallor(), ce => CanGetAvgCallor());
                return getAvgCallor;
            }
        }





        private DelegateCommand getCountOfVegatebles;
        private void GetCountOfVegatebles()
        {
            try
            {
                string response = _dbCrud.GetNumber(RequestList.GetNumberOfVegateble).ToString();
                MessageBox.Show(response, "Information");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
        private bool CanGetCountOfVegatebles() { return true; }
        public ICommand GetCountOfVegateblesCommand
        {
            get
            {
                if (getCountOfVegatebles == null)
                    getCountOfVegatebles = new DelegateCommand(ex => GetCountOfVegatebles(), ce => CanGetCountOfVegatebles());
                return getCountOfVegatebles;
            }
        }


        private DelegateCommand getCountOfFruit;
        private void GetCountOfFruit()
        {
            try
            {
                string response = _dbCrud.GetNumber(RequestList.GetNumberOfFruit).ToString();
                MessageBox.Show(response, "Information");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
        private bool CanGetCountOfFruit() { return true; }
        public ICommand GetCountOfFruitCommand
        {
            get
            {
                if (getCountOfFruit == null)
                    getCountOfFruit = new DelegateCommand(ex => GetCountOfFruit(), ce => CanGetCountOfFruit());
                return getCountOfFruit;
            }
        }




        private string call_down;
        public string Call_down 
        { 
            get { return call_down; }
            set { call_down = value; OnPropertyChanged(nameof(Call_down)); }
        }
        
        private string call_up;
        public string Call_up
        {
            get { return call_up; }
            set { call_up = value; OnPropertyChanged(nameof(Call_up)); }
        }

        
        private DelegateCommand getDownCall;
        private void GetDownCall()
        {
            try
            {
                FetchData(RequestList.GetAllDownCall.Replace("[CALLORIES_DOWN]", Call_down));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
        private bool CanGetDownCall() { return true; }
        public ICommand GetDownCallCommand
        {
            get
            {
                if (getDownCall == null)
                    getDownCall = new DelegateCommand(ex => GetDownCall(), ce => CanGetDownCall());
                return getDownCall;
            }
        }

        private DelegateCommand getUpCall;
        private void GetUpCall()
        {
            try
            {
                FetchData(RequestList.GetAllUpCall.Replace("[CALLORIES_UP]", Call_up));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
        private bool CanGetUpCall() { return true; }
        public ICommand GetUpCallCommand
        {
            get
            {
                if (getUpCall == null)
                    getUpCall = new DelegateCommand(ex => GetUpCall(), ce => CanGetUpCall());
                return getUpCall;
            }
        }

        private DelegateCommand getBeetwCall;
        private void GetBeetwCall()
        {
            try
            {
                FetchData(RequestList.GetAllBettwenCall
                        .Replace("[CALLORIES_DOWN]", Call_up)
                        .Replace("[CALLORIES_UP]", Call_down)
                        );
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
        private bool CanGetBeetwCall() { return true; }
        public ICommand GetBeetwCallCommand
        {
            get
            {
                if (getBeetwCall == null)
                    getBeetwCall = new DelegateCommand(ex => GetBeetwCall(), ce => CanGetBeetwCall());
                return getBeetwCall;
            }
        }

    }
}
