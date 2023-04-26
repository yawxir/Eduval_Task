using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookStoreOpsController : Controller
    {
        public IActionResult Index()
        {
            var objViewModel = new BooksViewModel();
            var objDAL = new DBOps();      
            objViewModel.Racks = objDAL.GetAllRacks();
            objViewModel.Shelfs = objDAL.GetAllShelves();
            var lstData = objDAL.GetDataFromDB(new BooksDataModel());
            objViewModel.BooksList = lstData;
            return View("~/Views/BookStoreOps/BookStoreOps.cshtml", objViewModel);
        }




        [HttpPost]
        public IActionResult ProcessABook([FromBody]  BooksDataModel obkBookModel)
        {

            var objDAL = new DBOps();
            if (obkBookModel.OperationType=="I")
            {
                objDAL.InsertNewBook(obkBookModel);
            }
            else  
            {              
                objDAL.UpdateDelete(obkBookModel);
            }
            

            var lstData = objDAL.GetDataFromDB(new BooksDataModel());
            return Json(lstData);
        }



        [HttpPost]
        public IActionResult SearchData([FromBody] BooksDataModel obkBookModel)
        { 

            var objDAL = new DBOps();
            var lstData = objDAL.GetDataFromDB(obkBookModel);
            return Json(lstData);
        }
    }
}
