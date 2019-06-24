using _1612018_1612175_PlaneTicketSelling.model;
using _1612018_1612175_PlaneTicketSelling.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612018_1612175_PlaneTicketSelling.controller
{
    class reservationController
    {
        public static IEnumerable<Object> loadDataPhieuDat()
        {
            var Reservation = (from HD in DataProvider.ins.DB.HOADONs
                               join CB in DataProvider.ins.DB.CHUYENBAYs on HD.MaCB equals CB.MaCB
                               join LB in DataProvider.ins.DB.LICHBAYs on new { HD.MaCB, HD.NgayDi } equals new { LB.MaCB, LB.NgayDi }
                               where HD.IsDatCho == true && HD.IsAvailable == true && LB.IsAvailable == true
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
            return Reservation;
        }

        public static void translation(int macb, string makh, DateTime ngaybay, int soVeThuong, int soVeVip, int tongTien, DateTime ngayMua)
        {
            HOADON n = DataProvider.ins.DB.HOADONs.Where(x => x.MaCB == macb && x.MaKH == makh && x.NgayDi == ngaybay).SingleOrDefault();
            n.SoVeThuong = soVeThuong;
            n.SoVeVip = soVeVip;
            n.TongTien = tongTien;
            n.NgayMua = ngayMua;
            n.IsDatCho = false;
            DataProvider.ins.DB.SaveChanges();
        }

        // Kiểm tra hóa đơn
        public static void removeReservation()
        {
            DateTime tomorow = DateTime.Today.AddDays(1);
            List<HOADON> lstPD = DataProvider.ins.DB.HOADONs.Where(x => x.IsDatCho == true && x.NgayDi == tomorow).ToList();
            DataProvider.ins.DB.HOADONs.RemoveRange(lstPD);
            DataProvider.ins.DB.SaveChanges();
        }

        public static void kiemTraHoaDon()
        {
            HOADON[] lstHD = DataProvider.ins.DB.HOADONs.Where(x => x.IsAvailable == true && DateTime.Compare(x.NgayDi, DateTime.Today) < 0).ToArray();
            for(int i = 0;i<lstHD.Count();i++)
            {
                receiptMngController.deleteReceipt(lstHD[i]);
            }            
        }

        public static void updateFlight()
        {
            LICHBAY[] lstHD = DataProvider.ins.DB.LICHBAYs.Where(x => x.IsAvailable == true && DateTime.Compare(x.NgayDi, DateTime.Today) < 0).ToArray();
            
            for(int i = 0;i< lstHD.Count();i++)
            {
                FlightMngController.deleteSheduleFor(lstHD[i].MaCB, lstHD[i].NgayDi);
            }
        }
    }
}
