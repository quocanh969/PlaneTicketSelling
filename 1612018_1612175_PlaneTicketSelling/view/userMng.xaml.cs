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
    /// Interaction logic for userMng.xaml
    /// </summary>
    public partial class userMng : UserControl
    {
        private List<USER> lstUSER;
        private List<KHACH> lstGUEST;

        public bool isEditting = false;

        public userMng()
        {
            InitializeComponent();
        }

        // Nhóm Combo button --------------------------------------------------------    

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchStr = txtSearchBox.Text;
            if (searchStr == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin cần tìm");
            }
            else
            {
                if (tab.SelectedIndex == 0)
                {// Nhân viên
                    switch(cmbFilter.SelectedIndex)
                    {
                        case 0:
                            {
                                if (searchStr.Length > 9)
                                {
                                    MessageBox.Show("CMND nhân viên chỉ có 9 ký tự");
                                }
                                else
                                {
                                    UserController.searchStaffForID(searchStr, ref lstUSER);
                                    dataNhanVien.ItemsSource = lstUSER;
                                }
                                break;
                            }
                        case 1:
                            {
                                UserController.searchStaffForHoTen(searchStr, ref lstUSER);
                                dataNhanVien.ItemsSource = lstUSER;
                                break;
                            }
                        case 2:
                            {
                                UserController.searchStaffForSDT(searchStr, ref lstUSER);
                                dataNhanVien.ItemsSource = lstUSER;
                                break;
                            }
                        default: break;
                    }                    
                }
                else
                {// Khách hàng
                    switch (cmbFilter.SelectedIndex)
                    {
                        case 0:
                            {
                                if(searchStr.Length>9)
                                {
                                    MessageBox.Show("ID khách hàng chỉ có 9 kí tự");
                                }
                                else
                                {
                                    UserController.searchGuestForID(searchStr, ref lstGUEST);
                                    dataKhachHang.ItemsSource = lstGUEST;
                                }
                                break;
                            }
                        case 1:
                            {
                                UserController.searchGuestForHoTen(searchStr, ref lstGUEST);
                                dataKhachHang.ItemsSource = lstGUEST;
                                break;
                            }
                        case 2:
                            {
                                UserController.searchGuestForSDT(searchStr, ref lstGUEST);
                                dataKhachHang.ItemsSource = lstGUEST;
                                break;
                            }
                        default: break;
                    }
                }
            }
        }

        private void BtnRefreshCancel_Click(object sender, RoutedEventArgs e)
        {
            if(tab.SelectedIndex == 0)
            {// Nhân viên
                dataNhanVien.ItemsSource = null;
                loadDataStaff();
            }
            else
            {// Khách hàng
                dataKhachHang.ItemsSource = null;
                loadDataGuest();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (tab.SelectedIndex == 0)
            {// Nhân viên
                USER selectedUser = (USER)dataNhanVien.SelectedItem;
                if (selectedUser != null)
                {
                    if (selectedUser.IsAdmin == true)
                    {// Quản lý
                        UserDetailScreen uds = new UserDetailScreen(2, 3, selectedUser, null);
                        uds.ShowDialog();
                    }
                    else
                    {// Nhân viên bình thường
                        UserDetailScreen uds = new UserDetailScreen(1, 3, selectedUser, null);
                        uds.ShowDialog();
                    }
                    dataNhanVien.ItemsSource = null;
                    loadDataStaff();
                }
                else
                {
                    MessageBox.Show("Bạn vẫn chưa chọn nhân viên cần sửa");
                }
            }
            else
            {// Khách hàng
                KHACH selectedGuest = (KHACH)dataKhachHang.SelectedItem;
                if (selectedGuest != null)
                {
                    UserDetailScreen uds = new UserDetailScreen(3, 3, null, selectedGuest);
                    uds.ShowDialog();
                    dataKhachHang.ItemsSource = null;
                    loadDataGuest();
                }
                else
                {
                    MessageBox.Show("Bạn vẫn chưa chọn khách hàng cần sửa");
                }
            }
        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            if (tab.SelectedIndex == 0)
            {// Nhân viên                
                USER selectedUser = (USER)dataNhanVien.SelectedItem;
                if (selectedUser != null)
                {
                    if (selectedUser.IsAdmin == true)
                    {// Quản lý
                        UserDetailScreen uds = new UserDetailScreen(2, 2, selectedUser, null);
                        uds.Show();
                    }
                    else
                    {// Nhân viên bình thường
                        UserDetailScreen uds = new UserDetailScreen(1, 2, selectedUser, null);
                        uds.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn vẫn chưa chọn nhân viên cần xem");
                }
            }
            else
            {// Khách hàng
                KHACH selectedGuest = (KHACH)dataKhachHang.SelectedItem;
                if (selectedGuest != null)
                {
                    UserDetailScreen uds = new UserDetailScreen(3, 2, null, selectedGuest);
                    uds.Show();
                }
                else
                {
                    MessageBox.Show("Bạn vẫn chưa chọn khách hàng cần xem");
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (tab.SelectedIndex == 0)
            {// Nhân viên
                USER selectedUser= (USER)dataNhanVien.SelectedItem;                
                if (selectedUser != null)
                {
                    deleteStaff(selectedUser);
                    dataNhanVien.ItemsSource = null;
                    loadDataStaff();
                }
                else
                {
                    MessageBox.Show("Bạn vẫn chưa chọn khách hàng cần xóa");
                }
            }
            else
            {// Khách hàng
                KHACH selectedGuest = (KHACH)dataKhachHang.SelectedItem;
                if (selectedGuest != null)
                {
                    deleteGuest(selectedGuest);
                    dataKhachHang.ItemsSource = null;
                    loadDataGuest();
                }
                else
                {
                    MessageBox.Show("Bạn vẫn chưa chọn khách hàng cần xóa");
                }
            }
        }

        // Nhóm khách hàng -------------------------------------------------------------
        public void loadDataGuest()
        {
            lstGUEST = UserController.loadDataGuest();
            dataKhachHang.ItemsSource = lstGUEST;
            lblSoKH.Content = lstGUEST.Count().ToString();
        }

        private void deleteGuest(KHACH guest)
        {
            UserController.deleteGuest(guest);
            loadDataGuest();
        }

        private void BtnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            UserDetailScreen uds = new UserDetailScreen(3, 1, null, null);
            uds.ShowDialog();
            loadDataGuest();
        }


        // Nhóm Staff ------------------------------------------------------------------
        public void loadDataStaff()
        {
            lstUSER = UserController.loadDataStaff();
            dataNhanVien.ItemsSource = lstUSER;
            lblSoNV.Content = lstUSER.Count().ToString();
        }

        private void deleteStaff(USER staff)
        {
            UserController.deleteStaff(staff);
            loadDataStaff();
        }

        private void BtnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            UserDetailScreen uds = new UserDetailScreen(1, 1, null, null);
            uds.ShowDialog();
            loadDataStaff();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.Visibility == Visibility.Visible)
            {
                loadDataStaff();
                loadDataGuest();
            }
        }
    }
}
