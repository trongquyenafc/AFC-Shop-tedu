﻿namespace TedShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TeduShop.Model.Models;
    using TeduShop.Common;
    using System.Data.Entity.Validation;
    using System.Diagnostics;

    internal sealed class Configuration : DbMigrationsConfiguration<TedShop.Data.TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;//sua lai true la de khoi tao migration 
        }

        protected override void Seed(TedShop.Data.TeduShopDbContext context)
        {
            // CreateProductCategorySample(context);
            //  CreateSlide(context);
            //  CreatePage(context);
            CreateContactDetail(context);
        }


        private void CreateProductCategorySample(TedShop.Data.TeduShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() {Name="Điện lạnh",Alias="dien-lanh",Status=true},
                   new ProductCategory() { Name="Viễn thông",Alias="vien-thong",Status=true },
                  new ProductCategory() { Name="Đồ gia dụng",Alias="do-gia-dung",Status=true },
                   new ProductCategory() { Name="Mỹ phẩm",Alias="my-pham",Status=true }

                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }


        private void CreateUser(TeduShopDbContext context)
        {
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TeduShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "tedu",
            //    Email = "tedu.international@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Technology Education"

            //};

            //manager.Create(user, "123654$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("tedu.international@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateFooter(TeduShopDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId)==0)
            {
                string content = "";
            }
        }

        private void CreateSlide(TeduShopDbContext context)
        {

            if (context.Slides.Count()==0)
            {
                List<Slide> listSlide = new List<Slide>();
                listSlide.Add(new Slide {
                    Name = "Slide 1",
                    DisplayOrder =1,
                    Status =true,
                    Url ="#"
                    ,Image= "/Assets/client/images/bag.jpg",
                    Content = @"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur 
                            adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                        <span class=""on-get"">GET NOW</span>"


                });

                listSlide.Add(new Slide { Name = "Slide 2",
                    DisplayOrder = 2,
                    Status = true,
                    Url = "#",
                    Image = "/Assets/client/images/bag1.jpg",
                      Content= @"<h2>FLAT 50% 0FF</h2>
								<label>FOR ALL PURCHASE <b>VALUE</b></label>
								<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>					
								<span class=""on-get"">GET NOW</span>"
                });


                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
     

            //if (context.Slides.Count() == 0)
            //{
            //    List<Slide> listSlide = new List<Slide>()
            //    {
            //        new Slide() {
            //            Name ="Slide 1",
            //            DisplayOrder =1,
            //            Status =true,
            //            Url ="#",
            //            Image ="/Assets/client/images/bag.jpg",
            //            Content =@"	<h2>FLAT 50% 0FF</h2>
            //                    <label>FOR ALL PURCHASE <b>VALUE</b></label>
            //                    <p>Lorem ipsum dolor sit amet, consectetur 
            //                adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
            //            <span class=""on-get"">GET NOW</span>" },
            //        new Slide() {
            //            Name ="Slide 2",
            //            DisplayOrder =2,
            //            Status =true,
            //            Url ="#",
            //            Image ="/Assets/client/images/bag1.jpg",
            //        Content=@"<h2>FLAT 50% 0FF</h2>
            //                    <label>FOR ALL PURCHASE <b>VALUE</b></label>

            //                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >

            //                    <span class=""on-get"">GET NOW</span>"},
            //    };
            //    context.Slides.AddRange(listSlide);
            //    context.SaveChanges();
            //}
        }
        private void CreatePage(TeduShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                try
                {
                    var page = new Page()
                    {
                        Name = "Giới thiệu",
                        Alias = "gioi-thieu",
                        Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium",
                        Status = true

                    };
                    context.Pages.Add(page);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }

            }
        }

        private void CreateContactDetail(TeduShopDbContext context)
        {


            if (context.ContactDetails.Count()==0)
            {
                try
                {
                    var contactDetail = new ContactDetail()
                    {
                        Name = "Shop thoi trang tedu",
                        Address = "Ngõ 77 Xuân La",
                        Email = "tedu@gmail.com",
                        Lat = 21.0633645,
                        Lng = 105.8053274,
                        Phone = "095423233",
                        Website = "http://tedu.com.vn",
                                  Other = "",
                                Status = true
                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
           
        }


    }
}
