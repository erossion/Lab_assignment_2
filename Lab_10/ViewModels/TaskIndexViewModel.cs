using Lab_10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Lab_10.ViewModels
{
    public class TaskIndexViewModel
    {
        public IPagedList<Tasks> Tasks { get; set; }
        public string Search { get; set; }
        public IEnumerable<TeamWithCount> CatsWithCount { get; set; }
        public string Team { get; set; }
        public string Status { get; set; }
        public string SortBy { get; set; }
        public Dictionary<string, string> Sorts { get; set; }

        public IEnumerable<SelectListItem> CatFilterItems
        {
            get
            {
                var allCats = CatsWithCount.Select(cc => new SelectListItem
                {
                    Value = cc.TeamName,
                    Text = cc.CatNameWithCount
                });
                return allCats;
            }
        }
    }
    public class TeamWithCount
    {
        public int TaskCount { get; set; }
        public string TeamName { get; set; }
        public string CatNameWithCount
        {
            get
            {
                return TeamName + " (" + TaskCount.ToString() + ")";
            }
        }
    }
}