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
    /// Interaction logic for scheduleMng.xaml
    /// </summary>
    public partial class scheduleMng : UserControl
    {
        private List<SANBAY> lstSB;
        public bool isEditting = false;

        public scheduleMng()
        {
            InitializeComponent();
            loadAirportData();
        }

        public void loadAirportData()
        {
            dataSanBay.ItemsSource = null;
            lstSB = AirportController.loadAirportData();
            dataSanBay.ItemsSource = lstSB;
            lblSoSB.Content = lstSB.Count();            
        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            isEditting = false;
            GlobalItem.selectedAirport = (SANBAY)dataSanBay.SelectedItem;
            detailAirportUC.Visibility = Visibility.Visible;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            isEditting = true;
            GlobalItem.selectedAirport = (SANBAY)dataSanBay.SelectedItem;
            detailAirportUC.Visibility = Visibility.Visible;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            AirportController.deleteAirport((SANBAY)dataSanBay.SelectedItem);
            dataSanBay.ItemsSource = null;
            loadAirportData();
        }

        private void BtnRefreshCancel_Click(object sender, RoutedEventArgs e)
        {
            loadAirportData();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            switch(cmbFilter.SelectedIndex)
            {
                case 0:
                    {                    
                        if(txtSearchBox.Text.Length > 5)
                        {
                            MessageBox.Show("Mã sân bay chi có 5 kí tự");
                        }
                        else
                        {
                            AirportController.searchByID(ref lstSB, txtSearchBox.Text);
                            dataSanBay.ItemsSource = lstSB;
                        }                        
                        break;
                    }
                case 1:
                    {                        
                        AirportController.searchByTen(ref lstSB, txtSearchBox.Text);
                        dataSanBay.ItemsSource = lstSB;
                        break;
                    }
                case 2:
                    {                     
                        AirportController.searchByDiaDiem(ref lstSB, txtSearchBox.Text);
                        dataSanBay.ItemsSource = lstSB;
                        break;
                    }
                default: break;
            }
        }

        private void BtnAddAirport_Click(object sender, RoutedEventArgs e)
        {
            addAirportUC.Visibility = Visibility.Visible;
        }
    }
}
