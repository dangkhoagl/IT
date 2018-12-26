using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXWebApplication2.Models;

namespace DXWebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView

            return View(NorthwindDataProvider.GetCustomers());    
            
        }

        public ActionResult GridViewPartialView() 
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView", NorthwindDataProvider.GetCustomers());
        }


        public ActionResult HtmlEditor2Partial()
        {
            return PartialView("_HtmlEditor2Partial");
        }
        public ActionResult HtmlEditor2PartialImageSelectorUpload()
        {
            HtmlEditorExtension.SaveUploadedImage("HtmlEditor2", HomeControllerHtmlEditor2Settings.ImageSelectorSettings);
            return null;
        }
        public ActionResult HtmlEditor2PartialImageUpload()
        {
            HtmlEditorExtension.SaveUploadedFile("HtmlEditor2", HomeControllerHtmlEditor2Settings.ImageUploadValidationSettings, HomeControllerHtmlEditor2Settings.ImageUploadDirectory);
            return null;
        }

        [ValidateInput(false)]
        public ActionResult FileManagerPartial()
        {
            return PartialView("_FileManagerPartial", HomeControllerFileManagerSettings.Model);
        }

        public FileStreamResult FileManagerPartialDownload()
        {
            return FileManagerExtension.DownloadFiles("FileManager", HomeControllerFileManagerSettings.Model);
        }
    }
    public class HomeControllerFileManagerSettings
    {
        public const string RootFolder = @"~\Content\Files";

        public static string Model { get { return RootFolder; } }
    }

    public class HomeControllerHtmlEditor2Settings
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
                    imageSelectorSettings.UploadCallbackRouteValues = new { Controller = "Home", Action = "HtmlEditor2PartialImageSelectorUpload" };
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