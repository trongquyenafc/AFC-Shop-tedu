﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedShop.Data.Infrastructure;
using TeduShop.Model.Models;
namespace TedShop.Data.Repositories
{

    public interface IFooterRepository : IRepository<Footer>
    {

    }
   public class FooterRepository:RepositoryBase<Footer>,IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory): base(dbFactory)
        {

        }

    }
}