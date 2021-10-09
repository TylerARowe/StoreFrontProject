using Storefront.DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;//This was added for the .Include() method

//Added after installing NuGet for PagedList.MVC
using PagedList;

namespace StoreFront.UI.MVC.Controllers
{
    public class FiltersController : Controller
    {
        private StoreFrontDB db = new StoreFrontDB();

        //Jquery datatables client side ezmple, create a view with Lsit scaffolding for Books
        public ActionResult Clientside()
        {
            var albums = db.Albums.Include(a => a.AlbumTitle).Include(a => a.AlbumStatus).Include(a => a.Genre).Include(a => a.RecordLabel);
            return View(albums.ToList());
        }

        public ActionResult ArtistsQS(string searchFilter)
        {
            //If its the firs time viewing the page or the user just hits search without entering anything, return all artists.
            if (String.IsNullOrEmpty(searchFilter))
            {
                var artists = db.Artists;
                return View(artists.ToList());
            }
            else
            {
                //this is written in linq method syntax
                var searchResults = db.Artists.Where(x => x.FirstName.ToLower().Contains(searchFilter.ToLower()) || x.LastName.Contains(searchFilter) ||
                x.Country.Contains(searchFilter) || x.State.Contains(searchFilter) || x.ZipCode.Contains(searchFilter));

                return View(searchResults.ToList());

            }
           
        }

        //Server side paging example with PageList.MVC (NuGet)
        public ActionResult AlbumsMVCPaging(string searchString, int page = 1)//default to first page if nothing is passed to the param
        {
            //Variable for how many records to show for page size, this variable can be changed based on how much I want to show.
            int pageSize = 5;


            //for PagedList to function, we MUST sort our collection
            var albums = db.Albums.OrderBy(x => x.AlbumTitle).ToList();

            #region Search

            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(x => x.AlbumTitle.Contains(searchString)).ToList();
            }

            ViewBag.SearchString = searchString;
            #endregion

            //params to return to the view: collection as a paged list, page, pageSize
            return View(albums.ToPagedList(page, pageSize));
        }
        
    }
}