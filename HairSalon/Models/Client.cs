using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylistId;

    public Client(string newName, int newStylistId, int newId =0)
    {
      _id = newId;
      _name = newName;
      _stylistId = newStylistId;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@ClientName, @stylist_id);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@ClientName";
      name.Value = _name;
      cmd.Parameters.Add(name);

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylist_id";
      stylistId.Value = this._stylistId;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

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
        int clientStylistId = rdr.GetInt32(2);

        Client newClient = new Client(clientName, clientStylistId, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = (@thisId);";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int ClientId = 0;
      string ClientName = "";
      int ClientStylistId = 0;

      while (rdr.Read())
      {
        ClientId = rdr.GetInt32(0);
        ClientName = rdr.GetString(1);
        ClientStylistId = rdr.GetInt32(2);
      }

      Client foundClient = new Client(ClientName, ClientStylistId, ClientId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundClient;
    }


  }
}
