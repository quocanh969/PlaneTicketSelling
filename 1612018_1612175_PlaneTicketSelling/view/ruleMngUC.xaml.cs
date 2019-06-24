using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ruleMngUC.xaml
    /// </summary>
    public partial class ruleMngUC : UserControl
    {
        public ruleMngUC()
        {
            InitializeComponent();
            readInitValue();
            accessDenied();
        }

        // Đọc giá trị
        public void readInitValue()
        {
            rule1_5.Content = GlobalItem.DungToiDa.ToString();
            rule1_2.Content = GlobalItem.BayToiThieu.ToString();
            rule1_4.Content = GlobalItem.DungToiThieu.ToString();
            rule1_3.Content = GlobalItem.SBTrungGianToiDa.ToString();
            normalSeatTxt.Text = GlobalItem.veThuong.ToString();
            vipSeatTxt.Text = GlobalItem.veVIP.ToString();
            rule3_2.Content = GlobalItem.ngayHuyVe.ToString();
        }

        // Kiểm tra phân cấp người dùng
        public void accessDenied()
        {
            if (GlobalItem.currenUser.IsAdmin == false)
            {
                btnSave.Visibility = Visibility.Collapsed;
                // Quy định 1                
                increase1_2.Visibility = Visibility.Collapsed;
                increase1_3.Visibility = Visibility.Collapsed;
                increase1_4.Visibility = Visibility.Collapsed;
                increase1_5.Visibility = Visibility.Collapsed;
                decrease1_2.Visibility = Visibility.Collapsed;
                decrease1_3.Visibility = Visibility.Collapsed;
                decrease1_4.Visibility = Visibility.Collapsed;
                decrease1_5.Visibility = Visibility.Collapsed;

                // Quy định 2
                normalSeatTxt.IsEnabled = false;
                vipSeatTxt.IsEnabled = false;

                // Quy định 3
                increase3_1.Visibility = Visibility.Collapsed;
                increase3_2.Visibility = Visibility.Collapsed;
                decrease3_1.Visibility = Visibility.Collapsed;
                decrease3_2.Visibility = Visibility.Collapsed;
            }
        }

        // Nhóm quy định 1        
        private void btnIncrease1_2Click(object sender, RoutedEventArgs e)
        {
            string temp = rule1_2.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue == 360)
            {
                return;
            }
            currentValue += 30;
            rule1_2.Content = currentValue.ToString();
        }
        private void btnIncrease1_3Click(object sender, RoutedEventArgs e)
        {
            string temp = rule1_3.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue == 5)
            {
                return;
            }
            currentValue += 1;
            rule1_3.Content = currentValue.ToString();
        }
        private void btnIncrease1_4Click(object sender, RoutedEventArgs e)
        {
            string temp = rule1_4.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue == 60)
            {
                return;
            }
            currentValue += 30;
            if (Convert.ToInt32(rule1_5.Content.ToString()) < currentValue)
            {
                rule1_5.Content = currentValue.ToString();
            }
            rule1_4.Content = currentValue.ToString();
        }
        private void btnIncrease1_5Click(object sender, RoutedEventArgs e)
        {
            string temp = rule1_5.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue == 360)
            {
                return;
            }
            currentValue += 30;
            rule1_5.Content = currentValue.ToString();
        }
        private void btnDecrease1_2Click(object sender, RoutedEventArgs e)
        {
            string temp = rule1_2.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue == 60)
            {
                return;
            }
            currentValue -= 30;
            rule1_2.Content = currentValue.ToString();
        }
        private void btnDecrease1_3Click(object sender, RoutedEventArgs e)
        {
            string temp = rule1_3.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue == 1)
            {
                return;
            }
            currentValue -= 1;
            rule1_3.Content = currentValue.ToString();
        }
        private void btnDecrease1_4Click(object sender, RoutedEventArgs e)
        {
            string temp = rule1_4.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue == 30)
            {
                return;
            }
            currentValue -= 30;
            rule1_4.Content = currentValue.ToString();
        }
        private void btnDecrease1_5Click(object sender, RoutedEventArgs e)
        {
            string temp = rule1_5.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue <= Convert.ToInt32(rule1_4.Content.ToString()))
            {
                return;
            }
            currentValue -= 30;
            rule1_5.Content = currentValue.ToString();
        }

        // Nhóm quy định 2


        // Nhóm quy định 3
        private void btnIncrease3_1Click(object sender, RoutedEventArgs e)
        {
            string temp = rule3_1.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue == 20)
            {
                return;
            }
            currentValue += 1;
            rule3_1.Content = currentValue.ToString();
        }
        private void btnIncrease3_2Click(object sender, RoutedEventArgs e)
        {
            string temp = rule3_2.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue == 10)
            {
                return;
            }
            currentValue += 1;
            rule3_2.Content = currentValue.ToString();
        }
        private void btnDecrease3_1Click(object sender, RoutedEventArgs e)
        {
            string temp = rule3_1.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue == 1)
            {
                return;
            }
            currentValue -= 1;
            rule3_1.Content = currentValue.ToString();
        }
        private void btnDecrease3_2Click(object sender, RoutedEventArgs e)
        {
            string temp = rule3_2.Content.ToString();
            int currentValue = Convert.ToInt32(temp);
            if (currentValue == 1)
            {
                return;
            }
            currentValue -= 1;
            rule3_2.Content = currentValue.ToString();
        }

        // Xử lý khung nhập chỉ cho nhập số
        private static readonly Regex _regex = new Regex("[^0-9.]+");
        private void normalSeatTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
        private void vipSeatTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }

        // Lưu thay đổi
        private void btnSaveClick(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["minStopTime"].Value = rule1_4.Content.ToString();
            config.AppSettings.Settings["maxStopTime"].Value = rule1_5.Content.ToString();
            config.AppSettings.Settings["minFlyTime"].Value = rule1_2.Content.ToString();
            config.AppSettings.Settings["maxAPs"].Value = rule1_3.Content.ToString();
            config.AppSettings.Settings["expDate"].Value = rule3_2.Content.ToString();
            config.AppSettings.Settings["normalSeatPrice"].Value = normalSeatTxt.Text;
            config.AppSettings.Settings["vipSeatPrice"].Value = vipSeatTxt.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            GlobalItem.DungToiThieu = Convert.ToInt32(ConfigurationManager.AppSettings["minStopTime"]);
            GlobalItem.DungToiDa = Convert.ToInt32(ConfigurationManager.AppSettings["maxStopTime"]);
            GlobalItem.BayToiThieu = Convert.ToInt32(ConfigurationManager.AppSettings["minFlyTime"]);
            GlobalItem.SBTrungGianToiDa = Convert.ToInt32(ConfigurationManager.AppSettings["maxAPs"]);
            GlobalItem.veThuong = Convert.ToInt32(ConfigurationManager.AppSettings["normalSeatPrice"]);
            GlobalItem.veVIP = Convert.ToInt32(ConfigurationManager.AppSettings["vipSeatPrice"]);
            GlobalItem.ngayHuyVe = Convert.ToInt32(ConfigurationManager.AppSettings["expDate"]);

            MessageBox.Show("Thay đổi đã được lưu thành công!");
            readInitValue();
        }
    }
}
