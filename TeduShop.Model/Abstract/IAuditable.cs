using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Abstract
{
    public interface IAuditable
    {


        DateTime? CreateDate { set; get; }
        string CreateBy { set; get; }
        DateTime? UpdateDate { set; get; }
        string UpdateBy { set; get; }
        string Metakeyword { set; get; }
        string MetaDescription { set; get; }
        bool Status { set; get; }
    }
}
