using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2_Server_Picker
{
    public static class AppInstance
    {
        public static MainForm Get()
        {
            foreach (Form f in Application.OpenForms)
                if (f is MainForm a) return a;
            throw new InvalidOperationException("App form not created yet.");
        }
    }
}
