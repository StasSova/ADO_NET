using System;
using System.Collections.Generic;

namespace DbCntx;

public partial class HistoryOfSell
{
    public HistoryOfSell() { }

    public HistoryOfSell(HistoryOfSell historyOfSell)
    {
        if (historyOfSell != null)
        {
            Id = historyOfSell.Id;
            ItemId = historyOfSell.ItemId;
            ManagerId = historyOfSell.ManagerId;
            BuyersCompany = historyOfSell.BuyersCompany;
            Count = historyOfSell.Count;
            Date = historyOfSell.Date;
        }
    }

    public int Id { get; set; }

    public int? ItemId { get; set; }

    public int? ManagerId { get; set; }

    public int? BuyersCompany { get; set; }

    public int Count { get; set; }

    public DateOnly Date { get; set; }

    public virtual Company BuyersCompanyNavigation { get; set; }

    public virtual Item Item { get; set; }

    public virtual Manager Manager { get; set; }
}

