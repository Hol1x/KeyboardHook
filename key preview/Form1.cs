using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Utilities;
using WindowsInput;
using System.Diagnostics;


namespace key_preview {
	public partial class Form1 : Form {
		globalKeyboardHook gkh = new globalKeyboardHook();

        public Form1()
        {
            InitializeComponent();
            Process[] MyProcess = Process.GetProcesses();
            for (int i = 0; i < MyProcess.Length; i++)
                if (!String.IsNullOrEmpty(MyProcess[i].MainWindowTitle))
                {
                    comboBox1.Items.Add(MyProcess[i].MainWindowTitle);
                }
        }

		private void Form1_Load(object sender, EventArgs e) {
			//gkh.HookedKeys.Add(Keys.A);
			//gkh.HookedKeys.Add(Keys.B);
            //gkh.HookedKeys.Add(Keys.C);
			gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
			gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
            
            
		}

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void bringToFront(string title)
        {
            // Get a handle to the Calculator application.
            IntPtr handle = FindWindow(null, title);

            // Verify that Calculator is a running process.
            if (handle == IntPtr.Zero)
            {
                return;
            }

            // Make Calculator the foreground application
            SetForegroundWindow(handle);
        }

        void gkh_KeyUp(object sender, KeyEventArgs e) {
			lstLog.Items.Add("Up\t" + e.KeyCode.ToString());
			e.Handled = true;
		}

		void gkh_KeyDown(object sender, KeyEventArgs e) {
			lstLog.Items.Add("Down\t" + e.KeyCode.ToString());
			e.Handled = true;
		}

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            Process[] MyProcess = Process.GetProcesses();
            for (int i = 0; i < MyProcess.Length; i++)
                if (!String.IsNullOrEmpty(MyProcess[i].MainWindowTitle))
                {
                    comboBox1.Items.Add(MyProcess[i].MainWindowTitle);
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bringToFront(this.comboBox1.GetItemText(this.comboBox1.SelectedItem));
            InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.VK_A);
            InputSimulator.SimulateKeyDown(VirtualKeyCode.VK_0);
            //InputSimulator.SimulateKeyPress(VirtualKeyCode.SPACE);

        }
    }
}