using _12_Migration.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using DLL_DbContext;
using DLL_DbContext.Implementation;
using DLL_DbContext.Interfaces;
using DLL_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12_Migration
{
    public partial class VM_Main : ObservableObject
    {
        public IDataBaseAPI dataBaseAPI { get; set; }
        [ObservableProperty] ObservableCollection<VM_Game> games;
        [ObservableProperty] ObservableCollection<VM_GameStudio> studios;
        [ObservableProperty] ObservableCollection<VM_GameStyle> styles;

        public VM_Main() 
        {
            dataBaseAPI = new DataBaseAPI_D(new GameDbContext());
            FetchData();
        }
        public VM_Main(GameDbContext context) 
        {
            dataBaseAPI = new DataBaseAPI_D(context);
            FetchData();
        }
        public VM_Main(IDataBaseAPI API)
        {
            dataBaseAPI = API;
            FetchData();
        }
        
        private async Task FetchData()
        {
            //var games = await dataBaseAPI.Get<M_GameStyle>();
            //Styles = new ObservableCollection<VM_GameStyle>(games.Select(g => new VM_GameStyle(g)));

            var gameModels = await dataBaseAPI.Get<M_Game>();
            Games = new ObservableCollection<VM_Game>(gameModels.Select(g => new VM_Game(g)));

            //var studioModels = await dataBaseAPI.Get<M_GameStudio>();
            //Studios = new ObservableCollection<VM_GameStudio>(studioModels.Select(s => new VM_GameStudio(s)));
        }
    }
}
