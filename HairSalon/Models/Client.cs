using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Client(string newName, int newId =0)
    {
      Id = newId;
      Name = newName;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name) VALUES (@ClientName);";
      cmd.Parameters.AddWithValue("@ClientName", this.Name);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);

        Client newClient = new Client(clientName, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
    //
    // public static Client Find(int id)
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT * FROM clients WHERE id = (@thisId);";
    //
    //   MySqlParameter thisId = new MySqlParameter();
    //   thisId.ParameterName = "@thisId";
    //   thisId.Value = id;
    //   cmd.Parameters.Add(thisId);
    //
    //   var rdr = cmd.ExecuteReader() as MySqlDataReader;
    //
    //   int ClientId = 0;
    //   string ClientName = "";
    //   int ClientStylistId = 0;
    //
    //   while (rdr.Read())
    //   {
    //     ClientId = rdr.GetInt32(0);
    //     ClientName = rdr.GetString(1);
    //     ClientStylistId = rdr.GetInt32(2);
    //   }
    //
    //   Client foundClient = new Client(ClientName, ClientStylistId, ClientId);
    //
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //
    //   return foundClient;
    // }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
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
      cmd.CommandText = @"TRUNCATE clients;";
      cmd.ExecuteNonQuery();
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
