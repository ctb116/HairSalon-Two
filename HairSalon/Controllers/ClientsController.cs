using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using HairSalon;
using System;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Client> clientList = Client.GetAll();
      return View(clientList);
    }

    [HttpGet("/clients/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/clients")]
    public ActionResult Create(string newClientName, string newClientBirthday)
    {
      Client newClient = new Client(newClientName, Convert.ToDateTime(newClientBirthday));
      newClient.Save();



      return RedirectToAction("Index");
    }

    // [HttpGet("/stylists/{id}/clients/new")]
    // public ActionResult CreateForm(int stylistId)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Stylist stylist = Stylist.Find(stylistId);
    //   return View(stylist);
    // }
  }
}
