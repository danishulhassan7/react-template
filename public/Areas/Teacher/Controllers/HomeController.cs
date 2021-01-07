using edu_course.Gateway;
using edu_course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace edu_course.Areas.Teacher.Controllers
{
    public class HomeController : Controller
    {
        // GET: Teacher/Home
        Digital_LearningEntities db = new Digital_LearningEntities();


        public ActionResult dashboard()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpGet]
        public ActionResult Profile()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            TeacherDBHandle sadb = new TeacherDBHandle();

            int userid = Convert.ToInt32((Session["Teacher"]));
            var sab = db.Teachers.Find(userid);
            Session["imgPath"] = sab.Image;
            tbl_TeacherValidation sa = sadb.GetProfileData(userid);
            return View(sa);
        }
        [HttpPost]
        public ActionResult Profile(edu_course.Models.tbl_TeacherValidation sa)
        {
          try
            { 
            edu_course.Models.Teacher teacher = new edu_course.Models.Teacher();
            int userid = Convert.ToInt32((Session["Teacher"]));

            var l = db.loginTables.FirstOrDefault(t => t.UserId == userid);

            if (ModelState.IsValid)
            {
                if (sa.UserImageFIle != null)
                {

                    if (l != null)
                    {
                        Session["TeacherName"] = sa.Name;
                        l.UserId = userid;
                        l.Name = sa.Name;
                        l.Password = sa.Password;
                        l.Email = sa.Email;
                        l.RoleID = 3;
                        db.Entry(l).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                    string filename = Path.GetFileNameWithoutExtension(sa.UserImageFIle.FileName);
                    string extension = Path.GetExtension(sa.UserImageFIle.FileName);
                    filename = DateTime.Now.ToString("yymmssff") + extension;




                    teacher.Image = "~/Content/img/users/" + filename;
                    teacher.Name = sa.Name;
                    teacher.Email = sa.Email;
                    teacher.Password = sa.Password;
                    teacher.Id = userid;





                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (sa.UserImageFIle.ContentLength <= 1000000)
                        {
                            db.Entry(teacher).State = EntityState.Modified;



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
                    teacher.Image = Session["imgPath"].ToString();
                    if (l != null)
                    {
                        Session["TeacherName"] = sa.Name;
                        l.UserId = userid;
                        l.Name = sa.Name;
                        l.Password = sa.Password;
                        l.Email = sa.Email;
                        l.RoleID = 3;
                        db.Entry(l).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    teacher.Name = sa.Name;
                    teacher.Email = sa.Email;
                    teacher.Password = sa.Password;
                    teacher.Id = userid;
                    db.Entry(teacher).State = EntityState.Modified;

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


        [HttpGet]
        public ActionResult addonlinetest()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            int teacherid = Convert.ToInt32(Session["Teacher"]);
            int tempclassid;
            int originalclassid;
            var getteacherid = db.Teachers.Find(teacherid);
           tempclassid = getteacherid.Class_Id;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;


            CourseDBHandle gc = new CourseDBHandle();

            List<tbl_CourseAssigntoTeacherValidation> list = gc.GetTeacherAssignedCourse(teacherid, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");
            



            return View();
        }
        [HttpPost]
        public JsonResult addonlinetest(QuestionOptionViewModel QuestionOption)
        {
            int teacherid = Convert.ToInt32(Session["Teacher"]);
         
            int tempclassid;
            int schoolid;
            int originalclassid;
            var getteacherid = db.Teachers.Find(teacherid);
            tempclassid = getteacherid.Class_Id;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;


            CourseDBHandle gc = new CourseDBHandle();

            List<tbl_CourseAssigntoTeacherValidation> list = gc.GetTeacherAssignedCourse(teacherid, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");
            OnlineTest q = new OnlineTest();
            q.Role_Id = Convert.ToInt32(Session["RoleId"]);
            q.UserId = Convert.ToInt32(Session["Teacher"]);
            var getteachid = db.Teachers.Find(teacherid);
            schoolid = getteachid.School_Id;
            q.SchoolId = schoolid;
            //OnlineTest test = new OnlineTest();
            q.CourseId = QuestionOption.CourseId;
            q.QuestionName = QuestionOption.QuestionName;
            q.IsActive = true;
            q.ClassId = originalclassid;
            q.CreatedDate = DateTime.Now;
            q.Duration = "1 hour";
            q.IsMultiple = false;
            db.OnlineTests.Add(q);
            db.SaveChanges();

            int questionId = q.QuestionId;
            foreach (var item in QuestionOption.ListOfOptions)
            {
                OnlineTestQuestionOption objoption = new OnlineTestQuestionOption();
                objoption.OptionName = item;
                objoption.QuestionId = questionId;
                db.OnlineTestQuestionOptions.Add(objoption);
                db.SaveChanges();
            }
            OnlineTestAnswer objanswer = new OnlineTestAnswer();
            objanswer.AnswerText = QuestionOption.AnswerText;
            objanswer.RoleId = Convert.ToInt32(Session["RoleId"]);
            objanswer.UserId = Convert.ToInt32(Session["Teacher"]);
            objanswer.SchoolId = schoolid;



            objanswer.QuestionId = questionId;
            db.OnlineTestAnswers.Add(objanswer);
            db.SaveChanges();

            return Json(new { message = "Data Successfully Added", success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult addtest(QuestionOptionViewModel QuestionOption)
        {
            //CourseDBHandle gc = new CourseDBHandle();
            //List<CourseDBHandle> list = gc.GetCourse();
            //ViewBag.course = new SelectList(list, "courseId", "courseName");
            OnlineTest q = new OnlineTest();
            q.Role_Id = Convert.ToInt32(Session["RoleId"]);


            q.UserId = Convert.ToInt32(Session["Ad"]);


            //OnlineTest test = new OnlineTest();
            q.CourseId = QuestionOption.CourseId;
            q.QuestionName = QuestionOption.QuestionName;
            q.IsActive = true;
            q.CreatedDate = DateTime.Now;
            q.Duration = "1 hour";
            q.IsMultiple = false;
            db.OnlineTests.Add(q);
            db.SaveChanges();

            int questionId = q.QuestionId;
            foreach (var item in QuestionOption.ListOfOptions)
            {
                OnlineTestQuestionOption objoption = new OnlineTestQuestionOption();
                objoption.OptionName = item;
                objoption.QuestionId = questionId;
                db.OnlineTestQuestionOptions.Add(objoption);
                db.SaveChanges();
            }
            OnlineTestAnswer objanswer = new OnlineTestAnswer();
            objanswer.AnswerText = QuestionOption.AnswerText;
            objanswer.RoleId = Convert.ToInt32(Session["RoleId"]);
            objanswer.UserId = Convert.ToInt32(Session["Ad"]);
            objanswer.SchoolId = Convert.ToInt32(Session["Ad"]);



            objanswer.QuestionId = questionId;
            db.OnlineTestAnswers.Add(objanswer);
            db.SaveChanges();

            return Json(new { message = " Successfully Added", success = true }, JsonRequestBehavior.AllowGet);
        }
        /*
                public ActionResult viewonlinetest()
            {
                    if (Session["Teacher"] == null)
                    {
                        return RedirectToAction("login", "Home", new { area = "" });
                    }
                    int teacherid = Convert.ToInt32(Session["Teacher"]);
                    var data = db.OnlineTests.Where(x => x.UserId == teacherid).ToList();


                    return View(data);

                }
                */
        public ActionResult viewonlinetest()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var data = db.OnlineTests.ToList();

            return View(data);

        }

        [HttpGet]
        public ActionResult addtestfile()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int teacherid = Convert.ToInt32(Session["Teacher"]);

            int tempclassid;
            int schoolid;
            int originalclassid;

            var getteacherid = db.Teachers.Find(teacherid);
            tempclassid = getteacherid.Class_Id;
            schoolid = getteacherid.School_Id;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;
            CourseDBHandle gc = new CourseDBHandle();

            List<tbl_CourseAssigntoTeacherValidation> list = gc.GetTeacherAssignedCourse(teacherid, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");
            return View();
        }
        [HttpPost]
        public ActionResult addtestfile(tbl_TestFileValidation tfile)
        {

            int teacherid = Convert.ToInt32(Session["Teacher"]);

            int tempclassid;
            int schoolid;
            int originalclassid;

            var getteacherid = db.Teachers.Find(teacherid);
            tempclassid = getteacherid.Class_Id;
            schoolid = getteacherid.School_Id;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;
            CourseDBHandle gc = new CourseDBHandle();
            tfile.SchoolId = schoolid;
            tfile.ClassId = originalclassid;
            List<tbl_CourseAssigntoTeacherValidation> list = gc.GetTeacherAssignedCourse(teacherid, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");


            if (tfile.UserdocFIle == null)
            {
                ModelState.AddModelError("CustomError", "Please Select File");
                return View();
            }
            if (!(tfile.UserdocFIle.ContentType == "application/pdf" || tfile.UserdocFIle.ContentType == "application/pdf"))
            {
                ModelState.AddModelError("CustomError", "Select Doc or Pdf extention file only ");
                return View();
            }

            try
            {
                ManualTest mantestfile = new ManualTest();

                string fileName = Guid.NewGuid() + Path.GetExtension(tfile.UserdocFIle.FileName);
                tfile.UserdocFIle.SaveAs(Path.Combine(Server.MapPath("~/file_upload"), fileName));


                mantestfile.TestUrl = fileName;
                mantestfile.CourseId = tfile.CourseId;
                mantestfile.ClassId = tfile.ClassId;
                mantestfile.CreatedDate = DateTime.Now;
                mantestfile.Duration = tfile.Duration;
                mantestfile.SchoolId = tfile.SchoolId;
                mantestfile.UserId = Convert.ToInt32(Session["Teacher"]);




                db.ManualTests.Add(mantestfile);
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

        public ActionResult viewtestfile()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int teacherid = Convert.ToInt32(Session["Teacher"]);
            var data = db.ManualTests.Where(x=>x.UserId== teacherid).ToList();
            return View(data);

        }

        
















        [HttpGet]

        public ActionResult articleform()
        {
            if (Session["Teacher"] == null)
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


            article.UserId = Convert.ToInt32(Session["Teacher"]);

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
        public ActionResult viewarticle()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int teacherid = Convert.ToInt32(Session["Teacher"]);
            var data = db.Articles.Where(x=>x.UserId==teacherid).ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult updatearticle(int id)
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<ArticleType> list = db.ArticleTypes.ToList();
            ViewBag.articletypelist = new SelectList(list, "Article_TypeId", "Article_Typename ");


            var data = db.Articles.Where(x => x.ArticleId == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updatearticle(Article dat, int id)
        {

            try
            {
                List<ArticleType> list = db.ArticleTypes.ToList();
                ViewBag.articletypelist = new SelectList(list, "Article_TypeId", "Article_Typename ");

                var data = db.Articles.Where(x => x.ArticleId == id).First();

                if (data != null)
                {
                    data.Article_TypeId = dat.Article_TypeId;
                    data.Title = dat.Title;
                    data.shortDes = dat.shortDes;
                    data.longDes = dat.longDes;

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
            return RedirectToAction("viewarticle","Home");



        }
        [HttpPost]

        public JsonResult DeleteArticle(int articleId)
        {

            bool resul = false;
            Article sc = db.Articles.SingleOrDefault(x => x.ArticleId == articleId);
            if (sc != null)
            {
                db.Articles.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }












        [HttpGet]
        public ActionResult assignment()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int teacherid = Convert.ToInt32(Session["Teacher"]);

            int tempclassid;
            int schoolid;
            int originalclassid;

            var getteacherid = db.Teachers.Find(teacherid);
            tempclassid = getteacherid.Class_Id;
            schoolid = getteacherid.School_Id;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;
            CourseDBHandle gc = new CourseDBHandle();

            List<tbl_CourseAssigntoTeacherValidation> list = gc.GetTeacherAssignedCourse(teacherid, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");
            return View();
        }
        [HttpPost]

        public ActionResult assignment(tbl_AssignmentValidation a)
        {

            int teacherid = Convert.ToInt32(Session["Teacher"]);

            int tempclassid;
            int schoolid;
            int originalclassid;

            var getteacherid = db.Teachers.Find(teacherid);
            tempclassid = getteacherid.Class_Id;
            schoolid = getteacherid.School_Id;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;

            a.SchoolId = schoolid;
            a.ClassId = originalclassid;
            CourseDBHandle gc = new CourseDBHandle();

            List<tbl_CourseAssigntoTeacherValidation> list = gc.GetTeacherAssignedCourse(teacherid, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");


            if (a.UserdocFIle == null)
            {
                ModelState.AddModelError("CustomError", "Please Select File");
                return View();
            }
            if (!(a.UserdocFIle.ContentType == "application/pdf" || a.UserdocFIle.ContentType == "application/pdf"))
            {
                ModelState.AddModelError("CustomError", "Select Doc or Pdf extention file only ");
                return View();
            }

            try
            {
                SchoolAssignment scassignemnt = new SchoolAssignment();


                string fileName = Guid.NewGuid() + Path.GetExtension(a.UserdocFIle.FileName);
                a.UserdocFIle.SaveAs(Path.Combine(Server.MapPath("~/file_upload"), fileName));

                scassignemnt.AssignmentUrl = fileName;
                scassignemnt.AssignmentName = a.AssignmentName;
                scassignemnt.CourseId = a.CourseId;
                scassignemnt.ClassId = a.ClassId;
                scassignemnt.CreatedDate = DateTime.Now;
                scassignemnt.Duration = a.Duration;
                scassignemnt.SchoolId = a.SchoolId;
                scassignemnt.UserId = Convert.ToInt32(Session["Teacher"]);

                db.SchoolAssignments.Add(scassignemnt);

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







        public ActionResult viewassignment(int? courseid)
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<SubmitAssignment> sa = entities.SubmitAssignments.ToList();
            List<School> s = entities.Schools.ToList();
            List<SchoolAssignment> sca = entities.SchoolAssignments.ToList();
            List<Course> c = entities.Courses.ToList();
            var query = from sas in sa
                        join ss in s on sas.SchoolId equals ss.School_Id into table1
                        from ss in table1.DefaultIfEmpty()
                        join sss in sca on sas.AssignmentId equals sss.AssignmentId into table2
                        from sss in table2.DefaultIfEmpty()
                        join ssss in c on sas.CourseId equals ssss.courseId into table3
                        from ssss in table3.DefaultIfEmpty()
                        select new AssignmentDetail { sub = sas, s = ss, sc = sss, c = ssss };
            return View(query);

        }


        public ActionResult Class()
        {

            return View();
        }

        public ActionResult test()
        {
            return View();
        }
      
        public ActionResult notes()
        {
            return View();
        }
      
        [HttpGet]
        public ActionResult addstudentresultform()
        {
            var Class = new SelectList(db.Tbl_Class.ToList(), "Class_Id", "Name");
            ViewData["Classes"] = Class;
            var Course = new SelectList(db.Courses.ToList(), "courseId", "courseName");
            ViewData["Courses"] = Course;
            var StudentRegno = new SelectList(db.Tbl_Class.ToList(), "Class_Id", "Name");
            ViewData["StudentRegno"] = StudentRegno;
        
            return View();
        }
        [HttpPost]
        public ActionResult addstudentresultform(StudentResult r)
        {
            var Class = new SelectList(db.Tbl_Class.ToList(), "Class_Id", "Name");
            ViewData["Classes"] = Class;
            var Course = new SelectList(db.Courses.ToList(), "courseId", "courseName");
            ViewData["Courses"] = Course;
            var StudentRegno = new SelectList(db.Students.ToList(), "Id", "RegNo");
            ViewData["StudentRegno"] = StudentRegno;
            return View();
        }
        public ActionResult viewstudentresultform()
        {
            return View();
        }

        [HttpGet]
        public ActionResult addnotes()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int teacherid = Convert.ToInt32(Session["Teacher"]);

            int tempclassid;
            int schoolid;
            int originalclassid;

            var getteacherid = db.Teachers.Find(teacherid);
            tempclassid = getteacherid.Class_Id;
            schoolid = getteacherid.School_Id;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;
            CourseDBHandle gc = new CourseDBHandle();

            List<tbl_CourseAssigntoTeacherValidation> list = gc.GetTeacherAssignedCourse(teacherid, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");
            return View();
        }
        [HttpPost]
        public ActionResult addnotes(tbl_LectureValidation lec)
        {
            int teacherid = Convert.ToInt32(Session["Teacher"]);

            int tempclassid;
            int schoolid;
            int originalclassid;

            var getteacherid = db.Teachers.Find(teacherid);
            tempclassid = getteacherid.Class_Id;
            schoolid = getteacherid.School_Id;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;

            lec.SchoolId = schoolid;
            lec.ClassId = originalclassid;
            CourseDBHandle gc = new CourseDBHandle();

            List<tbl_CourseAssigntoTeacherValidation> list = gc.GetTeacherAssignedCourse(teacherid, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");


            if (lec.UserdocFIle == null)
            {
                ModelState.AddModelError("CustomError", "Please Select File");
                return View();
            }
            if (!(lec.UserdocFIle.ContentType == "application/pdf" || lec.UserdocFIle.ContentType == "application/pdf"))
            {
                ModelState.AddModelError("CustomError", "Select Doc or Pdf extention file only ");
                return View();
            }

            try
            {
                Lecture teaclecture = new Lecture();

                string fileName = Guid.NewGuid() + Path.GetExtension(lec.UserdocFIle.FileName);
                lec.UserdocFIle.SaveAs(Path.Combine(Server.MapPath("~/file_upload"), fileName));

                teaclecture.LectureUrl = fileName;
                teaclecture.Lecturename = lec.Lecturename;
                teaclecture.CourseId = lec.CourseId;
                teaclecture.ClassId = lec.ClassId;
                teaclecture.CreatedDate = DateTime.Now;
                teaclecture.Lecture_Description = lec.Lecture_Description;
                teaclecture.SchoolId = lec.SchoolId;
                teaclecture.VideoLink = lec.VideoLink;
                teaclecture.UserId = Convert.ToInt32(Session["Teacher"]);

                db.Lectures.Add(teaclecture);

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

    

        public ActionResult viewnotes()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int teacherid = Convert.ToInt32(Session["Teacher"]);


            int schoolid;


            var getteacherid = db.Teachers.Find(teacherid);

            schoolid = getteacherid.School_Id;



            var query = (from sas in db.Lectures
                         join co in db.Courses on sas.CourseId equals co.courseId
                         where sas.SchoolId == schoolid 
                         select new tbl_LectureValidation
                         {
                             Lecturename = sas.Lecturename,
                             Date = sas.CreatedDate,
                             VideoLink = sas.VideoLink,
                             LectureUrl = sas.LectureUrl
                         }).ToList();



            return View(query);
        }
        public ActionResult GetStudentRegno()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int teacherid = Convert.ToInt32(Session["Teacher"]);
            CourseDBHandle gc = new CourseDBHandle();

            List<Tbl_UserEnrollInCourseValidation> Regnolist = gc.GetStudentRegisterationNoSimilarCourse(teacherid);
            ViewBag.Regno = new SelectList(Regnolist, "EnrollmentId", "RegistrationId");
            return View();
        }
        [HttpPost]
        public ActionResult GetStudentRegno(FormCollection formcollection)
        {
            try
            {
                int teacherid = Convert.ToInt32(Session["Teacher"]);
                CourseDBHandle gc = new CourseDBHandle();

                List<Tbl_UserEnrollInCourseValidation> Regnolist = gc.GetStudentRegisterationNoSimilarCourse(teacherid);
                ViewBag.Regno = new SelectList(Regnolist, "EnrollmentId", "RegistrationId");
                var text = formcollection["hidText"];
                return RedirectToAction("result", new { RegNo = text });

            }
            catch (Exception ex)
            {
                ViewBag.Message = "There is some problem in processing";
                return View();
            }
        }



        [HttpGet]
        public ActionResult result(string RegNo)
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            int teacherid = Convert.ToInt32(Session["Teacher"]);

            Session["Regno"] = RegNo;


            CourseDBHandle gc = new CourseDBHandle();

            List<tbl_CourseAssigntoTeacherValidation> list = gc.GetTeacherStudentSimilarCourse(teacherid, RegNo);
            ViewBag.course = new SelectList(list, "courseId", "courseName");
            List<Exam> examlist = db.Exams.ToList();
            ViewBag.exam = new SelectList(examlist, "ExamId", "ExamName");

            var query = (from s in db.Students
                         join c in db.Tbl_Class on s.Class_Id equals c.Class_Id
                         join cs in db.Sections on s.Section_Id equals cs.SectionID
                         where s.RegNo == RegNo
                         select new StudentMasterViewModel()
                         {
                             RegNo = s.RegNo,
                             StudentIdtable = s.Id,
                             ClassId = s.Class_Id,
                             SectionId = s.Section_Id,
                             Name = s.Name,
                             ClassName = c.Name,
                             SectionName = cs.SectionName



                         }).FirstOrDefault();
            //ViewData["query"] = query;
            //StudentMasterViewModel objstudentMasterViewModel = new StudentMasterViewModel();

            //List<StudentModel> listOfStudentModel =
            //(
            //    from objStu in db.StudentMasters
            //    join objExam in db.Exams on objStu.Exam_Id equals objExam.ExamId
            //    join objclass in db.Tbl_Class on objStu.Id equals objclass.Class_Id
            //    join objReg in db.Students on objStu.Id equals objReg.Id
            //    select new StudentModel()
            //    {
            //        StudentId=objStu.Id,
            //        ClassName = objclass.Name,
            //        RegNumer = objReg.RegNo,
            //        ExamName = objExam.ExamName,
            //        Name = objStu.Name,


            //    }
            //).ToList();


            return View(query);




        }

        [HttpPost]
        public ActionResult result(tbl_StudentView objstudentViewModel)
        {

            int teacherid = Convert.ToInt32(Session["Teacher"]);
            int schoolid;
            var data = db.Teachers.Find(teacherid);
            schoolid = data.School_Id;


            string RegNo = Session["Regno"].ToString();



            CourseDBHandle gc = new CourseDBHandle();
            List<tbl_CourseAssigntoTeacherValidation> list = gc.GetTeacherStudentSimilarCourse(teacherid, RegNo);
            ViewBag.course = new SelectList(list, "courseId", "courseName");

            StudentMaster objStudentMaster = new StudentMaster()
            {

                Name = objstudentViewModel.Name,
                ClassName = objstudentViewModel.ClassName,
                CreatedBy = teacherid,
                SectionName = objstudentViewModel.SectionName,
                Exam_Id = Convert.ToInt32(objstudentViewModel.Exam_Id),
                RegNo = objstudentViewModel.RegNo

            };
            db.StudentMasters.Add(objStudentMaster);
            db.SaveChanges();

            foreach (var item in objstudentViewModel.ListofStudentMarks)
            {
                StudentResult objstudentResult = new StudentResult()
                {

                    CourseId = item.CourseId,
                    TotalMarks = item.TotalMarks,
                    MarksObtained = item.MarksObtained,
                    SchoolId = schoolid,
                    Percentage = item.Percentage,
                    Exam_Id = Convert.ToInt32(objstudentViewModel.Exam_Id),
                    ClassId = objstudentViewModel.ClassId,
                    CreatedDate = DateTime.Now,
                    StudentMasterId = objStudentMaster.Id,
                    StudentId = objstudentViewModel.StudentIdtable,
                    SectionId = objstudentViewModel.SectionId
                };
                db.StudentResults.Add(objstudentResult);
                db.SaveChanges();
            }


            return Json(new { message = "Data Successfully Added", status = true }, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult GetStudentMarks(int studentId)
        {
            List<StudentMarksModel> listofStudentMarksModel = (from obj in db.StudentResults
                                                               join objcourse in db.Courses on obj.CourseId equals objcourse.courseId
                                                               where obj.StudentResultId == studentId
                                                               select new StudentMarksModel()
                                                               {

                                                                   CourseName = objcourse.courseName,

                                                                   studentId = studentId,
                                                                   MarksObtained = obj.MarksObtained,
                                                                   TotalMarks = obj.TotalMarks,
                                                                   Percentage = obj.Percentage
                                                               }



                                                               ).ToList();

            return PartialView("_StudentMarksPartial", listofStudentMarksModel);
        }


        public JsonResult LoadStudent()
        {
            int teacherid = Convert.ToInt32(Session["Teacher"]);


            List<StudentModel> listOfStudentModel =
              (
                  from objStu in db.StudentMasters
                  join objExam in db.Exams on objStu.Exam_Id equals objExam.ExamId
                  where objStu.CreatedBy == teacherid
                  //join objclass in db.Tbl_Class on objStu.Id equals objclass.Class_Id
                  //join objReg in db.Students on objStu.Id equals objReg.Id
                  select new StudentModel()
                  {
                      ClassName = objStu.Name,
                      RegNumer = objStu.RegNo,
                      ExamName = objExam.ExamName,
                      StudentId = objStu.Id,
                      Name = objStu.Name,

                  }
              ).ToList();


            return Json(listOfStudentModel, JsonRequestBehavior.AllowGet);
        }



        public ActionResult viewAnnouncment()
        {
            int teacherid = Convert.ToInt32(Session["Teacher"]);


            int schoolid;


            var getteacherid = db.Teachers.Find(teacherid);

            schoolid = getteacherid.School_Id;



            var query = from sas in db.Announcements
                        join ss in db.Schools on sas.SchoolId equals ss.School_Id into table1
                        from ss in table1.DefaultIfEmpty()
                        where sas.SchoolId == schoolid
                        select new tbl_Announcementvalidation { SchoolName = ss.School_Name, Announcement_Title = sas.Announcement_Title, Announcement_Id = sas.Announcement_Id,Announcement_Body = sas.Announcement_Body };

            ViewBag.query = query;

            return View();
        }
        //[HttpPost]
        //[ValidateInput(false)]
        //public JsonResult announcementbody(int announceid)
        //{


                                
        //    var data = db.Announcements.Where(x => x.Announcement_Id == announceid).SingleOrDefault();

        //    var announce = data.Announcement_Body;
            


        //    return Json(announce, JsonRequestBehavior.AllowGet);
        //}

    }
}