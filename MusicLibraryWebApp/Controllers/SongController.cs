using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicLibraryWebApp.Models;

namespace MusicLibraryWebApp.Controllers {
    public class SongController : Controller {
        // GET: Song

        DBAccess obj = new DBAccess();
        public ActionResult ViewSongs() {
            return View();
        }
        [HttpGet]
        public ActionResult AddSong() {
            Song s = new Song() { Ano = int.Parse(Request.QueryString["ano"]) };
            return View(s);
        }

        [HttpPost]
        public ActionResult AddSong(Song s) {
            string filename = s.Mp3File.FileName;
            string ext = Path.GetExtension(filename);
            List<string> extensions = new List<string> { ".mp3", ".wav", ".m4a" };
            if (extensions.Contains(ext)) {
                string path = Server.MapPath(Path.Combine("~/Songs", filename));
                s.Mp3File.SaveAs(path);
                s.FilePath = $"/Songs/{filename}";
                obj.ConOpen();
                string q = $"INSERT INTO Song VALUES('{Session["sid"]}', " +
                    $"'{s.Ano}', '{s.Sno}', '{s.Stitle}', '{s.FilePath}')";
                obj.IUD(q);
                obj.ConClose();
            }

            return View();
        }












    }
}