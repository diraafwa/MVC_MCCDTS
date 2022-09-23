using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4MCC.Models
{//harus ada CRUD
    public class KaryawanController : Controller
    {
        SqlConnection sqlConnection;

        /*
         * Data Source -> Server
         * Initial Catalog -> Database
         * User ID -> username
         * Password -> password
         * Connect Timeout
         */
        string connectionString =
            "Data Source = DESKTOP-AFA1311; Initial Catalog = aspdb; User ID = MCCDTS; Password = 12345;";

        //READ
        public IActionResult Index()
        {
            string query = "SELECT * FROM karyawan";

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            List<Karyawan> karyawans = new List<Karyawan>();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Karyawan karyawan = new Karyawan();
                            karyawan.KaryawanID = Convert.ToInt32(sqlDataReader[0]);
                            karyawan.Nik = sqlDataReader[1].ToString();
                            karyawan.Nama = sqlDataReader[2].ToString();
                            karyawans.Add(karyawan);
                        }
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return View(karyawans);
        }
     //CREATE -> get & post

        //GET
        public IActionResult Create()
        {

            return View(new Karyawan());
        }

        //POST-> melakukan pengiriman data menggunakan model karyawan
        [HttpPost]
        public IActionResult Create(Karyawan karyawan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                
                SqlParameter sqlParameter1 = new SqlParameter();
                sqlParameter1.ParameterName = "@karyawanid";
                sqlParameter1.Value = karyawan.KaryawanID;

                SqlParameter sqlParameter2 = new SqlParameter();
                sqlParameter2.ParameterName = "@nik";
                sqlParameter2.Value = karyawan.Nik;

                SqlParameter sqlParameter3 = new SqlParameter();
                sqlParameter3.ParameterName = "@nama";
                sqlParameter3.Value = karyawan.Nama;

                cmd.Parameters.Add(sqlParameter1);
                cmd.Parameters.Add(sqlParameter2);
                cmd.Parameters.Add(sqlParameter3);

                try
                {
                    cmd.CommandText = "INSERT INTO Karyawan VALUES (@karyawanid, @nik, @nama)";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }

        //UPDATE -> get & post
        //get
        public IActionResult Update()
        {

            return View(new Karyawan());
        }
        //DELETE -> get & post
    }
}
