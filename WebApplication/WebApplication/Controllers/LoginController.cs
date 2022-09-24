using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private string systemConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["System"].ConnectionString;
        // GET: Login
        public ActionResult Index()
        {
            bool username = false; //帳號是否存在
            bool password = false; //密碼是否存在
            bool ip = false; //IP是否存在
            bool gp08 = false; //網站登入權限是否存在

            if (Session["username"] != null && Session["username"].ToString().Trim() != "") //如果帳號存在於Session
            {
                username = true; //帳號存在
            }
            if (Session["password"] != null && Session["password"].ToString().Trim() != "") //如果密碼存在於Session
            {
                password = true; //密碼存在
            }
            if (Session["ip"] != null && Session["ip"].ToString().Trim() != "") //如果IP存在於Session
            {
                ip = true; //IP存在
            }
            if (Session["gp08"] != null && Session["gp08"].ToString().Trim() != "") //如果網站登入權限存在於Session
            {
                gp08 = true; //網站登入權限存在
            }

            if(username && password && ip && gp08) //以上皆存在
            {
                return RedirectToAction("Index", "Home"); //進入首頁
            }
            else
            {
                return View(); //進入登入畫面
            }      
        }

        [HttpPost]
        public ActionResult Login(FormCollection post) /*登入驗證*/
        {
            Models.LogModel logContext = new Models.LogModel(); //Log DBContext
            Models.Log Log = new Models.Log(); //宣告Log Model

            string username = post["username"]; //帳號(明碼)
            string password = post["password"]; //密碼(明碼)

            string usernameMD5 = Function.MD5(username); //帳號(MD5)
            string passwordMD5 = Function.MD5(password); //密碼(MD5)

            string clientIP = Function.ClientIP(); //客戶端IP

            Models.AccountModel accountContext = new Models.AccountModel(); //Account DBContext
            var accountList = accountContext.Account.ToList(); //Account Table
            var account = (from data in accountList
                           where Function.MD5(data.ac01) == usernameMD5
                           select data
                          ).ToList(); //查詢帳號Row

            if (account.Count == 1) //帳號是否存在，且只查詢一列
            {
                var ac11 = account[0].ac11; //帳號權限群組代碼

                string sql = $"SELECT * FROM [System].[dbo].[Group] WHERE gp01 = '{ac11}'"; //查詢權限SQL
                DataTable group = Function.SQLServer(systemConnectionString, sql); //向SQL Server下指令

                bool permission = false; //是否有權限
                bool gp08 = false; //網站登入權限
                bool gp09 = false; //系統日誌開關

                if (group.Rows.Count == 1) //權限是否存在，且只查詢一列
                {
                    permission = true; //權限存在
                    gp08 = Convert.ToBoolean(group.Rows[0]["gp08"]); //擁有網站登入權限
                    gp09 = Convert.ToBoolean(group.Rows[0]["gp09"]); //系統日誌開啟
                }

                Log.lg01 = username; //操作帳號
                Log.lg02 = DateTime.Now; //操作時間
                Log.lg03 = "Login/Login"; //URL
                Log.lg04 = "登入驗證"; //事件名稱
                Log.lg05 = clientIP; //IP   

                if (account[0].ac07 == passwordMD5) //密碼正確
                {

                    if (Convert.ToBoolean(account[0].ac08))
                    {

                        if (permission) //權限是否存在
                        {

                            if (gp08) //擁有網站登入權限
                            {
                                Session.Timeout = 30; //更改Session過期時間，預設為20分鐘
                                Session["username"] = username; //將帳號放入Session
                                Session["password"] = passwordMD5; //將密碼(MD5)放入Session
                                Session["ip"] = clientIP; //將客戶端IP放入Session
                                for (int i = 8; i <= group.Columns.Count; i++) //主權限數量迴圈
                                {
                                    Session["gp" + i.ToString("00")] = Convert.ToBoolean(group.Rows[0]["gp" + i.ToString("00")]); //將所有權限放入Session
                                }

                                Log.lg06 = "登入成功"; //備註

                                if (gp09)
                                {
                                    logContext.Log.Add(Log); //加入集合
                                    logContext.SaveChanges(); //儲存變更
                                }

                                return RedirectToAction("Index", "Home"); //進入首頁
                            }
                            else //無網站登入權限
                            {
                                TempData["message"] = "此用戶無權限登入。"; //Alert訊息
                            }
                        }
                        else //權限不存在
                        {
                            TempData["message"] = "此用戶權限群組無效。"; //Alert訊息
                        }
                    }
                    else
                    {
                        TempData["message"] = "此用戶已遭到鎖定。"; //Alert訊息
                    }
                }
                else //密碼錯誤
                {
                    TempData["message"] = "密碼或帳號錯誤。"; //Alert訊息
                }

                Log.lg06 = TempData["message"].ToString(); //備註

                if (gp09) //如果啟用系統日誌開關
                {  
                    logContext.Log.Add(Log); //加入集合
                    logContext.SaveChanges(); //儲存變更
                }
            }
            else //帳號不存在
            {
                TempData["message"] = "帳號或密碼錯誤。"; //Alert訊息      
            }

            return RedirectToAction("Index", "Login"); //跳回原頁面
        }

        [HttpPost]
        public ActionResult Register(FormCollection post) /*註冊*/
        {
            Models.LogModel logContext = new Models.LogModel(); //Log DBContext
            Models.Log Log = new Models.Log(); //宣告Log Model

            string username = post["username"]; //帳號 (名碼)
            string password = post["password"]; //密碼 (名碼)
            string chkpassword = post["chkpassword"]; //確認密碼 (名碼)
            string lastname = post["lastname"]; //姓氏
            string firstname = post["firstname"]; //名字

            string passwordMD5 = Function.MD5(password); //密碼(MD5)
            string clientIP = Function.ClientIP(); //客戶端IP

            if (password == chkpassword) //判斷密碼與確認密碼是否相符
            {
                Models.AccountModel accountContext = new Models.AccountModel(); //Account DBContext
                var accountList = accountContext.Account.ToList(); //Account Table
                var account = (from data in accountList
                               where data.ac01.ToLower() == username.ToLower()
                               select data
                              ).ToList(); //查詢帳號Row

                if(account.Count == 0) //判斷帳號是否存在
                {
                    Models.Account Accounts = new Models.Account(); //宣告Account Model
                    Accounts.ac01 = username; //帳號
                    Accounts.ac02 = username; //建檔人員
                    Accounts.ac03 = DateTime.Now; //建檔時間
                    Accounts.ac04 = username; //修改人員
                    Accounts.ac05 = DateTime.Now; //修改時間
                    Accounts.ac06 = ""; //備註
                    Accounts.ac07 = passwordMD5; //密碼
                    Accounts.ac08 = true; //帳號狀態
                    Accounts.ac09 = lastname; //姓氏
                    Accounts.ac10 = firstname; //名字
                    Accounts.ac11 = 2; //權限群組代碼
                    accountContext.Account.Add(Accounts); //加入集合
                    accountContext.SaveChanges(); //儲存變更

                    TempData["message"] = "註冊成功。"; //Alert訊息

                    Log.lg01 = username; //操作帳號
                    Log.lg02 = DateTime.Now; //操作時間
                    Log.lg03 = "Login/Register"; //URL
                    Log.lg04 = "註冊"; //事件名稱
                    Log.lg05 = clientIP; //IP   
                    Log.lg06 = TempData["message"].ToString(); //備註
                    logContext.Log.Add(Log); //加入集合
                    logContext.SaveChanges(); //儲存變更
                }
                else
                {
                    TempData["message"] = "帳號已存在。"; //Alert訊息
                }
            }
            else
            {
                TempData["message"] = "確認密碼與密碼不相符。"; //Alert訊息  
            }

            return RedirectToAction("Index", "Login"); //跳回原頁面
        }
    }
}