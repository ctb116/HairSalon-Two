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
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=bradley_catherine_test;";
    }
    public void Dispose()
    {
      Client.Truncate();
    }

    [TestMethod]
    public void GetAll_ReturnClientsInList_True()
    {
      Client newClient = new Client("Bane");
      newClient.Save();

      int result = Client.GetAll().Count;

      Assert.AreEqual(1, result);
    }
  }
}
