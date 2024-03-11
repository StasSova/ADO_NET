using System;
using System.Collections.Generic;

namespace DbCntx;

public partial class Company
{
    public Company()
    {
        Name = "Company name";
    }
    public Company(Company company)
    {
        if (company != null)
        {
            Id = company.Id;
            Name = company.Name;

            // Копируем коллекцию HistoryOfSells
            HistoryOfSells = new List<HistoryOfSell>(company.HistoryOfSells.Select(h => new HistoryOfSell(h)));
        }
    }
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<HistoryOfSell> HistoryOfSells { get; set; } = new List<HistoryOfSell>();
}
