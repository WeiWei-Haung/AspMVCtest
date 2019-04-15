using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AspMVCtest.Models
{
    public class MyDataBase
    {
        public List<City> GetCityList()
        {
            try
            {
                string connString = "server=127.0.0.1;port=3306;user id=root;password=;database=aspmvctest;charset=utf8;"; //建立資料庫連現線字串
                MySqlConnection conn = new MySqlConnection();

                conn.ConnectionString = connString; //將連線跟字串連結起來

                string sql = @"SELECT * FROM `city`";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                List<City> list = new List<City>();

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        City city = new City();
                        city.CityId = dr["id"].ToString();
                        city.CityName = dr["city"].ToString();
                        list.Add(city);
                    }
                }

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                return list;
            }
            catch (Exception ex)
            {
                String error = ex.ToString();
                return null;
            }

            
            
        }
    }
}