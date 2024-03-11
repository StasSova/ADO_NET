using System;
using System.Collections.Generic;

namespace DbCntx;

public partial class Item
{
    public Item()
    {
        Name = "Item";
        Type = new TypeOfItem();
    }
    public Item(Item item)
    {
        if (item != null)
        {
            Id = item.Id;
            Name = item.Name;
            TypeId = item.TypeId;
            Count = item.Count;
            CostPrice = item.CostPrice;
            Price = item.Price;

            // Создаем новый экземпляр объекта TypeOfItem или просто присваиваем ссылку на существующий объект
            Type = item.Type;

            // Присваиваем значение свойству TypeId, так как оно является внешним ключом
            TypeId = item.TypeId;
        }
    }
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? TypeId { get; set; }

    public int? Count { get; set; }

    public decimal? CostPrice { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<HistoryOfSell> HistoryOfSells { get; set; } = new List<HistoryOfSell>();

    private TypeOfItem? _type;
    public virtual TypeOfItem? Type
    {
        get { return _type; }
        set
        {
            _type = value;
            this.TypeId = value?.Id;
        }
    }
}
