using edu_course.Gateway;
using edu_course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace edu_course.Areas.Principle.Controllers
{

    public class HomeController : Controller
    {
        // GET: Principle/Home

        Digital_LearningEntities db = new Digital_LearningEntities();



        public JsonResult GetTeachersByDeptId(int deptId)
        {
            var teachers = db.Teachers.Where(t => t.Class_Id == deptId).Select(Class_Id => new { Class_Id.Id, Class_Id.Name }).ToList();

            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoursesByDeptId(int deptId)
        {
            var courses = db.Courses.Where(t => t.Class_Id == deptId).Select(x => new { x.courseId, x.Code }).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseById(int courseId)
        {
            

            var courses = db.Courses.Where(t => t.courseId == courseId).Select(x => new { x.courseId, x.courseName });
            return Json(courses, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveAssignCourse(CourseAssignToTeacher assignCourse)
        {
            var asignCoursesList = db.CourseAssignToTeachers.Where(t => t.courseId == assignCourse.courseId && t.Course.Status == true).ToList();
            if (asignCoursesList.Count > 0)
            {

                return Json(false);
            }
            else



            {

                int schoolid = Convert.ToInt32(Session["school"]);
                assignCourse.School_Id = schoolid;

                db.CourseAssignToTeachers.Add(assignCourse);

                db.SaveChanges();


                var teacher = db.Teachers.FirstOrDefault(t => t.Id == assignCourse.TeacherId);

                
                var course = db.Courses.FirstOrDefault(t => t.courseId == assignCourse.courseId);
                if (course != null)
                {
                    course.Status = true;
                    course.AssignTo = teacher.Name;
                    db.Entry(course).State = EntityState.Modified;

                    //db.Courses.Add(course);
                    db.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

                //return Json(false);
            }
        }


        public ActionResult CourseInfo(int Class_Id)
        {
            try
            {
                var courses = db.Courses.Where(t => t.Class_Id == Class_Id).Select(x => new { x.courseId, x.Code, x.AssignTo, x.courseName }).ToList();
                return Json(courses, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Please Try later";
                return View();
            }
        }


        public ActionResult UnassignCourses()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }

        public JsonResult UnassignAllCourses(bool name)
        {
            int schoolid = Convert.ToInt32(Session["school"]);

            var courses = db.Courses.Where(c => c.Status == true && c.User_Id == schoolid).ToList();
            if (courses.Count == 0)
            {
                return Json(false);
            }
            else
            {
                foreach (var course in courses)
                {
                    course.Status = false;
                    course.AssignTo = "";
                    db.Entry(course).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Json(true);

            }
        }
        [HttpGet]
        public ActionResult Profile()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            SchoolDBHandle sadb = new SchoolDBHandle();

            int userid = Convert.ToInt32((Session["school"]));
            var sab = db.SuperAdmins.Find(userid);
            Session["imgPath"] = sab.ad_imageurl;
            tbl_schoolValidation sa = sadb.GetProfileData(userid);

            return View(sa);
        }

        [HttpPost]
        public ActionResult Profile(tbl_schoolValidation sa)

        {
            School school = new School();
            try
            {
                //loginTable l = new loginTable();
                int userid = Convert.ToInt32((Session["school"]));

                var l = db.loginTables.FirstOrDefault(t => t.UserId == userid);

                if (ModelState.IsValid)
                {
                    if (sa != null)
                    {

                        if (l != null)
                        {
                            Session["schoolName"] = sa.School_Name;
                            l.UserId = userid;
                            l.Name = sa.School_Name;
                            l.Password = sa.Password;
                            l.Email = sa.School_Email;
                            l.RoleID = 2;
                            db.Entry(l).State = EntityState.Modified;

                            db.SaveChanges();
                        }
                        string filename = Path.GetFileNameWithoutExtension(sa.UserImageFIle.FileName);
                        string extension = Path.GetExtension(sa.UserImageFIle.FileName);
                        filename = DateTime.Now.ToString("yymmssff") + extension;




                        school.School_Image = "~/Content/img/users/" + filename;
                        school.School_Name = sa.School_Name;
                        school.School_Email = sa.School_Email;
                        school.Password = sa.Password;
                        school.School_Id = userid;





                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                        {
                            if (sa.UserImageFIle.ContentLength <= 1000000)
                            {
                                db.Entry(school).State = EntityState.Modified;



                                string oldImgPath = Request.MapPath(Session["imgPath"].ToString());

                                if (db.SaveChanges() > 0)
                                {
                                    filename = Path.Combine(Server.MapPath("~/Content/img/users/"), filename);
                                    sa.UserImageFIle.SaveAs(filename);
                                    if (System.IO.File.Exists(oldImgPath))
                                    {
                                        System.IO.File.Delete(oldImgPath);
                                    }


                                    ViewBag.Message = "Data Updated";
                                    return RedirectToAction("Profile");
                                }
                            }
                            else
                            {
                                ViewBag.msg = "File Size must be Equal or less than 1mb";
                            }
                        }
                        else
                        {
                            ViewBag.msg = "Inavlid File Type";
                        }
                    }

                    //}
                    else
                    {
                        school.School_Image = Session["imgPath"].ToString();
                        if (l != null)
                        {
                            Session["schoolName"] = sa.School_Name;
                            l.UserId = userid;
                            l.Name = sa.School_Name;
                            l.Password = sa.Password;
                            l.Email = sa.School_Email;
                            l.RoleID = 2;
                            db.Entry(l).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        school.School_Name = sa.School_Name;
                        school.School_Email = sa.School_Email;
                        school.Password = sa.Password;
                        school.School_Id = userid;
                        db.Entry(school).State = EntityState.Modified;

                        if (db.SaveChanges() > 0)
                        {


                            ViewBag.Message = "Data Updated";
                            return RedirectToAction("Profile");
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Updated";
                return View();
            }
            return View();
        }



        public ActionResult Index()

        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpGet]
        public ActionResult addteacher()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int schoolid = Convert.ToInt32(Session["school"]);
            ClassDBHandle gc = new ClassDBHandle();
            List<tbl_ClassValidation> list = gc.GetClass(schoolid);
            ViewBag.school = new SelectList(list, "Class_Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult addteacher(tbl_TeacherValidation teacher)
        {
            try
            {
                int schoolid = Convert.ToInt32(Session["school"]);
                ClassDBHandle gc = new ClassDBHandle();

                List<tbl_ClassValidation> list = gc.GetClass(schoolid);
                ViewBag.school = new SelectList(list, "Class_Id", "Name");
                teacher.Reg_No = RegNo(teacher);

                edu_course.Models.Teacher a = new edu_course.Models.Teacher();

                var userWithSameEmail = db.Teachers.Where(m => m.Email == teacher.Email).SingleOrDefault(); //checking if the emailid already exits for any user
                if (userWithSameEmail == null)
                {
                    string filename = Path.GetFileNameWithoutExtension(teacher.UserImageFIle.FileName);
                    string extension = Path.GetExtension(teacher.UserImageFIle.FileName);
                    filename = DateTime.Now.ToString("yymmssff") + extension;

                    a.Image = "~/Content/img/users/" + filename;
                    //image ko folder me save krwanay ke leye
                    filename = Path.Combine(Server.MapPath("~/Content/img/users/"), filename);
                    teacher.UserImageFIle.SaveAs(filename);
                    a.Class_Id = teacher.Class_Id;
                    a.School_Id = Convert.ToInt32(Session["school"]);
                    a.Name = teacher.Name;
                    a.Reg_No = teacher.Reg_No;
                    a.Address = teacher.Address;
                    a.Email = teacher.Email;
                    a.Contact = teacher.Contact;
                    a.Password = teacher.Password;

                    a.JoiningDate = DateTime.Now;

                    db.Teachers.Add(a);

                    db.SaveChanges();
                }

                else
                {
                    ViewBag.Message = "User with this Email Already Exist";
                    return View();
                }


                int teacherlatestid = a.Id;
                loginTable l = new loginTable();
                l.UserId = teacherlatestid;
                l.Name = a.Name;
                l.Password = a.Password;
                l.RoleID = 3;
                l.Email = a.Email;
                db.loginTables.Add(l);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Data Submitted";

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }

            return View();

        }
        public ActionResult viewsteacher()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var id = Convert.ToInt32(Session["school"]);


            var data = db.Teachers.Where(x => x.School_Id == id).ToList();
            return View(data);
        }






        [HttpGet]
        public ActionResult updateTeacher(int id)
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int schoolid = Convert.ToInt32(Session["school"]);
            ClassDBHandle gc = new ClassDBHandle();
            List<tbl_ClassValidation> list = gc.GetClass(schoolid);
            ViewBag.school = new SelectList(list, "Class_Id", "Name");



            var data = db.Teachers.Where(x => x.Id == id).First();


            return View(data);

        }

        [HttpPost]

        public ActionResult updateTeacher(edu_course.Models.Teacher tea, int id)
        {

            try
            {
                int schoolid = Convert.ToInt32(Session["school"]);
                ClassDBHandle gc = new ClassDBHandle();
                List<tbl_ClassValidation> list = gc.GetClass(schoolid);
                ViewBag.school = new SelectList(list, "Class_Id", "Name");

                var data = db.Teachers.Where(x => x.Id == id).First();

                if (data != null)
                {

                    data.Name = tea.Name;
                    data.Contact = tea.Contact;
                    data.Email = tea.Email;
                    data.Address = tea.Address;
                    data.Class_Id = tea.Class_Id;


                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Data Successfully Updated";
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
            return RedirectToAction("viewsteacher", "Home");



        }















        [HttpGet]
        public ActionResult addstudent()
        {

            int schoolid = Convert.ToInt32(Session["school"]);
            ClassDBHandle gc = new ClassDBHandle();
            List<tbl_ClassValidation> list = gc.GetClass(schoolid);
            ViewBag.schoolclass = new SelectList(list, "Class_Id", "Name");
            SectionDBHandle section = new SectionDBHandle();
            List<tbl_SectionValidation> sectionlist = section.GetSection(schoolid);
            ViewBag.schoolsection = new SelectList(sectionlist, "SectionID", "SectionName");

            return View();
        }

        [HttpPost]
        public ActionResult addstudent(edu_course.Models.tbl_StudentValidation s)
        {

            try
            {
                int schoolid = Convert.ToInt32(Session["school"]);
                ClassDBHandle gc = new ClassDBHandle();

                List<tbl_ClassValidation> list = gc.GetClass(schoolid);
                ViewBag.schoolclass = new SelectList(list, "Class_Id", "Name");
                SectionDBHandle section = new SectionDBHandle();
                List<tbl_SectionValidation> sectionlist = section.GetSection(schoolid);
                ViewBag.schoolsection = new SelectList(sectionlist, "SectionID", "SectionName");


                edu_course.Models.Student a = new edu_course.Models.Student();
                var userWithSameEmail = db.Students.Where(m => m.Email == s.Email).SingleOrDefault(); //checking if the emailid already exits for any user
                if (userWithSameEmail == null)
                {

                    s.RegNo = StudentRegNo(s, s.Name);

                    string filename = Path.GetFileNameWithoutExtension(s.UserImageFIle.FileName);
                    string extension = Path.GetExtension(s.UserImageFIle.FileName);
                    filename = DateTime.Now.ToString("yymmssff") + extension;

                    a.ImagePath = "~/Content/img/users/" + filename;
                    //image ko folder me save krwanay ke leye
                    filename = Path.Combine(Server.MapPath("~/Content/img/users/"), filename);
                    s.UserImageFIle.SaveAs(filename);
                    a.Class_Id = s.Class_Id;
                    a.Section_Id = s.Section_Id;
                    a.School_Id = Convert.ToInt32(Session["school"]);
                    a.Name = s.Name;
                    a.RegNo = s.RegNo;
                    a.Address = s.Address;
                    a.Email = s.Email;
                    a.ContactNo = s.ContactNo;
                    a.Password = s.Password;

                    a.RegisterationDate = DateTime.Now;

                    db.Students.Add(a);
                    db.SaveChanges();
                }

                else
                {
                    ViewBag.Message = "User with this Email Already Exist";
                    return View();
                }

                int studentlatestid = a.Id;
                loginTable l = new loginTable();
                l.UserId = studentlatestid;
                l.Name = a.Name;
                l.Password = a.Password;
                l.RoleID = 4;
                l.Email = a.Email;
                db.loginTables.Add(l);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Data Submitted";

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
            return View();
        }
        
        public ActionResult viewstudent()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var id = Convert.ToInt32(Session["school"]);


            var data = db.Students.Where(x => x.School_Id == id).ToList();
            return View(data);
        }





        [HttpGet]
        public ActionResult updateStudent(int id)
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int schoolid = Convert.ToInt32(Session["school"]);
            ClassDBHandle gc = new ClassDBHandle();
            List<tbl_ClassValidation> list = gc.GetClass(schoolid);
            ViewBag.schoolclass = new SelectList(list, "Class_Id", "Name");
            SectionDBHandle section = new SectionDBHandle();
            List<tbl_SectionValidation> sectionlist = section.GetSection(schoolid);
            ViewBag.schoolsection = new SelectList(sectionlist, "SectionID", "SectionName");


            var data = db.Students.Where(x => x.Id == id).First();


            return View(data);

        }

        [HttpPost]

        public ActionResult updateStudent(edu_course.Models.Student stu, int id)
        {

            try
            {
                int schoolid = Convert.ToInt32(Session["school"]);
                ClassDBHandle gc = new ClassDBHandle();
                List<tbl_ClassValidation> list = gc.GetClass(schoolid);
                ViewBag.schoolclass = new SelectList(list, "Class_Id", "Name");
                SectionDBHandle section = new SectionDBHandle();
                List<tbl_SectionValidation> sectionlist = section.GetSection(schoolid);
                ViewBag.schoolsection = new SelectList(sectionlist, "SectionID", "SectionName");


                var data = db.Students.Where(x => x.Id == id).First();

                if (data != null)
                {

                    data.Name = stu.Name;
                    data.ContactNo = stu.ContactNo;
                    data.Email = stu.Email;
                   data.Address = stu.Address;
                    data.Class_Id = data.Class_Id;
                    data.Section_Id = data.Section_Id;

                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Data Successfully Updated";
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
            return RedirectToAction("viewstudent","Home");



        }












        [HttpGet]
        public ActionResult addClass()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult addClass(tbl_ClassValidation cv)
        {
            try
            {

                Tbl_Class c = new Tbl_Class();

                c.SchoolId = Convert.ToInt32(Session["school"]);
                c.Name = cv.Name;
                db.Tbl_Class.Add(c);

                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Data Submitted";

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
            return View();

        }


        public ActionResult viewsClass()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var id = Convert.ToInt32(Session["school"]);


            var data = db.Tbl_Class.Where(x => x.SchoolId == id).ToList();
            return View(data);
        }







        [HttpGet]
        public ActionResult updateClass(int id)
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var data = db.Tbl_Class.Where(x => x.Class_Id == id).First();


            return View(data);

        }

        [HttpPost]

        public ActionResult updateClass(Tbl_Class dat, int id)
        {

            try
            {

                var data = db.Tbl_Class.Where(x => x.Class_Id == id).First();

                if (data != null)
                {

                    data.Name = dat.Name;

                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Data Successfully Updated";
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
            return RedirectToAction("viewsClass","Home");



        }









        [HttpGet]
        public ActionResult addSection()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult addSection(tbl_SectionValidation sv)
        {
            try
            {
                Section s = new Section();

                s.SchoolId = Convert.ToInt32(Session["school"]);
                s.SectionName = sv.SectionName;
                db.Sections.Add(s);

                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Data Submitted";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
            return View();
        }
        [HttpGet]
        public ActionResult viewsSection()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var id = Convert.ToInt32(Session["school"]);


            var data = db.Sections.Where(x => x.SchoolId == id).ToList();
            return View(data);
        }


        [HttpGet]
        public ActionResult updateSection(int id)
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var data = db.Sections.Where(x => x.SectionID == id).First();


            return View(data);

        }

        [HttpPost]

        public ActionResult updateSection(Section dat, int id)
        {

            try
            {
                
                var data = db.Sections.Where(x => x.SectionID == id).First();

                if (data != null)
                {

                    data.SectionName = dat.SectionName;
                    
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Data Successfully Updated";
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
            return RedirectToAction("viewsSection","Home");



        }














        [HttpGet]
        public ActionResult AddCourse()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int schoolid = Convert.ToInt32(Session["school"]);
            ClassDBHandle gc = new ClassDBHandle();
            List<tbl_ClassValidation> list = gc.GetClass(schoolid);
            ViewBag.school = new SelectList(list, "Class_Id", "Name");
            
            return View();
        }
        [HttpPost]
        public ActionResult AddCourse(tbl_coursevalidation cou)
        {
            int schoolid = Convert.ToInt32(Session["school"]);
            ClassDBHandle gc = new ClassDBHandle();
            List<tbl_ClassValidation> list = gc.GetClass(schoolid);
            ViewBag.school = new SelectList(list, "Class_Id", "Name");
            Course cc = new Course();

            try
            {


                cou.Role_Id = Convert.ToInt32(Session["RoleID"]);

                cc.User_Id = schoolid;
                cc.Role_Id = cou.Role_Id;
                cc.CreatedDate = DateTime.Now;
                cc.courseDescription = cou.courseDescription;
                cc.courseName = cou.courseName;
                cc.courseType = cou.courseType;
                cc.Class_Id = cou.Class_Id;
                cc.Code = cou.Code;
                //cc.longDes = cou.longDes;
                //cc.duration = cou.duration;
                db.Courses.Add(cc);


                db.SaveChanges();
                ViewBag.Message = "Data Submitted";
                ModelState.Clear();
            }

            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }

            return View();

        }
        public ActionResult courseAsssignation()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            //Tbl_Class c = new Tbl_Class();
            //ViewBag.schoolclass = db.Tbl_Class.ToList();


            int schoolid = Convert.ToInt32(Session["school"]);
          
            ClassDBHandle gc = new ClassDBHandle();
            List<tbl_ClassValidation> list = gc.GetClass(schoolid);
          
     

            ViewData["class_name"] = new SelectList(list, "Class_Id", "Name");


            return View();
        }
        [HttpPost]

        public ActionResult courseAsssignation(CourseAssignToTeacher c)
        {

            int schoolid = Convert.ToInt32(Session["school"]);
         
            ClassDBHandle gc = new ClassDBHandle();
            List<tbl_ClassValidation> list = gc.GetClass(schoolid);
     

            ViewData["class_name"] = new SelectList(list, "Class_Id", "Name");


            return View();
        }
      



        public ActionResult coursestatistic()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int schoolid = Convert.ToInt32(Session["school"]);
            ClassDBHandle gc = new ClassDBHandle();
            List<tbl_ClassValidation> list = gc.GetClass(schoolid);
            ViewData["class_name"] = new SelectList(list, "Class_Id", "Name");
            return View();
        }
     
        public ActionResult Enrollment()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            //ViewBag.CourseId = new SelectList(db.Courses, "courseId", "Code");
            //ViewBag.StudentList = db.Students.ToList();
            int schoolid = Convert.ToInt32(Session["school"]);
            int RoleID = 2;
            StudentDBHandle gc = new StudentDBHandle();

            List<tbl_StudentValidation> list = gc.GetSchoolStudent(schoolid);

            ViewData["student_name"] = new SelectList(list, "RegNo", "RegNo");


            CourseDBHandle gc1 = new CourseDBHandle();

            List<tbl_OnlineTestValidation> courselist = gc1.GetSchoolCourse(schoolid, RoleID);

            ViewData["course_name"] = new SelectList(courselist, "courseId", "courseName");
            return View();



        }

        [HttpPost]
        public ActionResult Action(string RegNo)
        {

            var query = from s in db.Students
                        join c in db.Tbl_Class on s.Class_Id equals c.Class_Id join  cs in db.Sections on s.Section_Id equals cs.SectionID
                        where s.RegNo == RegNo
                        select new { Id = s.RegNo, Name = s.Name,Email=s.Email,Class=c.Name,Section=cs.SectionName };
            //var query = from c in db.Students where c.Id == Id select new { Id = c.Id, Name = c.Name, Email = c.Email };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
       

        public JsonResult IsEnrolled(string regNo, int courseId)
        {
            var enrollCourses = db.UserEnrollInCourses.Where(s => s.RegistrationId == regNo && s.CourseId == courseId);
            int itm = enrollCourses.Count();
            if (itm == 0)
            {
                return Json(false);
            }
            return Json(true);
        }

        public ActionResult EnrollCoursetoStudent(UserEnrollInCourse enrollCourse)
        {
            var enrollCourses = db.UserEnrollInCourses.Where(s => s.RegistrationId == enrollCourse.RegistrationId && s.CourseId == enrollCourse.CourseId).ToList();
            int itm = enrollCourses.Count();
            if (itm == 1)
            {
                int schoolid = Convert.ToInt32(Session["school"]);
                var id = enrollCourses[0].EnrollmentId;
                var date = DateTime.Now;
                enrollCourse.CourseId = id;
                enrollCourse.Enrolldate = date;
                enrollCourse.UserId = schoolid;
                enrollCourse.RoleId = 2;
                enrollCourse.IsUserActive = true;
                db.UserEnrollInCourses.Add(enrollCourse);
                db.SaveChanges();

            }
            else
            {
                int schoolid = Convert.ToInt32(Session["school"]);
                var date = DateTime.Now;
                enrollCourse.UserId = schoolid;
                enrollCourse.RoleId = 2;
                enrollCourse.IsUserActive = true;
                enrollCourse.Enrolldate = date;
                db.UserEnrollInCourses.Add(enrollCourse);
                db.SaveChanges();

            }

            return Json(true);
        }

       



        public ActionResult feedback()
        {
            return View();
        }
        public ActionResult addsupportform()
        {
            return View();
        }
        public ActionResult viewsupportform()
        {
            return View();
        }
        [HttpGet]
        public ActionResult detailsupportform()
        {
            return View();
        }
        [HttpPost]
        public ActionResult detailsupportform(StudentComplain sc)
        {
            return View();
        }


        [HttpGet]
        public ActionResult addannouncement()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult addannouncement(tbl_Announcementvalidation anouncement)
        {

            try
            {
                Announcement a = new Announcement();


                int userid = Convert.ToInt32(Session["school"]);



                a.Announcement_Title = anouncement.Announcement_Title;
                a.Announcement_Body = anouncement.Announcement_Body;
                a.RoleId = 2;
                a.UserId = userid;
                a.SchoolId = userid;


                a.CreatedDate = DateTime.Now;

                db.Announcements.Add(a);

                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Data Submitted";


            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }

            return View();
        }
        [HttpGet]
        public ActionResult articleform()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<ArticleType> list = db.ArticleTypes.ToList();
            ViewBag.articletypelist = new SelectList(list, "Article_TypeId", "Article_Typename ");


            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult articleform(tbl_ArticleValidation article)
        {
            List<ArticleType> list = db.ArticleTypes.ToList();
            ViewBag.articletypelist = new SelectList(list, "Article_TypeId", "Article_Typename ");

            article.Role_Id = Convert.ToInt32(Session["RoleId"]);


            article.UserId = Convert.ToInt32(Session["school"]);

            try
            {
                Article ar = new Article();


                string filename = Path.GetFileNameWithoutExtension(article.UserImageFIle.FileName);
                string extension = Path.GetExtension(article.UserImageFIle.FileName);
                filename = DateTime.Now.ToString("yymmssff") + extension;
                article.imgPath = "~/Content/img/" + filename;
                //image ko folder me save krwanay ke leye
                filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                article.UserImageFIle.SaveAs(filename);
                ar.statusId = 1;
                ar.imgPath = article.imgPath;
                ar.Article_TypeId = article.Article_TypeId;
                ar.Title = article.Title;
                ar.shortDes = article.shortDes;
                ar.longDes = article.longDes;
                ar.CreatedDate = DateTime.Now;
                ar.Role_Id = article.Role_Id;
                ar.UserId = article.UserId;
                ar.SchoolId = article.UserId;



                db.Articles.Add(ar);

                db.SaveChanges();



                ModelState.Clear();
                ViewBag.Message = "Data Submitted";

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }


            return View();
        }

        [HttpGet]
        public ActionResult addComplainform()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult addComplainform(tblClientorSchoorComplainValidation complain)
        {
            complain.RoleId = Convert.ToInt32(Session["RoleId"]);


            complain.UserId = Convert.ToInt32(Session["school"]);


            Client_SchoolComplain cscomplain = new Client_SchoolComplain();
            cscomplain.complain_description = complain.complain_description;
            cscomplain.complain_date = DateTime.Now;
            cscomplain.RoleId = complain.RoleId;
            cscomplain.UserId = complain.UserId;
            db.Client_SchoolComplain.Add(cscomplain);
            db.SaveChanges();

            ViewBag.Message = "Data Submitted";

            ModelState.Clear();

            return View();
        }



        public ActionResult viewComplainform()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var UserId = Convert.ToInt32(Session["school"]);


            var data = (from com in db.Client_SchoolComplain
                        join sc in db.Schools on com.RoleId equals sc.School_Id
                        where com.RoleId == UserId
                        select new  tblClientorSchoorComplainValidation
                        {
                            complain_Id =com.complain_Id,
                            ScholName = sc.School_Name,
                            UserId = UserId,
                            complain_description = com.complain_description,
                            replymsg = com.replymsg,
                            complain_date =com.complain_date,
                            

                        }).ToList();

            //var data = db.Client_SchoolComplain.ToList();


            
            return View(data);
        }



       
        public ActionResult detailcomplainform()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }


        public ActionResult viewStudentComplainform()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            int schoolid = Convert.ToInt32(Session["school"]);
            var data = (from complain in db.StudentComplains
                        join sc in db.Students on complain.UserId equals sc.Id
                        join scoo in db.Schools on complain.School_Id equals scoo.School_Id
                        where complain.School_Id == schoolid
                        select new tblStudentComplainValidation()
                        {
                            complain_id = complain.complain_id,
                            StudentName = sc.Name,
                            SchoolName = scoo.School_Name,
                            complain_description = complain.complain_description,
                            complain_date = complain.complain_date

                        }).ToList();



            return View(data);
        }

        [HttpGet]
        public ActionResult viewStudentComplainReply()
        {
            if (Session["school"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }


            return View();
        }
        [HttpPost]
        public ActionResult viewStudentComplainReply(tblStudentComplainValidation sa, int id)
        {
            try
            {


                int userid = Convert.ToInt32((Session["school"]));

                var com = db.StudentComplains.FirstOrDefault(m => m.complain_id == id);

                if (com != null)
                {
                    com.UserId = userid;
                    
                    com.complain_id = id;
                    com.ReplyMsg = sa.ReplyMsg;

                    db.Entry(com).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Data Successfully Submitted";
                    ModelState.Clear();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
            return View();
        }







        private string RegNo(tbl_TeacherValidation teacher)
        {
            int id = db.Teachers.Count(s => (s.Class_Id == teacher.Class_Id)
                    && (teacher.JoiningDate.Year == teacher.JoiningDate.Year)) + 1;
            Tbl_Class aClass = db.Tbl_Class.Where(d => d.Class_Id == teacher.Class_Id).FirstOrDefault();
            string registrationId = aClass.Name + "-" + teacher.JoiningDate.Year + "-";

            string addZero = "";
            int len = 3 - id.ToString().Length;
            for (int i = 0; i < len; i++)
            {
                addZero = "0" + addZero;
            }

            return registrationId + addZero + id;
        }
        private string StudentRegNo(tbl_StudentValidation student,string name)
        {
            int id = db.Students.Count(s => (s.Section_Id == student.Section_Id)
                    && (student.RegisterationDate.Year == student.RegisterationDate.Year)) + 1;
           //edu_course.Models.Student st= db.Students.Where(d => d.Id == student.Id).FirstOrDefault();
            string registrationId = name + "-" + student.RegisterationDate.Year + "-";

            string addZero = "";
            int len = 3 - id.ToString().Length;
            for (int i = 0; i < len; i++)
            {
                addZero = "0" + addZero;
            }

            return registrationId + addZero + id;
        }
    }
}