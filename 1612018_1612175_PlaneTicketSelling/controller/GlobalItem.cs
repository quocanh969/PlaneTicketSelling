using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1612018_1612175_PlaneTicketSelling.model;

namespace _1612018_1612175_PlaneTicketSelling.controller
{
    public class GlobalItem
    {
        public static USER currenUser;
                

        public static string NoteContent;
        public static int ThoiGianDung;

        public static CHUYENBAY selectedFlight;
        public static int FlightOfSelectedShedule;
        public static DateTime DateOfSelectedShedule;

        public static SANBAY selectedAirport;

        public static HOADON selectedReceipt;
        public static HOADON selectedReservation;


        // Quy định
        public static int DungToiThieu = Convert.ToInt32(ConfigurationManager.AppSettings["minStopTime"]);
        public static int DungToiDa = Convert.ToInt32(ConfigurationManager.AppSettings["maxStopTime"]);
        public static int BayToiThieu = BayToiThieu = Convert.ToInt32(ConfigurationManager.AppSettings["minFlyTime"]);
        public static int SBTrungGianToiDa = Convert.ToInt32(ConfigurationManager.AppSettings["maxAPs"]);

        public static int veThuong = Convert.ToInt32(ConfigurationManager.AppSettings["normalSeatPrice"]);
        public static int veVIP = Convert.ToInt32(ConfigurationManager.AppSettings["vipSeatPrice"]);

        public static int ngayHuyVe = Convert.ToInt32(ConfigurationManager.AppSettings["expDate"]);
    }
}
