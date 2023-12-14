using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Etlap
{
    public class EtlapService
    {
        MySqlConnection connection;

        public EtlapService()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "etlapdb";

            connection = new MySqlConnection(builder.ConnectionString);
        }

        private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public List<Etel> GetAll()
        {
            List<Etel> etlap = new List<Etel>();
            OpenConnection();
            string sql = "SELECT * FROM etlap";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Etel etel = new Etel();
                    etel.Id = reader.GetInt32("id");
                    etel.Nev = reader.GetString("nev");
                    etel.Leiras = reader.GetString("leiras");
                    etel.Ar = reader.GetInt32("ar");
                    etel.Kategoria = reader.GetString("kategoria");
                    etlap.Add(etel);
                }
            }
            CloseConnection();
            return etlap;
        }

        //CREATE
        public bool Create(Etel etel)
        {
            OpenConnection();
            string sql = "INSERT INTO etlap(nev,leiras,ar,kategoria) VALUES(@nev,@leiras,@ar,@kategoria)";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@nev", etel.Nev);
            command.Parameters.AddWithValue("@leiras", etel.Leiras);
            command.Parameters.AddWithValue("@ar", etel.Ar);
            command.Parameters.AddWithValue("@kategoria", etel.Kategoria);
            int erintettSorok = command.ExecuteNonQuery();
            CloseConnection();
            return erintettSorok == 1;
        }

        //UPDATEEGYELEMSZAZALEK
        public bool UpdateEgyElemSzazalek(int id, Etel ujEtel)
        {
            OpenConnection();
            string sql = "UPDATE etlap SET nev = @nev,leiras = @leiras,ar = @ar,kategoria = @kategoria WHERE etlap.id = @id";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@nev", ujEtel.Nev);
            command.Parameters.AddWithValue("@leiras", ujEtel.Leiras);
            command.Parameters.AddWithValue("@ar", ujEtel.Ar);
            command.Parameters.AddWithValue("@kategoria", ujEtel.Kategoria);
            int erintettSorok = command.ExecuteNonQuery();
            CloseConnection();
            return erintettSorok == 1;
        }

        //UPDATEEGYELEMFORINT
        public bool UpdateEgyElemForint(int id, Etel ujEtel)
        {
            OpenConnection();
            string sql = "UPDATE etlap SET nev = @nev,leiras = @leiras,ar = @ar,kategoria = @kategoria WHERE etlap.id = @id";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@nev", ujEtel.Nev);
            command.Parameters.AddWithValue("@leiras", ujEtel.Leiras);
            command.Parameters.AddWithValue("@ar", ujEtel.Ar);
            command.Parameters.AddWithValue("@kategoria", ujEtel.Kategoria);
            int erintettSorok = command.ExecuteNonQuery();
            CloseConnection();
            return erintettSorok == 1;
        }

        //UPDATEOSSZESSZAZALEK
        public bool UpdateOsszesSzazalek(double ertek)
        {
            OpenConnection();
            string sql = "UPDATE etlap SET ar = ar*(1+(@ertek/100))";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@ertek", ertek);
            int erintettSorok = command.ExecuteNonQuery();
            CloseConnection();
            return erintettSorok != 0;
        }

        //UPDATEOSSZESFORINT
        public bool UpdateOsszesForint(double ertek)
        {
            OpenConnection();
            string sql = "UPDATE etlap SET ar = ar + @ertek";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@ertek", ertek);
            int erintettSorok = command.ExecuteNonQuery();
            CloseConnection();
            return erintettSorok != 0;
        }

        //DELETE
        public bool Delete(int id)
        {
            OpenConnection();
            string sql = $"DELETE FROM etlap WHERE id = @id";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);
            int erintettSorok = command.ExecuteNonQuery();
            CloseConnection();
            return erintettSorok == 1;
        }

    }
}
