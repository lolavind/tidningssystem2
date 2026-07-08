using Microsoft.Data.SqlClient;
using PrenumerantAPI.Models;

namespace PrenumerantAPI.DAL
{
    public class PrenumerantDAL
    {
        private readonly string _connectionString;

        public PrenumerantDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Prenumerant> HamtaAlla()
        {
            var lista = new List<Prenumerant>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT * FROM tbl_prenumeranter", connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Prenumerant
                    {
                        PrenumerationsNummer = (int)reader["prenumerationsnummer"],
                        PersonNummer = reader["personnummer"].ToString()!,
                        ForNamn = reader["fornamn"].ToString()!,
                        EfterNamn = reader["efternamn"].ToString()!,
                        UtdelningsAdress = reader["utdelningsadress"].ToString()!,
                        PostNummer = reader["postnummer"].ToString()!,
                        Ort = reader["ort"].ToString()!,
                        TelefonNummer = reader["telefonnummer"].ToString()!
                    });
                }
            }
            return lista;
        }

        public Prenumerant? HamtaEnPrenumerant(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(
                    "SELECT * FROM tbl_prenumeranter WHERE prenumerationsnummer = @id",
                    connection);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Prenumerant
                    {
                        PrenumerationsNummer = (int)reader["prenumerationsnummer"],
                        PersonNummer = reader["personnummer"].ToString()!,
                        ForNamn = reader["fornamn"].ToString()!,
                        EfterNamn = reader["efternamn"].ToString()!,
                        UtdelningsAdress = reader["utdelningsadress"].ToString()!,
                        PostNummer = reader["postnummer"].ToString()!,
                        Ort = reader["ort"].ToString()!,
                        TelefonNummer = reader["telefonnummer"].ToString()!
                    };
                }
            }
            return null;
        }

        public void LaggTill(Prenumerant p)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(@"
                    INSERT INTO tbl_prenumeranter 
                    (personnummer, fornamn, efternamn, utdelningsadress, postnummer, ort, telefonnummer)
                    VALUES (@personnummer, @fornamn, @efternamn, @adress, @postnummer, @ort, @telefon)",
                    connection);

                cmd.Parameters.AddWithValue("@personnummer", p.PersonNummer);
                cmd.Parameters.AddWithValue("@fornamn", p.ForNamn);
                cmd.Parameters.AddWithValue("@efternamn", p.EfterNamn);
                cmd.Parameters.AddWithValue("@adress", p.UtdelningsAdress);
                cmd.Parameters.AddWithValue("@postnummer", p.PostNummer);
                cmd.Parameters.AddWithValue("@ort", p.Ort);
                cmd.Parameters.AddWithValue("@telefon", p.TelefonNummer);
                cmd.ExecuteNonQuery();
            }
        }

        public void Uppdatera(Prenumerant p)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(@"
                    UPDATE tbl_prenumeranter SET
                    personnummer = @personnummer,
                    fornamn = @fornamn,
                    efternamn = @efternamn,
                    utdelningsadress = @adress,
                    postnummer = @postnummer,
                    ort = @ort,
                    telefonnummer = @telefon
                    WHERE prenumerationsnummer = @id",
                    connection);

                cmd.Parameters.AddWithValue("@id", p.PrenumerationsNummer);
                cmd.Parameters.AddWithValue("@personnummer", p.PersonNummer);
                cmd.Parameters.AddWithValue("@fornamn", p.ForNamn);
                cmd.Parameters.AddWithValue("@efternamn", p.EfterNamn);
                cmd.Parameters.AddWithValue("@adress", p.UtdelningsAdress);
                cmd.Parameters.AddWithValue("@postnummer", p.PostNummer);
                cmd.Parameters.AddWithValue("@ort", p.Ort);
                cmd.Parameters.AddWithValue("@telefon", p.TelefonNummer);
                cmd.ExecuteNonQuery();
            }
        }

        public void TaBort(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(
                    "DELETE FROM tbl_prenumeranter WHERE prenumerationsnummer = @id",
                    connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}