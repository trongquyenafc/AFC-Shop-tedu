using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduShop.Model.Models;
using TeduShop.Web.Models;
namespace TeduShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {

        public static  void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVm)
        {
            postCategory.ID = postCategoryVm.ID;
            postCategory.Name = postCategoryVm.Name;
            postCategory.Description = postCategoryVm.Description;
            postCategory.Alias = postCategoryVm.Alias;
            postCategory.ParentID = postCategoryVm.ParentID;
            postCategory.DisplayOrder = postCategoryVm.DisplayOrder;
            postCategory.Image = postCategoryVm.Image;
            postCategory.HomeFlag = postCategoryVm.HomeFlag;

            postCategory.CreateDate = postCategoryVm.CreateDate;
            postCategory.CreateBy = postCategoryVm.CreateBy;
            postCategory.UpdateDate = postCategoryVm.UpdateDate;
            postCategory.UpdateBy = postCategoryVm.UpdateBy;
            postCategory.Metakeyword = postCategoryVm.Metakeyword;
            postCategory.MetaDescription = postCategoryVm.MetaDescription;
            postCategory.Status = postCategoryVm.Status;
        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVm)
        {
            productCategory.ID = productCategoryVm.ID;
            productCategory.Name = productCategoryVm.Name;
            productCategory.Description = productCategoryVm.Description;
            productCategory.Alias = productCategoryVm.Alias;
            productCategory.ParentID = productCategoryVm.ParentID;
            productCategory.DisplayOrder = productCategoryVm.DisplayOrder;
            productCategory.Images = productCategoryVm.Image;
            productCategory.HomeFlag = productCategoryVm.HomeFlag;

            productCategory.CreateDate = productCategoryVm.CreateDate;
            productCategory.CreateBy = productCategoryVm.CreateBy;
            productCategory.UpdateDate = productCategoryVm.UpdateDate;
            productCategory.UpdateBy = productCategoryVm.UpdateBy;
            productCategory.Metakeyword = productCategoryVm.Metakeyword;
            productCategory.MetaDescription = productCategoryVm.MetaDescription;
            productCategory.Status = productCategoryVm.Status;

        }

        public static void UpdatePost(this Post post, PostViewModel postVm)
        {
            post.ID = postVm.ID;
            post.Name = postVm.Name;
            post.Description = postVm.Description;
            post.Alias = postVm.Alias;
            post.CategoryID = postVm.CategoryID;
            post.Content = postVm.Content;
            post.Image = postVm.Image;
            post.HomeFlag = postVm.HomeFlag;
            post.ViewCount = postVm.ViewCount;

            post.CreateDate = postVm.CreateDate;
            post.CreateBy = postVm.CreateBy;
            post.UpdateDate = postVm.UpdateDate;
            post.UpdateBy = postVm.UpdateBy;
            post.Metakeyword = postVm.Metakeyword;
            post.MetaDescription = postVm.MetaDescription;
            post.Status = postVm.Status;
        }


        public static void UpdateProduct(this Product product, ProductViewModel productVm)
        {
            product.ID = productVm.ID;
            product.Name = productVm.Name;
            product.Description = productVm.Description;
            product.Alias = productVm.Alias;
            product.CategoryID = productVm.CategoryID;
            product.Content = productVm.Content;
            product.Images = productVm.Images;
            product.Moreimages = productVm.MoreImages;
            product.Price = productVm.Price;
            product.PromotionPrice = productVm.PromotionPrice;
            product.Waranty = productVm.Waranty;
            product.HomeFlag = productVm.HomeFlag;
            product.HotFlag = productVm.HotFlag;
            product.ViewCount = productVm.ViewCount;
            product.Quantity = productVm.Quantity;
            product.CreateDate = productVm.CreateDate;
            product.CreateBy = productVm.CreateBy;
            product.UpdateDate = productVm.UpdateDate;
            product.UpdateBy = productVm.UpdateBy;
            product.Metakeyword = productVm.Metakeyword;
            product.MetaDescription = productVm.MetaDescription;
            product.Tags = productVm.Tags;
            product.Status = productVm.Status;
        }


        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackVm)
        {
            feedback.Name = feedbackVm.Name;
            feedback.Email = feedbackVm.Email;
            feedback.Message = feedbackVm.Message;
            feedback.Status = feedbackVm.Status;
            feedback.CreatedDate = DateTime.Now;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderVm)
        {
            order.CustomerName = orderVm.CustomerName;
            order.CustomerAddress = orderVm.CustomerAddress;
            order.CustomerEmail = orderVm.CustomerEmail;
            order.CustomerMobile = orderVm.CustomerMobile;
            order.CustomerMessage = orderVm.CustomerMessage;
            order.PaymentMethod = orderVm.PaymentMethod;
            order.CreatedDate = DateTime.Now;
            order.CreatedBy = orderVm.CreatedBy;
            order.Status = orderVm.Status;
            order.CustomerId = orderVm.CustomerId;
 
        }



    }
}