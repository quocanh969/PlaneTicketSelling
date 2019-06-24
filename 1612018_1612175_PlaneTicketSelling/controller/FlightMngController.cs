using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using _1612018_1612175_PlaneTicketSelling.model;

namespace _1612018_1612175_PlaneTicketSelling.controller
{
    public class FlightMngController
    {
        public static int getLightIDAutomatic()
        {
            return DataProvider.ins.DB.CHUYENBAYs.Count();
        }

        public static IEnumerable<Object> LoadSheduleData()
        {
            var Flights = (from LB in DataProvider.ins.DB.LICHBAYs
                           join CB in DataProvider.ins.DB.CHUYENBAYs
                           on LB.MaCB equals CB.MaCB
                           where LB.IsAvailable == true && CB.IsAvailable == true
                           select new
                           {
                               LB.MaCB,
                               CB.SBDi,
                               CB.SBDen,
                               LB.NgayDi,
                               LB.GioDi,
                               CB.ThoiGianBay,
                               LB.SoGheThuong,
                               LB.SoGheVip,
                           }).ToList();
            return Flights;
        }

        public static List<CHUYENBAY> LoadFlightData()
        {
            List<CHUYENBAY> Flights = DataProvider.ins.DB.CHUYENBAYs.Where(x=>x.IsAvailable == true).ToList();
            return Flights;
        }

        public static List<SANBAY> LoadAirportData()
        {
            return DataProvider.ins.DB.SANBAYs.Where(x=>x.IsAvailable == true).ToList();
        }
        
        // Nhóm get phần tử
        public static LICHBAY getLBByID(int id, DateTime ngaydi)
        {
            return DataProvider.ins.DB.LICHBAYs.Where(x => x.MaCB == id && x.NgayDi == ngaydi).SingleOrDefault();
        }

        public static CHUYENBAY getCBByID(int id)
        {
            return DataProvider.ins.DB.CHUYENBAYs.Where(x => x.MaCB == id).SingleOrDefault();
        }

        public static void loadSBTGList(int id, DateTime ngaydi, ref List<SANBAY> lstSB, ref List<string> lstNote, ref List<int> lstStop)
        {
            lstSB = (from SBTG in DataProvider.ins.DB.SBTRUNGGIANs
                     join SB in DataProvider.ins.DB.SANBAYs
                     on SBTG.MaSB equals SB.MaSB
                     where SBTG.MaCB == id && SBTG.NgayDi == ngaydi && SB.IsAvailable == true
                     select new
                     {
                         MaSB = SB.MaSB,
                         TenSB = SB.TenSB,
                         DiaDiem = SB.DiaDiem,
                     }).ToList().Select(x=>new SANBAY
                     {
                         MaSB = x.MaSB,
                         TenSB = x.TenSB,
                         DiaDiem = x.DiaDiem,
                     }).ToList();
            
            lstNote = DataProvider.ins.DB.SBTRUNGGIANs.Where(x => x.MaCB == id && x.NgayDi == ngaydi).Select(x => x.GhiChu).ToList();
            lstStop = DataProvider.ins.DB.SBTRUNGGIANs.Where(x => x.MaCB == id && x.NgayDi == ngaydi)
                .Select(x => x.ThoiGianDung.Value.Hours*60 + x.ThoiGianDung.Value.Minutes).ToList();
        }

        // Nhóm kiểm tra phần tử
        public static bool shduleChecking(int macb, DateTime ngaybay)
        {
            List<LICHBAY> lstLB = DataProvider.ins.DB.LICHBAYs.Where(x => x.MaCB == macb && x.NgayDi == ngaybay && x.IsAvailable == true).ToList();
            if(lstLB.Count() > 0)
            {
                return false;            
            }
            else
            {
                return true;
            }
        }

        // Nhóm lịch bay
        public static void SearchForMACB(ref List<Object> lstItem, int _macb)
        {
            lstItem = lstItem.Where(x => (int)x.GetType().GetProperty("MaCB").GetValue(x) == _macb).ToList();         
        }

        public static void SearchForSBDI(ref List<Object> lstItem, string _sbdi)
        {
            lstItem = lstItem.Where(x => (string)x.GetType().GetProperty("SBDi").GetValue(x) == _sbdi).ToList();
        }

        public static void SearchForSBDEN(ref List<Object> lstItem, string _sbden)
        {
            lstItem = lstItem.Where(x => (string)x.GetType().GetProperty("SBDen").GetValue(x) == _sbden).ToList();
        }

        public static void SearchForNGAYDI(ref List<Object> lstItem, DateTime _ngaydi)
        {
            lstItem = lstItem.Where(x => (DateTime)x.GetType().GetProperty("NgayDi").GetValue(x) == _ngaydi).ToList();
        }

        public static void SearchForGIOBAY(ref List<Object> lstItem, TimeSpan _giobay)
        {
            lstItem = lstItem.Where(x => (TimeSpan)x.GetType().GetProperty("GioDi").GetValue(x) == _giobay).ToList();
        }

        // Nhóm chuyến bay
        public static void SearchFlightForMACB(ref List<CHUYENBAY> lstCB, int macb)
        {
            lstCB = lstCB.Where(x => x.MaCB == macb).ToList();
        }

