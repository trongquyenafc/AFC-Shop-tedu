using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeduShop.Model.Models;
using TedShop.Data.Repositories;
using TedShop.Data.Infrastructure;
using System.Linq;
namespace TeduShop.UnitTest.RepositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {


        IDbFactory dbFactory;
        IPostCategoryRepository objRepository;
        IUnitOfWork unitOfWork;
        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new PostCategoryRepository(dbFactory);
            unitOfWork =  new UnitOfWork(dbFactory);
        }


        [TestMethod]
        public void PostCategory_Repository_GetAll()
        {
            var list = objRepository.GetAll().ToList();
            Assert.AreEqual(3, list.Count);
        }


        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            PostCategory category = new PostCategory();
            category.Name = "Test category";
            category.Alias = "Test-category";
            category.Status = true;

            var result = objRepository.Add(category);
            unitOfWork.Commit();

            Assert.IsNotNull(result);//kiem tra khac null
            Assert.AreEqual(3, result.ID);// kiem tra du lieu moi tao ra ,  co id bang 3 khong
        }
    }
}