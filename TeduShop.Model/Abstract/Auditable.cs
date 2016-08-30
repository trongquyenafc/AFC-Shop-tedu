using System;
using System.ComponentModel.DataAnnotations;

namespace TeduShop.Model.Abstract
{
    public abstract class Auditable : IAuditable
    {
        [MaxLength(256)]
        public string CreateBy{ set; get; }

        public DateTime? CreateDate { set; get; }

        [MaxLength(256)]
        public string UpdateBy { set; get; }
     

        public DateTime? UpdateDate { set; get; }
     


        [MaxLength(256)]
        public string MetaDescription { set; get; }
        [MaxLength(256)]
        public string Metakeyword { set; get; }
      

        public bool Status { set; get; }
       
    }
}