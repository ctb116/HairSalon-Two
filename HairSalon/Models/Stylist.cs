using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Stylist
    {

      public int Id { get; set; }
      public string Name { get; set; }

      public Stylist(string newName, int newId = 0)
      {
        Id = newId;
        Name = newName;
      }

      // public void AddClient(Client clientList)
      // {
      //   _clients.Add(clientList);
      // }
      //
      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@StylistName);";
        cmd.Parameters.AddWithValue("@StylistName", this.Name);

        cmd.ExecuteNonQuery();
        Id = (int) cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public static List<Stylist> GetAll()
      {
        List<Stylist> allStylists = new List<Stylist> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();

        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while (rdr.Read())
        {
          int stylistId = rdr.GetInt32(0);
          string stylistName = rdr.GetString(1);

          Stylist newStylist = new Stylist(stylistName, stylistId);
          allStylists.Add(newStylist);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allStylists;
      }

      public static Stylist Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists WHERE id = @searchId;";
        cmd.Parameters.AddWithValue("@seachId", id);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int StylistId = 0;
        string StylistName = "";

        while(rdr.Read())
        {
          StylistId = rdr.GetInt32(0);
          StylistName = rdr.GetString(1);
        }
        Stylist foundStylist = new Stylist(StylistName, StylistId);

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return foundStylist;
      }
      //
      // public List<Client> GetClients()
      // {
      //   List<Client> allStylistClients = new List<Client> {};
      //   MySqlConnection conn = DB.Connection();
      //   conn.Open();
      //   var cmd = conn.CreateCommand() as MySqlCommand;
      //   cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";
      //
      //   MySqlParameter stylistId = new MySqlParameter();
      //   stylistId.ParameterName = "@stylist_id";
      //   stylistId.Value = this._id;
      //   cmd.Parameters.Add(stylistId);
      //
      //
      //   var rdr = cmd.ExecuteReader() as MySqlDataReader;
      //   while(rdr.Read())
      //   {
      //     int clientId = rdr.GetInt32(0);
      //     string clientName = rdr.GetString(1);
      //     int clientStylistId = rdr.GetInt32(2);
      //     Client newClient = new Client(clientName, clientStylistId, clientId);
      //
      //     allStylistClients.Add(newClient);
      //   }
      //   conn.Close();
      //
      //   if (conn != null)
      //   {
      //     conn.Dispose();
      //   }
      //   return allStylistClients;
      // }

      public static void Delete(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";
        cmd.Parameters.AddWithValue("@thisId", id);

        cmd.ExecuteNonQuery();
        conn.Close();

        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public static void DeleteAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"TRUNCATE stylists;";

        cmd.ExecuteNonQuery();
        conn.Close();

        if (conn != null)
        {
          conn.Dispose();
        }
      }

    }
  }
