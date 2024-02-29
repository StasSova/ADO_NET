using _08_CodeFirst_DLL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_CodeFirst_DLL.ViewModel
{
    public abstract class VM_Page : VM_Base
    {
        public virtual void Initialize(object parameter) { }
    }
}
