using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1612018_1612175_PlaneTicketSelling.model;

namespace _1612018_1612175_PlaneTicketSelling.controller
{
    public class AirportController
    {
        public static List<SANBAY> loadAirportData()
        {
            return DataProvider.ins.DB.SANBAYs.Where(x=>x.IsAvailable == true).ToList();
        }

        // Nhóm search -------------------------------------------------------------
        public static void searchByID(ref List<SANBAY> lstSB, string masb)
        {
            lstSB = lstSB.Where(x => x.MaSB.Contains(masb) == true).ToList();
        }

        public static void searchByTen(ref List<SANBAY> lstSB, string tensb)
        {
            lstSB = lstSB.Where(x => x.TenSB.Contains(tensb) == true).ToList();
        }

        public static void searchByDiaDiem(ref List<SANBAY> lstSB, string diadiem)
        {
            lstSB = lstSB.Where(x => x.DiaDiem.Contains(diadiem) == true).ToList();
        }

        // Search chuyến bay
        public static List<CHUYENBAY> searchCBForMASB(string masb)
        {
            StringBuilder sb = new StringBuilder(masb);
            int l = sb.Length;
            while (l < 5)
            {
                l++;
                sb.Append(" ");
            }
            masb = sb.ToString();
            return DataProvider.ins.DB.CHUYENBAYs.Where(x =>x.IsAvailable == true && x.SBDen == masb || x.SBDi == masb).ToList();
        }

        // Add sân bay
        public static void addNewAirport(string masb, string tensb, string diadiem)
        {
            DataProvider.ins.DB.SANBAYs.Add(new SANBAY()
            {
                MaSB = masb,
                TenSB = tensb,
                DiaDiem = diadiem,
                IsAvailable = true,
            });
            DataProvider.ins.DB.SaveChanges();
        }

        // Kiểm tra mã sân bay
        public static bool validateAirport(string masb)
        {            
            if (DataProvider.ins.DB.SANBAYs.Where(x=>x.MaSB == masb).ToList().Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Sữa sân bay
        public static void updateAirport(string masb, string tensb, string diadiem)
        {
            SANBAY _new = DataProvider.ins.DB.SANBAYs.Where(x => x.MaSB == masb).SingleOrDefault();
            _new.TenSB = tensb;
            _new.DiaDiem = diadiem;
            DataProvider.ins.DB.SaveChanges();
        }

        // Xóa sân bay
        public static void deleteAirport(SANBAY airport)
        {
            SANBAY t = DataProvider.ins.DB.SANBAYs.Where(x => x.MaSB == airport.MaSB).SingleOrDefault();
            t.IsAvailable = false;
            DataProvider.ins.DB.SaveChanges();
        }
    }
}
