using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DesktopVideo
{
    public partial class Form1 : Form
    {
        string FilePath = Application.StartupPath + "\\bg.mp4";
        string INIPath = Application.StartupPath + "\\dpv.ini";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            Icon ico= System.Drawing.Icon.ExtractAssociatedIcon(Application.StartupPath + "\\icon.ico");
            notifyIcon1.Icon = ico;
            this.Icon = ico;

            this.Text = INIC.Ini_Read("set", "title", INIPath);
            label1.Text = INIC.Ini_Read("set", "text", INIPath);
            label2.Text = "作者：" + INIC.Ini_Read("set", "author", INIPath);
            IntPtr HWND = WallpaperUtils.GetWorkerW();
            Player(HWND);

        }
        PlayerForm playerForm = null;
        private void Player(IntPtr parentHwnd)
        {
            if (!File.Exists(FilePath))
            {
                MessageBox.Show("视频文件不存在!");
                return;
            }
            else
            {
                playerForm = new PlayerForm(FilePath);
                IntPtr child = playerForm.Handle;
                if (IntPtr.Zero == Win32.User32.SetParent(child, parentHwnd))
                {
                    MessageBox.Show("error", "error");
                }
            }
            playerForm.Show();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.jayshonyves.net/category/soft/desktopvideo.html");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            playerForm.p.Post(MCIPlayer.Cmd.stop);
            playerForm.Close();
            this.Close();
            System.Environment.Exit(0);
            //Application.Exit();
        }
    }
}
