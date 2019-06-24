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
    /// Interaction logic for addNewShedule.xaml
    /// </summary>
    public partial class addNewShedule : UserControl
    {
        private List<CHUYENBAY> lstCB;

        private List<string> lstNote = new List<string>();
        private List<int> lstStop = new List<int>();

        private List<SANBAY> lstSBTG = new List<SANBAY>();

        private LICHBAY selected;
        private CHUYENBAY selectedCB;

        private StringBuilder error = new StringBuilder();

        private flightMng parentWind;

        private int h, m, normal , vip;
        private bool IsNoteUpdated = false;

        public addNewShedule()
        {
            InitializeComponent();
        }

        private void CmbMaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmbMaCB.SelectedIndex;
            TimeSpan temp = lstCB.ElementAt(index).ThoiGianBay.Value;
            txtSBDi.Text = lstCB.ElementAt(index).SBDi;
            txtSBDen.Text = lstCB.ElementAt(index).SBDen;
            txtDefaultHrs.Text = temp.Hours.ToString();
            txtDefaultMins.Text = temp.Minutes.ToString();            
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        // Thêm sb trung gian
        private void btnAddTransitClick(object sender, RoutedEventArgs e)
        {
            if (lstSBTG.Count() == GlobalItem.SBTrungGianToiDa)
            {
                MessageBox.Show($"Số sân bay trung gian tối đa là {GlobalItem.SBTrungGianToiDa}");
            }
            else
            {
                IsNoteUpdated = true;
                listView_SBTG.ItemsSource = null;
                lstSBTG.Add((SANBAY)cmbSBTrungGian.SelectedItem);
                listView_SBTG.ItemsSource = lstSBTG;
                lstNote.Add("");
                lstStop.Add(GlobalItem.DungToiThieu);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject ucParent = this.Parent;

            while (!(ucParent is UserControl))
            {
                ucParent = LogicalTreeHelper.GetParent(ucParent);
            }

            parentWind = (flightMng)ucParent;

            if (this.Visibility == Visibility.Visible)
            {
                lstCB = FlightMngController.LoadFlightData();
                cmbMaCB.ItemsSource = lstCB;
                cmbSBTrungGian.ItemsSource = FlightMngController.LoadAirportData();

                if(parentWind.IsSheduleEdit == true)
                {// Xem - sữa
                    lblScheTitle.Content = "CHI TIẾT LỊCH BAY";

                    selected = FlightMngController.getLBByID(GlobalItem.FlightOfSelectedShedule, GlobalItem.DateOfSelectedShedule);
                    selectedCB = FlightMngController.getCBByID(GlobalItem.FlightOfSelectedShedule);

                    
                    

                    cmbMaCB.IsEnabled = false;
                    txtSBDi.IsEnabled = false;
                    txtSBDen.IsEnabled = false;
                    dp_ngaybay.IsEnabled = false;

                    txtNormalSeats.IsEnabled = false;
                    txtVIPSeats.IsEnabled = false;

                    int index = GlobalItem.FlightOfSelectedShedule - 1;
                    cmbMaCB.SelectedItem = selectedCB;
                    txtSBDi.Text = selectedCB.SBDi;
                    txtSBDen.Text = selectedCB.SBDen;
                    txtDefaultHrs.Text = selectedCB.ThoiGianBay.Value.Hours.ToString();
                    txtDefaultMins.Text = selectedCB.ThoiGianBay.Value.Minutes.ToString();

                    txtMins.Text = selected.GioDi.Value.Minutes.ToString();
                    txtHrs.Text = selected.GioDi.Value.Hours.ToString();
                    dp_ngaybay.SelectedDate = selected.NgayDi;

                    txtNormalSeats.Text = selected.SoGheThuong.ToString();
                    txtVIPSeats.Text = selected.SoGheVip.ToString();

                    FlightMngController.loadSBTGList(selected.MaCB, selected.NgayDi, ref lstSBTG, ref lstNote, ref lstStop);
                    listView_SBTG.ItemsSource = lstSBTG;

                    AddCombo.Visibility = Visibility.Hidden;
                    EditCombo.Visibility = Visibility.Visible;
                    SeatCombo.Visibility = Visibility.Visible;
                }
                else
                {// Them mới
                    lblScheTitle.Content = "THÊM MỚI LỊCH BAY";

                    cmbMaCB.SelectedIndex = 0;
                    txtSBDen.IsEnabled = false;
                    txtSBDi.IsEnabled = false;
                    txtSBDi.Text = lstCB.ElementAt(0).SBDi;
                    txtSBDen.Text = lstCB.ElementAt(0).SBDen;
                    txtDefaultHrs.Text = lstCB.ElementAt(0).ThoiGianBay.Value.Hours.ToString();
                    txtDefaultMins.Text = lstCB.ElementAt(0).ThoiGianBay.Value.Minutes.ToString();

                    AddCombo.Visibility = Visibility.Visible;
                    EditCombo.Visibility = Visibility.Hidden;
                    SeatCombo.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                cmbMaCB.IsEnabled = true;
                txtSBDi.IsEnabled = true;
                txtSBDen.IsEnabled = true;
                dp_ngaybay.IsEnabled = true;



                parentWind.loadLichBayData();
            }
        }

        // Add Combo ------------------------------------------------------------

        private void BtnAddShedule_Click(object sender, RoutedEventArgs e)
        {
            if (Checking() == false)
            {
                MessageBox.Show(error.ToString());
                error.Clear();
            }
            else
            {
                try
                {
                    LICHBAY newLB = new LICHBAY()
                    {
                        MaCB = cmbMaCB.SelectedIndex + 1,
                        GioDi = new TimeSpan(int.Parse(txtHrs.Text), int.Parse(txtMins.Text), 0),
                        NgayDi = dp_ngaybay.SelectedDate.Value,
                        SoGheThuong = 0,
                        SoGheVip = 0,
                        IsAvailable = true,
                    };

                    FlightMngController.addNewShedule(newLB);
                    try
                    {
                        FlightMngController.addListSBTG(lstSBTG, lstNote, lstStop, cmbMaCB.SelectedIndex + 1, dp_ngaybay.SelectedDate.Value);
                        this.Visibility = Visibility.Collapsed;
                    }
                    catch
                    {
                        MessageBox.Show("Sân bay trung gian bị trùng");
                    }
                }
                catch
                {
                    MessageBox.Show("Đã tồn tại lịch bay này rồi");
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            cmbMaCB.SelectedIndex = 0;
            dp_ngaybay.SelectedDate = null;
            txtHrs.Text = "";
            txtMins.Text = "";
            listView_SBTG.ItemsSource = null;
            lstNote.Clear();
            lstStop.Clear();
            lstSBTG.Clear();
        }

        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            int index = listView_SBTG.SelectedIndex;
            addNote noteWind = new addNote(lstNote[index], lstStop[index]);
            noteWind.ShowDialog();            

            if(GlobalItem.NoteContent != lstNote[index] || GlobalItem.ThoiGianDung != lstStop[index])
            {
                IsNoteUpdated = true;
            }

            lstNote[index] = GlobalItem.NoteContent;
            lstStop[index] = GlobalItem.ThoiGianDung;
            GlobalItem.NoteContent = null;
            GlobalItem.ThoiGianDung = 0;
        }

        private void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            int index = listView_SBTG.SelectedIndex;
            lstNote.RemoveAt(index);
            lstStop.RemoveAt(index);
            lstSBTG.RemoveAt(index);
            listView_SBTG.ItemsSource = null;
            listView_SBTG.ItemsSource = lstSBTG;
        }
        
        private bool Checking()
        {
            bool isError = false;
            CHUYENBAY cb = (CHUYENBAY)cmbMaCB.SelectedItem;
            
            // Kiểm tra giờ bay
            if (!int.TryParse(txtHrs.Text, out h))
            {                
                error.Append("Giờ bay sai\n");
                isError = true;
            }

            // Kiểm tra phút bay
            if (!int.TryParse(txtMins.Text, out m))
            {
                error.Append("Phút bay sai\n");
                isError = true;
            }

            // Kiểm tra sân bay trung gian
            string pre = "";
            foreach(var i in lstSBTG)
            {
                if(i.MaSB == cb.SBDi || i.MaSB == cb.SBDen || i.MaSB == pre)
                {                    
                    error.Append("Sân bay trung gian không hợp lý\n");
                    isError = true;
                    break;
                }
                pre = i.MaSB;
            }

            if(dp_ngaybay.SelectedDate == null)
            {
                error.Append("Thông tin ngày bay có vấn đề\n");
                isError = true;
            }
            else if (DateTime.Compare(dp_ngaybay.SelectedDate.Value, DateTime.Today) < 0)
            {
                error.Append("Thông tin ngày bay có vấn đề\n");
                isError = true;
            }
            else
            {
                // do nothing
            }

            if(parentWind.IsSheduleEdit == true)
            {
                if (int.TryParse(txtNormalSeats.Text, out normal) == false || int.TryParse(txtVIPSeats.Text, out vip) == false)
                {
                    error.Append("Thông tin số ghế bị sai\n");
                    isError = true;
                }
            }

            if (isError) return false;
            else return true;
        }

        // Edit combo -----------------------------------------------------------
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (Checking() == false)
            {
                MessageBox.Show(error.ToString());
                error.Clear();
            }
            else
            {
                if (IsEditing())
                {
                   FlightMngController.updateShedule(selected.MaCB, selected.NgayDi, new TimeSpan(h, m, 0), normal, vip);
                }

                if(IsNoteUpdated)
                {
                    FlightMngController.updateSBTG(selected.MaCB, selected.NgayDi, lstSBTG, lstNote, lstStop);
                }

                GlobalItem.FlightOfSelectedShedule = -1;
                GlobalItem.DateOfSelectedShedule = new DateTime();
                this.Visibility = Visibility.Collapsed;
            }
        }

        private bool IsEditing()
        {
            if (h != selected.GioDi.Value.Hours || normal != selected.SoGheThuong ||
                m != selected.GioDi.Value.Minutes || vip != selected.SoGheVip)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
