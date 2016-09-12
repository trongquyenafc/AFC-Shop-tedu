﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ProductViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Alias { set; get; }

        public int CategoryID { set; get; }

        public string Images { set; get; }

        public string MoreImages { set; get; }

        public decimal Price { set; get; }

        public decimal? PromotionPrice { set; get; }

        public int? Waranty { set; get; }

        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }

        public bool? HotFlag { set; get; }

        public int? ViewCount { set; get; }

        public string CreateBy { set; get; }

        public DateTime? CreateDate { set; get; }

        public string UpdateBy { set; get; }


        public DateTime? UpdateDate { set; get; }
        public int Quantity { set; get; }


        public string MetaDescription { set; get; }

        public string Metakeyword { set; get; }


        public bool Status { set; get; }
        public string Tags { set; get; }
        public virtual ProductCategoryViewModel ProductCategory { set; get; }
    }
}