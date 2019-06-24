using _1612018_1612175_PlaneTicketSelling.model;
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
    /// Interaction logic for addBooking.xaml
    /// </summary>
    public partial class addBooking : UserControl
    {
        private reservantionMng parent;

        private List<KHACH> lstGuest;
        private List<LICHBAY> lstLB;

        private StringBuilder error = new StringBuilder();

        private int normal, vip;
        private int oriNormal = 0, oriVip = 0;

        public addBooking()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            UserDetailScreen uds = new UserDetailScreen(3, 1, null, null);
            uds.ShowDialog();
            lstGuest = UserController.loadDataGuest();
            cmbKH.ItemsSource = null;            
            cmbKH.SelectedIndex = 0;
        }

        private void BtnTinhTien_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtNormalSeats.Text, out normal))
            {
                MessageBox.Show("Số vé thường phải là số tự nhiên");
                return;
            }
            if (!int.TryParse(txtVIPSeats.Text, out vip))
            {
                MessageBox.Show("Số vé vip phải là số tự nhiên");
                return;
            }

            txtTongTien.Text = (normal * GlobalItem.veThuong + vip * GlobalItem.veVIP).ToString();
        }

        private void BtnLapDon_Click(object sender, RoutedEventArgs e)
        {
            if (Checking())
            {
                try
                {
                    KHACH guest = (KHACH)cmbKH.SelectedItem;
                    LICHBAY lb = (LICHBAY)cmbMaCB.SelectedItem;
                    txtTongTien.Text = (normal * GlobalItem.veThuong + vip * GlobalItem.veVIP).ToString();
                    receiptMngController.addReceipt(new HOADON()
                    {
                        MaCB = lb.MaCB,
                        MaKH = guest.ID,
                        NgayDi = lb.NgayDi,
                        NgayMua = dpNgayMua.SelectedDate.Value,
                        SoVeThuong = normal,
                        TongTien = int.Parse(txtTongTien.Text),
                        SoVeVip = vip,
                        IsDatCho = true,
                        IsAvailable = true,
                    });
                    receiptMngController.updateFlightForReceipt(lb.MaCB, lb.NgayDi, normal, vip, oriNormal, oriVip);
                    this.Visibility = Visibility.Hidden;
                }
                catch
                {
                    MessageBox.Show("Có vẻ bạn đã mua/đặt vé để đi chuyến này rồi, vào hóa đơn để sữa lại thôi.");
                }
            }
            else
            {
                MessageBox.Show(error.ToString());
                error.Clear();
            }
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            txtNormalSeats.Text = "";
            txtVIPSeats.Text = "";
            cmbMaCB.SelectedIndex = 0;
            cmbKH.SelectedIndex = 0;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (parent.method == 2)
            {
                this.Visibility = Visibility.Hidden;
            }
            else // sửa
            {
                if (Checking())
                {
                    KHACH guest = (KHACH)cmbKH.SelectedItem;
                    LICHBAY lb = (LICHBAY)cmbMaCB.SelectedItem;
                    receiptMngController.updateReceipt(lb.MaCB, guest.ID, lb.NgayDi, normal, vip, int.Parse(txtTongTien.Text), dpNgayMua.SelectedDate.Value);
                    receiptMngController.updateFlightForReceipt(lb.MaCB, lb.NgayDi, normal, vip, oriNormal, oriVip);
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show(error.ToString());
                    error.Clear();
                }
            }
        }

        private void BtnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            if (Checking())
            {
                KHACH guest = (KHACH)cmbKH.SelectedItem;
                LICHBAY lb = (LICHBAY)cmbMaCB.SelectedItem;
                reservationController.translation(lb.MaCB, guest.ID, lb.NgayDi, normal, vip, int.Parse(txtTongTien.Text), dpNgayMua.SelectedDate.Value);
                this.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show(error.ToString());
                error.Clear();
            }
            
        }

        // HỦy vé
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan interval = GlobalItem.selectedReservation.NgayDi.Subtract(DateTime.Today);
            if (interval.Days <= GlobalItem.ngayHuyVe)
            {
                MessageBox.Show($"Thời hạn hủy vé là {GlobalItem.ngayHuyVe} ngày trước ngày bay\nHôm nay đã hết hạn.");
            }
            else
            {
                receiptMngController.updateFlightForReceipt(GlobalItem.selectedReservation.MaCB, GlobalItem.selectedReservation.NgayDi,
                    -GlobalItem.selectedReservation.SoVeThuong.Value, -GlobalItem.selectedReservation.SoVeVip.Value, 0, 0);
                receiptMngController.clearReceipt(GlobalItem.selectedReservation);                
                this.Visibility = Visibility.Hidden;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                DependencyObject ucParent = this.Parent;

                while (!(ucParent is UserControl))
                {
                    ucParent = LogicalTreeHelper.GetParent(ucParent);
                }

                parent = (reservantionMng)ucParent;

                lstGuest = UserController.loadDataGuest();
                cmbKH.ItemsSource = lstGuest;
                cmbKH.SelectedIndex = 0;
                
                lstLB = receiptMngController.loadLichBayData();
                cmbMaCB.ItemsSource = lstLB;
                cmbMaCB.SelectedIndex = 0;

                switch (parent.method)
                {
                    case 1:// Thêm
                        {
                            addComboBtn.Visibility = Visibility.Visible;
                            detailComboBtn.Visibility = Visibility.Hidden;
                            ManChe.Visibility = Visibility.Hidden;

                            searchPanel.Visibility = Visibility.Visible;

                            dpNgayMua.SelectedDate = DateTime.Today;
                            dpNgayMua.IsEnabled = false;

                            txtNormalSeats.Text = "";
                            txtVIPSeats.Text = "";

                            break;
                        }
                    case 2:// Xem
                        {
                            addComboBtn.Visibility = Visibility.Hidden;
                            detailComboBtn.Visibility = Visibility.Visible;
                            btnCancel.Visibility = Visibility.Hidden;
                            btnThanhToan.Visibility = Visibility.Hidden;
                            ManChe.Visibility = Visibility.Visible;

                            searchPanel.Visibility = Visibility.Hidden;

                            cmbKH.ItemsSource = lstGuest;

                            KHACH guest = lstGuest.Where(x => x.ID == GlobalItem.selectedReservation.MaKH).ToList().SingleOrDefault();
                            cmbKH.SelectedItem = guest;
                            txtTenKH.Text = guest.HoTen;
                            txtDialNumber2.Text = guest.SDT;

                            cmbMaCB.SelectedItem = lstLB.Where(x => x.MaCB == GlobalItem.selectedReservation.MaCB && x.NgayDi == GlobalItem.selectedReservation.NgayDi
                            && guest.ID == GlobalItem.selectedReservation.MaKH)
                                .SingleOrDefault();

                            txtNormalSeats.Text = GlobalItem.selectedReservation.SoVeThuong.ToString();
                            txtVIPSeats.Text = GlobalItem.selectedReservation.SoVeVip.ToString();
                            txtTongTien.Text = GlobalItem.selectedReservation.TongTien.ToString();

                            dpNgayMua.SelectedDate = GlobalItem.selectedReservation.NgayMua;

                            break;
                        }
                    case 3:// Sửa
                        {
                            addComboBtn.Visibility = Visibility.Hidden;
                            detailComboBtn.Visibility = Visibility.Visible;
                            btnCancel.Visibility = Visibility.Visible;
                            btnThanhToan.Visibility = Visibility.Visible;
                            ManChe.Visibility = Visibility.Hidden;

                            searchPanel.Visibility = Visibility.Visible;

                            cmbKH.IsEnabled = false;
                            cmbMaCB.IsEnabled = false;

                            cmbKH.ItemsSource = lstGuest;

                            KHACH guest = lstGuest.Where(x => x.ID == GlobalItem.selectedReservation.MaKH).ToList().SingleOrDefault();
                            cmbKH.SelectedItem = guest;
                            txtTenKH.Text = guest.HoTen;
                            txtDialNumber2.Text = guest.SDT;

                            cmbMaCB.SelectedItem = lstLB.Where(x => x.MaCB == GlobalItem.selectedReservation.MaCB && x.NgayDi == GlobalItem.selectedReservation.NgayDi
                            && guest.ID == GlobalItem.selectedReservation.MaKH)
                                .SingleOrDefault();

                            txtNormalSeats.Text = GlobalItem.selectedReservation.SoVeThuong.ToString();
                            txtVIPSeats.Text = GlobalItem.selectedReservation.SoVeVip.ToString();
                            txtTongTien.Text = GlobalItem.selectedReservation.TongTien.ToString();

                            oriNormal = GlobalItem.selectedReservation.SoVeThuong.Value;
                            oriVip = GlobalItem.selectedReservation.SoVeVip.Value;

                            dpNgayMua.SelectedDate = GlobalItem.selectedReservation.NgayMua;
                            break;
                        }
                    default: break;
                }
            }
            else
            {
                GlobalItem.selectedReservation = null;
                dpNgayMua.IsEnabled = true;
                cmbKH.IsEnabled = true;
                cmbMaCB.IsEnabled = true;
                txtTongTien.Text = "";
                parent.loadDataPhieuDat();
            }
        }

        private void CmbMaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LICHBAY lb = (LICHBAY)cmbMaCB.SelectedItem;
            dpNgayHetHan.SelectedDate = lb.NgayDi.AddDays(-1);
        }

        private void CmbKH_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KHACH selected = (KHACH)cmbKH.SelectedItem;
            if (selected != null)
            {
                txtDialNumber2.Text = selected.SDT;
                txtTenKH.Text = selected.HoTen;
            }
            else
            {
                txtDialNumber2.Text = "";
                txtTenKH.Text = "";
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            lstGuest = UserController.loadDataGuest();
            UserController.searchGuestForID(txtSearchBox.Text, ref lstGuest);
            cmbKH.ItemsSource = lstGuest;
            cmbKH.SelectedIndex = 0;
        }

        private bool Checking()
        {
            bool isError = false;
            if (!int.TryParse(txtNormalSeats.Text, out normal))
            {
                error.Append("Số vé thường phải là số tự nhiên");
                isError = true;
            }
            if (!int.TryParse(txtVIPSeats.Text, out vip))
            {
                error.Append("Số vé vip phải là số tự nhiên");
                isError = true;
            }

            if (parent.method == 3 && dpNgayMua.SelectedDate != null && DateTime.Compare(dpNgayMua.SelectedDate.Value, DateTime.Today) < 0)
            {
                error.Append("Thông tin ngày mua không hợp lệ");
                isError = true;
            }

            if (isError == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
