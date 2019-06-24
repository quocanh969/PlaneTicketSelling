using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1612018_1612175_PlaneTicketSelling.model;

namespace _1612018_1612175_PlaneTicketSelling.controller
{
    class receiptMngController
    {
        public static DataTable LoadData()
        {
            List<HOADON> Receipts = DataProvider.ins.DB.HOADONs.ToList();

            var dt = new DataTable();
            dt.Columns.Add("MaKH", typeof(string));
            dt.Columns.Add("MaCB", typeof(int));
            dt.Columns.Add("NgayDi", typeof(DateTime));
            dt.Columns.Add("NgayMua", typeof(DateTime));
            dt.Columns.Add("SoVeThuong", typeof(int));
            dt.Columns.Add("SoVeVip", typeof(int));
            dt.Columns.Add("Tong tien", typeof(double));

            foreach(var item in Receipts)
            {
                var row = dt.NewRow();
                row["MaKH"] = item.MaKH;
                row["MaCB"] = item.MaCB;
                row["NgayDi"] = item.NgayDi;
                row["NgayMua"] = item.NgayMua;
                row["SoVeThuong"] = item.SoVeThuong;
                row["SoVeVip"] = item.SoVeVip;
                row["Tong tien"] = item.TongTien;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static DataTable LoadDataOnMonth(int month, int year)
        {
            List<HOADON> Receipts = new List<HOADON>();
            if (month != 0)
            {
                Receipts = DataProvider.ins.DB.HOADONs
                                    .Where(x => x.NgayMua.Value.Year == year && x.NgayMua.Value.Month == month).ToList();
            }
            else
            {
                Receipts = DataProvider.ins.DB.HOADONs
                                    .Where(x => x.NgayMua.Value.Year == year).ToList();
            }
            var dt = new DataTable();
            dt.Columns.Add("MaKH", typeof(string));
            dt.Columns.Add("MaCB", typeof(int));
            dt.Columns.Add("NgayDi", typeof(DateTime));
            dt.Columns.Add("NgayMua", typeof(DateTime));
            dt.Columns.Add("SoVeThuong", typeof(int));
            dt.Columns.Add("SoVeVip", typeof(int));
            dt.Columns.Add("Tong tien", typeof(double));

            foreach (var item in Receipts)
            {
                var row = dt.NewRow();
                row["MaKH"] = item.MaKH;
                row["MaCB"] = item.MaCB;
                row["NgayDi"] = item.NgayDi;
                row["NgayMua"] = item.NgayMua;
                row["SoVeThuong"] = item.SoVeThuong;
                row["SoVeVip"] = item.SoVeVip;
                row["Tong tien"] = item.TongTien;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static DataTable LoadDataOnDate(DateTime date)
        {
            List<HOADON> Receipts = DataProvider.ins.DB.HOADONs
                                    .Where(x => x.NgayMua.Value == date).ToList();
            
            var dt = new DataTable();
            dt.Columns.Add("MaKH", typeof(string));
            dt.Columns.Add("MaCB", typeof(int));
            dt.Columns.Add("NgayDi", typeof(DateTime));
            dt.Columns.Add("NgayMua", typeof(DateTime));
            dt.Columns.Add("SoVeThuong", typeof(int));
            dt.Columns.Add("SoVeVip", typeof(int));
            dt.Columns.Add("Tong tien", typeof(double));

            foreach (var item in Receipts)
            {
                var row = dt.NewRow();
                row["MaKH"] = item.MaKH;
                row["MaCB"] = item.MaCB;
                row["NgayDi"] = item.NgayDi;
                row["NgayMua"] = item.NgayMua;
                row["SoVeThuong"] = item.SoVeThuong;
                row["SoVeVip"] = item.SoVeVip;
                row["Tong tien"] = item.TongTien;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static List<HOADON> LoadReceiptData()
        {
            List<HOADON> receipts = DataProvider.ins.DB.HOADONs.ToList();
            return receipts;
        }

        // ---------------------------------------------------------------------

        public static IEnumerable<Object> loadDataHoaDon()
        {
            
            var Receipts = (from HD in DataProvider.ins.DB.HOADONs
                           join CB in DataProvider.ins.DB.CHUYENBAYs on HD.MaCB equals CB.MaCB
                           join LB in DataProvider.ins.DB.LICHBAYs on new { HD.MaCB, HD.NgayDi} equals new {LB.MaCB, LB.NgayDi }
                           where HD.IsDatCho == false && HD.IsAvailable == true && LB.IsAvailable == true
                           select new
                           {
                               MaCB = HD.MaCB,
                               MaKH = HD.MaKH,
                               NgayDi = HD.NgayDi,
                               SBDi = CB.SBDi,
                               SBDen = CB.SBDen,
                               SoVeThuong = HD.SoVeThuong,
                               SoVeVip = HD.SoVeVip,
                           }).ToList();
            return Receipts;
        }

        public static HOADON getHoaDon(int macb, string makh, DateTime ngaybay)
        {
            return DataProvider.ins.DB.HOADONs.Where(x => x.MaCB == macb && x.MaKH == makh && x.NgayDi == ngaybay).SingleOrDefault();
        }
        
        public static void addReceipt(HOADON receipt)
        {
            DataProvider.ins.DB.HOADONs.Add(receipt);
            DataProvider.ins.DB.SaveChanges();
        }

        public static List<LICHBAY> loadLichBayData()
        {
            return DataProvider.ins.DB.LICHBAYs.Where(x=>x.IsAvailable == true).ToList();
        }

        public static void updateReceipt(int macb, string makh, DateTime ngaybay, int soVeThuong, int soVeVip, int tongTien, DateTime ngayMua)
        {
            HOADON n = DataProvider.ins.DB.HOADONs.Where(x => x.MaCB == macb && x.MaKH == makh && x.NgayDi == ngaybay).SingleOrDefault();
            n.SoVeThuong = soVeThuong;
            n.SoVeVip = soVeVip;
            n.TongTien = tongTien;
            n.NgayMua = ngayMua;
            DataProvider.ins.DB.SaveChanges();
        }

        public static void deleteReceipt(HOADON receipt)
        {
            HOADON t = DataProvider.ins.DB.HOADONs.Where(x => x.MaCB == receipt.MaCB && x.NgayDi == receipt.NgayDi && x.MaKH == receipt.MaKH).SingleOrDefault();
            t.IsAvailable = false;
            DataProvider.ins.DB.SaveChanges();
        }

        public static void SearchForMAKH(ref List<Object> lstItem, string makh)
        {
            lstItem = lstItem.Where(x => (string)x.GetType().GetProperty("MaKH").GetValue(x) == makh).ToList();
        }

        public static void clearReceipt(HOADON receipt)
        {
            DataProvider.ins.DB.HOADONs.Remove(receipt);
            DataProvider.ins.DB.SaveChanges();
        }

        public static bool isDatVe(int macb, string makh, DateTime ngaybay)
        {
            if(DataProvider.ins.DB.HOADONs.Where(x=>x.MaCB == macb && x.MaKH == makh && x.NgayDi == ngaybay && x.IsDatCho == true).ToList().Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isConCho(int macb, DateTime ngaybay, int normal, int vip)
        {
            LICHBAY lb = DataProvider.ins.DB.LICHBAYs.Where(x => x.MaCB == macb && x.NgayDi == ngaybay).SingleOrDefault();
            CHUYENBAY cb = DataProvider.ins.DB.CHUYENBAYs.Where(x => x.MaCB == macb).SingleOrDefault();
            if(lb.SoGheThuong + normal > cb.GheThuong || lb.SoGheVip + vip > cb.GheVip)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void updateFlightForReceipt(int macb, DateTime ngaybay, int normal, int vip, int orinornam, int orivip)
        {
            LICHBAY lb = DataProvider.ins.DB.LICHBAYs.Where(x => x.MaCB == macb && x.NgayDi == ngaybay).SingleOrDefault();
            lb.SoGheThuong += (normal - orinornam);
            lb.SoGheVip += (vip - orivip);
            DataProvider.ins.DB.SaveChanges();
        }
    }
}
