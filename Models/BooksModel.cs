using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{

    public class BooksViewModel
    {

        public List<SelectListItem> Racks { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Shelfs { get; set; } = new List<SelectListItem>();
        public List<BooksDataModel> BooksList { get; set; }= new List<BooksDataModel>();
    }


    public class BooksDataModel
    {
        public string BookId            { get; set; }
        public string BookTitle          { get; set; }
        public string AuthorName            { get; set; }
        public string Price              { get; set; }
        public string ShelfID                { get; set; }
        public string RackID                { get; set; }
        public string DeleteFlag                { get; set; }
        public string IsAvialalbe           { get; set; }
        public string OperationType                 { get; set; }
    }
}