        public static void SearchFlightForSBDI(ref List<CHUYENBAY> lstCB, string _sbdi)
        {
            lstCB = lstCB.Where(x => x.SBDi == _sbdi).ToList();
        }

        public static void SearchFlightForSBDEN(ref List<CHUYENBAY> lstCB, string _sbden)
        {
            lstCB = lstCB.Where(x => x.SBDen == _sbden).ToList();
        }
        // Nhóm thêm phân tử mới
        public static void addNewFlight(CHUYENBAY newFlight)
        {
            DataProvider.ins.DB.CHUYENBAYs.Add(newFlight);
            DataProvider.ins.DB.SaveChanges();
        }

        public static void addNewShedule(LICHBAY newShedule)
        {
            DataProvider.ins.DB.LICHBAYs.Add(newShedule);
            DataProvider.ins.DB.SaveChanges();
        }
                
        public static void addListSBTG(List<SANBAY> lstSBTG, List<string> lstContent, List<int> lstStop, int maCB, DateTime ngaydi)
        {
            List<SBTRUNGGIAN> lstTG = new List<SBTRUNGGIAN>();
            for(int i = 0; i<lstSBTG.Count();i++)
            {
                lstTG.Add(new SBTRUNGGIAN()
                {
                    MaCB = maCB,
                    NgayDi = ngaydi,
                    MaSB = lstSBTG.ElementAt(i).MaSB,
                    ThoiGianDung = new TimeSpan(lstStop.ElementAt(i)/60,lstStop.ElementAt(i)%60,0),
                    GhiChu = lstContent[i],
                });
            }
            DataProvider.ins.DB.SBTRUNGGIANs.AddRange(lstTG);
            DataProvider.ins.DB.SaveChanges();
        }

        // Nhóm hàm xóa phần tử
        public static void deleteFlight(CHUYENBAY flight)
        {
            CHUYENBAY t = DataProvider.ins.DB.CHUYENBAYs.Where(x=>x.MaCB == flight.MaCB).SingleOrDefault();
            t.IsAvailable = false;
            DataProvider.ins.DB.SaveChanges();
        }

        public static void deleteSheduleFor(int macb, DateTime ngaydi)
        {
            // Xóa lịch bay
            LICHBAY deleted = DataProvider.ins.DB.LICHBAYs.Where(x => x.MaCB == macb && x.NgayDi == ngaydi).SingleOrDefault();
            deleted.IsAvailable = false;
            DataProvider.ins.DB.SaveChanges();

            // Xóa SB trung gian
            List<SBTRUNGGIAN> lstSBTG = DataProvider.ins.DB.SBTRUNGGIANs.Where(x => x.MaCB == macb && x.NgayDi == ngaydi).ToList();
            DataProvider.ins.DB.SBTRUNGGIANs.RemoveRange(lstSBTG);
            DataProvider.ins.DB.SaveChanges();
        }

        // Nhóm sữa thông tin
        public static void updateFlight(int macb, TimeSpan thoigianbay, string sbdi, string sbden, int ghethuong, int ghevip)
        {
            CHUYENBAY _new = DataProvider.ins.DB.CHUYENBAYs.Where(x => x.MaCB == macb).SingleOrDefault();
            _new.ThoiGianBay = thoigianbay;
            _new.SBDi = sbdi;
            _new.SBDen = sbden;
            _new.GheThuong = ghethuong;
            _new.GheVip = ghevip;
            DataProvider.ins.DB.SaveChanges();
        }

        public static void updateShedule(int macb, DateTime ngayBayGoc, TimeSpan giodi, int ghethuong, int ghevip)
        {
            LICHBAY _new = DataProvider.ins.DB.LICHBAYs.Where(x => x.MaCB == macb && x.NgayDi == ngayBayGoc).SingleOrDefault();            
            _new.GioDi = giodi;
            _new.SoGheThuong = ghethuong;
            _new.SoGheVip = ghevip;
            DataProvider.ins.DB.SaveChanges();
        }

        public static void updateSBTG(int macb, DateTime ngaydi, List<SANBAY> lstSBTG, List<string> lstContent, List<int> lstStop)
        {
            // Xóa sân bay trung giản cũ
            List<SBTRUNGGIAN> deleteList = DataProvider.ins.DB.SBTRUNGGIANs.Where(x => x.MaCB == macb && x.NgayDi == ngaydi).ToList();
            DataProvider.ins.DB.SBTRUNGGIANs.RemoveRange(deleteList);
            DataProvider.ins.DB.SaveChanges();

            // Cập nhật cái mới
            List<SBTRUNGGIAN> lstTG = new List<SBTRUNGGIAN>();
            for (int i = 0; i < lstSBTG.Count(); i++)
            {
                lstTG.Add(new SBTRUNGGIAN()
                {
                    MaCB = macb,
                    NgayDi = ngaydi,
                    MaSB = lstSBTG.ElementAt(i).MaSB,
                    ThoiGianDung = new TimeSpan(lstStop.ElementAt(i) / 60, lstStop.ElementAt(i) % 60, 0),
                    GhiChu = lstContent[i],
                });
            }
            DataProvider.ins.DB.SBTRUNGGIANs.AddRange(lstTG);
            DataProvider.ins.DB.SaveChanges();
        }
    }
}
