using _1612018_1612175_PlaneTicketSelling.controller;
using _1612018_1612175_PlaneTicketSelling.model;
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
    /// Interaction logic for addFlight.xaml
    /// </summary>
    public partial class addFlight : UserControl
    {
        private flightMng parentWind;
        private int h, m, VIP, Normal;
        private SANBAY sbdi, sbden;
        private List<SANBAY> lstSB;

        private StringBuilder error = new StringBuilder();

        public addFlight()
        {
            InitializeComponent();
                       
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {            
            this.Visibility = Visibility.Collapsed;
        }

        // Add Combo ---------------------------------------------------------

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(Checking() == false)
            {
                MessageBox.Show(error.ToString());
                error.Clear();
            }
            else
            {
                try
                {
                    CHUYENBAY newCB = new CHUYENBAY()
                    {
                        ThoiGianBay = new TimeSpan(h, m, 0),
                        GheThuong = Normal,
                        GheVip = VIP,
                        SBDi = sbdi.MaSB,
                        SBDen = sbden.MaSB,
                        IsAvailable = true,
                    };

                    FlightMngController.addNewFlight(newCB);
                    this.Visibility = Visibility.Collapsed;
                }
                catch
                {
                    MessageBox.Show("Đã tồn tại chuyến bay này rồi");
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            cmbSBDi.SelectedIndex = 0;
            cmbSBDen.SelectedIndex = 0;
            txtHrs.Text = "";
            txtMins.Text = "";
            txtNormalSeats.Text = "";
            txtVIPSeats.Text = "";
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.Visibility == Visibility.Visible)
            {
                lstSB = FlightMngController.LoadAirportData();
                DependencyObject ucParent = this.Parent;
                
                while (!(ucParent is UserControl))
                {
                    ucParent = LogicalTreeHelper.GetParent(ucParent);
                }

                parentWind = (flightMng)ucParent;
                                
                // Cập nhật dữ liệu                
                cmbSBDi.ItemsSource = lstSB;
                cmbSBDen.ItemsSource = lstSB;

                if(parentWind.IsFlightEdit == true)
                {// Xem / sữa
                    lblCBTitle.Content = "CHUYẾN BAY SỐ" + GlobalItem.selectedFlight.MaCB;

                    txtFlightID.Text = GlobalItem.selectedFlight.MaCB.ToString();
                    txtFlightID.IsEnabled = false;
                    txtMins.Text = GlobalItem.selectedFlight.ThoiGianBay.Value.Minutes.ToString();
                    txtHrs.Text = GlobalItem.selectedFlight.ThoiGianBay.Value.Hours.ToString();
                    txtNormalSeats.Text = GlobalItem.selectedFlight.GheThuong.ToString();
                    txtVIPSeats.Text = GlobalItem.selectedFlight.GheVip.ToString();
                    cmbSBDi.SelectedItem = lstSB.Where(x => x.MaSB == GlobalItem.selectedFlight.SBDi).SingleOrDefault();
                    cmbSBDen.SelectedItem = lstSB.Where(x => x.MaSB == GlobalItem.selectedFlight.SBDen).SingleOrDefault();

                    AddCombo.Visibility = Visibility.Hidden;
                    EditCombo.Visibility = Visibility.Visible;
                }
                else
                {// Thêm mới
                    lblCBTitle.Content = "THÊM CHUYẾN BAY MỚI";

                    txtFlightID.Text = (parentWind.soChuyenBay + 1).ToString();
                    if (parentWind.IsFlightEdit == true)
                    {
                        GlobalItem.selectedFlight = null;
                    }
                    AddCombo.Visibility = Visibility.Visible;
                    EditCombo.Visibility = Visibility.Hidden;
                }                
            }
            else
            {
                DependencyObject ucParent = this.Parent;

                while (!(ucParent is UserControl))
                {
                    ucParent = LogicalTreeHelper.GetParent(ucParent);
                }

                flightMng parentWind = (flightMng)ucParent;

                txtFlightID.IsEnabled = true;
                parentWind.loadChuyenBayData();
            }
        }
        
        private bool Checking()
        {
            bool isError = false;

            sbdi = (SANBAY)cmbSBDi.SelectedItem;
            sbden = (SANBAY)cmbSBDen.SelectedItem;

            // Kiểm tra giờ bay
            if (!int.TryParse(txtHrs.Text, out h))
            {
                txtHrs.BorderBrush = Brushes.Red;
                txtHrs.Foreground = Brushes.Red;
                error.Append("Giờ bay phải là số\n");
                isError = true;
            }
            else if (!int.TryParse(txtMins.Text, out m))
            { // Kiểm tra phút bay
                txtMins.BorderBrush = Brushes.Red;
                txtMins.Foreground = Brushes.Red;
                error.Append("Phút bay phải là số\n");
                isError = true;
            }
            else if(h*60 + m < GlobalItem.BayToiThieu)
            {
                error.Append($"Thời gian bay tối thiểu là {GlobalItem.BayToiThieu} phút\n");
                isError = true;
            }



            // Kiểm tra số ghế thường
            if (!int.TryParse(txtVIPSeats.Text, out VIP))
            {
                error.Append("Số ghế thường phải là số\n");
                isError = true;
            }

            // Kiểm tra số ghế VIP
            if (!int.TryParse(txtNormalSeats.Text, out Normal))
            {
                error.Append("Số ghế VIP phải là số\n");
                isError = true;
            }

            // Kiểm tra sân bay đến và sân bay đi
            if (sbdi == null)
            {
                cmbSBDi.BorderBrush = Brushes.Red;
                error.Append("Sân bay đi không được rỗng\n");
                isError = true;
            }

            // Kiểm tra sân bay đi
            if (sbden == null)
            {
                cmbSBDen.BorderBrush = Brushes.Red;
                error.Append("Sân bay đến không được rỗng\n");
                isError = true;
            }

            // Kiểm tra sân đến và đi có trùng nhau
            if(sbdi == sbden)
            {
                cmbSBDi.BorderBrush = Brushes.Red;
                cmbSBDen.BorderBrush = Brushes.Red;
                error.Append("Sân bay đi và sân bay đến không được trùng nhau\n");
                isError = true;
            }
            

            if (isError) return false;
            else return true;
            
        }

        // Edit Combo --------------------------------------------------------
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (Checking() == false)
            {
                MessageBox.Show(error.ToString());
                error.Clear();
            }
            else
            {
                if(IsEditing())
                {
                    FlightMngController.updateFlight(GlobalItem.selectedFlight.MaCB, new TimeSpan(h, m, 0), sbdi.MaSB, sbden.MaSB, Normal, VIP);
                }
                
                GlobalItem.selectedFlight = null;
                this.Visibility = Visibility.Collapsed;
            }
        }

        private bool IsEditing()
        {
            if(h != GlobalItem.selectedFlight.ThoiGianBay.Value.Hours ||
                m != GlobalItem.selectedFlight.ThoiGianBay.Value.Minutes ||
                Normal != GlobalItem.selectedFlight.GheThuong ||
                VIP != GlobalItem.selectedFlight.GheVip ||
                sbdi.MaSB != GlobalItem.selectedFlight.SBDi || sbden.MaSB != GlobalItem.selectedFlight.SBDen)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
