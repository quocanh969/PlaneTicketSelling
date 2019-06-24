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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {   
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            int valid = AccountValidate();
            if (valid == 0)
            {
                txtBox_notice.Text = "*Tài khoản không tồn tại";
                txtBox_notice.Foreground = Brushes.Red;
                txtBox_notice.Visibility = Visibility.Visible;
            }
            else if ( valid == -1)
            {
                txtBox_notice.Text = "*Sai mật khẩu";
                txtBox_notice.Foreground = Brushes.Red;
                txtBox_notice.Visibility = Visibility.Visible;
            }
            else
            {                
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }

        private int AccountValidate()
        {

            return UserController.UserValidate(txtBox_UserName.Text, passBox_Password.Password.ToString());   
        }

        private void keyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) BtnLogin_Click(this, new RoutedEventArgs());
        }

        private void isFocusing(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtBox_UserName.IsKeyboardFocused)
            {
                txtBox_UserName.Background = Brushes.WhiteSmoke;
                txtBox_notice.Visibility = Visibility.Hidden;
                if (txtBox_UserName.Text.Length > 0) txtBox_UserName.Text = "";
            }
            if (passBox_Password.IsKeyboardFocused)
            {
                passBox_Password.Background = Brushes.WhiteSmoke;
                txtBox_notice.Visibility = Visibility.Hidden;
                if (passBox_Password.ToString().Length > 0) passBox_Password.Clear();
            }
        }
    }
}
