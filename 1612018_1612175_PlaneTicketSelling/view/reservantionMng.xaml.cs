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
using _1612018_1612175_PlaneTicketSelling.model;

namespace _1612018_1612175_PlaneTicketSelling.view
{
    /// <summary>
    /// Interaction logic for reservantionMng.xaml
    /// </summary>
    public partial class reservantionMng : UserControl
    {
        public int method;
        // Hình thức:
        // 1 - Thêm mới
        // 2 - Xem
        // 3 - sữa

        private List<Object> reservationData;

        public reservantionMng()
        {
            InitializeComponent();
            loadDataPhieuDat();
        }

        private void addReservationBtnClick(object sender, RoutedEventArgs e)
        {
            method = 1;
            addBookingUC.Visibility = Visibility.Visible;            
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            switch (cmbFilter.SelectedIndex)
            {
                case 0:
                    {
                        if (txtSearchBox.Text.Length > 9)
                        {
                            MessageBox.Show("Mã khách hàng chỉ gồm 9 ký tự");
                        }
                        else
                        {
                            StringBuilder makh = new StringBuilder(txtSearchBox.Text);
                            int l = makh.Length;
                            while (l < 9)
                            {
                                l++;
                                makh.Append(" ");
                            }
                            receiptMngController.SearchForMAKH(ref reservationData, makh.ToString());
                            dataPhieuDat.ItemsSource = reservationData;
                        }
                        break;
                    }
                case 1:
                    {
                        DateTime ngaydi = dpSearchBox.SelectedDate.Value;
                        FlightMngController.SearchForNGAYDI(ref reservationData, ngaydi);
                        dataPhieuDat.ItemsSource = reservationData;
                        break;
                    }
                case 2:
                    {
                        if (txtSearchBox.Text.Length > 5)
                        {
                            MessageBox.Show("Mã sân bay chỉ gồm 5 ký tự");
                        }
                        else
                        {
                            StringBuilder sbdi = new StringBuilder(txtSearchBox.Text);
                            int l = sbdi.Length;
                            while (l < 5)
                            {
                                l++;
                                sbdi.Append(" ");
                            }
                            FlightMngController.SearchForSBDI(ref reservationData, sbdi.ToString());
                            dataPhieuDat.ItemsSource = reservationData;
                        }
                        break;
                    }
                case 3:
                    {
                        if (txtSearchBox.Text.Length > 5)
                        {
                            MessageBox.Show("Mã sân bay chỉ gồm 5 ký tự");
                        }
                        else
                        {
                            StringBuilder sbden = new StringBuilder(txtSearchBox.Text);
                            int l = sbden.Length;
                            while (l < 5)
                            {
                                l++;
                                sbden.Append(" ");
                            }
                            FlightMngController.SearchForSBDEN(ref reservationData, sbden.ToString());
                            dataPhieuDat.ItemsSource = reservationData;
                        }
                        break;
                    }
                default: break;
            }
        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            if (dataPhieuDat.SelectedItem != null)
            {
                method = 2;
                Object item = (Object)dataPhieuDat.SelectedItem;
                GlobalItem.selectedReservation = receiptMngController.getHoaDon((int)item.GetType().GetProperty("MaCB").GetValue(item),
                    (string)item.GetType().GetProperty("MaKH").GetValue(item), (DateTime)item.GetType().GetProperty("NgayDi").GetValue(item));
                addBookingUC.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Bạn vẫn chưa chọn hóa đơn cần xem");
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataPhieuDat.SelectedItem != null)
            {
                method = 3;
                Object item = (Object)dataPhieuDat.SelectedItem;
                GlobalItem.selectedReservation = receiptMngController.getHoaDon((int)item.GetType().GetProperty("MaCB").GetValue(item),
                    (string)item.GetType().GetProperty("MaKH").GetValue(item), (DateTime)item.GetType().GetProperty("NgayDi").GetValue(item));
                addBookingUC.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Bạn vẫn chưa chọn hóa đơn cần xem");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataPhieuDat.SelectedItem != null)
            {
                Object item = (Object)dataPhieuDat.SelectedItem;
                GlobalItem.selectedReservation = receiptMngController.getHoaDon((int)item.GetType().GetProperty("MaCB").GetValue(item),
                    (string)item.GetType().GetProperty("MaKH").GetValue(item), (DateTime)item.GetType().GetProperty("NgayDi").GetValue(item));
                receiptMngController.deleteReceipt(GlobalItem.selectedReservation);
                GlobalItem.selectedReservation = null;
                loadDataPhieuDat();
            }
            else
            {
                MessageBox.Show("Bạn vẫn chưa chọn hóa đơn cần xem");
            }
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFilter.SelectedIndex == 1)
            {
                dpSearchBox.Visibility = Visibility.Visible;
                txtSearchBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                dpSearchBox.Visibility = Visibility.Collapsed;
                txtSearchBox.Visibility = Visibility.Visible;
            }
        }

        public void loadDataPhieuDat()
        {
            reservationData = reservationController.loadDataPhieuDat().ToList();
            dataPhieuDat.ItemsSource = null;
            dataPhieuDat.ItemsSource = reservationData;
            lblSo_pd.Content = reservationData.Count();
        }

        private void BtnRefreshCancel_Click(object sender, RoutedEventArgs e)
        {
            loadDataPhieuDat();
        }
    }
}
