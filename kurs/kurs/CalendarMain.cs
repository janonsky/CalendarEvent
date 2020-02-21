using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalendarEvent;
using System.Media;


namespace kurs
{
    public partial class CalendarMain : Form
    {
        public static int ActiveEvents = 0;
        public CalendarMain()
        {
            InitializeComponent();
            EventManager.LoadEvents(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/data.json");
            Configuration.LoadConfig(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/config.json");
            monthCalendar1.BoldedDates = EventManager.GetEventStarted();
            monthCalendar2.BoldedDates = EventManager.GetEventStarted();
            timer1.Start();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecordEvent newForm = new RecordEvent(this,default(DateTime),0,DateTime.Now);
            newForm.Show();
            this.button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("T");
            var events = EventManager.GetEventByDateTimeNow();
            events.ForEach(x => OnEventStarted(x));
        }
        private void OnEventStarted(Event e)
        {
            Notification form = new Notification(this, e);
            form.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            EventManager.SaveEvents();
            Configuration.SaveConfig();
        }
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e) => OnDateSelected(sender, e);

        private void OnDateSelected(object sender, DateRangeEventArgs e)
        {
            HideListBox();
            if (!EventManager.IsDateContainsEvent(e.Start))
            {
                RecordEvent newForm = new RecordEvent(this, default(DateTime), 0, e.Start);
                newForm.Show();
            }
            else
            {
                listBox1.Visible = true;
                listBox1.Location = new Point(this.PointToClient(Cursor.Position).X, this.PointToClient(Cursor.Position).Y);
                OnListFill(EventManager.GetEventsByDate(e.Start));
            }
        }

        private void HideListBox()
        {
            listBox1.Items.Clear();
            listBox1.Visible = false;
        }
        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e) => OnDateSelected(sender, e);
     
        private void listBox1_Click(object sender, EventArgs e)
        {
          
        }
        private void OnListFill(List<Event>input)
        {
            foreach (var x in input)
            {
                listBox1.Items.Add(x.Name);
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            var x = listBox1.Items[listBox1.SelectedIndex];
            var y = EventManager.GetEvent((string)x);
            RecordEvent newForm = new RecordEvent(this,y.NotifyTime,y.HashCode,y.StartTime,y.EndTime,y.Name,y.Place,y.Description);
            listBox1.Visible = false;
            listBox1.Items.Clear();
            newForm.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!monthCalendar2.Visible)
            {
                this.Size = new Size(849, 639);
                button1.Location=new Point(340,500);
                label1.Location = new Point(10,560);
                HideListBox();
                monthCalendar2.Visible = true;
                monthCalendar1.Visible = false;
                button2.Visible = false;
            }
        }
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventManager.SaveEvents();
            Configuration.SaveConfig();
            this.Close();
        }
        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            if (textBox1.Visible == true)
                button3.Visible = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            if (textBox1.Visible == false)
                button3.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(EventManager.events.Count==0)
                MessageBox.Show("Событий нет");
            else
                new EventList().Show();
        }

        private void настройкаУведомленияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotificationSetting settingForm = new NotificationSetting();
            settingForm.Show();
        }
    }
}
