using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using MusicLibraryWebApp.Models;

namespace MusicLibraryWebApp.Controllers {
    public class SingerController : Controller {

        DBAccess db = new DBAccess();
        // GET: Singer
        public ActionResult SignUpSinger() {
            return View();
        }

        [HttpPost]
        public ActionResult SignUpSinger(Singer s) {
            s.Sid = s.Email.Split('@')[0];
            db.ConOpen();
            string query = $"INSERT INTO Singer VALUES('{s.Sid}', '{s.Sname}', '{s.Country}', '{s.Gender}', '{s.Email}', '{s.Password}')";
            db.IUD(query);
            db.ConClose();  
            return View(s);
        }

        [HttpGet]
        public ActionResult login() {
            return View();
        }

        [HttpPost]
        public ActionResult login(Singer s) {
            db.ConOpen();
            db.cmd = new SqlCommand($"SELECT sid, sname FROM Singer WHERE sid = '{s.Sid}' AND password = '{s.Password}'", db.conn);
            SqlDataReader sdr  = db.cmd.ExecuteReader();
            sdr.Read();
            if (sdr.HasRows) {
                Session["Sid"] = sdr["sid"].ToString();
                Session["Sname"] = sdr["sname"].ToString();
            }
            else {
                Response.Write("<script>alert('Invalid User');</script>");
            }
            sdr.Close();
            db.ConClose();
            
            return RedirectToAction("DashBoard");
        }

        [HttpGet]
        public ActionResult DashBoard() {
            if (Session["sid"] != null) 
                return View();
            
            return RedirectToAction("login");
        }

        [HttpGet]
        public ActionResult logout() {
            Session.RemoveAll();
            return RedirectToAction("login");
        }
    }
}