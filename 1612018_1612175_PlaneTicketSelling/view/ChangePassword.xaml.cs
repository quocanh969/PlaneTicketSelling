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
using _1612018_1612175_PlaneTicketSelling.model;
using _1612018_1612175_PlaneTicketSelling.controller;

namespace _1612018_1612175_PlaneTicketSelling.view
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private bool isEdit = false;

        public ChangePassword(bool isEditting, string password)
        {
            InitializeComponent();

            isEdit = isEditting;

            if(!isEdit)
            {
                currentPass.Visibility = Visibility.Hidden;
                newPass.Visibility = Visibility.Hidden;
                confirmPass.Visibility = Visibility.Hidden;

                btnCancel.Visibility = Visibility.Collapsed;                

                pass.Visibility = Visibility.Visible;
                txtPassword.Text = password;
            }
            else
            {
                currentPass.Visibility = Visibility.Visible;
                newPass.Visibility = Visibility.Visible;
                confirmPass.Visibility = Visibility.Visible;

                btnCancel.Visibility = Visibility.Visible;

                pass.Visibility = Visibility.Hidden;
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit == true)
            {
                if (txtBoxCurrentPass.Password != GlobalItem.currenUser.Pass)
                {
                    MessageBox.Show("Mật khẩu bạn nhập vào không đúng");
                }
                else if (txtBoxNewPass.Password != txtBoxConfirm.Password)
                {
                    MessageBox.Show("Mật khẩu xác nhận không đúng");
                }
                else
                {
                    USER _new = DataProvider.ins.DB.USERs.Where(x => x.Username == GlobalItem.currenUser.Username).SingleOrDefault();
                    _new.Pass = txtBoxNewPass.Password;
                    DataProvider.ins.DB.SaveChanges();
                    this.Close();
                }                
            }
            else
            {
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtBoxCurrentPass.Password = "";
            txtBoxNewPass.Password = "";
            txtBoxConfirm.Password = "";
        }
    }
}
