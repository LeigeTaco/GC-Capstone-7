using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Capstone7.Models;

namespace Capstone7.Controllers
{
    
    public class NorthwindController : ApiController
    {

        public class SimpleProduct
        {
            public int ProductID { set; get; }
            public string ProductName { set; get; }

            public SimpleProduct(Product p)
            {
                ProductID = p.ProductID;
                ProductName = p.ProductName;
            }
        }

        //public class SearchParams
        //{
        //    public int cat { get; set; }
        //    public int 
        //}

        private List<Product> _GetProductList()
        {

            NorthwindEntities ORM = new NorthwindEntities();
            return ORM.Products.ToList();

        }

        [HttpGet]
        //[Route("api/product")]
        public List<SimpleProduct> Product([FromUri]int? id = null, [FromUri]int? cat = null, [FromUri]int? sup = null, [FromUri]decimal? max = null, [FromUri]bool? current = false)
        {

            List<SimpleProduct> output = new List<SimpleProduct>();
            Product byID = new Product();
            List<Product> temp = _GetProductList();

            if (id != null)
            {
                int _id = (int)id;
                temp = temp.Where(x => x.ProductID == _id).ToList();
            }

            if (cat != null)
            {
                int _cat = (int)cat;
                temp = temp.Where(x => x.CategoryID == _cat).ToList();
            }

            if (sup != null)
            {
                int _sup = (int)sup;
                temp = temp.Where(x => x.SupplierID == _sup).ToList();
            }

            if (max != null)
            {
                decimal _max = (decimal)max;
                temp = temp.Where(x => x.UnitPrice == _max).ToList();
            }

            if (current == true)
            {
                temp = temp.Where(x => x.Discontinued == false).ToList();
            }

            foreach (Product p in temp)
            {
                output.Add(new SimpleProduct(p));
            }

            return output;

        }

    }
}