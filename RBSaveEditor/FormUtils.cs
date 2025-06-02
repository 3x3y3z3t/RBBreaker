using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSaveEditor
{
    public static  class FormUtils
    {
        public static void InvokeIfRequired(this Control _control, Action _action)
        {
            if (_control.InvokeRequired)
            {
                _control.Invoke(_action);
            }
            else
            {
                _action();
            }
        }


    }

}
