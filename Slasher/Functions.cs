using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slasher
{
    class Functions
    {
        public static OpenFileDialog openfiledialog = new OpenFileDialog
        {
            Filter = "Script File|*.txt;*.lua|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true,
            Title = "Abrir archivo"
        };
    }
}
