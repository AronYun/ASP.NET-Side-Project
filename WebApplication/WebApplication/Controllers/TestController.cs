using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1A()
        {
            return View("IndexHello"); //呼叫相同目錄底下，檔名為IndexHello的View(.cshtml)。
        }

        public ActionResult Index1B()
        {
            return View("~/Views/Test/IndexHello.cshtml"); //呼叫該路徑的View(.cshtml)。
        }

        public string Index1C()
        {
            return "回傳一個字串，<h3>String</h3>"; //頁面會顯示此字串
        }

        public ActionResult Index1D()
        {
            return Content("回傳一個字串，<h3>Content</h3>"); //頁面會顯示此字串
            //return Content("回傳一個字串，<h3>Content</h3>", "text/Plain", System.Text.Encoding.UTF8);
        }

        public ActionResult Index1E()
        {
            return Redirect("https://www.google.com.tw/"); //導向其他網頁
            //return Redirect("Index1A");
        }

        protected override void HandleUnknownAction(string actionName) //找不到Action導向
        {
            Response.Redirect("https://tw.yahoo.com/"); 
            base.HandleUnknownAction(actionName);
        }

        public ActionResult Index3()
        {
            ViewData["A"] = "數據：ViewData[A]";
            ViewBag.B = "數據：ViewBag.B";
            TempData["C"] = "數據：TempData[C]";

            return View();
        }

        public ActionResult Index3A()
        {
            ViewData["XYZ"] = "您好，我是ViewData[XYZ]";
            ViewBag.XYZ = "抱歉！改了內容，我是ViewBag.XYZ"; //會覆蓋值

            return View();
        }
    }
}