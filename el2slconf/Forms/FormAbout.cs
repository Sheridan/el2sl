using System.Windows.Forms;

namespace Bng.EL2SL.Configurator
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "mailto:sheridan@stavcom.ru");
        }
    }
}