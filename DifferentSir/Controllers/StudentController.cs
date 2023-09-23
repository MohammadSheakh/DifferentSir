using DifferentSir.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DifferentSir.Controllers
{
    public class StudentController : Controller
    {
        // ekhane kichu student create korte hobe .. 
        // jeta amra view te show korbo 
        IList<Student> students = new List<Student>() {
            new Student(){StudentName = "Test1",
                StudentId = 1,
                Age = 1},
            new Student(){StudentName = "Test2",
                StudentId = 2,
                Age = 2},
            new Student(){StudentName = "Test3",
                StudentId = 3,
                Age = 3}
    }; // so, list of students nilam

        // ❤️❤️ public ar static er moddhe difference ta bujhte hobe ..  
        
        /**
        public StudentController() {

            // now for loop use kore value assign korbo 
            for (int i = 0; i < 10; i++)
            {
                // Student Create kora mane Student Model class er object create
                // kora .. lets do that 
                Student student = new Student()
                {
                    StudentName = "Mohammad Sheakh" + (i + 1), // brackate na dile jog hobe na 
                    StudentId = i,
                    Age = i + 2,

                };
                // object create done .. ekhon eta list e add korte hobe 
                students.Add(student);
            }
        }
        
        */

        
        


        // GET: Student
        [HttpGet] // by default .. we have not to mention this 
        public ActionResult Index()
        {
            

            return View(students.OrderBy(s => s.StudentId).ToList()); // bujhi nai 😢
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            // get student from the database 
            var std = students.Where(s => s.StudentId == id).FirstOrDefault();

            return View(std);
        }

        /**
         *  Default value provider collection evaluates values from the following sources:

            Previously bound action parameters, when the action is a child action
            Form fields (Request.Form)
            The property values in the JSON Request body (Request.InputStream), but only when the request is an AJAX request
            Route data (RouteData.Values)
            Querystring parameters (Request.QueryString)
            Posted files (Request.Files)
         */

        [HandleError] // Shared folder er Error.cshtml file show korbe .. different error er jonno different page show kora jabe 
        [HandleError(ExceptionType = typeof(NullReferenceException), View = "~/Views/Shared/Error.cshtml")] // 😢 error show kore na 
        [HttpPost]
        public ActionResult Edit(Student std/* int id, string name */) // EditByComplexType
        {
            // Multiple value update korte chaile 

            if (ModelState.IsValid)
            {
                // update student to db


                var id = std.StudentId;
                var name = std.StudentName;
                var age = std.Age;
                //var standardName = std.standard.StandardName; // 😢 error ashtese 

                // update database here .. 
                // ekhane amra jei student er object ta ashse .. sheta ke id dhore khuje ber kore list theke Remove 
                // kore dibo .. then list e new object ta add kore dibo



                // but ei approch ta amar valo lage nai .. single value edit kora gele valo hoito 
                var student = students.Where(s => s.StudentId == std.StudentId).FirstOrDefault();
                students.Remove(student);
                students.Add(std); // object ta add kore dilam .

                return RedirectToAction("Index"); // Index Action er view te pathay dilam 
            }
            return View(std);
        }

        [HttpPost]
        public ActionResult EditByFormCollection(FormCollection fc)
        {
            // multiple value update korte chaile 

            var id = fc["StudentId"];
            var name = fc["StudentName"];
            var age = fc["Age"];
            var standardName = fc["standard.StandardName"]; // sure na eita ami 

            // update database here .. 

            return RedirectToAction("Index"); // Index Action er view te pathay dilam 
        }

        // Specific property update korte chaile .. Bind Attribute
        //The Bind attribute will improve the performance by only bind properties that you needed.

        [HttpPost]
        public ActionResult EditSpecificValue([Bind(Include = "StudentId, StudentName")] Student std)
        {
            // amra chaile Exclude o korte pari .. je ei chara baki gula update korbo 
            var name = std.StudentName;

            //write code to update student 

            return RedirectToAction("Index");
        }



        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)] // multiple action verb 
        [ActionName("Find")] // GetById er nam ekhon theke Find // localhost/Student/Find instead of GetById
        public ActionResult GetById(int id)
        {
            // get student from the database 
            
            return View();
        }

        [NonAction] // public method but we do not want to treat it as an action method. 
        public int returnValue(int id)
        {
            return id;
        }



        //  This method handles all your unhandled errors with error code 500. 
        // It does not require to enable the <customErrors> config in web.config. 
        
        protected override void OnException(ExceptionContext filterContext) // finally  ei function er error show kortese 
        {
            filterContext.ExceptionHandled = true;

            //Log the error!!

            //Redirect to action
            filterContext.Result = RedirectToAction("Error", "InternalError"); // ei line ta bujhi nai 😢

            // OR return specific view
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };
        }
        
    }
}