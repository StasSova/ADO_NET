using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany;
public static class Extensions
{
    public static List<T> Sort<T, TKey>(
       this IEnumerable<T> list,
       Func<T, TKey> keyExtractor,
       ListSortDirection direction
    )
    {
        return direction == ListSortDirection.Ascending ?
            list.OrderBy(keyExtractor).ToList() :
            list.OrderByDescending(keyExtractor).ToList();
    }

    public static void Sort<T, TKey>(
        this ObservableCollection<T> collection,
        Func<T, TKey> keyExtractor,
        ListSortDirection direction
    )
    {
        var sorted = direction == ListSortDirection.Ascending ?
            collection.OrderBy(keyExtractor).ToList() :
            collection.OrderByDescending(keyExtractor).ToList();

        for (var i = 0; i < sorted.Count; i++)
        {
            collection.Move(collection.IndexOf(sorted[i]), i);
        }
    }
}
