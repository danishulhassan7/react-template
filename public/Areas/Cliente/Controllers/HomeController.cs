using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using edu_course.Gateway;
using edu_course.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace edu_course.Areas.Cliente.Controllers
{
   
    public class HomeController : Controller
    {
       

        Digital_LearningEntities db = new Digital_LearningEntities();
        // GET: Client/Home
        public ActionResult dashboard()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            return View();
        }
        [HttpGet]
        public ActionResult Profile()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            ClientDBHandle sadb = new ClientDBHandle();

            int userid = Convert.ToInt32((Session["Cliente"]));
            var sab = db.Clients.Find(userid);
            Session["imgPath"] = sab.Image;
            tbl_ClientValidation sa = sadb.GetProfileData(userid);

            return View(sa);
        }

        [HttpPost]
        public ActionResult Profile(tbl_ClientValidation sa)

        {
            Client client = new Client();
            try
            {
                //loginTable l = new loginTable();
                int userid = Convert.ToInt32((Session["Cliente"]));

                var l = db.loginTables.FirstOrDefault(t => t.UserId == userid);

                if (ModelState.IsValid)
                {
                    if (sa != null)
                    {

                        if (l != null)
                        {
                            Session["ClientName"] = sa.UserName;
                            l.UserId = userid;
                            l.Name = sa.UserName;
                            l.Password = sa.Password;
                            l.Email = sa.Email;
                            l.RoleID = 5;
                            db.Entry(l).State = EntityState.Modified;

                            db.SaveChanges();
                        }
                        string filename = Path.GetFileNameWithoutExtension(sa.UserImageFIle.FileName);
                        string extension = Path.GetExtension(sa.UserImageFIle.FileName);
                        filename = DateTime.Now.ToString("yymmssff") + extension;




                        client.Image = "~/Content/img/users/" + filename;
                        client.UserName = sa.UserName;
                        client.Email = sa.Email;
                        client.Password = sa.Password;
                        client.UserId = userid;





                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                        {
                            if (sa.UserImageFIle.ContentLength <= 1000000)
                            {
                                db.Entry(client).State = EntityState.Modified;



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
                        client.Image = Session["imgPath"].ToString();
                        if (l != null)
                        {
                            Session["ClientName"] = sa.UserName;
                            l.UserId = userid;
                            l.Name = sa.UserName;
                            l.Password = sa.Password;
                            l.Email = sa.Email;
                            l.RoleID = 5;
                            db.Entry(l).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        client.UserName = sa.UserName;
                        client.Email = sa.Email;
                        client.Password = sa.Password;
                        client.UserId = userid;
                        db.Entry(client).State = EntityState.Modified;

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
        public ActionResult addarticle()
        {
            if (Session["Cliente"] == null)
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
                a.statusId = 2;
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
        public ActionResult viewarticle()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var user =Convert.ToInt32(Session["Cliente"]);


            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Article> artis = entities.Articles.ToList();
            List<loginTable> logo = entities.loginTables.ToList();
            var query = from a in artis
                        join al in logo on a.UserId equals al.UserId 
                        join ad in db.SuperAdmins on  a.UserId equals ad.ad_Id
                        where a.Role_Id == 1 &&  al.UserId == user

                        select new ArticleDetail { art = a , log=al, admi  = ad};

            ViewBag.artis = query;
            
            return View();
        }




        public ActionResult course(int PageNumber = 1)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            CourseDBHandle gc = new CourseDBHandle();
            List<tbl_coursevalidation> list = gc.GetCourseClient();
            ViewBag.TotalPages = Math.Ceiling(list.Count() / 12.0);
            ViewBag.PageNumber = PageNumber;
            list = list.Skip((PageNumber - 1) * 12).Take(5).ToList();

            ViewBag.course = list;
            return View();
        }
        public ActionResult Enrollment(int courseid)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            using (Digital_LearningEntities entities = new Digital_LearningEntities())
            {
                //Response.Write(submit);
                return View(entities.Courses.Single(x => x.courseId == courseid));
            }



        }
        [HttpPost]
        public ActionResult Enrollment(Course enrollCourse)
        {
            int clientid = Convert.ToInt32(Session["Cliente"]);

            UserEnrollInCourse e = new UserEnrollInCourse();

            var enrollCourses = db.UserEnrollInCourses.Where(s => s.RoleId == 5 && s.CourseId == enrollCourse.courseId && s.UserId == clientid && s.statusId == 2).SingleOrDefault();
            if (enrollCourses == null)
            {
                var date = DateTime.Now;
                e.CourseId = enrollCourse.courseId;
                e.Enrolldate = date;
                e.UserId = clientid;
                e.RoleId = 5;
                e.IsUserActive = true;
                e.statusId = 1;
                db.UserEnrollInCourses.Add(e);
                db.SaveChanges();
                TempData["msg"] = "Please Wait For Approval";


            }
            else
            {
                TempData["msg"] = "You Are Already Enrolled in this course";
            }

            return RedirectToAction("course");
        }

        public ActionResult MyCourses()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var user = Convert.ToInt32(Session["Cliente"]);

            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<UserEnrollInCourse> clientes = entities.UserEnrollInCourses.ToList();
            List<Course> course = entities.Courses.ToList();


            List<Status> status = entities.Status.ToList();
            var query = from c in clientes
                        join cou in course on c.CourseId equals cou.courseId into table1
                        from cou in table1.DefaultIfEmpty()

                        where c.statusId == 2 && c.UserId == user && c.RoleId == 5




                        select new ViewStatus { usercourse = c, coursename = cou };
            return View(query);
        }


        public ActionResult notesview(int? courseId)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            if (courseId != null)
            {
                TempData["id"] = courseId;
                var data = db.Lectures.Where(x => x.CourseId == courseId).ToList();
                return View(data);
            }
            return View();
        }


        public ActionResult viewonlinetest(int courseId)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
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

                result.UserId = Convert.ToInt32(Session["Cliente"]);
                result.Role_Id = Convert.ToInt32(Session["RoleId"]);

                db.OnlineTestResults.Add(result);
                db.SaveChanges();
            }
            return Json(new { message = "Data Successfully Added", success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFinalResult()
        {
            int counterquestion = 0;
            decimal percentage = 0;
            int counterrightanswers = 0;
            string temp;
            decimal temp2 = 0;
            string coursename;
            string username;
            var user = db.Clients.Find(Session["Cliente"]);
            username = user.UserName;

            var course = db.Courses.Find(Session["CourseId"]);
            coursename = course.courseName;

            if (Session["Cliente"] == null)
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


            //Session.Abandon();

            foreach (var item in UserResult)
            {
                if (item.QuestionName != null)
                {
                    counterquestion++;
                }

            }
            foreach (var item in UserResult)
            {
                temp = item.Status;
                if (temp.Equals("Correct"))
                {
                    counterrightanswers++;
                }
            }
            ViewBag.totalmarks = counterquestion;
            ViewBag.MarksObtained = counterrightanswers;
            temp2 = Convert.ToDecimal(counterrightanswers) / Convert.ToDecimal(counterquestion);
            percentage = temp2 * 100;
            ViewBag.counterquestion = percentage;
            ViewBag.username = username;
            ViewBag.coursename = coursename;




            return View(UserResult);
        }


        public ActionResult CertificateGeneration(decimal Percentage, string Coursename, string Username, int totalmarks, int MarksObtained)
        {
            Session["percentage"] = Percentage;
            Session["Coursename"] = Coursename;
            Session["Username"] = Username;
            Session["totalmarks"] = totalmarks;
            Session["MarksObtained"] = MarksObtained;




            List<CertificateType> certificatetype = db.CertificateTypes.ToList();
            ViewBag.certificate = new SelectList(certificatetype, "Certificate_Type_Id", "Certificate_Type_Name");
            return View("TestCertificate");
        }
        [HttpPost]
        public ActionResult CertificateGeneration(ClientCertificate c)
        {
            decimal percentage = Convert.ToDecimal(Session["percentage"]);
            decimal roundedValue = Math.Round(percentage, 2, MidpointRounding.AwayFromZero);

            List<CertificateType> certificatetype = db.CertificateTypes.ToList();
            ViewBag.certificate = new SelectList(certificatetype, "Certificate_Type_Id", "Certificate_Type_Name");
            if (c.Certificate_Type_Id == 1)
            {
                c.UserName = Session["Username"].ToString();
                c.Percentage = roundedValue;
                c.TotalMarks = Convert.ToInt32(Session["totalmarks"]);
                c.Issue_Date = DateTime.Now;
                c.MarksObtained = Convert.ToInt32(Session["MarksObtained"]);
                c.CourseName = Session["Coursename"].ToString();
                db.ClientCertificates.Add(c);
                db.SaveChanges();
                ViewBag.Message = "You Have to Pay Some Amount For This";

            }
            if (c.Certificate_Type_Id == 2)

            {


                c.UserName = Session["Username"].ToString();
                c.Percentage = roundedValue;
                c.TotalMarks = Convert.ToInt32(Session["totalmarks"]);

                c.MarksObtained = Convert.ToInt32(Session["MarksObtained"]);
                c.Issue_Date = DateTime.Now;

                c.CourseName = Session["Coursename"].ToString();
                db.ClientCertificates.Add(c);
                db.SaveChanges();
                ViewBag.successfull = true;
                //return RedirectToAction("Download_PDF");

            }
            return View("TestCertificate");


        }

        public ActionResult Download_PDF()
        {

            ReportDocument rprt = new ReportDocument();
            rprt.Load(Path.Combine(Server.MapPath("~/Report"), "Certificate.rpt"));
            SqlConnection con = new SqlConnection(@"server=DESKTOP-VSODOF4\SQLEXPRESS; database=Digital_Learning; Integrated Security=SSPI");
            SqlCommand cmd = new SqlCommand("[Digital_Learning].get_Certificate", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rprt.SetDataSource(dt);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rprt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);


            return File(stream, "application/pdf", "CustomerList.pdf");

        }

        public ActionResult result()
        {
            return View();
        }
        [HttpGet]
        public ActionResult paymenform()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            List<Bank> listt = db.Banks.ToList();
            ViewBag.bank = new SelectList(listt, "BankId", "BankName");

            List<TranscationType> lists = db.TranscationTypes.ToList();
            ViewBag.transaction = new SelectList(lists, "TranscationTypeId", "TransactionType");


            return View();
        }
        [HttpPost]
        public ActionResult paymenform(tbl_paymentValidation payment)
        {
            try
            {
                if (payment.UserImageFIle == null)
                {
                    ModelState.AddModelError("NoFile", "Upload File");
                }
                else
                {
                    PaymentConfirmation pay = new PaymentConfirmation();
                    List<TranscationType> lists = db.TranscationTypes.ToList();
                    ViewBag.transaction = new SelectList(lists, "TranscationTypeId", "TransactionType");

                    List<Bank> listt = db.Banks.ToList();
                    ViewBag.bank = new SelectList(listt, "BankId", "BankName");
                    string filename = Path.GetFileNameWithoutExtension(payment.UserImageFIle.FileName);
                    string extension = Path.GetExtension(payment.UserImageFIle.FileName);
                    filename = DateTime.Now.ToString("yymmssff") + extension;
                    pay.SlipUrl = "~/Content/img/" + filename;

                    filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                    payment.UserImageFIle.SaveAs(filename);


                    pay.BankId = payment.BankId;
                    pay.Account_No = payment.Account_No;

                    pay.TransactionTypeId = payment.TransactionTypeId;
                    pay.UserId = Convert.ToInt32(Session["Cliente"]);
                    pay.TransactionSlipNumber = payment.TransactionSlipNumber;
                    pay.IBAN = payment.IBAN;
                    db.PaymentConfirmations.Add(pay);

                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = "Data Submitted";

                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
            return View();
        }
        [HttpGet]
        public ActionResult addfeedback()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<Course> list = db.Courses.ToList();
            ViewBag.courses = new SelectList(list, "CourseId", "courseName");

            return View();
        }
        [HttpPost]
        public ActionResult addfeedback(tbl_clienteFeedback feed,int rating)
        {
            
            List<Course> list = db.Courses.ToList();
            ViewBag.courses = new SelectList(list, "CourseId", "courseName");
            var user = Convert.ToInt32(Session["Cliente"]);
            var  RoleId = Convert.ToInt32(Session["RoleId"]);
            UserFeedback usfeedback = new UserFeedback();
            usfeedback.Rating = rating;
            usfeedback.UserId = user;
            usfeedback.RoleId = RoleId;
            usfeedback.CourseId = feed.CourseId;
            usfeedback.Description = feed.Description;
            usfeedback.CreatedDate = DateTime.Now;
            db.UserFeedbacks.Add(usfeedback);
            db.SaveChanges();
            ViewBag.Message = "Feedback Submitted";

            ModelState.Clear();


            return View();
        }
        
        public ActionResult addComplainform()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult addComplainform(tblClientorSchoorComplainValidation complain)
        {
            complain.RoleId = Convert.ToInt32(Session["RoleId"]);


            complain.UserId = Convert.ToInt32(Session["Cliente"]);


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
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var UserId = Convert.ToInt32(Session["Cliente"]);
            var roleId = Convert.ToInt32(Session["RoleId"]);


            var data = (from com in db.Client_SchoolComplain
                        join sc in db.Clients on com.UserId equals sc.UserId
                        where com.RoleId == roleId && com.UserId == UserId
                        select new tblClientorSchoorComplainValidation
                        {
                            complain_Id = com.complain_Id,
                            ScholName = sc.UserName,
                            UserId = UserId,
                            complain_description = com.complain_description,
                            replymsg = com.replymsg,
                            complain_date = com.complain_date,


                        }).ToList();

            //var data = db.Client_SchoolComplain.ToList();



            return View(data);
        }


        public ActionResult detailcomplainform()
        {
            return View();
        }


        [HttpGet]
        public ActionResult TechnicalTips()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TechnicalTips(tbl_technicalTipsValidation tips)
        {
            try
            {
                TechnicTip tech = new TechnicTip();




                tech.Role_Id = Convert.ToInt32(Session["RoleId"]);
                tech.Title = tips.Title;
                tech.VedioPath = tips.VedioPath;
                tech.shortDes = tips.shortDes;
                tech.statusId = 2;
                tech.longDes = tips.longDes;
                tech.UserId = Convert.ToInt32(Session["Cliente"]);
                tech.CreatedDate = DateTime.Now;

                db.TechnicTips.Add(tech);

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

        public ActionResult viewTechnicalTips()
        {

            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var user = Convert.ToInt32(Session["Cliente"]);
            var name = (Session["ClientName"]).ToString();
            var query = (from a in db.TechnicTips

                         where a.Role_Id == 5 &&  a.UserId == user

                         select new tbl_technicalTipsValidation
                         {
                             tipsId = a.tipsId,
                             clientname = name,
                             Title = a.Title,
                             VedioPath = a.VedioPath,
                             longDes = a.longDes,
                             CreatedDate = a.CreatedDate,

                         }).ToList();




            return View(query);

        }
        [HttpPost]

        public JsonResult DeleteTechnicalTips(int tipsid)
        {

            bool resul = false;
            TechnicTip sc = db.TechnicTips.SingleOrDefault(x => x.tipsId == tipsid);
            if (sc != null)
            {
                db.TechnicTips.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult updateTechnicalTip(int id)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }


            var data = db.TechnicTips.Where(x => x.tipsId == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updateTechnicalTip(TechnicTip dat, int id)
        {

            try
            {

                var data = db.TechnicTips.Where(x => x.tipsId == id).First();

                if (data != null)
                {

                    data.Title = dat.Title;
                    data.VedioPath = dat.VedioPath;
                    data.shortDes = dat.shortDes;
                    data.longDes = dat.longDes;

                    db.Entry(data).State = System.Data.Entity.EntityState.Modified;
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
            return RedirectToAction("viewTechnicalTips", "Home");



        }



    }
}

