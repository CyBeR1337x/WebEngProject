using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;
using MusicLibraryWebApp.Models;

namespace MusicLibraryWebApp.Controllers {
	public class SongController : Controller {
		// GET: Song

		DBAccess obj = new DBAccess();
		public ActionResult ViewSongs(string sid, int ano) {
			string q = $"SELECT sno, stitle, filepath FROM Song WHERE sid = '{sid}' AND ano = '{ano}';";
			obj.ConOpen();
			SqlDataReader sdr = obj.GetData(q);
			List<Song> list = new List<Song>();
			while (sdr.Read())
				list.Add(new Song { Sno = int.Parse(sdr["sno"].ToString()), Stitle = sdr["stitle"].ToString(), FilePath = sdr["filepath"].ToString() });
			sdr.Close();
			obj.ConClose();
			return View(list);
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
				string path = Server.MapPath(Path.Combine("~/Songs", $"{s.Stitle}{ext}"));
				s.Mp3File.SaveAs(path);
				s.FilePath = $"/Songs/{s.Stitle}{ext}";
				obj.ConOpen();
				string q = $"INSERT INTO Song VALUES('{Session["sid"]}', " +
					$"'{s.Ano}', '{s.Sno}', '{s.Stitle}', '{s.FilePath}')";
				obj.IUD(q);
				obj.ConClose();
			}

			return View();
		}


		[HttpGet]
		public FileResult DownloadSong(string filePath) {
			string path = Server.MapPath("~") + filePath;
			byte[] bytes = System.IO.File.ReadAllBytes(path);
			return File(bytes, "application/octet-stream", filePath);
		}

	}
}