using System;
using System.Collections.Generic;

namespace DbCntx;

public partial class Manager
{
    public Manager()
    {
        Fname = "First name";
        Lname = "Last name";
    }
    public Manager(Manager manager)
    {
        if (manager != null)
        {
            Id = manager.Id;
            Fname = manager.Fname;
            Lname = manager.Lname;

            // Копируем коллекцию HistoryOfSells
            HistoryOfSells = new List<HistoryOfSell>(manager.HistoryOfSells.Select(h => new HistoryOfSell(h)));
        }
    }
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public virtual ICollection<HistoryOfSell> HistoryOfSells { get; set; } = new List<HistoryOfSell>();
}
