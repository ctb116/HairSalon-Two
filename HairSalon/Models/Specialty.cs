using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Specialty
    {

      public int Id { get; set; }
      public string Type { get; set; }

      public Specialty(string newType, int newId = 0)
      {
        Id = newId;
        Type = newType;
      }

      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO specialties (type) VALUES (@SpecialtyType);";
        cmd.Parameters.AddWithValue("@SpecialtyType", this.Type);

        cmd.ExecuteNonQuery();
        Id = (int) cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public static List<Specialty> GetAll()
      {
        List<Specialty> allSpecialties = new List<Specialty> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();

        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialties;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while (rdr.Read())
        {
          int specialtyId = rdr.GetInt32(0);
          string specialtyType = rdr.GetString(1);

          Specialty newSpecialty = new Specialty(specialtyType, specialtyId);
          allSpecialties.Add(newSpecialty);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allSpecialties;
      }

      public static Specialty Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialties WHERE id = @seachId;";
        cmd.Parameters.AddWithValue("@seachId", id);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        int SpecialtyId = 0;
        string SpecialtyType = "";

        while(rdr.Read())
        {
          SpecialtyId = rdr.GetInt32(0);
          SpecialtyType = rdr.GetString(1);
        }
        Specialty foundSpecialty = new Specialty(SpecialtyType, SpecialtyId);

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return foundSpecialty;
      }

      public List<Stylist> GetStylists()
      {
        List<Stylist> allSpecialtyStylists = new List<Stylist> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT stylists.* FROM specialties
                            JOIN stylist_specialty ON (specialties.id = stylist_specialty.specialties_id)
                            JOIN stylists ON (stylist_specialty.stylist_id = stylists.id)
                            WHERE specialties.id = @specialtiesId;";
        cmd.Parameters.AddWithValue("@specialtiesId", Id);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int stylistId = rdr.GetInt32(0);
          string stylistName = rdr.GetString(1);

          Stylist newStylist = new Stylist(stylistName, stylistId);

          allSpecialtyStylists.Add(newStylist);
        }
        conn.Close();

        if (conn != null)
        {
          conn.Dispose();
        }
        return allSpecialtyStylists;
      }

      public static void DeleteAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM specialties;";

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
        cmd.CommandText = @"TRUNCATE specialties;";
        cmd.ExecuteNonQuery();
        conn.Close();

        if (conn != null)
        {
          conn.Dispose();
        }
      }
    }
  }
