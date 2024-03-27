using _04_StationeryCompany.MVVM.ViewModelResponseDB;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany.MVVM.Insert.ViewModel
{
    public partial class VM_InsertItem : ObservableObject
    {
        [ObservableProperty] private VM_Item item;
        [ObservableProperty] private StorageCollection storage;
        [ObservableProperty] private VM_TypeOfItem selectedTypeItem;
        public VM_InsertItem()
        {
            this.Item = new VM_Item(new DataBaseResponse.M_Item());
            this.Storage = StorageCollection.Instance;
            this.SelectedTypeItem = Storage.TypeOfItems[0];
            this.Item.Id = SelectedTypeItem.Id;
        }
    }
}
