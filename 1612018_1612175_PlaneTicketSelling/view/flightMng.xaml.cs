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
    /// Interaction logic for flightMng.xaml
    /// </summary>
    public partial class flightMng : UserControl
    {
        public int soChuyenBay = 0;
        public int soLichBay = 0;

        public bool IsFlightEdit = false;
        public bool IsSheduleEdit = false;

        private List<CHUYENBAY> FlightData;
        private List<Object> SheduleData;

        private string[] lstFilterForFlight = { "MaCB" ,"SBDi","SBDen","NgayDi","GioBay"};
        private string[] lstFilterForShedule = { "MaCB", "SBDi", "SBDen" };

        

        public flightMng()
        {
            InitializeComponent();            
        }



        private void flightDetailBtnClick(object sender, RoutedEventArgs e)
        {
            if(tab.SelectedIndex == 0)
            {// lịch bay
                if (dataLichBay.SelectedItem != null)
                {
                    IsSheduleEdit = true;
                    Object item = dataLichBay.SelectedItem;
                    GlobalItem.FlightOfSelectedShedule = (int)item.GetType().GetProperty("MaCB").GetValue(item);
                    GlobalItem.DateOfSelectedShedule = (DateTime)item.GetType().GetProperty("NgayDi").GetValue(item);
                    addSheduleUC.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Bạn vẫn chưa chọn chuyến bay cần xem");
                }
            }
            else
            {// chuyen bay
                if (dataChuyenBay.SelectedItem != null)
                {
                    IsFlightEdit = true;
                    GlobalItem.selectedFlight = (CHUYENBAY)dataChuyenBay.SelectedItem;
                    addFlightUC.Visibility = Visibility.Visible;                    
                }
                else
                {
                    MessageBox.Show("Bạn vẫn chưa chọn chuyến bay cần xem");
                }
            }
        }

        private void btnAddFlightClick(object sender, RoutedEventArgs e)
        {
            addFlightUC.Visibility = Visibility.Visible;
            IsFlightEdit = false;
        }

        private void BtnAddFlightShedule_Click(object sender, RoutedEventArgs e)
        {
            addSheduleUC.Visibility = Visibility.Visible;
        }

        public void loadLichBayData()
        {
            // Load dữ liệu cho bản lịch bay
            SheduleData = FlightMngController.LoadSheduleData().ToList();
            dataLichBay.ItemsSource = null;
            dataLichBay.ItemsSource = SheduleData;
            soLichBay = SheduleData.Count();
            lblSo_lich_bay.Content = soLichBay;
        }

        public void loadChuyenBayData()
        { 
            // Load dữ liệu cho bản chuyến bay
            FlightData = FlightMngController.LoadFlightData();
            dataChuyenBay.ItemsSource = null;
            dataChuyenBay.ItemsSource = FlightData;
            soChuyenBay = FlightMngController.getLightIDAutomatic();
            lblSo_chuyen_bay.Content = soChuyenBay;
        }

        // Quản lý search box khi combo box thay đổi
        private void CmbSearchFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(cmbSearchFilter.SelectedIndex)
            {
                case 0:
                case 1:
                case 2:
                    {
                        txtSearchBox.Visibility = Visibility.Visible;
                        dpSearchBox.Visibility = Visibility.Collapsed;
                        lblTimeSearch.Visibility = Visibility.Collapsed;
                        timeSearchBox.Visibility = Visibility.Collapsed;
                        break;
                    }
                case 3:
                    {
                        txtSearchBox.Visibility = Visibility.Collapsed;
                        dpSearchBox.Visibility = Visibility.Visible;
                        lblTimeSearch.Visibility = Visibility.Collapsed;
                        timeSearchBox.Visibility = Visibility.Collapsed;
                        break;
                    }
                case 4:
                    {
                        txtSearchBox.Visibility = Visibility.Collapsed;
                        dpSearchBox.Visibility = Visibility.Collapsed;
                        lblTimeSearch.Visibility = Visibility.Visible;
                        timeSearchBox.Visibility = Visibility.Visible;
                        break;
                    }
                default: break;
            }
        }

        // Quản lý combo box khi đổi tab
        private void Tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            if(tab.SelectedItem == tabItemChuyenBay)
            {
                cmbSearchFilter.ItemsSource = lstFilterForShedule;
            }
            else
            {
                cmbSearchFilter.ItemsSource = lstFilterForFlight;
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(Check())
            {
                if(tab.SelectedItem == tabItemChuyenBay) // Chuyến bay
                {
                    switch(cmbSearchFilter.SelectedIndex)
                    {
                        case 0:
                            {
                                int macb = int.Parse(txtSearchBox.Text);
                                FlightMngController.SearchFlightForMACB(ref FlightData,macb);
                                dataChuyenBay.ItemsSource = FlightData;
                                break;
                            }
                        case 1:
                            {
                                StringBuilder sbdi = new StringBuilder(txtSearchBox.Text);
                                int l = sbdi.Length;
                                while (l < 5)
                                {
                                    l++;
                                    sbdi.Append(" ");
                                }
                                int length = sbdi.Length;
                                FlightMngController.SearchFlightForSBDI(ref FlightData, sbdi.ToString());
                                dataChuyenBay.ItemsSource = FlightData;
                                break;
                            }
                        case 2:
                            {
                                StringBuilder sbden = new StringBuilder(txtSearchBox.Text);
                                int l = sbden.Length;
                                while (l < 5)
                                {
                                    l++;
                                    sbden.Append(" ");
                                }
                                FlightMngController.SearchFlightForSBDI(ref FlightData, sbden.ToString());
                                dataChuyenBay.ItemsSource = FlightData;
                                break;
                            }                        
                        default: break;
                    }
                }
                else
                {                    
                    // Lịch bay
                    switch (cmbSearchFilter.SelectedIndex)
                    {
                        case 0:
                            {
                                int macb = int.Parse(txtSearchBox.Text);
                                FlightMngController.SearchForMACB(ref SheduleData, macb);
                                dataLichBay.ItemsSource = SheduleData;
                                break;
                            }
                        case 1:
                            {
                                StringBuilder sbdi = new StringBuilder(txtSearchBox.Text);
                                int l = sbdi.Length;
                                while(l < 5)
                                {
                                    l++;
                                    sbdi.Append(" ");
                                }
                                FlightMngController.SearchForSBDI(ref SheduleData, sbdi.ToString());
                                dataLichBay.ItemsSource = SheduleData;

                                break;
                            }
                        case 2:
                            {
                                StringBuilder sbden = new StringBuilder(txtSearchBox.Text);
                                int l = sbden.Length;
                                while (l < 5)
                                {
                                    l++;
                                    sbden.Append(" ");
                                }
                                FlightMngController.SearchForSBDEN(ref SheduleData, sbden.ToString());
                                dataLichBay.ItemsSource = SheduleData;

                                break;
                            }
                        case 3:
                            {
                                DateTime ngaydi = dpSearchBox.SelectedDate.Value;
                                FlightMngController.SearchForNGAYDI(ref SheduleData, ngaydi);
                                dataLichBay.ItemsSource = SheduleData;
                                break;
                            }
                        case 4:
                            {
                                TimeSpan giobay = new TimeSpan(int.Parse(hourSearchBox.Text), int.Parse(minSearchBox.Text), 0);
                                FlightMngController.SearchForGIOBAY(ref SheduleData, giobay);
                                dataLichBay.ItemsSource = SheduleData;
                                break;
                            }
                        default: break;
                    }
                }
            }
            else
            {
                // do nothing
            }
        }

        // Kiểm tra khung search
        private bool Check()
        {
            string searchStr = txtSearchBox.Text;
            switch (cmbSearchFilter.SelectedIndex)
            {
                case 0:
                    {
                        if (!int.TryParse(searchStr, out int n))
                        {
                            MessageBox.Show("Mã chuyến bay là số, vui lòng nhập lại");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case 1:
                case 2:                    
                    {
                        int l = searchStr.Count();
                        if (l > 5 || l == 0)
                        {
                            MessageBox.Show("Mã sân bay chỉ có tối đa 5 kí tự");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case 3:
                    {
                        if(dpSearchBox.SelectedDate == null)
                        {
                            MessageBox.Show("Vui lòng chọn một ngày");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case 4:
                    {                            
                        if(!int.TryParse(hourSearchBox.Text,out int t1) || !int.TryParse(minSearchBox.Text,out int t2))
                        {
                            MessageBox.Show("Thời gian bạn nhập vào không đúng");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default: return false;
            }                
        }

        private void BtnRefreshCancel_Click(object sender, RoutedEventArgs e)
        {
            if(tab.SelectedIndex == 0) // lịch bay
            {
                loadLichBayData();
            }
            else
            {
                loadChuyenBayData();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(tab.SelectedIndex == 0)
            {// lịch bay
                if(dataLichBay.SelectedItem != null)
                {
                    Object item = (Object)dataLichBay.SelectedItem;
                    int macb = (int)item.GetType().GetProperty("MaCB").GetValue(item);
                    DateTime ngaydi = (DateTime)item.GetType().GetProperty("NgayDi").GetValue(item);
                    FlightMngController.deleteSheduleFor(macb, ngaydi);
                    dataLichBay.ItemsSource = null;
                    loadLichBayData();
                }
                else
                {
                    MessageBox.Show("Bạn vẫn chưa chọn lịch bay cần xóa");
                }
            }
            else
            {// chuyển bay
                if(dataChuyenBay.SelectedItem != null)
                {
                    FlightMngController.deleteFlight((CHUYENBAY)dataChuyenBay.SelectedItem);
                    dataChuyenBay.ItemsSource = null;
                    loadChuyenBayData();
                }
                else
                {
                    MessageBox.Show("Bạn vẫn chưa chọn chuyến bay cần xóa");
                }
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.Visibility == Visibility.Visible)
            {
                loadLichBayData();
                loadChuyenBayData();
            }
        }
    }
}
