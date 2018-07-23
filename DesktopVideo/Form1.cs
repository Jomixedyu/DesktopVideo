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
        string AppPath = Application.StartupPath;
        string FilePath = Application.StartupPath + "\\bg.mp4";
        string INIPath = Application.StartupPath + "\\dpv.ini";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            Icon ico= System.Drawing.Icon.ExtractAssociatedIcon(AppPath + "\\icon.ico");
            notifyIcon1.Icon = ico;
            this.Icon = ico;

            this.Text = INIC.Ini_Read("set", "title", INIPath);
            label1.Text = INIC.Ini_Read("set", "text", INIPath);
            label2.Text = "壁纸作者：" + INIC.Ini_Read("set", "author", INIPath);
            LOADINI();

        }
        string[] filename;
        private void LOADINI()
        {
            int Rint;
            Rint = Convert.ToInt32(INIC.Ini_Read("bg", "count", INIPath));
            filename=new string[Rint];
            for(int i = 0; i < Rint; i++)
            {
                listBox1.Items.Add(INIC.Ini_Read("bg", "bg" + (i + 1) + "_name", INIPath));
                filename[i] = AppPath + "\\bg\\" + INIC.Ini_Read("bg", "bg" + (i + 1) + "_file", INIPath);
            }
            
        }
        PlayerForm playerForm = null;
        private void Player(IntPtr parentHwnd,string fp)
        {
            if (!File.Exists(fp))
            {
                MessageBox.Show("视频文件不存在!");
                return;
            }
            else
            {

                if(playerForm ==null || playerForm.IsDisposed)
                {
                    playerForm = new PlayerForm(fp);
                    if (IntPtr.Zero == Win32.User32.SetParent(playerForm.Handle, parentHwnd))
                        MessageBox.Show("error", "error");
                }else
                {


                    if (!fp.Equals(playerForm.p.FilePath)) {
                        playerForm.p.Post(MCIPlayer.Cmd.close);
                        playerForm.p = new MCIPlayer(fp, "bg", playerForm.Handle, playerForm.rect);
                        playerForm.p.Replace(fp);
                        playerForm.p.Post(MCIPlayer.Cmd.play);
                        playerForm.p.Post(MCIPlayer.Cmd.loops);
                    }
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
            if (playerForm != null)
            {

            
            playerForm.p.Post(MCIPlayer.Cmd.stop);
            playerForm.Close();
            }
            this.Close();
            System.Environment.Exit(0);
            //Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (listBox1.SelectedIndex != -1)
            {
                IntPtr HWND = WallpaperUtils.GetWorkerW();
                Player(HWND,filename[listBox1.SelectedIndex]);
            }
        }
    }
}
