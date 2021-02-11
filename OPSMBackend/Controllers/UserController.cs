using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using OPSMBackend.DataEntities;
using OPSMBackend.Models;
using OPSMBackend.Models.Dto;
using OPSMBackend.Services.Roletypes;
using OPSMBackend.Services.User;

namespace OPSMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly IRoleTypesService roleTypesService;
        public UserController(IUserService userService, IRoleTypesService roleTypesService)
        {
            this.userService = userService;
            this.roleTypesService = roleTypesService;
        }

        [HttpPost("Login")]
        public ActionResult LoginUser(UserLogin userLogin)
        {
            if (userLogin != null && userLogin.password != null && userLogin.username != null)
            {
                var user = userService.GetUsers().ToList().Find(x => x.UserName.Equals(userLogin.username));
                if (user != null && user.Password.Equals(userLogin.password))
                {
                    Program.Logger.Info("Login success");
                    return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
                }
                else
                {
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Login failed", "Invalid username or password")));
                }
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper user details")));
            }

        }

        [HttpGet("UsersList")]
        public ActionResult GetListOfUsers()
        {
            var responseModel = new UserListResponseModel();

            var users = userService.GetUsers().ToList();
            List<UsersDto> usersDtos = new List<UsersDto>();
            users.ForEach(user => usersDtos.Add(new UsersDto().GetUsersDto(user)));

            var roleTypes = roleTypesService.GetRoleTypes().ToList();
            List<RoleTypeDto> roleTypeDtos = new List<RoleTypeDto>();
            roleTypes.ForEach(roleType => roleTypeDtos.Add(new RoleTypeDto().GetRoleTypeDto(roleType)));

            responseModel.Users = usersDtos;
            responseModel.RoleTypes = roleTypeDtos;

            if (responseModel != null && responseModel.Users != null && responseModel.RoleTypes != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No users found", "Please register a user")));
            }

        }

        [HttpPost("NewUser")]
        public ActionResult AddNewUser(UsersDto usersDto)
        {
            if (usersDto != null)
            {
                var user = usersDto.GetUser(usersDto);
                if (user != null)
                {
                    try
                    {
                        userService.InsertUser(user);
                    }
                    catch (Exception e)
                    {
                        Program.Logger.Error(e);
                        return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while adding new user")));
                    }

                    return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
                }
                else
                {
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Something went wrong.")));
                }


            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper user details")));
            }
        }

        [HttpPut("UpdateUser")]
        public ActionResult UpdateUser(UsersDto usersDto)
        {
            if (usersDto != null)
            {

                try
                {
                    var userFromDb = userService.GetUser(usersDto.Id);
                    if(userFromDb != null)
                    {
                        var convertedUser = usersDto.GetUser(userFromDb, usersDto);
                        userService.UpdateUser(convertedUser);
                    }
                    else
                    {
                        throw new Exception("The user does not exist");
                    }
                    
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the user")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper user details")));
            }
        }

        [HttpDelete("DeleteUser")]
        public ActionResult DeleteUser(int id)
        {
            if(id > 0)
            {
                try
                {
                    userService.DeleteUser(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the user")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper user details")));
            }
        }

    }




}
