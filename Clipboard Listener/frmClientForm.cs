using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.IO;

namespace Clipboard_Listener
{
    public partial class frmClientForm : Form
    {
        public frmClientForm()
        {
            InitializeComponent();
            AddClipboardFormatListener(this.Handle);
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RemoveClipboardFormatListener(IntPtr hwnd);        
        private const int WM_CLIPBOARDUPDATE = 0x031D;

        protected override void WndProc(ref Message m)
        {
            Utility.DestPath= @"D:\Shared Folder";
            base.WndProc(ref m);

            

            if (m.Msg == WM_CLIPBOARDUPDATE)
            {
                string[] filePaths = Directory.GetFiles(Utility.DestPath);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);

                IDataObject iData = Clipboard.GetDataObject();

                try
                {
                    if (iData.GetDataPresent(DataFormats.Text))
                    {
                        string text = (string)iData.GetData(DataFormats.Text);
                        Utility.WriteText("Board.txt", text);
                    }
                    else
                    {
                        StringCollection fileNames = Clipboard.GetFileDropList();
                        Utility.DumbFile(fileNames);                       
                    }
                }
                catch (Exception e) 
                {                    
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}
