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

namespace kurs
{
    public partial class RecordEvent : Form
    {
        private int hashCode;
        private static  int offSet=0;
        public RecordEvent()
        {
            InitializeComponent();
        }
        public RecordEvent(Form f,DateTime notifyTime=default(DateTime),int hash=0, DateTime startTime = default(DateTime), DateTime endTime = default(DateTime), string name = null, string place = null, string description = null)
        {
            hashCode = hash;
            InitializeComponent();
            f.BackColor = Color.White;
            if (hash != 0)
            {
                textBox1.Text = name;
                textBox3.Text = place;
                textBox4.Text = description;
                dateTimePicker2.Value = new DateTime(startTime.Year, startTime.Month, startTime.Day);
                dateTimePicker3.Value = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, startTime.Second);
                dateTimePicker1.Value = new DateTime(endTime.Year, endTime.Month, endTime.Day);
                dateTimePicker4.Value = new DateTime(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endTime.Minute, endTime.Second);
            }
            else
            {
                dateTimePicker2.Value = startTime;
                dateTimePicker3.Value = startTime;
                dateTimePicker1.Value = startTime;
                dateTimePicker4.Value = startTime;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var description=textBox4.Text;
            var name = textBox1.Text;
            if (string.IsNullOrWhiteSpace(name))
                name = "Безымянное событие";
            var place = textBox3.Text;
            var start = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day,
             dateTimePicker3.Value.Hour, dateTimePicker3.Value.Minute, dateTimePicker3.Value.Second);
            DateTime no = start;
            if (offSet != 0)
            {
                no = ConvertFromUnixTimestamp(ConvertToUnixTimestamp(start));
            }
            var end = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day,
             dateTimePicker4.Value.Hour, dateTimePicker4.Value.Minute, dateTimePicker4.Value.Second);
            if (hashCode != 0)
                EventManager.ChangeEvent(hashCode, no, start, end, name, place, description);
            else
                EventManager.AddEvent(new Event(no, start, end, name, place, description));
            EventManager.SaveEvents();
            MessageBox.Show("Событие сохраненно");
            textBox1.Text = "Название события:)";
            textBox3.Text = "Место:";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EventManager.RemoveEvent(hashCode);
            EventManager.SaveEvents();
            this.Close();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Название события:)")
            {
                textBox1.ResetText();
            }
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "Место:")
            {
                textBox3.ResetText();
            }
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "Описание события")
            {
                textBox4.ResetText();
            }
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp-offSet).AddHours(3);
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        private void toolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var text = e.ClickedItem.Text;
            if (text == "5 мин")
                offSet = 5 * 60;
            if (text == "15 мин")
                offSet = 15 * 60;
            if (text == "30 мин")
                offSet = 30 * 60;
            if (text == "1 час")
                offSet = 1 * 3600;
            if (text == "3 час")
                offSet = 3 * 3600;
            if (text == "6 часов")
                offSet = 6 * 3600;
            if (text == "12 часов")
                offSet = 12 * 3600;
            if (text == "24 час")
                offSet = 24 * 3600;
            MessageBox.Show("Время установленно");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
