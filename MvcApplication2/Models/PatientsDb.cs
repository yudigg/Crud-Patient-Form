using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcApplication2.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Insurance { get; set; }
    }
    public class PatientsDb
    {
        readonly string _connection;
        public PatientsDb(string connection)
        {
            _connection = connection;
        }
        public IEnumerable<Patient> GetAllPatients()
        {
            List<Patient> result = new List<Patient>();
            using (SqlConnection conn = new SqlConnection(_connection))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select * from patients";
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Patient p = new Patient();
                    p.Name = (string)reader["name"];
                    p.Id = (int)reader["Id"];
                    object age = reader["age"];
                    if (age != DBNull.Value)
                    {
                        p.Age = (int?)age;
                    }
                    p.Insurance = (string)reader["insurance"];

                    result.Add(p);
                }
                return result;
            }
        }
        public void AddPatient(string name, string insurance, int age)
        {
            List<Patient> result = new List<Patient>();
            using (SqlConnection conn = new SqlConnection(_connection))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"insert into Patients (Name,Age,Insurance)
                                    values(@name,@age,@insurance)";
                cmd.Parameters.AddWithValue("@name", name);
                if (age == null)
                {
                    cmd.Parameters.AddWithValue("@age", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@age", age);
                }
                cmd.Parameters.AddWithValue("@insurance", insurance);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public Patient GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connection))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                Patient p = new Patient();
                cmd.CommandText = "select * from Patients where id = @id ";
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    p.Name = (string)reader["name"];
                    p.Id = (int)reader["Id"];
                    object age = reader["age"];
                    if (age != DBNull.Value)
                    {
                        p.Age = (int?)age;
                    }
                    p.Insurance = (string)reader["insurance"];
                }
                return p;
            }

        }
        public void Update(string name, int age, string insurance, int id)
        {
            using (SqlConnection conn = new SqlConnection(_connection))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                Patient p = new Patient();
                cmd.CommandText = @"UPDATE Patients SET NAME = @NAME,
                                  AGE = @AGE,
                                  INSURANCE = @INSURANCE 
                                   where id = @id ";
                cmd.Parameters.AddWithValue("@NAME", name);
                cmd.Parameters.AddWithValue("@AGE", age);
                cmd.Parameters.AddWithValue("@INSURANCE", insurance);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connection))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "Delete from Patients where id = @id ";
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
