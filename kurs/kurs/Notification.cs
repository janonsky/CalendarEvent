using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalendarEvent;

namespace kurs
{ 
    public partial class Notification : Form
    {
        private Event eve;
        public Notification(Form f,Event e)
        {
            InitializeComponent();
            this.BackColor = Configuration.config.NotificationBackgroundColor;
            if (Configuration.config.NotificationSoundPath != "")
            {
                SoundPlayer sound = new SoundPlayer(Configuration.config.NotificationSoundPath);
                sound.Play();
            }
            this.eve = e;
            this.label1.Text = eve.Description;
            CalendarMain.ActiveEvents++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Uvedomlenie_Click(object sender, EventArgs e)
        {
            RecordEvent form = new RecordEvent(this, eve.NotifyTime, eve.HashCode, eve.StartTime, eve.EndTime, eve.Name, eve.Place, eve.Description);

            this.Close();
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            var offset = CalendarMain.ActiveEvents == 1 ? -150 :-(CalendarMain.ActiveEvents * 90)-60;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 320, Screen.PrimaryScreen.Bounds.Height + offset);
        }

        private void Notification_FormClosed(object sender, FormClosedEventArgs e)
        {
            CalendarMain.ActiveEvents--;
        }
    }
}
