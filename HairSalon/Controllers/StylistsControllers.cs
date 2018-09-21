
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> stylistList = Stylist.GetAll();
      return View(stylistList);
    }

    [HttpGet("/stylists/new")]
    public ActionResult CreateForm()
    {
      return View();
    }


    [HttpPost("/stylists")]
    public ActionResult IndexUpdate()
    {
      Stylist userInput = new Stylist(Request.Form["newStylistName"]);
      userInput.Save();

      //Redirects to Index() after posting. This prevents the bug of adding the same items when refreshing after posting. Index doesn't refer to the index.cshtml but rather the function called Index() in this controller. You can add a second parameter after this to specify which controller if needed.
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Details(int id)
    {
      // Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      // List<MagicCard> categoryCards = selectedStylist.GetMagicCards();
      // model.Add("category", selectedStylist);
      // model.Add("magiccards", categoryCards);
      return View("Details");
    }
  }
}
