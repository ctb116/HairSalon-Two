using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
  {
    public SpecialtyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=bradley_catherine_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }

    [TestMethod]
    public void GetAll_ReturnSpecialtiesInList_True()
    {
      Specialty newSpecialty = new Specialty("highlights");
      newSpecialty.Save();

      int result = Specialty.GetAll().Count;

      Assert.AreEqual(1, result);
    }
  }
}
