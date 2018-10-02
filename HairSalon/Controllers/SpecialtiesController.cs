using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class SpecialtiesController : Controller
  {
    [HttpGet("/specialties")]
    public ActionResult Index()
    {
      List<Specialty> specialtyList = Specialty.GetAll();
      return View(specialtyList);
    }

    [HttpGet("/specialties/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/specialties")]
    public ActionResult Create(string newSpecialtyName)
    {

      Specialty userInput = new Specialty(newSpecialtyName);
      userInput.Save();

      return RedirectToAction("Index");
    }
  }
}
