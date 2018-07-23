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
        public Rectangle rect;
        public string FileName;

        public PlayerForm(string fileName)
        {
            InitializeComponent();
            FileName = fileName;
        }

        private void PlayerForm_Load(object sender, EventArgs e)
        {

            rect = new Rectangle(new Point(0, 0), Screen.PrimaryScreen.Bounds.Size);
            p = new MCIPlayer(FileName, "bg", Handle, rect);
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            this.Location = new Point(0, 0);
            this.BackColor = Color.White;

            p.Post(MCIPlayer.Cmd.play);
            p.Post(MCIPlayer.Cmd.loops);
        }

        private void PlayerForm_Shown(object sender, EventArgs e)
        {


        }
    }
}
