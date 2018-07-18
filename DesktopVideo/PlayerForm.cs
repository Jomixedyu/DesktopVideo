using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DesktopVideo
{
    public partial class PlayerForm : Form
    {
        public MCIPlayer p { get; set; }
        Rectangle rect;
        private string FileName;
        public PlayerForm(string fileName)
        {
            InitializeComponent();
            FileName = fileName;
        }

        private void PlayerForm_Load(object sender, EventArgs e)
        {

        }

        private void PlayerForm_Shown(object sender, EventArgs e)
        {

            this.Size = Screen.PrimaryScreen.Bounds.Size;
            this.Location = new Point(0, 0);
            this.BackColor = Color.White;

            if (p != null)
            {
                p.Post(MCIPlayer.Cmd.close);
                p = null;
            }

            rect = new Rectangle(new Point(0, 0), Screen.PrimaryScreen.Bounds.Size);
            p = new MCIPlayer(FileName, "bg", this.Handle, rect);

            p.Post(MCIPlayer.Cmd.play);
            p.Post(MCIPlayer.Cmd.loops);



        }
    }
}
