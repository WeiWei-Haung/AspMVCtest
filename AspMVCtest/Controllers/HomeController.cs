using AspMVCtest.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMVCtest.Controllers
{
    public class HomeController : Controller
    {
       


        public class Student
        {
            public string id { get; set; }
            public string name { get; set; }
            public int score { get; set; }

            public Student()
            {
                id = string.Empty;
                name = string.Empty;
                score = 0;
            }
            public Student(string _id, string _name, int _score)
            {
                id = _id;
                name = _name;
                score = _score;
            }

            public override string ToString()
            {
                return $"學號:{id},姓名:{name},分數:{score}.";
            }



        }
        public ActionResult Index()
        {
            ViewBag.title = "hello world";

            DateTime date = DateTime.Now; //顯示現在時間
            Student data = new Student(); //建立student類別的物件
            List<Student> list = new List<Student>(); //建立student類別的list陣列
            list.Add(new Student("1", "小明", 80));
            list.Add(new Student("2", "小華", 70));
            list.Add(new Student("3", "小英", 60));
            list.Add(new Student("4", "小李", 50));
            list.Add(new Student("5", "小張", 90));

            ViewBag.Date = date;
            ViewBag.Student = data;
            ViewBag.List = list;
            return View();


        }

        public ActionResult Day8() //學習透過MODEL進行資料前後端傳送
        {
            DateTime date = DateTime.Now;
            ViewBag.Date = date;

            Student data = new Student("1","AAA", 80);
            return View(data);
        }


        //---------------------------------------------------------------------


        public ActionResult Day10() //學習透過GET 進行資料傳送
        {
            DateTime date = DateTime.Now;
            ViewBag.Date = date;

            Student data = new Student("1", "AAA", 80);
            return View(data);
        }

        public ActionResult TranscriptsDay10(string id, string name, int score) //學習透過GET表單傳送資料
        {
            Student data = new Student(id, name, score);
            return View(data);
        }

        //---------------------------------------------------------------------


        public ActionResult Day11() //學習透過POST 進行資料傳送
        {
            DateTime date = DateTime.Now;
            ViewBag.Date = date;

            Student data = new Student("2", "BBB", 80);
            return View(data);
        }

        [HttpPost]
        public ActionResult TranscriptsDay11(FormCollection post) //學習透過POST表單傳送資料
        {
            string id = post["id"];
            string name = post["name"];
            int score = Convert.ToInt32(post["score"]);//Convert.ToInt32將string資料轉為int
           
            Student data = new Student(id, name, score);
            return View(data);
                       
        }


        //---------------------------------------------------------------------


        public ActionResult Day16() //學習連結資料庫
        {
            string connString = "server=127.0.0.1;port=3306;user id=root;password=;database=aspmvctest;charset=utf8;"; //建立資料庫連現線字串
            MySqlConnection conn = new MySqlConnection();


            conn.ConnectionString = connString; //將連線跟字串連結起來
            if (conn.State != ConnectionState.Open)//判斷連線是否已打開 若無則打開連線
                conn.Open();

            string sql = @"INSERT INTO `City` (`Id`, `City`) VALUES
                           ('0', '基隆市'),
                           ('1', '臺北市'),
                           ('2', '新北市'),
                           ('3', '桃園市'),
                           ('4', '新竹市'),
                           ('5', '新竹縣'),
                           ('6', '宜蘭縣'),
                           ('7', '苗栗縣'),
                           ('8', '臺中市'),
                           ('9', '彰化縣'),
                           ('A', '南投縣'),
                           ('B', '雲林縣'),
                           ('C', '嘉義市'),
                           ('D', '嘉義縣'),
                           ('E', '臺南市'),
                           ('F', '高雄市'),
                           ('G', '屏東縣'),
                           ('H', '澎湖縣'),
                           ('I', '花蓮縣'),
                           ('J', '臺東縣'),
                           ('K', '金門縣'),
                           ('L', '連江縣');
                                            ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);//建立command
            int index = cmd.ExecuteNonQuery();// 執行SQL語法 ExecuteNonQuery如果用在新增修改刪除,成功會返回受影響的列數,失敗會傳回0，最後再判斷是否成功

            bool success = false;
            if (index > 0)
                success = true;
            else
                success = false;

            ViewBag.Success = success;

            conn.Clone();
            return View();
        }

        //---------------------------------------------------------------------
        



        public ActionResult Day17() //學習讀取資料庫資料 DataTable
        {
            string connString = "server=127.0.0.1;port=3306;user id=root;password=;database=aspmvctest;charset=utf8;"; //建立資料庫連現線字串
            MySqlConnection conn = new MySqlConnection();


            conn.ConnectionString = connString; //將連線跟字串連結起來
            if (conn.State != ConnectionState.Open)//判斷連線是否已打開 若無則打開連線
                conn.Open();

            string sql = @"SELECT * FROM `city` ";

            DataTable dt = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            adapter.Fill(dt);

            ViewBag.DT = dt;
            return View();
            
        }


        public ActionResult Day19() //學習讀取資料庫資料  DataReader連結資料庫
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

            using (MySqlDataReader dr =cmd.ExecuteReader())
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
            ViewBag.List = list;
            return View();


        }



        
        public ActionResult Day23() //學習AJAX技術 使用MyDataBase
        {
            MyDataBase db = new MyDataBase();
            List<City> citylist = db.GetCityList();

            ViewBag.list = citylist;
            return View();
        }
    }








}