using edu_course.Gateway;
using edu_course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace edu_course.Areas.Student.Controllers
{
    public class HomeController : Controller
    {
        Digital_LearningEntities db = new Digital_LearningEntities();

        // GET: Student/Home
        public ActionResult Index()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpGet]
        public ActionResult Profile()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            StudentDBHandle sadb = new StudentDBHandle();

            int userid = Convert.ToInt32((Session["Student"]));
            var sab = db.Students.Find(userid);
            Session["imgPath"] = sab.ImagePath;
            tbl_StudentValidation sa = sadb.GetProfileData(userid);

            return View(sa);
        }
        [HttpPost]
        public ActionResult Profile(tbl_StudentValidation sa)
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            try
            {
                tbl_StudentValidation student = new tbl_StudentValidation();
                int userid = Convert.ToInt32((Session["Student"]));

                var l = db.loginTables.FirstOrDefault(t => t.UserId == userid);

                if (ModelState.IsValid)
                {
                    if (sa.UserImageFIle != null)
                    {

                        if (l != null)
                        {
                            Session["StudentName"] = sa.Name;
                            l.UserId = userid;
                            l.Name = sa.Name;
                            l.Password = sa.Password;
                            l.Email = sa.Email;
                            l.RoleID = 4;
                            db.Entry(l).State = EntityState.Modified;

                            db.SaveChanges();
                        }
                        string filename = Path.GetFileNameWithoutExtension(sa.UserImageFIle.FileName);
                        string extension = Path.GetExtension(sa.UserImageFIle.FileName);
                        filename = DateTime.Now.ToString("yymmssff") + extension;




                        student.Image = "~/Content/img/users/" + filename;
                        student.Name = sa.Name;
                        student.Email = sa.Email;
                        student.Password = sa.Password;
                        student.Id = userid;





                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                        {
                            if (sa.UserImageFIle.ContentLength <= 1000000)
                            {
                                db.Entry(student).State = EntityState.Modified;



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
                        student.Image = Session["imgPath"].ToString();
                        if (l != null)
                        {
                            Session["name"] = sa.Name;
                            l.UserId = userid;
                            l.Name = sa.Name;
                            l.Password = sa.Password;
                            l.Email = sa.Email;
                            l.RoleID = 4;
                            db.Entry(l).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        student.Name = sa.Name;
                        student.Email = sa.Email;
                        student.Password = sa.Password;
                        student.Id = userid;
                        db.Entry(student).State = EntityState.Modified;

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
        public ActionResult kidstore()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<KidsStoryType> list = db.KidsStoryTypes.ToList();
            ViewBag.storytypelist = new SelectList(list, "KidsStoryTypeId", "KidsStoryName");


            return View();
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult kidstore(tbl_KidStory kid)
        {
            try
            {
                int studenid = Convert.ToInt32(Session["Student"]);
                int schoolid;
                var data = db.Teachers.Find(studenid);
                schoolid = data.School_Id;


                List<KidsStoryType> list = db.KidsStoryTypes.ToList();
                ViewBag.storytypelist = new SelectList(list, "KidsStoryTypeId", "KidsStoryName");

                KidsStory kd = new KidsStory();

                string filename = Path.GetFileNameWithoutExtension(kid.UserImageFIle.FileName);
                string extension = Path.GetExtension(kid.UserImageFIle.FileName);
                filename = DateTime.Now.ToString("yymmssff") + extension;
                kd.imgPath = "~/Content/img/" + filename;
                //image ko folder me save krwanay ke leye
                filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                kid.UserImageFIle.SaveAs(filename);
                kd.CreatedDate = DateTime.Now;
                kd.longDes = kid.longDes;
                kd.shortDes = kid.shortDes;
                kd.StoryTypeId = kid.StoryTypeId;
                kd.statusId = 1;
                kd.SchoolId = schoolid;
                kd.StoryTitle = kid.StoryTitle;
                db.KidsStories.Add(kd);

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
        public ActionResult viewstest()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int studentid = Convert.ToInt32(Session["Student"]);
            int tempclassid;
            int originalclassid;
            string Regno;

            var getstudentid = db.Students.Find(studentid);
            tempclassid = getstudentid.Class_Id;
            Regno = getstudentid.RegNo;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;

            CourseDBHandle gc = new CourseDBHandle();

            List<tblEnrollStudentInCourseValidation> list = gc.GetSudentEnrollCourse(Regno, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");



            return View();
        }
        [HttpPost]
        public ActionResult viewstest(tblEnrollStudentInCourseValidation testing)
        {
            try
            {
                int studentid = Convert.ToInt32(Session["Student"]);
                int tempclassid;
                int originalclassid;
                string Regno;
                var getstudentid = db.Students.Find(studentid);
                tempclassid = getstudentid.Class_Id;
                Regno = getstudentid.RegNo;
                var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
                originalclassid = classid.Class_Id;

                CourseDBHandle gc = new CourseDBHandle();

                List<tblEnrollStudentInCourseValidation> list = gc.GetSudentEnrollCourse(Regno, originalclassid);
                ViewBag.course = new SelectList(list, "courseId", "courseName");


                return RedirectToAction("viewuploadedtest", new { courseid = testing.courseId });
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Please try later";
                return View();
            }



        }
        public ActionResult assignment()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpGet]
        public ActionResult viewassignment(int? courseid)
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            if (courseid != null)
            {
                TempData["id"] = courseid;
                var data = db.SchoolAssignments.Where(x => x.CourseId == courseid).ToList();
                return View(data);
            }
            return View();
        }
        public ActionResult viewassignmentss()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int studentid = Convert.ToInt32(Session["Student"]);
            int tempclassid;
            int originalclassid;
            string Regno;

            var getstudentid = db.Students.Find(studentid);
            tempclassid = getstudentid.Class_Id;
            Regno = getstudentid.RegNo;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;
            //AB enroll course wala dropdown ajayega 
            CourseDBHandle gc = new CourseDBHandle();

            List<tblEnrollStudentInCourseValidation> list = gc.GetSudentEnrollCourse(Regno, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");



            return View();
        }
        [HttpPost]
        public ActionResult viewassignmentss(tblEnrollStudentInCourseValidation testing)
        {
            try
            {
                int studentid = Convert.ToInt32(Session["Student"]);
                int tempclassid;
                int originalclassid;
                string Regno;
                var getstudentid = db.Students.Find(studentid);
                tempclassid = getstudentid.Class_Id;
                Regno = getstudentid.RegNo;
                var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
                originalclassid = classid.Class_Id;
                //AB enroll course wala dropdown ajayega 
                CourseDBHandle gc = new CourseDBHandle();

                List<tblEnrollStudentInCourseValidation> list = gc.GetSudentEnrollCourse(Regno, originalclassid);
                ViewBag.course = new SelectList(list, "courseId", "courseName");


                //var data = db.SchoolAssignments.ToList();
                return RedirectToAction("viewassignment", new { courseid = testing.courseId });
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Please try later";
                return View();
            }


        }
        [HttpGet]

        public ActionResult assignmentsubmitted()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]

        public ActionResult assignmentsubmitted(tbl_AssignmentSubmittedValidation SA, int id)
        {
            int studentid = Convert.ToInt32(Session["Student"]);
            int tempclassid;
            int originalclassid;
            int schoolid;
            string Regno;
            var getstudentid = db.Students.Find(studentid);
            tempclassid = getstudentid.Class_Id;
            schoolid = getstudentid.School_Id;
            Regno = getstudentid.RegNo;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;



            if (SA.UserdocFIle == null)
            {
                ModelState.AddModelError("CustomError", "Please Select File");
                return View();
            }
            if (!(SA.UserdocFIle.ContentType == "application/pdf" || SA.UserdocFIle.ContentType == "application/pdf"))
            {
                ModelState.AddModelError("CustomError", "Select Doc or Pdf extention file only ");
                return View();
            }

            try
            {
                SubmitAssignment sassignemnt = new SubmitAssignment();


                string fileName = Guid.NewGuid() + Path.GetExtension(SA.UserdocFIle.FileName);
                SA.UserdocFIle.SaveAs(Path.Combine(Server.MapPath("~/file_upload"), fileName));

                sassignemnt.UploadUrl = fileName;
                sassignemnt.CourseId = Convert.ToInt32(TempData["id"]);
                sassignemnt.CreatedDate = DateTime.Now;
                sassignemnt.AssignmentId = id;
                sassignemnt.SchoolId = schoolid;
                sassignemnt.UserId = Convert.ToInt32(Session["Student"]);

                db.SubmitAssignments.Add(sassignemnt);

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
        public ActionResult testsubmit()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult testsubmit(tbl_uploadtestValidation tf, int id)
        {
            int studentid = Convert.ToInt32(Session["Student"]);
            int tempclassid;
            int originalclassid;
            int schoolid;
            string Regno;
            var getstudentid = db.Students.Find(studentid);
            tempclassid = getstudentid.Class_Id;
            schoolid = getstudentid.School_Id;
            Regno = getstudentid.RegNo;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;



            if (tf.UserdocFIle == null)
            {
                ModelState.AddModelError("CustomError", "Please Select File");
                return View();
            }
            if (!(tf.UserdocFIle.ContentType == "application/pdf" || tf.UserdocFIle.ContentType == "application/pdf"))
            {
                ModelState.AddModelError("CustomError", "Select Doc or Pdf extention file only ");
                return View();
            }

            try
            {
                SubmitManualTest stest = new SubmitManualTest();


                string fileName = Guid.NewGuid() + Path.GetExtension(tf.UserdocFIle.FileName);
                tf.UserdocFIle.SaveAs(Path.Combine(Server.MapPath("~/file_upload"), fileName));

                stest.Uploadurl = fileName;
                stest.CourseId = Convert.ToInt32(TempData["id"]);
                stest.CreatedDate = DateTime.Now;
                stest.TestId = id;
                stest.SchoolId = schoolid;
                stest.UserId = Convert.ToInt32(Session["Student"]);

                db.SubmitManualTests.Add(stest);

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

        public ActionResult viewonlinetest()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            int studentid = Convert.ToInt32(Session["Student"]);
            int tempclassid;
            int originalclassid;
            string Regno;
            var getstudentid = db.Students.Find(studentid);
            tempclassid = getstudentid.Class_Id;
            Regno = getstudentid.RegNo;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;
            //AB enroll course wala dropdown ajayega 
            CourseDBHandle gc = new CourseDBHandle();

            List<tblEnrollStudentInCourseValidation> list = gc.GetSudentEnrollCourse(Regno, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");


            return View();
        }
        [HttpPost]
        public ActionResult viewonlinetest(int courseId)
        {
            loginTable objuser = new loginTable();

            //Session["SName"] = userName;
            Session["CourseId"] = courseId;

            return View("ShowQuestion");

        }

        public PartialViewResult UserQuestionAnswer(CandidateAnswer objCandidateAnswer)
        {

            bool IsLast = false;


            if (objCandidateAnswer.AnswerText != null)
            {
                List<CandidateAnswer> objCandidateAnswers = Session["CadQuestionAnswer"] as List<CandidateAnswer>;

                if (objCandidateAnswers == null)
                {
                    objCandidateAnswers = new List<CandidateAnswer>();
                }

                objCandidateAnswers.Add(objCandidateAnswer);
                Session["CadQuestionAnswer"] = objCandidateAnswers;
            }
            int pageSize = 1;
            int pageNumber = 0;
            int CourseId = Convert.ToInt32(Session["CourseId"]);

            if (Session["CadQuestionAnswer"] == null)
            {
                pageNumber = pageNumber + 1;
            }
            else
            {
                List<CandidateAnswer> canAnswer = Session["CadQuestionAnswer"] as List<CandidateAnswer>;
                pageNumber = canAnswer.Count + 1;

            }

            List<OnlineTest> listonlinetest = new List<OnlineTest>();
            listonlinetest = db.OnlineTests.Where(model => model.CourseId == CourseId).ToList();
            if (pageNumber == listonlinetest.Count)
            {
                IsLast = true;
            }


            tbl_onlinetestAnswer objAnswer = new tbl_onlinetestAnswer();
            OnlineTest objquestion = new OnlineTest();
            objquestion = listonlinetest.Skip((pageNumber - 1) * pageSize).Take(pageSize).FirstOrDefault();

            objAnswer.IsLast = IsLast;
            objAnswer.QuestionId = objquestion.QuestionId;
            objAnswer.Questionname = objquestion.QuestionName;
            objAnswer.ListOfQuizOption = (from obj in db.OnlineTestQuestionOptions
                                          where obj.QuestionId == objquestion.QuestionId
                                          select new tbl_onlinetestoption()
                                          {
                                              OptionName = obj.OptionName,
                                              OptionId = obj.OptionId
                                          }).ToList();


            return PartialView("quizquestionoption", objAnswer);
        }

        public JsonResult SaveCandidateAnswer(CandidateAnswer objcandidateAnswer)
        {
            List<CandidateAnswer> canAnswer = Session["CadQuestionAnswer"] as List<CandidateAnswer>;

            if (objcandidateAnswer.AnswerText != null)
            {
                List<CandidateAnswer> objCandidateAnswers = Session["CadQuestionAnswer"] as List<CandidateAnswer>;
                if (objCandidateAnswers == null)
                {
                    objCandidateAnswers = new List<CandidateAnswer>();
                }
                objCandidateAnswers.Add(objcandidateAnswer);
                Session["CadQuestionAnswer"] = objCandidateAnswers;


            }



            foreach (var item in canAnswer)
            {
                OnlineTestResult result = new OnlineTestResult();
                result.AnswerText = item.AnswerText;
                result.QuestionId = item.QuestionId;

                result.UserId = Convert.ToInt32(Session["std"]);
                result.Role_Id = Convert.ToInt32(Session["RoleId"]);

                db.OnlineTestResults.Add(result);
                db.SaveChanges();
            }
            return Json(new { message = "Data Successfully Added", success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFinalResult()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<CandidateAnswer> listofQuestionAnswers;
            listofQuestionAnswers = Session["CadQuestionAnswer"] as List<CandidateAnswer>;
            var UserResult = (from result in listofQuestionAnswers
                              join objAnswer in db.OnlineTestAnswers on result.QuestionId equals objAnswer.QuestionId
                              join objQuestion in db.OnlineTests on result.QuestionId equals objQuestion.QuestionId

                              select new ResultModel()
                              {

                                  QuestionName = objQuestion.QuestionName,
                                  Answer = objAnswer.AnswerText,
                                  AnswerbyUser = result.AnswerText,
                                  Status = objAnswer.AnswerText == result.AnswerText ? "Correct" : "Wrong"

                              }).ToList();
            Session.Abandon();
            return View(UserResult);
        }
        public ActionResult viewuploadedtest(int? courseid)
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            if (courseid != null)
            {
                TempData["id"] = courseid;
                var data = db.ManualTests.Where(x => x.CourseId == courseid).ToList();
                return View(data);
            }
            return View();
        }



        [HttpGet]
        public ActionResult notesview(int? courseid)
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            if (courseid != null)
            {
                TempData["id"] = courseid;
                var data = db.Lectures.Where(x => x.CourseId == courseid).ToList();
                return View(data);
            }
            return View();
        }

        public ActionResult notesviewer()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int studentid = Convert.ToInt32(Session["Student"]);
            int tempclassid;
            int originalclassid;
            string Regno;

            var getstudentid = db.Students.Find(studentid);
            tempclassid = getstudentid.Class_Id;
            Regno = getstudentid.RegNo;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;

            CourseDBHandle gc = new CourseDBHandle();

            List<tblEnrollStudentInCourseValidation> list = gc.GetSudentEnrollCourse(Regno, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");



            return View();
        }
        [HttpPost]
        public ActionResult notesviewer(tblEnrollStudentInCourseValidation testing)
        {
            int studentid = Convert.ToInt32(Session["Student"]);
            int tempclassid;
            int originalclassid;
            string Regno;
            var getstudentid = db.Students.Find(studentid);
            tempclassid = getstudentid.Class_Id;
            Regno = getstudentid.RegNo;
            var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
            originalclassid = classid.Class_Id;

            CourseDBHandle gc = new CourseDBHandle();

            List<tblEnrollStudentInCourseValidation> list = gc.GetSudentEnrollCourse(Regno, originalclassid);
            ViewBag.course = new SelectList(list, "courseId", "courseName");


            return RedirectToAction("notesview", new { courseid = testing.courseId });


        }
        [HttpGet]
        public ActionResult addfeedback()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            try
            {
                int studentid = Convert.ToInt32(Session["Student"]);
                int tempclassid;
                int schoolid;
                int originalclassid;
                string Regno;
                var getstudentid = db.Students.Find(studentid);
                tempclassid = getstudentid.Class_Id;
                schoolid = getstudentid.School_Id;
                Regno = getstudentid.RegNo;
                var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
                originalclassid = classid.Class_Id;

                CourseDBHandle gc = new CourseDBHandle();

                List<tblEnrollStudentInCourseValidation> list = gc.GetSudentEnrollCourse(Regno, originalclassid);
                ViewBag.course = new SelectList(list, "courseId", "courseName");
                //ViewBag.Product_CategoryID = new SelectList(db.Courses, "courseId", "courseName");
                return View();

            }
            catch (Exception ex)
            {
                ViewBag.Message = "There is some error in processing";
                return View();
            }

        }
        [HttpPost]
        public ActionResult addfeedback(tbl_FeedbackValidation fv)
        {
            try
            {
                int studentid = Convert.ToInt32(Session["Student"]);
                int tempclassid;
                int schoolid;
                int originalclassid;
                string Regno;
                var getstudentid = db.Students.Find(studentid);
                tempclassid = getstudentid.Class_Id;
                schoolid = getstudentid.School_Id;
                Regno = getstudentid.RegNo;
                var classid = db.Tbl_Class.Where(x => x.Class_Id == tempclassid).SingleOrDefault();
                originalclassid = classid.Class_Id;

                fv.SchoolId = schoolid;
                CourseDBHandle gc = new CourseDBHandle();

                List<tblEnrollStudentInCourseValidation> list = gc.GetSudentEnrollCourse(Regno, originalclassid);
                ViewBag.course = new SelectList(list, "courseId", "courseName");
                UserFeedback f = new UserFeedback();
                f.CourseId = fv.CourseId;
                f.Description = fv.Description;
                f.CreatedDate = DateTime.Now;
                f.UserId = Convert.ToInt32(Session["Student"]);
                f.RoleId = 4;
                f.SchoolId = fv.SchoolId;

                db.UserFeedbacks.Add(f);
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

        public ActionResult viewfeedback()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpGet]

        public ActionResult adddiscussion()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var Course = new SelectList(db.Courses.ToList(), "courseId", "courseName");
            ViewData["Courses"] = Course;
            return View();
        }
        [HttpPost]

        public ActionResult adddiscussion(Discussion d)
        {
            var Course = new SelectList(db.Courses.ToList(), "courseId", "courseName");
            ViewData["Courses"] = Course;
            return View();
        }
        public ActionResult viewdiscussion()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult detaildiscussionform()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }


        public ActionResult contactform()
        {
            return View();
        }
        public ActionResult viewcontact()
        {
            return View();
        }
        public ActionResult resultform()
        {
            return View();
        }
        [HttpGet]
        public ActionResult addComplainform()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addComplainform(tblStudentComplainValidation complain)
        {
            int studentid = Convert.ToInt32(Session["Student"]);


            int schoolid;


            var getstudentid = db.Students.Find(studentid);
           
            schoolid = getstudentid.School_Id;
           
            
            complain.School_Id = schoolid;





            StudentComplain stdcomplain = new StudentComplain();
            stdcomplain.complain_description = complain.complain_description;
            stdcomplain.complain_date = DateTime.Now;
            stdcomplain.School_Id = complain.School_Id;
            stdcomplain.UserId = studentid;
             
            db.StudentComplains.Add(stdcomplain);
            db.SaveChanges();

            ViewBag.Message = "Data Submitted";

            ModelState.Clear();


            return View();
        }
        public ActionResult viewComplainform()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            int studentid = Convert.ToInt32(Session["Student"]);


            int schoolid;


            var getstudentid = db.Students.Find(studentid);

            schoolid = getstudentid.School_Id;





            var data = (from com in db.StudentComplains
                        join sc in db.Schools on com.School_Id equals sc.School_Id
                        where com.School_Id == schoolid
                        select new tblStudentComplainValidation
                        {
                            complain_id = com.complain_id,
                            SchoolName = sc.School_Name,
                            
                            complain_description = com.complain_description,
                            ReplyMsg = com.ReplyMsg,
 

                        }).ToList();


            return View(data);
        }
        public ActionResult detailcomplainform()
        {
            return View();
        }


        [HttpGet]
        public ActionResult kidStoryType()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }

        [HttpPost]
        public ActionResult kidStoryType(tbl_kidStoryTypeValidation kid)
        {
            try
            {
                KidsStoryType kidtype = new KidsStoryType();
                kidtype.KidsStoryName = kid.KidsStoryName;
                db.KidsStoryTypes.Add(kidtype);
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
        //[HttpGet]
        //public ActionResult kidstore()
        //{
        //    if (Session["Student"] == null)
        //    {
        //        return RedirectToAction("login", "Home", new { area = "" });
        //    }
        //    List<KidsStoryType> list = db.KidsStoryTypes.ToList();
        //    ViewBag.storytypelist = new SelectList(list, "KidsStoryTypeId", "KidsStoryName");


        //    return View();
        //}
        //[HttpPost]
        //[ValidateInput(false)]

        //public ActionResult kidstore(tbl_KidStory kid)
        //{
        //    try
        //    {

        //        List<KidsStoryType> list = db.KidsStoryTypes.ToList();
        //        ViewBag.storytypelist = new SelectList(list, "KidsStoryTypeId", "KidsStoryName");

        //        KidsStory kd = new KidsStory();

        //        string filename = Path.GetFileNameWithoutExtension(kid.UserImageFIle.FileName);
        //        string extension = Path.GetExtension(kid.UserImageFIle.FileName);
        //        filename = DateTime.Now.ToString("yymmssff") + extension;
        //        kd.imgPath = "~/Content/img/" + filename;
        //        //image ko folder me save krwanay ke leye
        //        filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
        //        kid.UserImageFIle.SaveAs(filename);
        //        kd.CreatedDate = DateTime.Now;
        //        kd.longDes = kid.longDes;
        //        kd.shortDes = kid.shortDes;
        //        kd.StoryTypeId = kid.StoryTypeId;
        //        kd.StoryTitle = kid.StoryTitle;



        //        db.KidsStories.Add(kd);

        //        db.SaveChanges();

        //        ModelState.Clear();
        //        ViewBag.Message = "Data Submitted";

        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = "Not Submitted";
        //        return View();
        //    }

        //    return View();
        //}

        [HttpGet]
        public ActionResult articletyped()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]

        public ActionResult articletyped(tbl_articletypeValidation art)
        {
            try
            {
                ArticleType arttype = new ArticleType();
                arttype.Article_Typename = art.Article_Typename;
                db.ArticleTypes.Add(arttype);
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
        public ActionResult addarticle()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<ArticleType> list = db.ArticleTypes.ToList();
            ViewBag.articletypelist = new SelectList(list, "Article_TypeId", "Article_Typename ");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addarticle(tbl_ArticleValidation article)
        {
            try
            {
                Article a = new Article();

                List<ArticleType> list = db.ArticleTypes.ToList();
                ViewBag.articletypelist = new SelectList(list, "Article_TypeId", "Article_Typename");


                string filename = Path.GetFileNameWithoutExtension(article.UserImageFIle.FileName);
                string extension = Path.GetExtension(article.UserImageFIle.FileName);
                filename = DateTime.Now.ToString("yymmssff") + extension;
                a.imgPath = "~/Content/img/" + filename;
                //image ko folder me save krwanay ke leye
                filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                article.UserImageFIle.SaveAs(filename);
                a.Article_TypeId = article.Article_TypeId;
                a.Role_Id = Convert.ToInt32(Session["RoleId"]);
                a.Title = article.Title;
                a.shortDes = article.shortDes;
                a.longDes = article.longDes;
                a.UserId = Convert.ToInt32(Session["Ad"]);
                a.CreatedDate = DateTime.Now;

                db.Articles.Add(a);

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


        public ActionResult viewAnnouncment()
        {
            int studentid = Convert.ToInt32(Session["Student"]);


            int schoolid;


            var getstudentid = db.Students.Find(studentid);

            schoolid = getstudentid.School_Id;



            var query = from sas in db.Announcements
                        join ss in db.Schools on sas.SchoolId equals ss.School_Id into table1
                        from ss in table1.DefaultIfEmpty()
                        where sas.SchoolId == schoolid
                        select new tbl_Announcementvalidation { SchoolName = ss.School_Name, Announcement_Title = sas.Announcement_Title, Announcement_Id = sas.Announcement_Id, Announcement_Body = sas.Announcement_Body };

            ViewBag.query = query;

            return View();
        }

    }
}