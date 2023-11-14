using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MusicLibraryWebApp.Models {
    public class Album {
        public string sid { get; set; }      
        public int ano { get; set; }
        public string title { get; set; }

        public DateTime date { get; set; }

        public string coverPath { get; set; } 
        
        public HttpPostedFileBase coverFile { get; set; }
    }
}