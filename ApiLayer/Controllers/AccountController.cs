using ApiLayer.Helpers;
using BusinessLayer.Extension;
using BusinessLayer.Implementation;
using CommonLayer.CommonModels;
using DataAccessLayer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiLayer.Controllers
{
    public class AccountController : ApiController
    {
        private readonly UserModel _userModel;
        private readonly UserRegistrationBs _userRegistrationBs;
        private readonly LoginBs _loginBs;
        private readonly CategoryBs _categoryBs;
        private readonly UserGroupBS _userGroupBs;
        APIResponseModel apiResponse;
        public AccountController()
        {
            _userModel = new UserModel();
            _userRegistrationBs = new UserRegistrationBs();
            _loginBs = new LoginBs();
            _categoryBs = new CategoryBs();
            _userGroupBs = new UserGroupBS();
            apiResponse = new APIResponseModel();
        }

        [HttpPost]

        public IHttpActionResult UserRegistration(UserModel model)
        {
            int res = 0;
            string otp = null;
            if (model != null)
            {

                var checkUserName = _userRegistrationBs.CheckUserName(model.UserName);
                if (checkUserName)
                {
                    apiResponse.Message = "UserName alreay exsist!";
                    return Ok(apiResponse);
                }
                otp = GetOTPPassword();
                model.OTPPassword = otp;
                model.OTPGeneratedTime = DateTime.Now;
                model.IsOTPCheck = false;
                res = _userRegistrationBs.Save(model);
                new SendSMS().Send(model.Contact, "OTP is " + otp + " for Registration");
            }
            if (res != 0)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Message = "User Registered Successfully";
            }
            else
            {

                apiResponse.IsSuccess = false;
                apiResponse.Message = "Registration Failed";

            }

            return Ok(apiResponse);


        }


        [HttpPost]

        public IHttpActionResult Login(UserModel model)
        {

            var userData = _loginBs.LoginAuthentication(model.UserName, model.Password);

            if (userData != null)
            {
                if (userData.IsOTPCheck == false)
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = "OTP Not Verified!";
                    return Ok(apiResponse);
                }

                apiResponse.Data = userData.Id;
                apiResponse.IsSuccess = true;
                return Ok(apiResponse);
            }

            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "User Or Password Incorrect!!";
                return Ok(apiResponse);
            }

            //if (Membership.ValidateUser(model.UserName, model.Password))
            //    return Ok("Login Successfully!");
            //else
            //    return Ok("User Or Password Incorrect!");


        }

        [HttpGet]
        public IHttpActionResult OTPAuthentication(string contactNo, string otpPassword)
        {
            var user = _loginBs.OTPAuthenticationCheck(contactNo, otpPassword);

            if (user != null)
            {
                DateTime OTPTime = user.OTPGeneratedTime.Value;
                DateTime expTime = OTPTime.AddMinutes(15);
                if (DateTime.Now <= expTime)
                {
                    //int userid = User.Identity.GetUserID();
                    //int userid = user.Id;

                    //var useraccountdata = _userRegistrationBs.GetById(userid);
                    //if (useraccountdata != null)
                    //{
                    //    useraccountdata.IsOTPCheck = true;

                    user.IsOTPCheck = true;
                    _userRegistrationBs.Save(user);
                    apiResponse.IsSuccess = true;
                    apiResponse.Data = user.Id;
                    _categoryBs.AddCategoryMapping(user.Id);
                    _userGroupBs.SaveUserGroupMap(user.Id);
                    apiResponse.Message = "OTP Password Varified Successfylly!";
                    return Ok(apiResponse);
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = "Your OTP Password is expired!";
                    return Ok(apiResponse);
                }

            }
            else
            {
                return Ok("OTP Password is incorect!");
            }


        }

        [HttpGet]
        public IHttpActionResult GetLogout(int userid)
        {

            // update device id as null  of user    
            var response = _userRegistrationBs.UpdateUser(userid, null, null);
            if (response == true)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Message = "User Logout Successfully";
            }
            else
            {
                apiResponse.IsSuccess = false;
            }

            return Ok(apiResponse);

        }

        [HttpGet]
        public IHttpActionResult GetAddPlatform(int userID, string deviceid, string platform)
        {

            // update device id and platform of user  for push notification    
            //int userid = User.Identity.GetUserID();
            var response = _userRegistrationBs.UpdateUser(userID, deviceid, platform);
            if (!response)
            {
                apiResponse.IsSuccess = response;
                apiResponse.Message = "User Not Found!";
            }
            apiResponse.IsSuccess = response;
            return Ok(apiResponse);
        }

        [NonAction]
        public string GetOTPPassword()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            characters += alphabets + small_alphabets + numbers;

            int length = 5;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }
        [HttpGet]
        public IHttpActionResult UserProfile(int id)
        {
            var res = _userRegistrationBs.GetById(id);


            if (res != null)
            {
                apiResponse.Data = res;
                apiResponse.IsSuccess = true;
                apiResponse.Message = "Data Found";
                return Ok(apiResponse);

            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "No Record Found";
                return Ok("No Record Found");
            }

        }

        [HttpPost]
        public IHttpActionResult UserProfileUpdate(UserModel model)
        {
            int i = 0;
            if (model.Id != 0)
            {
                i = _userRegistrationBs.Save(model);
            }

            if (i > 0)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Message = "Updated Successfully";
                return Ok(apiResponse);

            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Internal Server Error";
                return Ok(apiResponse);
            }
        }

        [HttpGet]
        public IHttpActionResult ForgorPassword(string UserName)
        {
            string otp;
            var res = _userRegistrationBs.UserRegistrationList().Where(x => x.UserName == UserName).FirstOrDefault();

            if (res != null)
            {
                otp = GetOTPPassword();
                new SendSMS().Send(res.Contact, "OTP is " + otp + " for Registration");

                UserModel obj = new UserModel();
                UserModel _user = new UserModel();
                _user.Id = res.Id;
                _user.Name = res.Name;
                _user.Area = res.Area;
                _user.Contact = res.Email;
                _user.Password = res.Password;
                _user.IsOTPCheck = res.IsOTPCheck;
                _user.OTPPassword = otp;
                _user.RoleId = res.RoleId;
                _user.UserTypeId = res.UserTypeId;
                _user.DeviceID = res.DeviceID;
                _user.Platform = res.Platform;
                _userRegistrationBs.Save(_user);
                apiResponse.Message = "OTP Sent";
                apiResponse.IsSuccess = true;
            }
            else
            {
                apiResponse.Message = "Incorrect Mobile Number";
                apiResponse.IsSuccess = false;

            }
            return Ok(apiResponse);

        }

        [HttpGet]
        public IHttpActionResult ResetPassword(string UserName, string NewPassword, string otp)
        {
            //var res = _userRegistrationBs.UserRegistrationList().Where(x => x.UserName == UserName).FirstOrDefault();

            //if (res != null && res.OTPPassword == otp)
            //{

            //    UserModel _user = new UserModel();
            //    _user.Id = res.Id;
            //    _user.Name = res.Name;
            //    _user.Area = res.Area;
            //    _user.Contact = res.Email;
            //    _user.Password = NewPassword;
            //    _user.IsOTPCheck = res.IsOTPCheck;
            //    _user.OTPPassword = res.OTPPassword;
            //    _user.RoleId = res.RoleId;
            //    _user.UserTypeId = res.UserTypeId;
            //    _user.DeviceID = res.DeviceID;
            //    _user.Platform = res.Platform;
            //    _userRegistrationBs.Save(_user);
            //    apiResponse.IsSuccess = true;
            //    apiResponse.Message = "Password reset successfully";


            //}
            //else
            //{
            //    apiResponse.IsSuccess = false;
            //    apiResponse.Message = "Reset Failed";
            //}
            if (UserName == null)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "UserName cannot be empty";
                return Ok(apiResponse);
            }
            var response = _userRegistrationBs.UpdateResetPassword(UserName, NewPassword, otp);
            if (!response)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Reset failed.Contact Admin";
            }
            apiResponse.IsSuccess = true;
            apiResponse.Message = "Password reset successfully";
            return Ok(apiResponse);
        }

    }
}
