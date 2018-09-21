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

    [HttpGet("/stylists/{id}/clients/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Details(int stylistId, int id)
    {
       Client client = Client.Find(id);
       Dictionary<string, object> model = new Dictionary<string, object>();
       Stylist stylist = Stylist.Find(stylistId);
       model.Add("clients", client);
       model.Add("stylists", stylist);
       return View(client);
    }

  }
}
