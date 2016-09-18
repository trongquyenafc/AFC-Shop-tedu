using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TeduShop.Common.Exceptions;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.App_Start;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [Authorize]
    [RoutePrefix("api/applicationUser")]
    public class ApplicationUserController : ApiControllerBase
    {
        private ApplicationUserManager _userManager;
        private IApplicationGroupService _appGroupService;
        private IApplicationRoleService _appRoleService;

        public ApplicationUserController(
            IApplicationGroupService appGroupService,
            IApplicationRoleService appRoleService,
            ApplicationUserManager userManager,
            IErrorService errorService)
            : base(errorService)
        {
            _appRoleService = appRoleService;
            _appGroupService = appGroupService;
            _userManager = userManager;
        }

        [Route("getlistpaging")]
        [HttpGet]
        [Authorize(Roles = "ViewUser")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _userManager.Users;
                IEnumerable<ApplicationUserViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(model);

                PaginationSet<ApplicationUserViewModel> pagedSet = new PaginationSet<ApplicationUserViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        //[Route("detail/{id}")]
        //[HttpGet]
        //[Authorize(Roles = "ViewUser")]
        //public HttpResponseMessage Details(HttpRequestMessage request, string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có giá trị.");
        //    }
        //    var user = _userManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
        //    }
        //    else
        //    {
        //        var applicationUserViewModel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user.Result);
        //        var listGroup = _appGroupService.GetListGroupByUserId(applicationUserViewModel.Id);
        //        applicationUserViewModel.Groups = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(listGroup);
        //        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
        //    }
        //}

        [Route("detail/{id}")]
        [HttpGet]
        [Authorize(Roles = "ViewUser")]
        public HttpResponseMessage Details(HttpRequestMessage request,string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " Khong co gia tri ");
            }
            var user = _userManager.FindByIdAsync(id);
            if (user==null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
            }
            else
            {
                var applicationUserviewmodel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user.Result);
                var listGroup = _appGroupService.GetListGroupByUserId(applicationUserviewmodel.Id);
                applicationUserviewmodel.Groups = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(listGroup);
                return request.CreateResponse(HttpStatusCode.OK, applicationUserviewmodel);
            }
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "AddUser")]
        public async Task<HttpResponseMessage> Create(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewmodel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newappuser = new ApplicationUser();
                newappuser.UpdateUser(applicationUserViewmodel);
                try
                {
                    newappuser.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(newappuser, applicationUserViewmodel.Password);

                    if (result.Succeeded)
                    {
                        List<ApplicationUserGroup> listusergroup = new List<ApplicationUserGroup>();
                        foreach (var group  in applicationUserViewmodel.Groups)
                        {
                            listusergroup.Add(new ApplicationUserGroup()
                            {
                                GroupId =  group.ID,
                                UserId =  newappuser.Id

                            });

                            var lisrole = _appRoleService.GetListRoleByGroupId(group.ID);
                            foreach (var role in lisrole)
                            {
                                await _userManager.RemoveFromRoleAsync(newappuser.Id, role.Name);
                                await _userManager.AddToRoleAsync(newappuser.Id, role.Name);
                            }
                        }

                        _appGroupService.AddUserToGroups(listusergroup, newappuser.Id);
                        _appGroupService.Save();
                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewmodel);
                    }
                    else
                    {
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                    }
                }
                catch (NameDuplicatedException ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }

            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }
        }



        //[HttpPost]
        //[Route("add")]
        //[Authorize(Roles = "AddUser")]
        //public async Task<HttpResponseMessage> Create(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var newAppUser = new ApplicationUser();
        //        newAppUser.UpdateUser(applicationUserViewModel);
        //        try
        //        {
        //            newAppUser.Id = Guid.NewGuid().ToString();
        //            var result = await _userManager.CreateAsync(newAppUser, applicationUserViewModel.Password);
        //            if (result.Succeeded)
        //            {
        //                var listAppUserGroup = new List<ApplicationUserGroup>();
        //                foreach (var group in applicationUserViewModel.Groups)
        //                {
        //                    listAppUserGroup.Add(new ApplicationUserGroup()
        //                    {
        //                        GroupId = group.ID,
        //                        UserId = newAppUser.Id
        //                    });
        //                    //add role to user
        //                    var listRole = _appRoleService.GetListRoleByGroupId(group.ID);
        //                    foreach (var role in listRole)
        //                    {
        //                        await _userManager.RemoveFromRoleAsync(newAppUser.Id, role.Name);
        //                        await _userManager.AddToRoleAsync(newAppUser.Id, role.Name);
        //                    }
        //                }
        //                _appGroupService.AddUserToGroups(listAppUserGroup, newAppUser.Id);
        //                _appGroupService.Save();

        //                return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
        //            }
        //            else
        //                return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
        //        }
        //        catch (NameDuplicatedException dex)
        //        {
        //            return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}



        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "UpdateUser")]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, ApplicationUserViewModel  applicationUserViewmodel)
        {
            if (ModelState.IsValid)
            {
                var appuser = await _userManager.FindByIdAsync(applicationUserViewmodel.Id);
                try
                {
                    appuser.UpdateUser(applicationUserViewmodel);
                    var result = await _userManager.UpdateAsync(appuser);
                    if (result.Succeeded)
                    {
                        var listAppUsergrop = new List<ApplicationUserGroup>();
                        foreach (var group in applicationUserViewmodel.Groups)
                        {
                            listAppUsergrop.Add(new ApplicationUserGroup()
                            {
                                GroupId =  group.ID,
                                UserId =  applicationUserViewmodel.Id

                            });


                            var listRole = _appRoleService.GetListRoleByGroupId(group.ID);
                            foreach (var role in listRole)
                           {
                               await _userManager.RemoveFromRoleAsync(appuser.Id, role.Name);
                               await _userManager.AddToRoleAsync(appuser.Id, role.Name);
                            }


                        }
                        _appGroupService.AddUserToGroups(listAppUsergrop, applicationUserViewmodel.Id);
                                      _appGroupService.Save();
                                       return request.CreateResponse(HttpStatusCode.OK, applicationUserViewmodel);
                    }
                    else
                    {
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                    }
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        //[HttpPut]
        //[Route("update")]
        //[Authorize(Roles = "UpdateUser")]
        //public async Task<HttpResponseMessage> Update(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var appUser = await _userManager.FindByIdAsync(applicationUserViewModel.Id);
        //        try
        //        {
        //            appUser.UpdateUser(applicationUserViewModel);
        //            var result = await _userManager.UpdateAsync(appUser);
        //            if (result.Succeeded)
        //            {
        //                var listAppUserGroup = new List<ApplicationUserGroup>();
        //                foreach (var group in applicationUserViewModel.Groups)
        //                {
        //                    listAppUserGroup.Add(new ApplicationUserGroup()
        //                    {
        //                        GroupId = group.ID,
        //                        UserId = applicationUserViewModel.Id
        //                    });
        //                    //add role to user
        //                    var listRole = _appRoleService.GetListRoleByGroupId(group.ID);
        //                    foreach (var role in listRole)
        //                    {
        //                        await _userManager.RemoveFromRoleAsync(appUser.Id, role.Name);
        //                        await _userManager.AddToRoleAsync(appUser.Id, role.Name);
        //                    }
        //                }
        //                _appGroupService.AddUserToGroups(listAppUserGroup, applicationUserViewModel.Id);
        //                _appGroupService.Save();
        //                return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
        //            }
        //            else
        //                return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
        //        }
        //        catch (NameDuplicatedException dex)
        //        {
        //            return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}

        //[HttpDelete]
        //[Route("delete")]
        //[Authorize(Roles = "DeleteUser")]
        //public async Task<HttpResponseMessage> Delete(HttpRequestMessage request, string id)
        //{
        //    var appUser = await _userManager.FindByIdAsync(id);
        //    var result = await _userManager.DeleteAsync(appUser);
        //    if (result.Succeeded)
        //        return request.CreateResponse(HttpStatusCode.OK, id);
        //    else
        //        return request.CreateErrorResponse(HttpStatusCode.OK, string.Join(",", result.Errors));
        //}



        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "DeleteUser")]
        public async Task<HttpResponseMessage> delete(HttpRequestMessage request, string id)
        {
            var appuser = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(appuser);
            if (result.Succeeded)
            {
                return request.CreateResponse(HttpStatusCode.OK, id);
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.OK, string.Join(",", result.Errors));
            }
        }
    }
}