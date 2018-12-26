using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gianhang.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FileManager()
        {
            return View();
        }

        public ActionResult ImageSlider()
        {
            return View();
        }

        public ActionResult Captcha()
        {
            ViewBag.ok = "";
            return View();
        }
        public ActionResult HtmlEditorPartial()
        {
            return PartialView("_HtmlEditorPartial");
        }
        public ActionResult HtmlEditorPartialImageSelectorUpload()
        {
            HtmlEditorExtension.SaveUploadedImage("HtmlEditor", HomeControllerHtmlEditorSettings.ImageSelectorSettings);
            return null;
        }
        public ActionResult HtmlEditorPartialImageUpload()
        {
            HtmlEditorExtension.SaveUploadedFile("HtmlEditor", HomeControllerHtmlEditorSettings.ImageUploadValidationSettings, HomeControllerHtmlEditorSettings.ImageUploadDirectory);
            return null;
        }
        

        [ValidateInput(false)]
        public ActionResult FileManagerPartial()
        {
            return PartialView("_FileManagerPartial", HomeControllerFileManagerSettings.Model);
        }

        public FileStreamResult FileManagerPartialDownload()
        {
            return FileManagerExtension.DownloadFiles(HomeControllerFileManagerSettings.DownloadSettings, HomeControllerFileManagerSettings.Model);
        }

        [ValidateInput(false)]
        public ActionResult CaptchaPartial()
        {
            return PartialView("_CaptchaPartial");
        }

        [HttpPost]
        public ActionResult CaptchaPatial(FormCollection collection)
        {
            if (CaptchaExtension.GetIsValid("Captcha"))
            {
                ViewBag.ok = "Successfull";
                return View("Captcha");
            }
            else {
                ViewBag.ok = "NOT Successfull";
                return View("Captcha");
            }
        }
    }
    public class HomeControllerFileManagerSettings
    {
        public const string RootFolder = @"~\Content\Files";

        public static string Model { get { return RootFolder; } }
        public static DevExpress.Web.Mvc.FileManagerSettings DownloadSettings
        {
            get
            {
                var settings = new DevExpress.Web.Mvc.FileManagerSettings { Name = "FileManager" };
                settings.SettingsEditing.AllowDownload = true;
                return settings;
            }
        }
    }

    public class HomeControllerHtmlEditorSettings
    {
        public const string ImageUploadDirectory = "~/Content/UploadImages/";
        public const string ImageSelectorThumbnailDirectory = "~/Content/Thumb/";

        public static DevExpress.Web.UploadControlValidationSettings ImageUploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".png" },
            MaxFileSize = 4000000
        };

        static DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings imageSelectorSettings;
        public static DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings ImageSelectorSettings
        {
            get
            {
                if (imageSelectorSettings == null)
                {
                    imageSelectorSettings = new DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings(null);
                    imageSelectorSettings.Enabled = true;
                    imageSelectorSettings.UploadCallbackRouteValues = new { Controller = "Home", Action = "HtmlEditorPartialImageSelectorUpload" };
                    imageSelectorSettings.CommonSettings.RootFolder = ImageUploadDirectory;
                    imageSelectorSettings.CommonSettings.ThumbnailFolder = ImageSelectorThumbnailDirectory;
                    imageSelectorSettings.CommonSettings.AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif" };
                    imageSelectorSettings.UploadSettings.Enabled = true;
                }
                return imageSelectorSettings;
            }
        }

        
    }

}