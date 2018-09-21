using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    [HttpGet("/stylists/{id}/clients")]
    public ActionResult Index()
    {
      List<Client> clientList = Client.GetAll();
      return View(clientList);
    }
  }
}
