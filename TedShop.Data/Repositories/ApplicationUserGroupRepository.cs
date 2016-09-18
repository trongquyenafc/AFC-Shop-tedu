﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Model.Models;
using TedShop.Data.Infrastructure;
namespace TedShop.Data.Repositories
{

    public interface IApplicationUserGroupRepository : IRepository<ApplicationUserGroup>
    {

    }
    public class ApplicationUserGroupRepository : RepositoryBase<ApplicationUserGroup>, IApplicationUserGroupRepository
    {
        public ApplicationUserGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
