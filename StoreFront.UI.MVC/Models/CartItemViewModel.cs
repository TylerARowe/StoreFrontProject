using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Storefront.DATA.EF;//Added for domain model (Book)

//Shopping Cart - Step 1 - added a class to the models folder with props and a ctor

namespace MVC3.UI.MVC.Models
{
    public class CartItemViewModel
    {
        public Album album { get; set; }

        public int Qty { get; set; }

        //fully-qualified ctor
        public CartItemViewModel(int qty, Album album)
        {
            //Property          is assigned the value of            param
            Qty = qty;
        }
    }
}