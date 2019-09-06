using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetaLifeStyle.Services
{
    public class ProductHandler
    {
        public static ProductCategory GetProductCategoryByProduct(Product product)
        {
            BetaDB db = new BetaDB();
            return db.ProductCategories.Find(product.ProductCatID);
        }

        public static ProductSubCategory GetProductSubCategoryByProduct(Product product)
        {
            BetaDB db = new BetaDB();
            return db.ProductSubCategories.SingleOrDefault(x=>x.ProductSubCatID ==product.ProductSubCatID);
        }

        public static List<ProductSize> GetProductSizesFromProduct(Product product)
        {
            BetaDB db = new BetaDB();
            List<ProductSize> ps = db.ProductSizes.Where(x => x.ProductSubCatID == product.ProductSubCatID).ToList();
            return ps;
        }
    }
}