using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedShop.Data.Infrastructure;
using TeduShop.Model.Models;
namespace TedShop.Data.Repositories
{
    public interface IMenuGroupRepository: IRepository<MenuGroup>
    {

    }

   public  class MenuGroupRepository:RepositoryBase<MenuGroup>,IMenuGroupRepository
    {

        public MenuGroupRepository(IDbFactory dbFactory) :base(dbFactory)
        {

        }
    }
}
