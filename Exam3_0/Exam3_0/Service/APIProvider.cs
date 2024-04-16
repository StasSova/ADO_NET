using BookContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam3_0.Service;
public static class APIProvider
{
    public static IDataBaseAPI API { get; set; }

    public static void Initialize(IDataBaseAPI api)
    {
        API = api;
    }
}
