using _04_StationeryCompany.MVVM.ViewModelResponseDB;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany.MVVM.Update.ViewModel
{
    public partial class VM_UpdateItem : ObservableObject
    {
        [ObservableProperty] private VM_Item item;
        [ObservableProperty] private StorageCollection storage;
        [ObservableProperty] private VM_TypeOfItem selectedTypeItem;
        public VM_UpdateItem(VM_Item item)
        {
            this.Item = new VM_Item(item);
            this.Storage = StorageCollection.Instance;
            this.SelectedTypeItem = Storage.TypeOfItems.FirstOrDefault(x => x.Id == Item.Id) ?? Storage.TypeOfItems[0];
            this.Item.Id = SelectedTypeItem.Id;
        }
        public IRelayCommand SaveCommand { get; }
        private void Save()
        {
            try
            {
                
            }
            catch { }
        }

    }
}
