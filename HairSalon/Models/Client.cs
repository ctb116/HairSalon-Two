using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using HairSalon;

namespace HairSalon.Models
{
  public class Client
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }

    public Client(string name, DateTime birthday, int newId =0)
    {
      Id = newId;
      Name = name;
      Birthday = birthday;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, birthday) VALUES (@ClientName, @ClientBirthday);";
      cmd.Parameters.AddWithValue("@ClientName", this.Name);
      cmd.Parameters.AddWithValue("@ClientBirthday", this.Name);

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
        DateTime clientBirthday = rdr.GetDateTime(2);

        Client newClient = new Client(clientName, clientBirthday, clientId);
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
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @seachId;";
      cmd.Parameters.AddWithValue("@seachId", id);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int clientId = 0;
      string clientName = "";
      DateTime clientBirthday = new DateTime (1111/11/11);

      while(rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        clientBirthday = rdr.GetDateTime(2);
      }
      Client foundClient = new Client(clientName, clientBirthday, clientId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
    }

    public void Edit(string newName, DateTime newBirthday)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET (name, birthday) = (@newName, @newBirthday) WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@searchId", Id);
      cmd.Parameters.AddWithValue("@newName", newName);
      cmd.Parameters.AddWithValue("@newBirthday", newBirthday);

      cmd.ExecuteNonQuery();
      Name = newName;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }


    public static void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

        //remove second and third DELETE command to allow StylistTests to work
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @thisID;";
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
