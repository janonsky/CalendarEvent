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
    public partial class EventList : Form
    {
        private int startIndex = 0;
        private  List<Event> events = new List<Event>();
        private List<Event> cachedEvents = new List<Event>();
        public EventList()
        {
            InitializeComponent();
            events = EventManager.events;
            FillButtons();
        }
        public void FillButtons()
        {
            int z = 0;
            int count = events.Count;
            if (startIndex + 10 < count)
                button11.Visible = true;
            else
                button11.Visible = false;
            if (startIndex != 0)
                button12.Visible = true;
            else
                button12.Visible = false;
           
            if (count == 0)
                return;
            if (count - startIndex < 10)
                count = count - startIndex;
            else
                count = 10;
            for (int i = startIndex; i < count+startIndex; i++)
            {
                Console.WriteLine(i);
                var e = events[i];
                cachedEvents.Add(e);
            }
            for (int i = this.Controls.Count - 1; i >= 0; i--) 
            {
                var button = this.Controls[i] as Button;
                if (button != null)
                {
                    if (button.Text == "Следующая страница" || button.Text == "Предыдущие страница")
                        continue;
                    if (z == count)
                        break;
                    button.Visible = true;
                    button.Text = $"{cachedEvents[z].Name} {cachedEvents[z].StartTime}";
                    z++;
                }
            }
        }
        public void OnButtonClick(int i)
        {
            var e = cachedEvents[i];
            var Form2 = new RecordEvent(this, e.NotifyTime, e.HashCode, e.StartTime, e.EndTime, e.Name, e.Place, e.Description);
            Form2.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnButtonClick(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnButtonClick(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OnButtonClick(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OnButtonClick(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OnButtonClick(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OnButtonClick(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OnButtonClick(6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OnButtonClick(7);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OnButtonClick(8);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OnButtonClick(9);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            cachedEvents.Clear();
            startIndex += 10;
            foreach (var x in this.Controls)
            {
                var button = x as Button;

                if (button != null)
                {
                    if (button.Text == "Следующая страница" || button.Text == "Предыдущие страница")
                        continue;
                        button.Visible = false;
                }
            }
            FillButtons();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            cachedEvents.Clear();
            startIndex -= 10;
            foreach (var x in this.Controls)
            {
                var button = x as Button;

                if (button != null)
                {
                    if (button.Text == "Следующая страница" || button.Text == "Предыдущая страница")
                        continue;
                        button.Visible = false;
                }
            }
            FillButtons();
        }
    }
}
