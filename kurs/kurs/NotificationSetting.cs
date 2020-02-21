using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalendarEvent;
using System.Media;
using System.IO;

namespace kurs
{
    public partial class NotificationSetting : Form
    {
        public NotificationSetting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Configuration.config.NotificationBackgroundColor = colorDialog.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SoundPlayer soun = new SoundPlayer(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Ring01.wav");
                soun.Play();
            Configuration.config.NotificationSoundPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Ring01.wav";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SoundPlayer soun = new SoundPlayer(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Ring02.wav");
                soun.Play();
            Configuration.config.NotificationSoundPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Ring02.wav";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SoundPlayer soun = new SoundPlayer(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Ring03.wav");
            soun.Play();
            Configuration.config.NotificationSoundPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Ring03.wav";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SoundPlayer soun = new SoundPlayer(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Ring04.wav");
            soun.Play();
            Configuration.config.NotificationSoundPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Ring04.wav";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SoundPlayer soun = new SoundPlayer(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Ring05.wav");
            soun.Play();
            Configuration.config.NotificationSoundPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Ring05.wav";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SoundPlayer soun = new SoundPlayer();
            soun.Stop();
            Configuration.config.NotificationSoundPath="";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new Notification(this, new Event(DateTime.Now, DateTime.Now, DateTime.Now, "test", "test", "test")).Show();
        }
    }
}
