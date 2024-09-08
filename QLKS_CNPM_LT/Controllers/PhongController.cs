using QLKS_CNPM_LT.Models;
using QLKS_CNPM_LT.Models.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace QLKS_CNPM_LT.Controllers
{
    public class PhongController : Controller
    {
        // GET: Phong
        private qlks_CNPMEntities db = new qlks_CNPMEntities();
        public ActionResult DSPhong()
        {
            var func_phong = new Phong_Func();
            return View(func_phong.toanBoPhong());
        }


        public ActionResult CTlOAIPhong()
        {

            ViewBag.Success = 0;
            string id = (string)RouteData.Values["id"];
            LOAIPHONG lp = db.LOAIPHONGs.Find(id);
            if (lp == null)
            {
                return View(new List<PHONG>());
            }
            ViewBag.Success = 1;
            ViewBag.TenLoai = lp.TenLoai;
            ViewBag.DuongDanAnh = lp.DuongDanAnh;
            ViewBag.GhiChu = lp.GhiChu;
            var listPhong = db.PHONGs.Where(m => m.MaLoai == id).ToList();
            return View(listPhong);
        }

        public ActionResult DatPhong()
        {
            if (Session["TaiKhoan"] == null)
            {
                Session["TrangTruoc"] = Request.RawUrl;
                return RedirectToAction("DangNhap", "CaNhan");
            }

            string MaPhong = (string)RouteData.Values["id"];
            var phong = db.PHONGs.Where(m => m.MA_PHONG == MaPhong).First();
            var loaiPhong = db.LOAIPHONGs.Where(m => m.MaLoai == phong.MaLoai).First();
            ViewBag.DuongDanAnh = loaiPhong.DuongDanAnh;
            ViewBag.TenLoai = loaiPhong.TenLoai;
            return View(phong);
        }


    }
}