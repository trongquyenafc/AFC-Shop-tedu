using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ProductCategoryViewModel
    {
        public int ID { set; get; }
        [Required(ErrorMessage = "Yêu cầu nhập tên danh mục")]
        public string Name { set; get; }


        [Required(ErrorMessage = "Yêu cầu nhập tiêu đề SEO")]
        public string Alias { set; get; }

        public string Description { set; get; }

        public int? ParentID { set; get; }
        public int? DisplayOrder { set; get; }

        public string Image { set; get; }

        public bool? HomeFlag { set; get; }

        public virtual IEnumerable<PostViewModel> Posts { set; get; }


        public string CreateBy { set; get; }

        public DateTime? CreateDate { set; get; }

        public string UpdateBy { set; get; }


        public DateTime? UpdateDate { set; get; }



        public string MetaDescription { set; get; }

        public string Metakeyword { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập trạng thái")]
        public bool Status { set; get; }


    }
}