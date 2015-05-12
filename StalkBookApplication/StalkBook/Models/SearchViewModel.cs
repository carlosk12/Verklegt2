using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Models
{
    public class SearchViewModel
    {
        public string userId { get; set; }
        public List<string> stalking { get; set; }
        public List<Profile> searchResult { get; set; }
        public string  searchString { get; set; }
    }
}