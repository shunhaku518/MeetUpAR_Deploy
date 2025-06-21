using MeetUpAR.Models;
using MeetUpAR.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MeetUpAR.Models.DataModels;
using Microsoft.Extensions.Options;

namespace MeetUpAR.Controllers
{
    public class MeetUpController : Controller
    {
        private readonly ILogger<MeetUpController> _logger;
        private readonly MeetUpSettings _meetUpSettings;

        public MeetUpController(ILogger<MeetUpController> logger, IOptions<MeetUpSettings> meetUpSettings)
        {
            _logger = logger;
            _meetUpSettings = meetUpSettings.Value;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            HttpContext.Session.DeleteSessionObjectByKey(nameof(MeetData));
            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ChooseLocation()
        {
            HttpContext.Session.DeleteSessionObjectByKey(nameof(MeetData));
            return View();
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ChooseLocation(MeetData model)
        {
            HttpContext.Session.SetSessionObjectAsJson(nameof(MeetData), model);
            return RedirectToAction(nameof(ChooseAR));
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ChooseAR()
        {
            MeetData? meetData = HttpContext.Session.GetSessionObjectFromJson<MeetData>(nameof(MeetData));
            if (meetData == null || string.IsNullOrEmpty(meetData.GoogleMapLatitude) || string.IsNullOrEmpty(meetData.GoogleMapLongitude))
            {
                return RedirectToAction("Index");
            }
            ARPictureChooseVM modelVM = new ARPictureChooseVM();
            modelVM.ARPictures = _meetUpSettings.ARobjs;
            return View(modelVM);
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ChooseAR(ARPictureChooseVM viewModel)
        {
            MeetData? meetData = HttpContext.Session.GetSessionObjectFromJson<MeetData>(nameof(MeetData));
            if (meetData == null || string.IsNullOrEmpty(meetData.GoogleMapLatitude) || string.IsNullOrEmpty(meetData.GoogleMapLongitude))
            {
                return RedirectToAction("Index");
            }
            ARPicture? aRPicture = viewModel.ARPictures.FirstOrDefault(x => x.IsSelected);
            if (aRPicture == null)
            {
                return RedirectToAction(nameof(ChooseAR));
            }
            meetData.ARPicture = aRPicture;
            HttpContext.Session.SetSessionObjectAsJson(nameof(MeetData), meetData);
            return RedirectToAction(nameof(ShareLocation));
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ShareLocation()
        {
            MeetData? meetData = HttpContext.Session.GetSessionObjectFromJson<MeetData>(nameof(MeetData));
            if (meetData == null || string.IsNullOrEmpty(meetData.GoogleMapLatitude) || string.IsNullOrEmpty(meetData.GoogleMapLongitude) || meetData.ARPicture == null)
            {
                return RedirectToAction("Index");
            }
            return View(meetData);
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ShowMap(string latitude, string longitude)
        {
            string redirectUrl = String.Format(_meetUpSettings.CopyLocationLink, latitude, longitude);
            ViewBag.RedirectUrl = redirectUrl;
            return View();
            //return Redirect(redirectUrl);
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ShowAR(string latitude, string longitude)
        {
            //string redirectUrl = String.Format(_meetUpSettings.CopyLocationLink, latitude, longitude);
            //ViewBag.RedirectUrl = redirectUrl;

            ViewBag.Latitude = latitude;
            ViewBag.Longitude = longitude;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
