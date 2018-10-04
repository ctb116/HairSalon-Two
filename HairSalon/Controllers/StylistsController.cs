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
    public ActionResult Create(string newStylistName, int SpecialtyId )
    {
      Stylist userInput = new Stylist(newStylistName);
      userInput.Save();
      userInput.AddSpecialty(SpecialtyId);

      //Redirects to Index() after posting. This prevents the bug of adding the same items when refreshing after posting. Index doesn't refer to the index.cshtml but rather the function called Index() in this controller. You can add a second parameter after this to specify which controller if needed.
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}")]
    [HttpPost("/stylists/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);

      List<Client> stylistClients = selectedStylist.GetClients();
      List<Specialty> stylistSpecialty = selectedStylist.GetSpecialties();
      model.Add("stylists", selectedStylist);
      model.Add("clients", stylistClients);
      model.Add("specialties", stylistSpecialty);
      return View("Details", model);
    }

    [HttpGet("stylists/{id}/addspecial")]
    public ActionResult AddSpecialty(int id)
    {
      Stylist findStylist = Stylist.Find(id);
      return View(findStylist);
    }

    [HttpPost("stylists/{id}/addspecial")]
    public ActionResult Special(int id, int SpecialtyId)
    {
      Stylist findStylist = Stylist.Find(id);
      findStylist.AddSpecialty(SpecialtyId);
      return RedirectToAction("Index");
    }

    [HttpGet("stylists/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Stylist editStylist = Stylist.Find(id);
      return View(editStylist);
    }


    [HttpPost("stylists/{id}/edit")]
    public ActionResult Edit(int id, string newName)
    {
      Stylist editStylist = Stylist.Find(id);
      editStylist.Edit(newName);
      return RedirectToAction("Index");
    }

    [HttpPost("/stylists/{id}/delete")]
    public ActionResult Delete(int id)
    {
        Stylist thisStylist = Stylist.Find(id);
        Stylist.Delete(thisStylist.Id);

        return RedirectToAction("Index");
    }

    [HttpPost("/stylists/deleteall")]
    public ActionResult DeleteAll()
    {
        Stylist.DeleteAll();

        return RedirectToAction("Index");
    }
  }
}
