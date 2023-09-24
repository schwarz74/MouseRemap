using System.Runtime.InteropServices;

namespace MouseRemap
{
    public partial class Form1 : Form
    {
        bool wasRightClick = false;
        bool wasLeftClick = false;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void mousePressToClickLeft_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mousePressToClickRight_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void onClick(object sender, EventArgs e)
        {
            if (mousePressToClickLeft.Checked)
            {
                //save prev state
                wasLeftClick = true;
                mousePressToClickLeft.Checked = false;
                mousePressToClickLeft.Enabled = false;
                mousePressToClickRight.Enabled = false;
            }
            else 
            {
                if (wasLeftClick)
                {
                    mousePressToClickLeft.Checked = true;
                    mousePressToClickLeft.Enabled = true;
                    mousePressToClickRight.Enabled = true;
                    wasLeftClick = false;
                }
            }
            if (mousePressToClickRight.Checked)
            {
                //save prev state
                wasRightClick = true;
                mousePressToClickRight.Checked = false;
                mousePressToClickRight.Enabled = false;
                mousePressToClickLeft.Enabled = false;
            }
            else
            {
                if (wasRightClick)
                {
                    mousePressToClickRight.Checked = true;
                    mousePressToClickRight.Enabled = true;
                    mousePressToClickLeft.Enabled = true;
                    wasRightClick = false;
                }
            }

        }
    }
}