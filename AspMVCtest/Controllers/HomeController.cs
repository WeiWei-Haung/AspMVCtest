using System;
using System.Collections.Generic;
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
    }


    //---------------------------------------------------------------------


}