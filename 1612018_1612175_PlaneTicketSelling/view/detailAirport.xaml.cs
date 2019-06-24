using _1612018_1612175_PlaneTicketSelling.controller;
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

namespace _1612018_1612175_PlaneTicketSelling.view
{
    /// <summary>
    /// Interaction logic for detailAirport.xaml
    /// </summary>
    public partial class detailAirport : UserControl
    {
        private scheduleMng parent;

        public detailAirport()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if(parent.isEditting == true)
            {
                if(txtTenSB.Text == "" || txtDiaDiem.Text == "")
                {
                    MessageBox.Show("Tên sân bay hoặc địa điểm vẫn còn trống");
                }
                else
                {
                    AirportController.updateAirport(GlobalItem.selectedAirport.MaSB, txtTenSB.Text, txtDiaDiem.Text);
                }
            }

            this.Visibility = Visibility.Collapsed;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject ucParent = this.Parent;

            if(!(ucParent is UserControl))
            {
                ucParent = LogicalTreeHelper.GetParent(ucParent);
            }

            parent = (scheduleMng)ucParent;

            if(this.Visibility == Visibility.Visible)
            {
                txtMaSB.Text = GlobalItem.selectedAirport.MaSB;
                txtTenSB.Text = GlobalItem.selectedAirport.TenSB;
                txtDiaDiem.Text = GlobalItem.selectedAirport.DiaDiem;
                lblSBTitle.Content = "Sân bay " + txtTenSB.Text;
                lblTitleOfData.Content = "Danh sách các chuyến bay bắt đầu/kết thúc ở " + txtTenSB.Text;

                dataChuyenBay.ItemsSource = AirportController.searchCBForMASB(txtMaSB.Text);

                if(parent.isEditting)
                {
                    manChe.Visibility = Visibility.Hidden;
                }
                else
                {
                    manChe.Visibility = Visibility.Visible;
                }
            }
            else
            {
                parent.isEditting = false;
                GlobalItem.selectedAirport = null;
                parent.loadAirportData();
            }
        }
    }
}
