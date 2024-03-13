using CommunityToolkit.Mvvm.ComponentModel;
using DLL_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_Migration.ViewModels;

public partial class VM_Game: ObservableObject
{
    [ObservableProperty] M_Game model;
    public VM_Game(M_Game model)
    {
        Model = model;
        Styles = new ObservableCollection<VM_GameStyle>(Model.Styles.Select(x => new VM_GameStyle(x)));
        Features = new ObservableCollection<VM_FeaturesOfGame>(Model.FeaturesOfGame.Select(x=> new VM_FeaturesOfGame(x)));
        Studios = new ObservableCollection<VM_GameStudio>(Model.Studios.Select(x=> new VM_GameStudio(x)));
    }
    public string Name 
    {
        get {  return Model.Name; }
        set
        {
            if (value != Model.Name)
            {
                Model.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        } 
    }
    public DateTime Release
    {
        get { return Model.Release; }
        set
        {
            if (value != Model.Release)
            {
                Model.Release = value;
                OnPropertyChanged(nameof(Release));
            }
        }
    }

    public int Copies
    {
        get { return Model.CopiesSold; }
        set
        {
            if (Model.CopiesSold != value)
            {
                Model.CopiesSold = value;
                OnPropertyChanged(nameof(Copies));
            }
        }
    }

    public ObservableCollection<VM_GameStyle> Styles { get; private set; }
    public ObservableCollection<VM_GameStudio> Studios { get; private set; }
    public ObservableCollection<VM_FeaturesOfGame> Features { get; private set; }
}
