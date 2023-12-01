using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MusicLibraryWebApp.Models;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace MusicLibraryWebApp.Controllers {
    public class AlbumController : Controller {
        DBAccess obj = new DBAccess();
        [HttpGet]
        public ActionResult AddAlbum() {
            if (Session["sid"] != null)
                return View();
            return RedirectToAction("login", "Singer");
        }
        [HttpPost]
        public ActionResult AddAlbum(Album album) {
            string newFileName = $"{Session["Sid"]}_{album.ano}.jpg";
            //string fn = album.coverFile.FileName;
            string path = Server.MapPath(Path.Combine("~/Images/", newFileName));
            album.coverPath = "/Images/" + newFileName;
            album.coverFile.SaveAs(path);

            string q = $"INSERT INTO ALBUM VALUES('{Session["Sid"]}', '{album.ano}', '{album.title}', '{album.date}', '{album.coverPath}')";
            obj.ConOpen();
            obj.IUD(q);
            obj.ConClose();

            return View(album);
        }

        [HttpGet]
        public ActionResult ShowAlbums(string sid) {
            string q = $"SELECT ano, coverpath, title FROM Album WHERE sid = '{sid}'";
            obj.ConOpen();
            SqlDataReader sdr = obj.GetData(q);
            List<Album> albumlist = new List<Album>();
            while (sdr.Read())
                albumlist.Add(new Album { ano = int.Parse(sdr["ano"].ToString()), coverPath = sdr["coverpath"].ToString(), title = sdr["title"].ToString() });

            sdr.Close();
            obj.ConClose();

            return View(albumlist);
        }

    }
}