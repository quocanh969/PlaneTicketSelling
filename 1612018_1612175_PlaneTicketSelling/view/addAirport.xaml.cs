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
using System.Windows.Navigation;
using System.Windows.Shapes;
using _1612018_1612175_PlaneTicketSelling.controller;

namespace _1612018_1612175_PlaneTicketSelling.view
{
    /// <summary>
    /// Interaction logic for addAirport.xaml
    /// </summary>
    public partial class addAirport : UserControl
    {
        private StringBuilder error = new StringBuilder();
                

        public addAirport()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.Visibility == Visibility.Collapsed)
            {
                DependencyObject ucParent = this.Parent;

                if(!(ucParent is UserControl))
                {
                    ucParent = LogicalTreeHelper.GetParent(ucParent);
                }

                scheduleMng parent = (scheduleMng)ucParent;
                parent.loadAirportData();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtMaSB.Text = "";
            txtTenSB.Text = "";
            txtDiaDiem.Text = "";
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (Checking())
            {
                StringBuilder sb = new StringBuilder(txtMaSB.Text);
                int l = sb.Length;
                while (l < 4)
                {
                    l++;
                    sb.Append(" ");
                }

                if (AirportController.validateAirport(sb.ToString()))
                {
                    AirportController.addNewAirport(sb.ToString(), txtTenSB.Text, txtDiaDiem.Text);
                    this.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Đã tồn tại mã sân bay này rồi");
                }
            }
            else
            {
                MessageBox.Show(error.ToString());
                error.Clear();
            }
        }

        private bool Checking()
        {
            bool isError = false;
            // Kiếm tra ô trống
            if(txtMaSB.Text == "" || txtTenSB.Text == "" || txtDiaDiem.Text == "")
            {
                error.Append("Thông tin các ô không được trống\n");
                isError = true;
            }

            // Kiểm tra mã sân bay
            if(txtMaSB.Text.Length > 5)
            {
                error.Append("Mã sân bay giới hạn trong 5 ký tự\n");
                isError = true;
            }

            return !isError;
        }
    }
}
