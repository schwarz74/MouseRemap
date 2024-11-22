


namespace MouseRemap
{
    public partial class Form1 : Form
    {
        bool wasRightClick = false;
        bool wasLeftClick = false;


        System.Media.SoundPlayer playerOff;
        System.Media.SoundPlayer playerOn;

        public Form1()
        {
            playerOff = new System.Media.SoundPlayer(new System.IO.MemoryStream(Resource1.bong_0));
            playerOn = new System.Media.SoundPlayer(new System.IO.MemoryStream(Resource1.bing_0));
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
            if (mousePressToClickLeft.Checked || mousePressToClickRight.Checked)
            {
                // play off sound    
                playerOff.Play();
            }
            else 
            { 
                // play on sound
                playerOn.Play();
            }

            // turn off
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
                // turn on
                if (wasLeftClick)
                {
                    mousePressToClickLeft.Checked = true;
                    mousePressToClickLeft.Enabled = true;
                    mousePressToClickRight.Enabled = true;
                    wasLeftClick = false;
                }
            }
            // turn off
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
                // turn on
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