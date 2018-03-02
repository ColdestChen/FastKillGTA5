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

        bool isOpen = true;

        public myForm()
        {
            InitializeComponent();
            // Ctrl + F8
            RegisterHotKey(this.Handle, 0, (int)KeyModifier.Control, Keys.F8.GetHashCode());            
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x0312 && isOpen == true)
            {
                switch (m.WParam.ToInt32())
                {
                    case 0:
                        killGTA5();
                        break;
                    default:
                        break;
                }                
            }
        }

        private void killGTA5()
        {
            Process[] ps = Process.GetProcessesByName("GTA5");
            foreach (Process p in ps)
            {                    
                p.Kill();                         
                p.WaitForExit();
                // https://msdn.microsoft.com/en-us/library/windows/desktop/system.diagnostics.process.waitforexit(v=vs.100)
            }
        }

        private void myForm_FormClosing(object sender, FormClosingEventArgs e)
        {            
            UnregisterHotKey(this.Handle, 0);
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if(isOpen == true)
            {
                btnSwitch.Text = "Start";
                btnSwitch.ForeColor = Color.Blue;
            }
            else
            {
                btnSwitch.Text = "Stop";
                btnSwitch.ForeColor = Color.Red;
            }
            
            // ^ --> XOR
            isOpen ^= true;
        }
    }

    /*
     * Source 
     * http://www.fluxbytes.com/csharp/how-to-register-a-global-hotkey-for-your-application-in-c/    
     * https://dotblogs.com.tw/yc421206/2013/01/02/86668 
     */
}
