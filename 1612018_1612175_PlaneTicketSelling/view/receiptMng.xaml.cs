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
    /// Interaction logic for receiptMng.xaml
    /// </summary>
    public partial class receiptMng : UserControl
    {
        public int method;
        // Hình thức:
        // 1 - Thêm mới
        // 2 - Xem
        // 3 - sữa

        private List<Object> receiptData;
        

        public receiptMng()
        {
            InitializeComponent();            
        }

        private void addReceiptBtnClick(object sender, RoutedEventArgs e)
        {
            method = 1;
            addReceiptUC.Visibility = Visibility.Visible;            
        }

        public void loadDataHoaDon()
        {
            receiptData = receiptMngController.loadDataHoaDon().ToList();
            dataHoaDon.ItemsSource = null;
            dataHoaDon.ItemsSource = receiptData;
            lblSo_hd.Content = receiptData.Count();            
        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            if (dataHoaDon.SelectedItem != null)
            {
                method = 2;
                Object item = (Object)dataHoaDon.SelectedItem;
                GlobalItem.selectedReceipt = receiptMngController.getHoaDon((int)item.GetType().GetProperty("MaCB").GetValue(item),
                    (string)item.GetType().GetProperty("MaKH").GetValue(item), (DateTime)item.GetType().GetProperty("NgayDi").GetValue(item));
                addReceiptUC.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Bạn vẫn chưa chọn hóa đơn cần xem");
            }
        }

        private void BtnRefreshCancel_Click(object sender, RoutedEventArgs e)
        {
            loadDataHoaDon();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataHoaDon.SelectedItem != null)
            {
                method = 3;
                Object item = (Object)dataHoaDon.SelectedItem;
                GlobalItem.selectedReceipt = receiptMngController.getHoaDon((int)item.GetType().GetProperty("MaCB").GetValue(item),
                    (string)item.GetType().GetProperty("MaKH").GetValue(item), (DateTime)item.GetType().GetProperty("NgayDi").GetValue(item));
                addReceiptUC.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Bạn vẫn chưa chọn hóa đơn cần xem");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataHoaDon.SelectedItem != null)
            {                
                Object item = (Object)dataHoaDon.SelectedItem;
                GlobalItem.selectedReceipt = receiptMngController.getHoaDon((int)item.GetType().GetProperty("MaCB").GetValue(item),
                    (string)item.GetType().GetProperty("MaKH").GetValue(item), (DateTime)item.GetType().GetProperty("NgayDi").GetValue(item));
                receiptMngController.deleteReceipt(GlobalItem.selectedReceipt);
                GlobalItem.selectedReceipt = null;
                loadDataHoaDon();
            }
            else
            {
                MessageBox.Show("Bạn vẫn chưa chọn hóa đơn cần xem");
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            switch(cmbFilter.SelectedIndex)
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
                            receiptMngController.SearchForMAKH(ref receiptData, makh.ToString());
                            dataHoaDon.ItemsSource = receiptData;
                        }
                        break;
                    }
                case 1:
                    {
                        DateTime ngaydi = dpSearchBox.SelectedDate.Value;
                        FlightMngController.SearchForNGAYDI(ref receiptData, ngaydi);
                        dataHoaDon.ItemsSource = receiptData;
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
                            FlightMngController.SearchForSBDI(ref receiptData, sbdi.ToString());
                            dataHoaDon.ItemsSource = receiptData;
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
                            FlightMngController.SearchForSBDEN(ref receiptData, sbden.ToString());
                            dataHoaDon.ItemsSource = receiptData;
                        }
                        break;
                    }
                default: break;
            }
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFilter.SelectedIndex == 1)
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

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.Visibility == Visibility.Visible)
            {
                loadDataHoaDon();
            }
        }
    }
}
