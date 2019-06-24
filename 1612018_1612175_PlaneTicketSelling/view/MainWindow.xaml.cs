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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtBox_user.Text = GlobalItem.currenUser.HoTen;
            //Cập nhật phân quyền
            if (GlobalItem.currenUser.IsAdmin != true)
            {
                roleIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.User;
                btnQLNhanSu.Visibility = Visibility.Collapsed;
                btnThongKe.Visibility = Visibility.Collapsed;
            }
            else
            {
                roleIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Lock;
            }

            // Kiểm tra và cập nhật thông tin lịch bay, hóa đơn, phiếu đặt
            reservationController.removeReservation(); // Xóa phiếu đặt hết hạn
            reservationController.kiemTraHoaDon();// Kiếm tra các hóa đơn đã qua ngày bay
            reservationController.updateFlight(); // Cập nhật các chuyến bay đã bay rồi

        }

        public void makeInactive()
        {
            byte r = 197; byte g = 153; byte b = 255;
            btnQLChuyenBay.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
            btnLapPhieu.Background = new SolidColorBrush(Color.FromRgb(r, g, b)); 
            btnQLDatVe.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
            btnQLHoaDon.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
            btnQLNhanSu.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
            btnQuyDinh.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
            btnThongKe.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        public void collapseAll()
        {
            flightMngUC.Visibility = Visibility.Collapsed;
            receiptMngUC.Visibility = Visibility.Collapsed;
            ruleMngUC.Visibility = Visibility.Collapsed;
            userMngUC.Visibility = Visibility.Collapsed;
            reservationMngUC.Visibility = Visibility.Collapsed;
            scheduleAndAirportMngUC.Visibility = Visibility.Collapsed;
            statisticsUC.Visibility = Visibility.Collapsed;
        }

        public void displayStatUC()
        {
            collapseAll();
            statisticsUC.Visibility = Visibility.Visible;
        }

        public void displayFlightMngUC()
        {
            collapseAll();
            flightMngUC.Visibility = Visibility.Visible;
        }

        public void displayReceiptMngUC()
        {
            collapseAll();
            receiptMngUC.Visibility = Visibility.Visible;
        }

        public void displayRuleMngUC()
        {
            collapseAll();
            ruleMngUC.Visibility = Visibility.Visible;
        }

        public void displayUserMngUC()
        {
            collapseAll();
            userMngUC.Visibility = Visibility.Visible;
        }

        public void displayScheduleMngUC()
        {
            collapseAll();
            scheduleAndAirportMngUC.Visibility = Visibility.Visible;
        }

        public void displayReservationMngUC()
        {
            collapseAll();
            reservationMngUC.Visibility = Visibility.Visible;
        }

        private void BtnQLChuyenBay_Click(object sender, RoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            makeInactive();
            btnQLChuyenBay.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            displayFlightMngUC();
        }

        private void BtnLapPhieu_Click(object sender, RoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            makeInactive();
            btnLapPhieu.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            displayScheduleMngUC();
        }

        private void BtnQLHoaDon_Click(object sender, RoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            makeInactive();
            btnQLHoaDon.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            displayReceiptMngUC();
        }

        private void BtnQLDatVe_Click(object sender, RoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            makeInactive();
            btnQLDatVe.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            displayReservationMngUC();
        }

        private void BtnQuyDinh_Click(object sender, RoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            makeInactive();
            btnQuyDinh.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            displayRuleMngUC();
        }

        private void BtnQLNhanSu_Click(object sender, RoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            makeInactive();
            btnQLNhanSu.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            displayUserMngUC();
        }

        private void BtnThongKe_Click(object sender, RoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            makeInactive();
            btnThongKe.Background = new SolidColorBrush(Color.FromRgb(113, 88, 226));
            displayStatUC();
        }

        private void btnSettingClick(object sender, RoutedEventArgs e)
        {
            UserDetailScreen udc;
            if(GlobalItem.currenUser.IsAdmin == true)
            {
                udc = new UserDetailScreen(2,3,GlobalItem.currenUser,null);
            }
            else
            {
                udc = new UserDetailScreen(1,3,GlobalItem.currenUser,null);
            }
            udc.ShowDialog();
            txtBox_user.Text = GlobalItem.currenUser.HoTen;
        }

        private void BtnLog_out_Click(object sender, RoutedEventArgs e)
        {
            GlobalItem.currenUser = null;
            Window nextWnd = new Login();
            nextWnd.Show();
            this.Close();            
        }
    }
}
