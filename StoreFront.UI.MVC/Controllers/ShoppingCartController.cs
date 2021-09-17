using Storefront.DATA.EF;
using MVC3.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC3.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        //Added for access to database
        private StoreFrontDB db = new StoreFrontDB();

        // GET: ShoppingCart
        //Shopping Cart - Step 4 - Modify Index action to return our Shopping Cart, scaffolded List View for CartItemViewModel (left DB Context EMPTY)
        public ActionResult Index()
        {
            //retrieve the Session shopping cart
            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            if (shoppingCart == null)
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }


            return View(shoppingCart);
        }

        //Shopping Cart - Step 3 - Create the action to instantiate our cart, and store our info in Session
        // Also added private BookStorePlusEntities db object (above Index())
        public ActionResult AddToCart(int qty, int bookId)
        {
            //Create an empty collection to store our cart info
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //Check if session-cart exists; if so, use it to populate our local variable
            //if not, we will instantiate it
            if (Session["cart"] != null)
            {
                //pull the keys/values from the Session variable, and store them in a local variable
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
            }
            else
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            //Find the book by the ID that was passed to this Action
            Album album = db.Albums.Find(bookId);//Find() vs. Where() -> Find returns a SINGLE object, Where returns a collection

            //Create the cart item, assign the book as a property as well as qty
            CartItemViewModel item = new CartItemViewModel(qty, album);

            //Add the item to the cart - BUT if we already have that book as a cart item, update the qty instead.
            if (shoppingCart.ContainsKey(album.AlbumID))
            {
                shoppingCart[album.AlbumID].Qty += qty;
            }
            else
            {
                shoppingCart.Add(album.AlbumID, item);
            }

            //Update the session variable so we can maintain info between requests
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");


            //return View();
        }


        //Shopping Cart - Step 6 - Create the RemoveFromCart action and remove the cart item from the Dictionary by it's Key
        public ActionResult RemoveFromCart(int id)
        {
            //Retrieve our session variable and store locally
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //remove the cart item
            shoppingCart.Remove(id);

            //Update session
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }


        //Shopping Cart - Step 7 - Create the UpdateCart action that accepts the BookID & Quantity, and updates our Session variable
        public ActionResult UpdateCart(int bookId, int qty)
        {
            //retrieve the session and store it locally
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //update the quantity tied to the bookId that was passed to this action
            shoppingCart[bookId].Qty = qty;

            //if cart item quantity is 0, remove that item from the cart
            if (shoppingCart[bookId].Qty == 0)
            {
                shoppingCart.Remove(bookId);
            }

            //Update session
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }

    }
}