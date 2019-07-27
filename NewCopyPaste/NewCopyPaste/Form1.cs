using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace NewCopyPaste
{
    public partial class CopyPaster : Form
    {
        [System.ComponentModel.Browsable(false)]
        public event System.Windows.Forms.MouseEventHandler MouseWheel;
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        public const int C = 0x43; //C key code
        public const int V = 0x56; //V key code

        static uint KEYEVENTF_KEYUP = 2;
        static byte VK_CONTROL = 0x11;
        static byte VK_SHIFT = 0x10;

        HotKey hk1 = new HotKey();
        HotKey hk2 = new HotKey();
        HotKey hk3 = new HotKey();
        HotKey hk4 = new HotKey();
        HotKey hk5 = new HotKey();
        HotKey hk6 = new HotKey();
        HotKey hk7 = new HotKey();
        HotKey hk8 = new HotKey();
        HotKey hk9 = new HotKey();
        HotKey hkA = new HotKey();
        HotKey hkB = new HotKey();
        HotKey hkC = new HotKey();

        List<string> Copied = new List<string>();
        int mainCounter = 0;
        bool entr = false;
        bool sepr = false;
        char[] separator = new char[] { '\r', '\n' };
        bool end = true;

        //initializing hotkeys
        public CopyPaster()
        {
            InitializeComponent();
            hk1.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hk1.Key = Keys.V;// назначаем любую кнопку
            hk1.HotKeyPressed += new KeyEventHandler(hk1_HotKeyPressed);// подписываемся на событие
            hk2.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hk2.Key = Keys.E;// назначаем любую кнопку
            hk2.HotKeyPressed += new KeyEventHandler(hk2_HotKeyPressed);// подписываемся на событие
            hk3.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hk3.Key = Keys.C;// назначаем любую кнопку
            hk3.HotKeyPressed += new KeyEventHandler(hk3_HotKeyPressed);// подписываемся на событие
            hk4.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hk4.Key = Keys.Enter;// назначаем любую кнопку
            hk4.HotKeyPressed += new KeyEventHandler(hk4_HotKeyPressed);// подписываемся на событие
            hk5.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hk5.Key = Keys.A;// назначаем любую кнопку
            hk5.HotKeyPressed += new KeyEventHandler(hk5_HotKeyPressed);// подписываемся на событие
            hk6.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hk6.Key = Keys.I;// назначаем любую кнопку
            hk6.HotKeyPressed += new KeyEventHandler(hk6_HotKeyPressed);// подписываемся на событие
            hk7.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hk7.Key = Keys.S;// назначаем любую кнопку
            hk7.HotKeyPressed += new KeyEventHandler(hk7_HotKeyPressed);// подписываемся на событие
            hk8.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hk8.Key = Keys.Up;// назначаем любую кнопку
            hk8.HotKeyPressed += new KeyEventHandler(hk8_HotKeyPressed);// подписываемся на событие
            hk9.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hk9.Key = Keys.Down;// назначаем любую кнопку
            hk9.HotKeyPressed += new KeyEventHandler(hk9_HotKeyPressed);// подписываемся на событие
            hkA.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hkA.Key = Keys.F;// назначаем любую кнопку
            hkA.HotKeyPressed += new KeyEventHandler(hkA_HotKeyPressed);// подписываемся на событие
            hkB.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hkB.Key = Keys.P;// назначаем любую кнопку
            hkB.HotKeyPressed += new KeyEventHandler(hkB_HotKeyPressed);// подписываемся на событие
            hkC.KeyModifier = HotKey.KeyModifiers.ShiftControl;// назначаем любой KeyModifiers;
            hkC.Key = Keys.Z;// назначаем любую кнопку
            hkC.HotKeyPressed += new KeyEventHandler(hkC_HotKeyPressed);// подписываемся на событие

            this.label1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.CopyPaster_MouseWheel);
        }

        //pasting
        private void hk1_HotKeyPressed(object sender, KeyEventArgs e)
        {
            if (Copied.Count > 0)
            {
                if (entr)
                    Clipboard.SetText(Copied[mainCounter] + "\r\n");
                else
                    Clipboard.SetText(Copied[mainCounter]);

                PressKeys(V);

                mainCounter++;
                if (mainCounter >= Copied.Count)
                {
                    mainCounter = 0;
                }
                label4.Text = Copied[(mainCounter + Copied.Count - 2) % Copied.Count];
                label3.Text = Copied[(mainCounter + Copied.Count - 1) % Copied.Count];
                label1.Text = Copied[mainCounter];
                label5.Text = Copied[(mainCounter + Copied.Count + 1) % Copied.Count];
                label6.Text = Copied[(mainCounter + Copied.Count + 2) % Copied.Count];
            }
        }
        //exit
        private void hk2_HotKeyPressed(object sender, KeyEventArgs e)
        {
            MessageBox.Show(
                 "Appication closed",
                 "Сообщение",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.DefaultDesktopOnly);
            Application.Exit();
        }
        //copying
        private void hk3_HotKeyPressed(object sender, EventArgs e)
        {
            hkcPress();
        }
        //entr option
        private void hk4_HotKeyPressed(object sender, KeyEventArgs e)
        {
            entr = !entr;
            MessageBox.Show(
                 "Auto-enter now is " + entr,
                 "Сообщение",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.DefaultDesktopOnly);
        }
        //adding
        private void hk5_HotKeyPressed(object sender, KeyEventArgs e)
        {
            PressKeys(C);
            if (Clipboard.ContainsText())
            {

                string text = Clipboard.GetText(TextDataFormat.UnicodeText);

                if (sepr)
                    if (end)
                        Copied.AddRange(text.Split(separator, StringSplitOptions.RemoveEmptyEntries));
                    else
                    {
                        string[] adding = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string add in adding)
                            Copied.Insert(0, add);
                    }
                else
                    if (end)
                    Copied.Add(text);
                else
                    Copied.Insert(0, text);

                mainCounter = 0;
                label4.Text = Copied[(mainCounter + Copied.Count - 2) % Copied.Count];
                label3.Text = Copied[(mainCounter + Copied.Count - 1) % Copied.Count];
                label1.Text = Copied[mainCounter];
                label5.Text = Copied[(mainCounter + Copied.Count + 1) % Copied.Count];
                label6.Text = Copied[(mainCounter + Copied.Count + 2) % Copied.Count];
            }
            else
            {
                MessageBox.Show(
                 "No Text In Clipboard",
                 "Сообщение",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        //info
        private void hk6_HotKeyPressed(object sender, KeyEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }
        //sepr option
        private void hk7_HotKeyPressed(object sender, KeyEventArgs e)
        {
            sepr = !sepr;
            MessageBox.Show(
                 "Separator now is " + sepr,
                 "Сообщение",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.DefaultDesktopOnly);
        }
        //previous entry
        private void hk8_HotKeyPressed(object sender, KeyEventArgs e)
        {
            previousEntry();
        }
        //next entry
        private void hk9_HotKeyPressed(object sender, KeyEventArgs e)
        {
            nextEntry();
        }
        //end option
        private void hkA_HotKeyPressed(object sender, KeyEventArgs e)
        {
            end = !end;
            string x = "";
            if (end)
                x = "end";
            else
                x = "start";

            MessageBox.Show(
                 "Inserting new entries at " + x,
                 "Сообщение",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.DefaultDesktopOnly);
        }
        //paste all entries
        private void hkB_HotKeyPressed(object sender, KeyEventArgs e)
        {
            string output = "";
            for(int i =0;i<Copied.Count;i++)
            {
                output += Copied[i];
                if (entr)
                    output += "\r\n";
            }
            Clipboard.SetText(output);
            PressKeys(V);
        }
        //delete last copied entry
        private void hkC_HotKeyPressed(object sender, KeyEventArgs e)
        {
            if (Copied.Count > 0)
            {
                if (end)
                    Copied.RemoveAt(Copied.Count - 1);
                else
                    Copied.RemoveAt(0);
            }
            if (Copied.Count == 0)
            {
                label4.Text = "no text";
                label3.Text = "no text";
                label1.Text = "no text";
                label5.Text = "no text";
                label6.Text = "no text";
            }
            else
            {
                label4.Text = Copied[(mainCounter + Copied.Count - 2) % Copied.Count];
                label3.Text = Copied[(mainCounter + Copied.Count - 1) % Copied.Count];
                label1.Text = Copied[mainCounter];
                label5.Text = Copied[(mainCounter + Copied.Count + 1) % Copied.Count];
                label6.Text = Copied[(mainCounter + Copied.Count + 2) % Copied.Count];
            }
        }
        //hide the form after the hide button was clicked
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }
        //simulate ctrl+key
        public static void PressKeys(byte key)
        {
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_CONTROL, 0, 0, 0);
            keybd_event(key, 0, 0, 0);
            Thread.Sleep(25);
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_SHIFT, 0, 0, 0);
            keybd_event(VK_CONTROL, 0, 0, 0);
            
        }
        //next entry
        private void button2_Click(object sender, EventArgs e)
        {
            nextEntry();
        }
        //previous entry
        private void button1_Click(object sender, EventArgs e)
        {
            previousEntry();
        }

        //help method for copying
        private void hkcPress()
        {
            bool success = true;
            PressKeys(C);
            do
            {
                try
                {
                    success = true;
                    if (Clipboard.ContainsText())
                    {
                        string text = Clipboard.GetText(TextDataFormat.UnicodeText);
                        Copied.Clear();
                        if (sepr)
                            if (end)
                                Copied.AddRange(text.Split(separator, StringSplitOptions.RemoveEmptyEntries));
                            else
                            {
                                string[] adding = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string add in adding)
                                    Copied.Insert(0, add);
                            }
                        else
                            if (end)
                            Copied.Add(text);
                        else
                            Copied.Insert(0, text);

                        mainCounter = 0;
                        label4.Text = Copied[(mainCounter + Copied.Count - 2) % Copied.Count];
                        label3.Text = Copied[(mainCounter + Copied.Count - 1) % Copied.Count];
                        label1.Text = Copied[mainCounter];
                        label5.Text = Copied[(mainCounter + Copied.Count + 1) % Copied.Count];
                        label6.Text = Copied[(mainCounter + Copied.Count + 2) % Copied.Count];

                    }
                    else
                    {
                        MessageBox.Show(
                         "No Text In Clipboard",
                         "Сообщение",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information,
                         MessageBoxDefaultButton.Button1,
                         MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
                catch (Exception)
                {
                    success = false;
                }
            } while (success == false);
        }

        //changing separator stuff
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                separator = new char[] { '\r', '\n' };
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                separator = new char[] { ' ', '\t' };
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                separator = new char[] { '.' };
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                separator = textBox1.Text.ToCharArray();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                separator = textBox1.Text.ToCharArray();
            }
        }

        private void CopyPaster_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(e.Delta>0)
            {
                previousEntry();
            }
            if(e.Delta<0)
            {
                nextEntry();
            }
        }



        //next entry method
        private void nextEntry()
        {
            mainCounter++;
            if (mainCounter >= Copied.Count)
            {
                mainCounter = 0;
            }
            if (Copied.Count > 0)
            {
                label4.Text = Copied[(mainCounter + Copied.Count - 2) % Copied.Count];
                label3.Text = Copied[(mainCounter + Copied.Count - 1) % Copied.Count];
                label1.Text = Copied[mainCounter];
                label5.Text = Copied[(mainCounter + Copied.Count + 1) % Copied.Count];
                label6.Text = Copied[(mainCounter + Copied.Count + 2) % Copied.Count];
            }
        }
        //previous entry method
        private void previousEntry()
        {
            mainCounter--;
            if (mainCounter < 0)
            {
                mainCounter = Copied.Count - 1;
            }
            if (Copied.Count > 0)
            {
                label4.Text = Copied[(mainCounter + Copied.Count - 2) % Copied.Count];
                label3.Text = Copied[(mainCounter + Copied.Count - 1) % Copied.Count];
                label1.Text = Copied[mainCounter];
                label5.Text = Copied[(mainCounter + Copied.Count + 1) % Copied.Count];
                label6.Text = Copied[(mainCounter + Copied.Count + 2) % Copied.Count];
            }
        }
    }
}
