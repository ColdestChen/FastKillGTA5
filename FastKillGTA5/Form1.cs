using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FastKillGTA5
{
    public partial class myForm : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        public myForm()
        {
            InitializeComponent();
            int id = 0;
            
            RegisterHotKey(this.Handle, id, (int)KeyModifier.Control, Keys.F8.GetHashCode());            
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {                
                //Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                //KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.
                if (id == 0)
                {                    
                    Process[] gta5 = Process.GetProcessesByName("GTA5");
                    foreach (Process p in gta5)
                    { 
                        p.Kill();
                    }
                } 
            }
        }

        private void myForm_FormClosing(object sender, FormClosingEventArgs e)
        {            
            UnregisterHotKey(this.Handle, 0);
        }
    }

    /*
     * Source 
     * http://www.fluxbytes.com/csharp/how-to-register-a-global-hotkey-for-your-application-in-c/    
     * https://dotblogs.com.tw/yc421206/2013/01/02/86668 
     */
}
