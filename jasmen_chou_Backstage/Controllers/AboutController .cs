using jasmen_chou_Backstage.Manager;
using jasmen_chou_Backstage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jasmen_chou_Backstage.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submission(UserInfo user)
        {
            #region 获取表单信息
            string firstname = user.FirstName;
            string lastname = user.LastName;
            string phonenumber = user.PhoneNumber;
            string email = user.Eamil;
            string messages = user.Messages;

            UserInfo Msg = new UserInfo
            {
                FirstName = firstname,
                LastName = lastname,
                PhoneNumber = phonenumber,
                Eamil = email,
                Messages = messages
            };
            
            #endregion

            #region 发送邮件至管理员处
            string FromMail = ConfigurationManager.AppSettings["FromMail"];
            string ToMial = ConfigurationManager.AppSettings["ToMial"];
            string AuthorizationCode = ConfigurationManager.AppSettings["AuthorizationCode"];
            string ReplyTo = ConfigurationManager.AppSettings["ReplyTo"];
            string CCMial = ConfigurationManager.AppSettings["CCMial"];

            bool sendRes = MySendMailHelpler.SendMail(FromMail, ToMial, AuthorizationCode, ReplyTo, CCMial, Msg, null);
            #endregion

            #region 将用户信息保存至数据库
            if (sendRes)
            {
                //string sql = "INSERT INTO User(firstname, lastname, mobile, email, messages) VALUES " +
                //"('" + firstname + "', '" + lastname + "' , '" + phonenumber + "', '" + email + "', '" + messages + "');";
                //int result = new MySqlDbHelper().ExecuteNonQuery(sql);
                Response.Redirect("~/About/About");
                return View();
            }
            else
            {
                Response.Redirect("~/About/About");
                return View();
            }
            #endregion
        }
    }
}