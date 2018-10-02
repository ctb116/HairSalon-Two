using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=bradley_catherine_test;Convert Zero Datetime=True;";
    }
    public void Dispose()
    {
      Client.Truncate();
    }

    [TestMethod]
    public void GetAll_ReturnClientsInList_True()
    {
      Client newClient = new Client("Bane", Convert.ToDateTime("2000-07-21"));
      newClient.Save();
      Console.WriteLine(newClient.Birthday);

      int result = Client.GetAll().Count;

      Assert.AreEqual(1, result);
    }
    [TestMethod]
    public void Save_ReturnClientNameAndBirthday_True()
    {
      Client newClient = new Client("Bane", Convert.ToDateTime("7/21/2000"));
      newClient.Save();
      Console.WriteLine(newClient.Birthday);

      DateTime result = Client.Find(newClient.Id).Birthday;
      

      string testname = Client.Find(newClient.Id).Name;
      Console.WriteLine(testname);


      Assert.AreEqual(Convert.ToDateTime("7/21/2000"), result);
    }
  }
}
