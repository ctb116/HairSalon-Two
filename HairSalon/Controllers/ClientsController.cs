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
    public ActionResult Create(string newClientName, string newClientBirthday, int StylistId)
    {
      Client newClient = new Client(newClientName, Convert.ToDateTime(newClientBirthday));
      newClient.Save();
      newClient.AddStylist(StylistId);
      return RedirectToAction("Index");
    }

    [HttpGet("clients/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Client editClient = Client.Find(id);
      return View(editClient);
    }

    [HttpPost("clients/{id}/edit")]
    public ActionResult Edit(int id, string newName, DateTime newBirthday)
    {
      Client editClient = Client.Find(id);
      editClient.Edit(newName, newBirthday);
      return RedirectToAction("Index");
    }

    [HttpPost("/clients/{id}/delete")]
    public ActionResult Delete(int id)
    {
        Client thisClient = Client.Find(id);
        Client.Delete(thisClient.Id);

        return RedirectToAction("Index");
    }

    [HttpPost("/clients/deleteall")]
    public ActionResult DeleteAll()
    {
        Client.DeleteAll();

        return RedirectToAction("Index");
    }
  }
}
