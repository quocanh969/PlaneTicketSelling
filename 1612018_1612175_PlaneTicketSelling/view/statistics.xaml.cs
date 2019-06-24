using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

namespace _1612018_1612175_PlaneTicketSelling.view
{
    /// <summary>
    /// Interaction logic for statistics.xaml
    /// </summary>
    public partial class statistics : UserControl
    {
        public statistics()
        {
            InitializeComponent();
            setResources();
        }

        public void setResources()
        {
            for (int i = 1; i <= 12; i++)
            {
                cmbMonth.Items.Add(i);
            }
            cmbMonth.SelectedIndex = 0;

            for (int i = 2019; i <= 2050; i++)
            {
                cmbYear.Items.Add(i);
            }
            cmbYear.SelectedIndex = 0;

            //datePicker.DisplayDateEnd = DateTime.Today;
            datePicker.SelectedDate = DateTime.Today;

            cmbType.SelectedIndex = 0;

            txtChooseMonth.Visibility = Visibility.Hidden;
            txtChooseYear.Visibility = Visibility.Hidden;
            cmbMonth.Visibility = Visibility.Hidden;
            cmbYear.Visibility = Visibility.Hidden;
        }

        private void exportBtnClick(object sender, RoutedEventArgs e)
        {
            int month = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            int year = Convert.ToInt32(cmbYear.SelectedValue.ToString());
            DateTime date = datePicker.SelectedDate.Value;
            DataTable dt = new DataTable();
            if (cmbType.SelectedIndex == 0)
            {
                dt = receiptMngController.LoadData();
            }
            else if (cmbType.SelectedIndex == 1)
            {
                dt = receiptMngController.LoadDataOnDate(date);
            }
            else if (cmbType.SelectedIndex == 2)
            {
                dt = receiptMngController.LoadDataOnMonth(month, year);
            }
            else
            {
                dt = receiptMngController.LoadDataOnMonth(0, year);
            }
            
            object missing = Type.Missing;            
            Microsoft.Office.Interop.Excel.Application excel = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet = null;
            Microsoft.Office.Interop.Excel.Range excelCellrange = null;
            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                wb = excel.Workbooks.Add();
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    excelSheet.Range["A1"].Offset[0, i].Value = dt.Columns[i].ColumnName;
                }

                double totalIncome = 0;
                int totalNormalSeats = 0;
                int totalVIPSeats = 0;
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    excelSheet.Range["A2"].Offset[i].Resize[1, dt.Columns.Count].Value = dt.Rows[i].ItemArray;
                    totalIncome += Convert.ToDouble(dt.Rows[i]["Tong tien"].ToString());
                    totalNormalSeats += Convert.ToInt32(dt.Rows[i]["SoVeThuong"].ToString());
                    totalVIPSeats += Convert.ToInt32(dt.Rows[i]["SoVeVIP"].ToString());
                }
                
                excelSheet.Range["I1"].Value = "Tổng thu nhập";
                excelSheet.Range["I2"].Value = totalIncome;

                excelSheet.Range["I4"].Value = "Số ghế thường:";
                excelSheet.Range["I5"].Value = totalNormalSeats;

                excelSheet.Range["I7"].Value = "Số ghế VIP:";
                excelSheet.Range["I8"].Value = totalVIPSeats;

                excelSheet.Range["K1"].Value = "Tổng số ghế đã bán";
                excelSheet.Range["K2"].Value = totalNormalSeats + totalVIPSeats;

                excel.Visible = true;
                wb.Activate();
            }
            catch(COMException ex)
            {
                MessageBox.Show("Error accessing Excel: " + ex.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbType.SelectedIndex == 0)
            {
                datePicker.Visibility = Visibility.Collapsed;
                cmbMonth.Visibility = Visibility.Collapsed;
                cmbYear.Visibility = Visibility.Collapsed;
                txtChooseMonth.Visibility = Visibility.Collapsed;
                txtChooseYear.Visibility = Visibility.Collapsed;
                txtDatePick.Visibility = Visibility.Collapsed;
            }
            else if (cmbType.SelectedIndex == 1)
            {
                datePicker.Visibility = Visibility.Visible;
                cmbMonth.Visibility = Visibility.Collapsed;
                cmbYear.Visibility = Visibility.Collapsed;
                txtChooseMonth.Visibility = Visibility.Collapsed;
                txtChooseYear.Visibility = Visibility.Collapsed;
                txtDatePick.Visibility = Visibility.Visible;
            }
            else if (cmbType.SelectedIndex == 2)
            {
                datePicker.Visibility = Visibility.Collapsed;
                cmbMonth.Visibility = Visibility.Visible;
                cmbYear.Visibility = Visibility.Visible;
                txtChooseMonth.Visibility = Visibility.Visible;
                txtChooseYear.Visibility = Visibility.Visible;
                txtDatePick.Visibility = Visibility.Collapsed;
            }
            else
            {
                datePicker.Visibility = Visibility.Collapsed;
                cmbMonth.Visibility = Visibility.Collapsed;
                cmbYear.Visibility = Visibility.Visible;
                txtChooseMonth.Visibility = Visibility.Collapsed;
                txtChooseYear.Visibility = Visibility.Visible;
                txtDatePick.Visibility = Visibility.Collapsed;
            }
        }

        private void cmbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime month = new DateTime();
            month.AddMonths(Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
        }

        private void cmbMonth_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void cmbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbYear_DropDownClosed(object sender, EventArgs e)
        {

        }
    }
}
