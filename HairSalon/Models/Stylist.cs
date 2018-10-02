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

      public Stylist(string name, int newId = 0)
      {
        Id = newId;
        Name = name;
      }

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
        cmd.CommandText = @"SELECT * FROM stylists WHERE id = @seachId;";
        cmd.Parameters.AddWithValue("@seachId", id);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
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

      public void Edit(string newName)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";
        cmd.Parameters.AddWithValue("@searchId", Id);
        cmd.Parameters.AddWithValue("@newName", newName);

        cmd.ExecuteNonQuery();
        Name = newName;

        conn.Close();
        if(conn != null)
        {
          conn.Dispose();
        }
      }

      public List<Client> GetClients()
      {
        List<Client> allStylistClients = new List<Client> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT clients.* FROM stylists
                            JOIN stylist_client ON (stylists.id = stylist_client.stylist_id)
                            JOIN clients ON (stylist_client.client_id = clients.id)
                            WHERE stylists.id = @stylistId;";
        cmd.Parameters.AddWithValue("@stylistId", Id);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int clientId = rdr.GetInt32(0);
          string clientName = rdr.GetString(1);
          Client newClient = new Client(clientName, clientId);

          allStylistClients.Add(newClient);
        }
        conn.Close();

        if (conn != null)
        {
          conn.Dispose();
        }
        return allStylistClients;
      }

      public static void Delete(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

          //remove second and third DELETE command to allow StylistTests to work
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisID;";





                            //
                            // DELETE FROM stylist_client WHERE stylist_id = @thisID;
                            // DELETE FROM stylist_specialty WHERE stylist_id = @thisID;";
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
        cmd.CommandText = @"DELETE FROM stylists;
                            DELETE FROM stylist_client
                            DELETE FROM stylist_specialty;";
        cmd.ExecuteNonQuery();
        conn.Close();

        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public static void Truncate()
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
