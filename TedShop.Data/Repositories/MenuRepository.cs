using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedShop.Data.Infrastructure;
using TeduShop.Model.Models;
namespace TedShop.Data.Repositories
{

    public interface IMenuRepository : IRepository<Menu>
    {
    }
    public  class MenuRepository: RepositoryBase<Menu>
    {


        public MenuRepository(IDbFactory dbFactory): base(dbFactory)
        {

        }

    }
}
