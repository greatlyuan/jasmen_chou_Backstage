using HslCommunication.BasicFramework;
using jasmen_chou_Backstage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace jasmen_chou_Backstage.Manager
{
    public static class MySendMailHelpler
    {
        #region 邮件系统发送
        /// <summary>
        /// 发送邮件的方法，需要提供完整的参数信息
        /// </summary>
        /// <param name="addr_from">发送地址</param>
        /// <param name="name">发送别名</param>
        /// <param name="addr_to">接收地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="priority">优先级</param>
        /// <param name="isHtml">邮件内容是否是HTML语言</param>
        /// <returns>发生是否成功，内容不正确会被视为垃圾邮件</returns>
        //public bool SendMail(string addr_from, string name, string[] addr_to, string subject, string body, MailPriority priority, bool isHtml)

        public static bool SendMailSystem(string email, string name, string messages)
        {
            try
            {
                SoftMail.MailSystem163.SendMail(email, name, messages);
                return true;
            }
            catch (SmtpException ex)
            {
                SoftMail.MailSystem163.SendMail(email, name, ex.Message);
                return false;
            }
        }
        #endregion

        #region 指定邮箱发送
        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="FromMial">发件人邮箱</param>
        /// <param name="ToMial">收件人邮箱(多个收件人地址用";"号隔开)</param>
        /// <param name="AuthorizationCode">发件人授权码</param>
        /// <param name="ReplyTo">对方回复邮件时默认的接收地址（不设置也是可以的）</param>
        /// <param name="CCMial">邮件的抄送者(多个抄送人用";"号隔开)</param>
        /// <param name="Msg">内容</param>
        /// <param name="File_Path">附件的地址</param>
        public static bool SendMail(string FromMail, string ToMial, string AuthorizationCode, string ReplyTo, string CCMial , UserInfo Msg, string File_Path)
        {
            try
            {
                //实例化一个发送邮件类。
                MailMessage mailMessage = new MailMessage();
                //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
                mailMessage.Priority = MailPriority.Normal;
                //发件人邮箱地址
                mailMessage.From = new MailAddress(FromMail);
                //抄送地址
                if (ToMial != "" && ToMial != null)
                {
                    //收件地址
                    List<string> ToMiallist = ToMial.Split(';').ToList();
                    for (int i = 0; i < ToMiallist.Count; i++)
                    {
                        mailMessage.To.Add(new MailAddress(ToMiallist[i]));
                    }
                }
                //对方回复邮件时默认的接收地址(不设置也是可以的哟)
                if (ReplyTo == "" || ReplyTo == null)
                {
                    ReplyTo = FromMail;
                }
                mailMessage.ReplyToList.Add(ReplyTo);
                //抄送地址
                if (CCMial != "" && CCMial != null)
                {
                    List<string> CCMiallist = CCMial.Split(';').ToList();
                    for (int i = 0; i < CCMiallist.Count; i++)
                    {
                        //邮件的抄送者，支持群发
                        mailMessage.CC.Add(CCMiallist[i]);
                    }
                }
                //如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
                mailMessage.SubjectEncoding = Encoding.GetEncoding(936);
                //邮件正文是否是HTML格式
                mailMessage.IsBodyHtml = true;

                //邮件标题。
                mailMessage.Subject = "客户邮件通知";

                #region html输出代码
                var text = "";
                text += "<div style=\"margin-bottom: 20px;background-color: #fff;border: 1px solid transparent;border-radius: 4px;box-shadow: 0 1px 1px rgba(0,0,0,.05);border-color: #337ab7;\">";
                text += "<div style=\"color: #fff;background-color: #337ab7;border-color: #337ab7;padding: 10px 15px;border-bottom: 1px solid transparent;border-top-left-radius: 3px;border-top-right-radius: 3px;\">";
                text += "<h3 style=\"margin-top: 0;margin-bottom: 0;font-size: 16px;color: inherit;\">客户信息</h3>";
                text += "</div>";
                text += "<div style=\"padding: 15px;\">";
                text += "<table style=\"width:100%;border:1px solid #aaa;border-collapse: collapse;\">";
                text += "<tbody>";
                //text += "<tr>";
                //text += "<th style=\"width:30%;text-align:left;background-color:#3F3F3F;padding: 3px;border: 1px solid #3F3F3F;color:#fff;\">名称</th>";
                //text += "<th style=\"width:70%;text-align:left;background-color:#3F3F3F;padding: 3px;border: 1px solid #3F3F3F;color:#fff;\">值</th>";
                //text += "</tr>";
                text += "<tr>";
                text += "<td style=\"border:1px solid #aaa;padding: 2px;\">姓名</td>";
                text += "<td style=\"border:1px solid #aaa;padding: 2px;\">" + Msg.FirstName + Msg.LastName + "</td>";
                text += "</tr>";
                text += "<tr>";
                text += "<td style=\"border:1px solid #aaa;padding: 2px;\">电话</td>";
                text += "<td style=\"border:1px solid #aaa;padding: 2px;\">" + Msg.PhoneNumber + "</td>";
                text += "</tr>";
                text += "<tr>";
                text += "<td style=\"border:1px solid #aaa;padding: 2px;\">邮箱</td>";
                text += "<td style=\"border:1px solid #aaa;padding: 2px;\">" + Msg.Eamil + "</td>";
                text += "</tr>";
                text += "</tbody>";
                text += "</table>";
                text += "</div>";
                text += "</div>";

                text += "<div style=\"margin-bottom: 20px;background-color: #fff;border: 1px solid transparent;border-radius: 4px;box-shadow: 0 1px 1px rgba(0,0,0,.05);border-color: #337ab7;\">";
                text += "<div style=\"color: #fff;background-color: #337ab7;border-color: #337ab7;padding: 10px 15px;border-bottom: 1px solid transparent;border-top-left-radius: 3px;border-top-right-radius: 3px;\">";
                text += "<h3 style=\"margin-top: 0;margin-bottom: 0;font-size: 16px;color: inherit;\">客户留言</h3>";
                text += "</div>";
                text += "<div style=\"padding: 15px;\">";
                text += Msg.Messages;
                text += "</div>";
                text += "</div>";
                #endregion

                //邮件内容。
                mailMessage.Body = text;

                //设置邮件的附件，将在客户端选择的附件先上传到服务器保存一个，然后加入到mail中  
                if (File_Path != "" && File_Path != null)
                {
                    //将附件添加到邮件
                    mailMessage.Attachments.Add(new Attachment(File_Path));
                    //获取或设置此电子邮件的发送通知。
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                }
                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient();

                #region 设置邮件服务器地址
                if (FromMail.Length != 0)
                {
                    //根据发件人的邮件地址判断发件服务器地址 默认端口一般是25
                    string[] addressor = FromMail.Trim().Split(new Char[] { '@', '.' });
                    switch (addressor[1])
                    {
                        case "163":
                            client.Host = "smtp.163.com";
                            break;
                        case "126":
                            client.Host = "smtp.126.com";
                            break;
                        case "yeah":
                            client.Host = "smtp.yeah.com";
                            break;
                        case "qq":
                            client.Host = "smtp.qq.com";
                            break;
                        case "gmail":
                            client.Host = "smtp.gmail.com";
                            break;
                        case "hotmail":
                            client.Host = "smtp.live.com";
                            break;
                        case "foxmail":
                            client.Host = "smtp.foxmail.com";
                            break;
                        case "sina":
                            client.Host = "smtp.sina.com.cn";
                            break;
                        default:
                            client.Host = "smtp.exmail.qq.com";
                            break;
                    }
                }
                #endregion

                //使用安全加密连接
                client.EnableSsl = true;
                //不和请求一块发送
                client.UseDefaultCredentials = false;
                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                client.Credentials = new NetworkCredential(FromMail, AuthorizationCode);
                //如果发送失败，SMTP 服务器将发送 失败邮件告诉我  
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //发送
                client.Send(mailMessage);
                Console.WriteLine("发送成功");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion
    }
}