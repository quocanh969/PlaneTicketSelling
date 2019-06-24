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
using System.Windows.Shapes;

namespace _1612018_1612175_PlaneTicketSelling.view
{
    /// <summary>
    /// Interaction logic for UserDetailScreen.xaml
    /// </summary>
    public partial class UserDetailScreen : Window
    {
        private USER user;
        private KHACH guest;
        
        private StringBuilder error = new StringBuilder();

        private int role, cru;
               
        public UserDetailScreen(int ROLE, int CRU, USER _user, KHACH _guest)
        {
            // role - giá trị vai trò
            // 1 - nhân viên staff
            // 2 - quản lý admin
            // 3 - khách hàng guest

            // CRU - giá trị phương thức
            // 1 - tạo mới
            // 2 - xem thông tin
            // 3 - cập nhật dữ liệu
            InitializeComponent();
            // Hiển thị icon tương ứng vơi tài khoảng hiện tại

            role = ROLE;
            cru = CRU;
            user = _user;
            guest = _guest;
            

            switch (role)
            {
                case 1:
                    {
                        userIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.User;
                        userLabel.Content = "STAFF";
                        cmbRole.SelectedIndex = 0;
                        cmbRole.IsEnabled = false;
                        break;
                    }
                case 2:
                    {
                        userIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Lock;
                        userLabel.Content = "ADMIN";
                        cmbRole.SelectedIndex = 1;
                        break;
                    }
                case 3:
                    {
                        userIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.User;
                        userLabel.Content = "GUEST";
                        username.Visibility = Visibility.Hidden;
                        
                        btnSetting.Visibility = Visibility.Hidden;
                        cmbRole.Visibility = Visibility.Hidden;
                        break;
                    }
                default: return;
            }

            switch(cru)
            {
                case 1:
                    {
                        cmbRole.IsEnabled = false;
                        btnSetting.Visibility = Visibility.Hidden;                                                
                        break;
                    }
                case 2:
                    {
                        cmbRole.IsEnabled = false;
                        if(role != 3)
                        {// Không phải khách hàng
                            txtUserName.Text = user.Username;
                            if (user.IsAdmin == false)
                            {
                                cmbRole.SelectedIndex = 0;
                            }
                            else
                            {
                                cmbRole.SelectedIndex = 1;
                            }
                            txtBoxTen.Text = user.HoTen;
                            txtBoxEmail.Text = user.Email;
                            txtBoxCMND.Text = user.CMND;
                            txtBoxDiaChi.Text = user.DiaChi;
                            txtBoxSDT.Text = user.SDT;
                            btnSetting.Content = "Xem mất khẩu";
                        }
                        else
                        {// Khách hàng
                            txtBoxTen.Text = guest.HoTen;
                            txtBoxEmail.Text = guest.Email;
                            txtBoxCMND.Text = guest.ID;
                            txtBoxDiaChi.Text = guest.DiaChi;
                            txtBoxSDT.Text = guest.SDT;
                        }

                        btnCancel.Visibility = Visibility.Hidden;
                        manChe.Visibility = Visibility.Visible;
                        break;
                    }
                case 3:
                    {
                        manChe.Visibility = Visibility.Hidden;
                        txtBoxCMND.IsEnabled = false;

                        if(role != 3)
                        {
                            txtUserName.Text = user.Username;
                            if (user.IsAdmin == false)
                            {
                                cmbRole.SelectedIndex = 0;
                            }
                            else
                            {
                                cmbRole.SelectedIndex = 1;
                            }
                            if(GlobalItem.currenUser.IsAdmin == true)
                            {
                                cmbRole.IsEnabled = true;
                            }
                            txtBoxTen.Text = user.HoTen;
                            txtBoxEmail.Text = user.Email;
                            txtBoxCMND.Text = user.CMND;
                            txtBoxDiaChi.Text = user.DiaChi;
                            txtBoxSDT.Text = user.SDT;                            
                        }
                        else
                        {// Khách hàng                            
                            txtBoxTen.Text = guest.HoTen;
                            txtBoxEmail.Text = guest.Email;
                            txtBoxCMND.Text = guest.ID;
                            txtBoxDiaChi.Text = guest.DiaChi;
                            txtBoxSDT.Text = guest.SDT;
                        }

                        break;
                    }
                default: break;
            }            
        }

        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
            if(cru == 2)
            {
                ChangePassword cp = new ChangePassword(false, user.Pass);
                cp.ShowDialog();
            }
            else // cru = 3 vì cru = 1 thì btnSetting không hiển thị
            {
                ChangePassword cp = new ChangePassword(true, user.Pass);
                cp.ShowDialog();
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            switch (cru)
            {
                case 1:
                    {
                        if(Checking(false) == true)
                        {
                            if (role != 3)
                            {
                                bool admin = false;
                                if(cmbRole.SelectedIndex == 1)
                                {
                                    admin = true;
                                }
                                UserController.addStaff(new USER()
                                {
                                    CMND = txtBoxCMND.Text,
                                    Username = txtUserName.Text,
                                    Pass = "1",
                                    SDT = txtBoxSDT.Text,
                                    Email = txtBoxEmail.Text,
                                    DiaChi = txtBoxDiaChi.Text,
                                    HoTen = txtBoxTen.Text,
                                    IsAdmin = admin,
                                    IsAvailable = true,
                                });
                                this.Close();
                            }
                            else
                            {
                                UserController.addGuest(new KHACH()
                                {
                                    ID = txtBoxCMND.Text,
                                    SDT = txtBoxSDT.Text,
                                    Email = txtBoxEmail.Text,
                                    DiaChi = txtBoxDiaChi.Text,
                                    HoTen = txtBoxTen.Text,
                                    IsAvailable = true,
                                });
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show(error.ToString());
                            error.Clear();
                        }
                        break;
                    }
                case 2:
                    {                        
                        this.Close();
                        break;
                    }
                case 3:
                    {
                        if(Checking(true) == true)
                        {
                            if (role != 3)
                            {// Nhân viên
                                if (cmbRole.SelectedIndex == 0)
                                {
                                    UserController.updateStaff(txtBoxCMND.Text, txtUserName.Text, txtBoxTen.Text, txtBoxDiaChi.Text, txtBoxSDT.Text, txtBoxEmail.Text, false);
                                }
                                else
                                {
                                    UserController.updateStaff(txtBoxCMND.Text, txtUserName.Text, txtBoxTen.Text, txtBoxDiaChi.Text, txtBoxSDT.Text, txtBoxEmail.Text, true);
                                }
                            }
                            else
                            {// Khách hàng

                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(error.ToString());
                            error.Clear();
                        }
                        break;
                    }
                default: break;
            }
        }

        private bool Checking(bool isEdit)
        {
            string email = txtBoxEmail.Text;
            string sdt = txtBoxSDT.Text;
            string _id = txtBoxCMND.Text;

            bool isError = false;

            if(txtBoxTen.Text == "" || txtBoxSDT.Text == "" || txtBoxDiaChi.Text == "" || txtBoxCMND.Text == "" )
            {
                error.Append("Bạn nhập thiếu dữ liệu Ten/SDT/DiaChi/CMND\n");
                isError = true;
            }
            else if(email.IndexOf('@') == -1)
            {
                error.Append("Email không hợp lệ");
                isError = true;
            }
            else if(numberStringChecking(sdt) == false)
            {
                error.Append("Số điện thoại không hợp lệ");
                isError = true;
            }
            else if(isEdit == false && (_id.Length > 9 || numberStringChecking(_id) == false))
            {
                error.Append("CMND/ID không hợp lệ, chứa chữ cái hoặc nhiều hơn 9 ký tự\n");
                isError = true;
            }            
            else
            {
                StringBuilder id = new StringBuilder(txtBoxCMND.Text);
                int l = id.Length;
                while(l < 9)
                {
                    id.Append(" ");
                    l++;
                }
                
                txtBoxCMND.Text = id.ToString();
                int lent = txtBoxCMND.Text.Length;
                if (role != 3)
                {
                    if (txtUserName.Text == "")
                    {
                        isError = true;
                    }
                    
                    if(isEdit == false && UserController.userValidate(txtBoxCMND.Text) == false)
                    {
                        error.Append("CMND này đã tồn tại\n");
                        isError = true;
                    }
                }
                else
                {
                    if(isEdit == false && UserController.guestValidate(txtBoxCMND.Text) == false)
                    {
                        error.Append("ID của khách hàng này đã tồn tại\n");
                        isError = true;
                    }
                }
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

        private bool numberStringChecking(string str)
        {

            for(int i = 0;i<str.Length;i++)
            {
                if(str.ElementAt(i) > 57 || str.ElementAt(i) < 48)
                {
                    return false;
                }
            }
            return true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {            
            if(cru != 0)
            {
                this.Close();
            }
            else
            {
                txtBoxCMND.Text = "";
                txtUserName.Text = "";
                txtBoxSDT.Text = "";
                txtBoxEmail.Text = "";
                txtBoxDiaChi.Text = "";
                txtBoxTen.Text = "";
                cmbRole.SelectedIndex = 0;
            }
        }
    }
}
