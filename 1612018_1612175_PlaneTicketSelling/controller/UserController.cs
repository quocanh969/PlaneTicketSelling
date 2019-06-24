using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1612018_1612175_PlaneTicketSelling.model;

namespace _1612018_1612175_PlaneTicketSelling.controller
{
    public class UserController
    {
        // Hàm kiểm tra tài khoản để đăng nhập
        public static int UserValidate(string username, string password)
        {
            USER accUser = DataProvider.ins.DB.USERs.Where(x => x.Username == username).SingleOrDefault();
            if(accUser == null)
            {// Không tồn tại tài khoản
                return 0;
            }
            else if(accUser.Pass == password)
            {// Đúng
                GlobalItem.currenUser = accUser;
                return 1;
            }
            else 
            {// Sai mật khẩu;
                return -1;
            }
        }

        // Nhóm hàm kiểm tra tài khoản
        public static bool userValidate(string cmnd)
        {
            if(DataProvider.ins.DB.USERs.Where(x=>x.CMND == cmnd).ToList().Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool guestValidate(string id)
        {
            if (DataProvider.ins.DB.KHACHs.Where(x => x.ID == id).ToList().Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Nhóm hàm load dữ liệu
        public static List<USER> loadDataStaff()
        {
            return DataProvider.ins.DB.USERs.Where(x=>x.IsAvailable == true).ToList();
        }

        public static List<KHACH> loadDataGuest()
        {
            return DataProvider.ins.DB.KHACHs.Where(x=>x.IsAvailable == true).OrderBy(x=>x.HoTen).ToList();
        }

        // Nhóm hàm search ----------------------------
        public static void searchStaffForID(string id, ref List<USER> lstUser)
        {
            lstUser = lstUser.Where(x => x.CMND.Contains(id) == true).ToList();
        }

        public static void searchStaffForHoTen(string hoten, ref List<USER> lstUser)
        {
            lstUser = lstUser.Where(x => x.HoTen.Contains(hoten) == true).ToList();
        }

        public static void searchStaffForSDT(string sdt, ref List<USER> lstUser)
        {
            lstUser = lstUser.Where(x => x.SDT.Contains(sdt) == true).ToList();
        }

        public static void searchGuestForID(string id, ref List<KHACH> lstGuest)
        {
            lstGuest = lstGuest.Where(x => x.ID.Contains(id) == true).ToList();
        }

        public static void searchGuestForHoTen(string hoten, ref List<KHACH> lstGuest)
        {
            lstGuest = lstGuest.Where(x => x.HoTen.Contains(hoten) == true).ToList();
        }

        public static void searchGuestForSDT(string sdt, ref List<KHACH> lstGuest)
        {
            lstGuest = lstGuest.Where(x => x.SDT.Contains(sdt) == true).ToList();
        }

        // Nhóm hàm staff ----------------------------
        public static void deleteStaff(USER staff)
        {
            USER t = DataProvider.ins.DB.USERs.Where(x => x.CMND == staff.CMND).SingleOrDefault();
            t.IsAvailable = false;
            DataProvider.ins.DB.SaveChanges();
        }

        public static void addStaff(USER staff)
        {
            DataProvider.ins.DB.USERs.Add(staff);
            DataProvider.ins.DB.SaveChanges();
        }

        public static void updateStaff(string cmnd, string user, string hoten, string diachi, string sdt, string email, bool role)
        {
            USER _new = DataProvider.ins.DB.USERs.Where(x => x.CMND == cmnd).SingleOrDefault();
            _new.Username = user;
            _new.HoTen = hoten;
            _new.DiaChi = diachi;
            _new.SDT = sdt;
            _new.Email = email;
            _new.IsAdmin = role;
            DataProvider.ins.DB.SaveChanges();
        }

        // Nhóm hàm guest ----------------------------
        public static void addGuest(KHACH guest)
        {
            DataProvider.ins.DB.KHACHs.Add(guest);
            DataProvider.ins.DB.SaveChanges();
        }

        public static void deleteGuest(KHACH guest)
        {
            KHACH t = DataProvider.ins.DB.KHACHs.Where(x=>x.ID == guest.ID).SingleOrDefault();
            t.IsAvailable = false;
            DataProvider.ins.DB.SaveChanges();
        }

        public static void updateGuest(string id, string hoten, string diachi, string sdt, string email)
        {
            KHACH _new = DataProvider.ins.DB.KHACHs.Where(x => x.ID == id).SingleOrDefault();
            _new.HoTen = hoten;
            _new.DiaChi = diachi;
            _new.SDT = sdt;
            _new.Email = email;
            DataProvider.ins.DB.SaveChanges();
        }
    }
}
