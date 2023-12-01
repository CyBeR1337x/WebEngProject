using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MusicLibraryWebApp.Models {
    public class Song {
        public string Sid { get; set;}
        public int Ano { get; set;}
        public int Sno { get; set; }
        public string Stitle { get; set; }  
        public string FilePath { get; set; }
        public HttpPostedFileBase Mp3File { get; set; }
    }
}