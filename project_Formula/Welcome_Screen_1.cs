using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_Formula
{
    public partial class Welcome_Screen_1 : Form
    {
        public Welcome_Screen_1()
        {
            InitializeComponent();
        }


        private void forward_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var menu_2 = new Menu();
            menu_2.Closed += (s, args) => this.Close();
            menu_2.Show();
        }
    }
}
