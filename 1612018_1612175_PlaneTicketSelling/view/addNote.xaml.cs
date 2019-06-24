using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using _1612018_1612175_PlaneTicketSelling.controller;

namespace _1612018_1612175_PlaneTicketSelling.view
{
    /// <summary>
    /// Interaction logic for addNote.xaml
    /// </summary>
    public partial class addNote : Window
    {
        int h, m;
        public addNote(string currentNote, int currentStop)
        {
            InitializeComponent();
            if (currentNote != "")
            {
                NoteContent.Text = currentNote;
            }
            txtHrs.Text = (currentStop/60).ToString();
            txtMins.Text = (currentStop % 60).ToString();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            GlobalItem.NoteContent = NoteContent.Text;
            if(Checking() == true)
            {
                GlobalItem.ThoiGianDung = h * 60 + m;
                this.Close();
            }            
        }

        private bool Checking()
        {            
            if(int.TryParse(txtHrs.Text, out h) == false)
            {
                MessageBox.Show("Giờ phải là số");
                return false;
            }
            if (int.TryParse(txtMins.Text, out m) == false)
            {
                MessageBox.Show("Phút phải là số");
                return false;
            }

            if(h*60 + m > GlobalItem.DungToiDa || h*60 + m < GlobalItem.DungToiThieu)
            {
                MessageBox.Show($"Thời gian dừng không phù hợp\nThời dừng tối đa là {GlobalItem.DungToiDa} phút\nThời gian dừng tối thiểu là {GlobalItem.DungToiThieu} phút");
                return false;
            }
            return true;
        }
    }
}
