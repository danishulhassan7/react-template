using edu_course.Gateway;
using edu_course.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace edu_course.Areas.Cliente.Controllers
{
    public class MainHomeController : Controller
    {
        // GET: Cliente/MainHome
        Digital_LearningEntities db = new Digital_LearningEntities();
        public ActionResult Index()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            CourseDBHandle c = new CourseDBHandle();
            List<jobModel> lists = c.GetJobs();
            lists = lists.Skip(0).Take(3).ToList();
            ViewBag.job = lists;
            CourseDBHandle gc = new CourseDBHandle();
            List<tbl_coursevalidation> list = gc.GetCourseClient();
            list = list.Skip(0).Take(3).ToList();
            ViewBag.course = list;

            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<KidsStory> ks = entities.KidsStories.ToList();
            List<KidsStoryType> kst = entities.KidsStoryTypes.ToList();
            var query = from k in ks
                        join kt in kst on k.StoryTypeId equals kt.KidsStoryTypeId into table1
                        from kt in table1.DefaultIfEmpty()
                        where k.statusId == 2
                        orderby k.StoryId descending

                        select new ArticleDetail { KS = k, KST = kt };
            query = query.Skip(0).Take(3).ToList();
            ViewBag.ks = query;

            List<TechnicTip> t = entities.TechnicTips.ToList();
            List<loginTable> ltt = entities.loginTables.ToList();
            var query0 = from tt in t
                         join lt in ltt on tt.UserId equals lt.UserId into table1
                         from lt in table1.DefaultIfEmpty()
                         where tt.statusId == 2
                         where tt.Role_Id == lt.RoleID
                         orderby tt.tipsId descending
                         select new ArticleDetail { tec = tt, log = lt };
            query0 = query0.Skip(0).Take(3).ToList();
            ViewBag.techni = query0;
            List<BusinessOffer> artii = entities.BusinessOffers.ToList();
            List<loginTable> logooo = entities.loginTables.ToList();
            var query01 = from ar in artii
                          join all in logooo on ar.UserId equals all.UserId into table1
                          from all in table1.DefaultIfEmpty()
                          where ar.Role_Id == all.RoleID
                          orderby ar.OfferId descending


                          select new ArticleDetail { bo = ar, log = all };
            query01 = query01.Skip(0).Take(3).ToList();
            ViewBag.artii = query01;

            List<Article> arti = entities.Articles.ToList();
            List<loginTable> logoo = entities.loginTables.ToList();
            var query1 = from a in arti
                         join al in logoo on a.UserId equals al.UserId into table1
                         from al in table1.DefaultIfEmpty()

                         orderby a.ArticleId descending


                         select new ArticleDetail { art = a, log = al };
            query1 = query1.Skip(0).Take(3).ToList();
            ViewBag.arti = query1;

            List<KidTalent> kid = entities.KidTalents.ToList();
            List<loginTable> logt = entities.loginTables.ToList();
            var query2 = from kit in kid
                         join lot in logt on kit.UserId equals lot.UserId into table1
                         from lot in table1.DefaultIfEmpty()

                         where kit.Role_Id == lot.RoleID
                         orderby kit.telentId descending
                         select new ArticleDetail { kt = kit, log = lot };
            query2 = query2.Skip(0).Take(3).ToList();
            ViewBag.telent = query2;

            List<Event> even = entities.Events.ToList();
            var query3 = from ev in even
                         select new ArticleDetail { e = ev };
            query3 = query3.Skip(0).Take(3).ToList();
            ViewBag.evens = query3;
            List<Blog> bl = entities.Blogs.ToList();
            var query4 = from b in bl
                         select new ArticleDetail { blo = b };
            query4 = query4.Skip(0).Take(3).ToList();
            ViewBag.blog = query4;

            //List<WebsiteReview> rv = entities.WebsiteReviews.ToList();
            //var query4 = from rrv in rv
            //             select new ArticleDetail { review = rrv };
            //query3 = query3.Skip(0).Take(3).ToList();
            //ViewBag.views = query3;
            List<webadd> adds = entities.webadds.ToList();
            var query5 = from evv in adds
                         select new ArticleDetail { wed = evv };
            query5 = query5.Skip(0).Take(3).ToList();
            ViewBag.we = query5;
            var data = (from d in entities.WebsiteReviews select new ArticleDetail { review = d }).ToList();

            return View(data);
        }
        public ActionResult blog(int PageNumber = 1)
        {
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Blog> bl = entities.Blogs.ToList();
            var query4 = from b in bl
                         select new ArticleDetail { blo = b };


            ViewBag.TotalPages = Math.Ceiling(query4.Count() / 5.0);
            ViewBag.PageNumber = PageNumber;
            query4 = query4.Skip((PageNumber - 1) * 5).Take(5).ToList();


            ViewBag.blog = query4;

            List<Blog> bls = entities.Blogs.ToList();
            var query5 = from b in bls
                         select new ArticleDetail { blo = b };

            query5 = query5.Skip(0).Take(4).ToList();
            ViewBag.bbl = query5;

            return View();
        }
        public ActionResult blogView(int id)
        {
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Blog> bls = entities.Blogs.ToList();
            var query5 = from b in bls
                         select new ArticleDetail { blo = b };

            query5 = query5.Skip(0).Take(4).ToList();
            ViewBag.kss = query5;

            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.Blogs.Single(x => x.BlogId == id));
            }

        }
        //[HttpGet]
        //public ActionResult login()
        //{
        //    if (Session["Ad"] != null)
        //    {
        //        return RedirectToAction("dashboard", "Home", new { Area = "Superadmin" });

        //    }
        //    else if (Session["school"] != null)
        //    {
        //        return RedirectToAction("Index", "Home", new { Area = "Principle" });

        //    }
        //    else if (Session["Teacher"] != null)
        //    {
        //        return RedirectToAction("dashboard", "Home", new { Area = "Teacher" });

        //    }
        //    else if (Session["Student"] != null)
        //    {
        //        return RedirectToAction("Index", "Home", new { Area = "Student" });

        //    }
        //    else if (Session["Cliente"] != null)
        //    {
        //        return RedirectToAction("dashboard", "Home", new { Area = "Cliente" });

        //    }
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult login(tbl_LoginTableValidation k)
        //{

        //    loginTable u = new loginTable();


        //    if (k != null)
        //    {
        //        u.Email = k.Email;
        //        u.Password = k.Password;

        //        var x = db.loginTables.Where(a => a.Email == u.Email && a.Password == u.Password).SingleOrDefault();
        //        if (x == null)
        //        {
        //            ViewBag.ErrorMessage = "Login Failed";
        //            return View();

        //        }
        //        else
        //        {
        //            if (x.RoleID == 1)
        //            {
        //                Session["Ad"] = x.UserId;
        //                Session["name"] = x.Name;
        //                Session["RoleId"] = x.RoleID;
        //                return RedirectToAction("dashboard", "Home", new { Area = "Superadmin" });
        //            }
        //            else if (x.RoleID == 2)
        //            {
        //                Session["school"] = x.UserId;
        //                Session["schoolName"] = x.Name;
        //                Session["RoleId"] = x.RoleID;
        //                return RedirectToAction("Index", "Home", new { Area = "Principle" });
        //            }
        //            else if (x.RoleID == 3)
        //            {
        //                Session["Teacher"] = x.UserId;
        //                Session["TeacherName"] = x.Name;
        //                Session["RoleId"] = x.RoleID;
        //                return RedirectToAction("dashboard", "Home", new { Area = "Teacher" });
        //            }
        //            else if (x.RoleID == 4)
        //            {
        //                Session["Student"] = x.UserId;
        //                Session["StudentName"] = x.Name;
        //                Session["RoleId"] = x.RoleID;
        //                return RedirectToAction("Index", "Home", new { Area = "Student" });
        //            }

        //            else if (x.RoleID == 5)
        //            {
        //                Session["Cliente"] = x.UserId;
        //                Session["ClientName"] = x.Name;
        //                Session["RoleId"] = x.RoleID;
        //                return RedirectToAction("dashboard", "Home", new { Area = "Cliente" });
        //            }
        //        }

        //    }
        //    else
        //    {
        //        return View("Index");

        //    }



        //    return View();



        //}
        //[HttpGet]
        //public ActionResult Logout()
        //{
        //    Session.Abandon();
        //    return RedirectToAction("login", "Home");
        //}
        //[HttpGet]

        //public ActionResult register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult register(tbl_ClientValidation uv)
        //{
        //    try
        //    {
        //        Client c = new Client();


        //        string filename = Path.GetFileNameWithoutExtension(uv.UserImageFIle.FileName);
        //        string extension = Path.GetExtension(uv.UserImageFIle.FileName);
        //        filename = DateTime.Now.ToString("yymmssff") + extension;
        //        c.Image = "~/Content/img/users/" + filename;

        //        filename = Path.Combine(Server.MapPath("~/Content/img/users/"), filename);
        //        uv.UserImageFIle.SaveAs(filename);

        //        c.UserName = uv.UserName;
        //        c.FatherName = uv.FatherName;
        //        c.CreatedOn = DateTime.Now;
        //        c.Email = uv.Email;
        //        c.Password = uv.Password;
        //        c.ConfirmPassword = uv.ConfirmPassword;
        //        c.Cnic = uv.Cnic;
        //        c.Contact_No = uv.Contact_No;
        //        c.status = 1;


        //        db.Clients.Add(c);
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


        //public ActionResult forgetpassword()
        //{
        //    return View();
        //}

        public ActionResult About()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Team> t = entities.Teams.ToList();
            var query3 = from tt in t
                         select new ArticleDetail { team = tt };
            query3 = query3.Skip(0).Take(4).ToList();
            ViewBag.teams = query3;
            return View();
        }
        public ActionResult Job(int PageNumber = 1)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            CourseDBHandle c = new CourseDBHandle();
            List<jobModel> lists = c.GetJobs();
            ViewBag.TotalPages = Math.Ceiling(lists.Count() / 12.0);
            ViewBag.PageNumber = PageNumber;
            lists = lists.Skip((PageNumber - 1) * 12).Take(12).ToList();
            ViewBag.job = lists;
            return View();
        }
        public ActionResult SingleJob(int id)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            CourseDBHandle gc = new CourseDBHandle();
            List<jobModel> list = gc.GetJobs();
            list = list.Skip(0).Take(2).ToList();
            ViewBag.job = list;
            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(gc.GetJobs().Single(x => x.jobId == id));
            }

            return View();
        }

        [HttpGet]
        public ActionResult Courses(int PageNumber = 1)
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
        public ActionResult SingleCourse(int id)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            CourseDBHandle gc = new CourseDBHandle();
            List<tbl_coursevalidation> list = gc.GetCourseClient();
            list = list.Skip(0).Take(2).ToList();
            ViewBag.course = list;
            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.Courses.Single(x => x.courseId == id));
            }

        }
        public ActionResult Collaborations(int PageNumber = 1)
        {
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Collaboration> cl = entities.Collaborations.ToList();
            var query4 = from c in cl
                         select new ArticleDetail { coll = c };


            ViewBag.TotalPages = Math.Ceiling(query4.Count() / 5.0);
            ViewBag.PageNumber = PageNumber;
            query4 = query4.Skip((PageNumber - 1) * 5).Take(5).ToList();


            ViewBag.coll = query4;

            List<Collaboration> cls = entities.Collaborations.ToList();
            var query5 = from cs in cls
                         select new ArticleDetail { coll = cs };

            query5 = query5.Skip(0).Take(4).ToList();
            ViewBag.colla = query5;

            return View();
        }
        public ActionResult CollaborationsView(int id)
        {
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Collaboration> cls = entities.Collaborations.ToList();
            var query5 = from c in cls
                         select new ArticleDetail { coll = c };

            query5 = query5.Skip(0).Take(4).ToList();
            ViewBag.ccs = query5;

            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.Collaborations.Single(x => x.CallobrationId == id));
            }

        }
        public ActionResult events(int PageNumber = 1)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Event> even = entities.Events.ToList();
            var query3 = from ev in even
                         select new ArticleDetail { e = ev };


            ViewBag.TotalPages = Math.Ceiling(query3.Count() / 12.0);
            ViewBag.PageNumber = PageNumber;
            query3 = query3.Skip((PageNumber - 1) * 12).Take(5).ToList();

            ViewBag.evens = query3;
            return View();
        }
        public ActionResult eventsView(int id)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Event> even = entities.Events.ToList();
            var query3 = from ev in even
                         select new ArticleDetail { e = ev };

            query3 = query3.Skip(0).Take(2).ToList();
            ViewBag.evens = query3;
            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.Events.Single(x => x.EventId == id));
            }

        }

        public ActionResult kidStory(int PageNumber = 1)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<KidsStory> ks = entities.KidsStories.ToList();
            List<KidsStoryType> kst = entities.KidsStoryTypes.ToList();
            var query = from k in ks
                        join kt in kst on k.StoryTypeId equals kt.KidsStoryTypeId into table1
                        from kt in table1.DefaultIfEmpty()
                        where k.statusId == 2
                        orderby k.StoryId descending

                        select new KidsStoryDetails { KS = k, KST = kt };
            ViewBag.TotalPages = Math.Ceiling(query.Count() / 5.0);
            ViewBag.PageNumber = PageNumber;
            query = query.Skip((PageNumber - 1) * 5).Take(5).ToList();
            ViewBag.ks = query;


            List<KidsStory> kss = entities.KidsStories.ToList();
            List<KidsStoryType> ksts = entities.KidsStoryTypes.ToList();
            var query1 = from k in kss
                         join kt in ksts on k.StoryTypeId equals kt.KidsStoryTypeId into table1
                         from kt in table1.DefaultIfEmpty()
                         where k.statusId == 2
                         orderby k.StoryId descending

                         select new KidsStoryDetails { KS = k, KST = kt };
            query1 = query1.Skip(0).Take(4).ToList();
            ViewBag.kss = query1;

            return View();


        }
        Digital_LearningEntities dbContext = new Digital_LearningEntities();
        public ActionResult ViewTopic(int? Id, int? page)
        {
            //int teacherid = Convert.ToInt32(Session["Teacher"]);


            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ClientMessageReplyViewModel vm = new ClientMessageReplyViewModel();
            var count = dbContext.ClientMessages.Count();

            decimal totalPages = count / (decimal)pageSize;
            ViewBag.TotalPages = Math.Ceiling(totalPages);
            vm.Messages = dbContext.ClientMessages.ToList().OrderBy(x => x.DatePosted).ToPagedList(pageNumber, pageSize);
            ViewBag.MessagesInOnePage = vm.Messages;
            ViewBag.PageNumber = pageNumber;



            return View(vm);
        }

        public ActionResult Discussion(int? Id)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<ClientMessage> clM = entities.ClientMessages.ToList();
            List<ClientReply> clR = entities.ClientReplies.ToList();
            List<loginTable> clT = entities.loginTables.ToList();
            var query1 = from clm in clM 
                         join clr in clR on clm.Id equals clr.MessageId into table1
                         from clr in table1.DefaultIfEmpty()
                         
                         join clt in clT on clm.UserId equals clt.UserId into table2
                         from clt in table2.DefaultIfEmpty()
                         where clm.Id == Id
                         where clm.Role_ID == clt.RoleID
                         
                         
                         
                         select new DisscussionDetail { cm=clm,cr=clr, lt=clt };
            
            ViewBag.dis = query1;

           
            List<ClientReply> cR = entities.ClientReplies.ToList();
           
            var query2 = from CR in cR
                         where CR.MessageId == Id
                         select new DisscussionDetail { cr=CR };

            ViewBag.disc = query2;
            
            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.ClientMessages.Single(x => x.Id == Id));
            }
            
            
        }
        [HttpPost]
        public JsonResult LeaveDiscussion(ClientReply a,int Id)
        {

            JsonResult result = new JsonResult();

            try
            {
                a.MessageId = Id;
                a.ReplyFrom = Session["ClientName"].ToString();
                a.ReplyDateTime = DateTime.Now;
                result.Data = new { Success = db.ClientReplies.Add(a) };
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };

            }
            return result;
        }
        [HttpGet]
        public ActionResult addTopic()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addTopic(ClientMessageReplyViewModel cm)
        {
            int msgid = 0;

            int clientid = Convert.ToInt32(Session["Cliente"]);

            
            ClientMessage messagetoPost = new ClientMessage();
            if (cm.Message.Subject != string.Empty && cm.Message.MessageToPost != string.Empty)
            {
                messagetoPost.DatePosted = DateTime.Now;
                messagetoPost.Subject = cm.Message.Subject;
                

                messagetoPost.MessageToPost = cm.Message.MessageToPost;
                messagetoPost.From = Session["ClientName"].ToString();
                
                messagetoPost.Role_ID = 5;
                messagetoPost.UserId = clientid;
                
                dbContext.ClientMessages.Add(messagetoPost);
                dbContext.SaveChanges();
                msgid = messagetoPost.Id;
            }

            return RedirectToAction("ViewTopic", "MainHome", new { Id = msgid });
        }
        [HttpPost]
        public ActionResult DeleteMessage(int messageId)
        {
            int clientid = Convert.ToInt32(Session["Cliente"]);
            int userid;
            int roleid;
            
            ClientMessage _messageToDelete = dbContext.ClientMessages.Find(messageId);
            

            userid = Convert.ToInt32(_messageToDelete.UserId);
            roleid = Convert.ToInt32(_messageToDelete.Role_ID);


            //Message _messageToDelete = dbContext.Messages.Find(messageId);
            if (userid == clientid && roleid == 5)
            {


                dbContext.ClientMessages.Remove(_messageToDelete);
                dbContext.SaveChanges();

                var _repliesToDelete = dbContext.ClientReplies.Where(i => i.MessageId == messageId).ToList();
                if (_repliesToDelete != null)
                {
                    foreach (var rep in _repliesToDelete)
                    {
                        dbContext.ClientReplies.Remove(rep);
                        dbContext.SaveChanges();
                    }
                }
            }
            else
            {
                TempData["ErrorMessage"] = "This is Not Your Post";

            }





            return RedirectToAction("Discussion", "MainHome");
        }
        public ActionResult kidStoryView(int id)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<KidsStory> kss = entities.KidsStories.ToList();
            List<KidsStoryType> ksts = entities.KidsStoryTypes.ToList();
            var query1 = from k in kss
                         join kt in ksts on k.StoryTypeId equals kt.KidsStoryTypeId into table1
                         from kt in table1.DefaultIfEmpty()
                         where k.statusId == 2
                         orderby k.StoryId descending

                         select new KidsStoryDetails { KS = k, KST = kt };
            query1 = query1.Skip(0).Take(4).ToList();
            ViewBag.kss = query1;
            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.KidsStories.Single(x => x.StoryId == id));
            }
        }
        public ActionResult Application()
        {
            return View();
        }
        public ActionResult blogs()
        {
            return View();
        }
        public ActionResult blogsingle()
        {
            return View();
        }
        public ActionResult blogform()
        {
            return View();
        }
        public ActionResult learningtip(int PageNumber = 1)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<TechnicTip> t = entities.TechnicTips.ToList();
            List<loginTable> ltt = entities.loginTables.ToList();
            var query0 = from tt in t
                         join lt in ltt on tt.UserId equals lt.UserId into table1
                         from lt in table1.DefaultIfEmpty()
                         where tt.statusId == 2
                         where tt.Role_Id == lt.RoleID
                         orderby tt.tipsId descending
                         select new ArticleDetail { tec = tt, log = lt };


            ViewBag.TotalPages = Math.Ceiling(query0.Count() / 5.0);
            ViewBag.PageNumber = PageNumber;
            query0 = query0.Skip((PageNumber - 1) * 5).Take(5).ToList();
            ViewBag.techni = query0;
            List<TechnicTip> te = entities.TechnicTips.ToList();
            List<loginTable> lttt = entities.loginTables.ToList();
            var query1 = from tt in te
                         join lt in lttt on tt.UserId equals lt.UserId into table1
                         from lt in table1.DefaultIfEmpty()
                         where tt.statusId == 2
                         where tt.Role_Id == lt.RoleID
                         orderby tt.tipsId descending
                         select new ArticleDetail { tec = tt, log = lt };
            query1 = query1.Skip(0).Take(3).ToList();
            ViewBag.technic = query1;



            return View();
        }
        public ActionResult learningtipview(int id)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<TechnicTip> te = entities.TechnicTips.ToList();
            List<loginTable> lttt = entities.loginTables.ToList();
            var query1 = from tt in te
                         join lt in lttt on tt.UserId equals lt.UserId into table1
                         from lt in table1.DefaultIfEmpty()
                         where tt.statusId == 2
                         where tt.Role_Id == lt.RoleID
                         orderby tt.tipsId descending
                         select new ArticleDetail { tec = tt, log = lt };
            query1 = query1.Skip(0).Take(4).ToList();
            ViewBag.technic = query1;

            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.TechnicTips.Single(x => x.tipsId == id));
            }


        }
        public ActionResult kidstalent(int PageNumber = 1)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<KidTalent> kid = entities.KidTalents.ToList();
            List<loginTable> logt = entities.loginTables.ToList();
            var query2 = from kit in kid
                         join lot in logt on kit.UserId equals lot.UserId into table1
                         from lot in table1.DefaultIfEmpty()
                         where kit.statusId == 2
                         where kit.Role_Id == lot.RoleID
                         orderby kit.telentId descending
                         select new ArticleDetail { kt = kit, log = lot };



            ViewBag.TotalPages = Math.Ceiling(query2.Count() / 5.0);
            ViewBag.PageNumber = PageNumber;
            query2 = query2.Skip((PageNumber - 1) * 5).Take(5).ToList();
            ViewBag.telent = query2;
            List<KidTalent> kids = entities.KidTalents.ToList();
            List<loginTable> logts = entities.loginTables.ToList();
            var query3 = from kits in kids
                         join lots in logts on kits.UserId equals lots.UserId into table1
                         from lots in table1.DefaultIfEmpty()
                         where kits.statusId == 2
                         where kits.Role_Id == lots.RoleID
                         orderby kits.telentId descending
                         select new ArticleDetail { kt = kits, log = lots };
            query3 = query3.Skip(0).Take(3).ToList();
            ViewBag.telents = query3;



            return View();
        }
        public ActionResult kidstalentview(int id)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<KidTalent> kids = entities.KidTalents.ToList();
            List<loginTable> logts = entities.loginTables.ToList();
            var query3 = from kits in kids
                         join lots in logts on kits.UserId equals lots.UserId into table1
                         from lots in table1.DefaultIfEmpty()
                         where kits.statusId == 2
                         where kits.Role_Id == lots.RoleID
                         orderby kits.telentId descending
                         select new ArticleDetail { kt = kits, log = lots };
            query3 = query3.Skip(0).Take(4).ToList();
            ViewBag.telents = query3;


            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.KidTalents.Single(x => x.telentId == id));
            }


        }
        public ActionResult businessOffer(int PageNumber = 1)
        {
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Article> artis = entities.Articles.ToList();
            List<loginTable> logo = entities.loginTables.ToList();
            var query = from a in artis
                        join al in logo on a.UserId equals al.UserId into table1
                        from al in table1.DefaultIfEmpty()
                        where a.statusId == 2
                        orderby a.ArticleId descending

                        select new ArticleDetail { art = a, log = al };

            ViewBag.TotalPages = Math.Ceiling(query.Count() / 5.0);
            ViewBag.PageNumber = PageNumber;
            query = query.Skip((PageNumber - 1) * 5).Take(5).ToList();
            ViewBag.artis = query;
            List<Article> arti = entities.Articles.ToList();
            List<loginTable> logoo = entities.loginTables.ToList();
            var query1 = from a in arti
                         join al in logoo on a.UserId equals al.UserId into table1
                         from al in table1.DefaultIfEmpty()
                         where a.statusId == 2
                         orderby a.ArticleId descending

                         select new ArticleDetail { art = a, log = al };
            query1 = query1.Skip(0).Take(4).ToList();
            ViewBag.arti = query1;


            return View();
        }

        public ActionResult businessOfferview(int id)
        {

            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Article> artis = entities.Articles.ToList();
            List<loginTable> logo = entities.loginTables.ToList();
            var query = from a in artis
                        join al in logo on a.UserId equals al.UserId into table1
                        from al in table1.DefaultIfEmpty()
                        where a.statusId == 2
                        select new ArticleDetail { art = a, log = al };
            query = query.Skip(0).Take(4).ToList();
            ViewBag.artis = query;
            //List<ArticleComment> artC = entities.ArticleComments.ToList();
            //var query3 = from ac in artC
            //                 //where ac.ArticleId == id
            //             select new ArticleDetail { artCom = ac };

            //ViewBag.coms = query3;


            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.Articles.Single(x => x.ArticleId == id));
            }


        }
        public ActionResult article(int PageNumber = 1)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Article> artis = entities.Articles.ToList();
            List<loginTable> logo = entities.loginTables.ToList();
            var query = from a in artis
                        join al in logo on a.UserId equals al.UserId into table1
                        from al in table1.DefaultIfEmpty()
                        where a.statusId == 2
                        orderby a.ArticleId descending

                        select new ArticleDetail { art = a, log = al };

            ViewBag.TotalPages = Math.Ceiling(query.Count() / 5.0);
            ViewBag.PageNumber = PageNumber;
            query = query.Skip((PageNumber - 1) * 5).Take(5).ToList();
            ViewBag.artis = query;
            List<Article> arti = entities.Articles.ToList();
            List<loginTable> logoo = entities.loginTables.ToList();
            var query1 = from a in arti
                         join al in logoo on a.UserId equals al.UserId into table1
                         from al in table1.DefaultIfEmpty()
                         where a.statusId == 2
                         orderby a.ArticleId descending

                         select new ArticleDetail { art = a, log = al };
            query1 = query1.Skip(0).Take(4).ToList();
            ViewBag.arti = query1;


            return View();
        }

        public ActionResult articleview(int id)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Article> artis = entities.Articles.ToList();
            List<loginTable> logo = entities.loginTables.ToList();
            var query = from a in artis
                        join al in logo on a.UserId equals al.UserId into table1
                        from al in table1.DefaultIfEmpty()
                        where a.statusId == 2
                        select new ArticleDetail { art = a, log = al };
            query = query.Skip(0).Take(4).ToList();
            ViewBag.artis = query;
            List<ArticleComment> artC = entities.ArticleComments.ToList();
            var query3 = from ac in artC
                         where ac.ArticleId == id
                         select new ArticleDetail { artCom = ac };

            ViewBag.coms = query3;

            //tbl_ArticleComment ad = new tbl_ArticleComment();
            //ArticleComment aa = new ArticleComment();



            //aa.ArticleId = ad.ArticleId;
            //aa.CommentedOn = DateTime.Now;
            //aa.CommentDescription = ad.CommentDescription;
            //aa.Rating = ad.Rating;


            //db.ArticleComments.Add(aa);

            //db.SaveChanges();
            //ModelState.Clear();
            //ViewBag.Message = "Data Submitted";


            //var tuple = new Tuple<Article, tbl_ArticleComment>(new Article(),new tbl_ArticleComment());


            //return View(tuple);

            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.Articles.Single(x => x.ArticleId == id));
            }


        }

        [HttpPost]
        public JsonResult LeaveComment(ArticleComment a)
        {

            JsonResult result = new JsonResult();

            try
            {
                a.CommentedOn = DateTime.Now;
                result.Data = new { Success = db.ArticleComments.Add(a) };
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };

            }
            return result;
        }

        public ActionResult articalform()
        {
            return View();
        }

        public ActionResult ContactOne()
        {
            return View();
        }

        public ActionResult ContactTwo()
        {
            return View();
        }
        public ActionResult Termandcondition()
        {
            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult gallery()
        {
            return View();
        }
        public ActionResult faq()
        {
            return View();
        }
        public ActionResult teamview(int PageNumber = 1)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var data = db.Teams.ToList();
            ViewBag.TotalPages = Math.Ceiling(data.Count() / 5.0);
            ViewBag.PageNumber = PageNumber;
            data = data.Skip((PageNumber - 1) * 5).Take(5).ToList();
            ViewBag.team = data;
            return View();
        }
        public ActionResult detailTeamview(int Id)
        {

            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Team> artis = entities.Teams.ToList();

            var query = from a in artis



                        select new ArticleDetail { team = a };
            query = query.Skip(0).Take(4).ToList();
            ViewBag.tt = query;



            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.Teams.Single(x => x.Team_Id == Id));
            }

        }
        public ActionResult detailBoardview(int Id)
        {

            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<AdvisoryBoard> artis = entities.AdvisoryBoards.ToList();

            var query = from a in artis



                        select new ArticleDetail { adv = a };
            query = query.Skip(0).Take(4).ToList();
            ViewBag.tt = query;



            using (Digital_LearningEntities entitiess = new Digital_LearningEntities())
            {

                return View(entitiess.AdvisoryBoards.Single(x => x.Board_Id == Id));
            }

        }
        public ActionResult advisoryBoardview(int PageNumber = 1)
        {
            var data = db.AdvisoryBoards.ToList();
            ViewBag.TotalPages = Math.Ceiling(data.Count() / 5.0);
            ViewBag.PageNumber = PageNumber;
            data = data.Skip((PageNumber - 1) * 5).Take(5).ToList();
            ViewBag.board = data;

            var datas = db.AdvisoryBoards.ToList();
            datas = datas.Skip(0).Take(4).ToList();
            ViewBag.boards = datas;
            return View();
        }
        [HttpGet]
        public ActionResult AdReview()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AdReview(tbl_WebsiteReviewValidation web, int rating)
        {
            WebsiteReview website = new WebsiteReview();
            website.Name = web.Name;
            website.Rating = web.Rating;
            website.CreateDate = DateTime.Now;
            website.Comment = web.Comment;
            website.ContactNo = web.ContactNo;
            website.Email = web.Email;
            db.WebsiteReviews.Add(website);
            db.SaveChanges();
            ViewBag.Message = "Website Review Submitted";

            ModelState.Clear();

            return View();
        }

        public ActionResult viewWebsiteReview(int PageNumber = 1)
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var query = db.WebsiteReviews.ToList();
            ViewBag.TotalPages = Math.Ceiling(query.Count() / 5.0);
            ViewBag.PageNumber = PageNumber;
            query = query.Skip((PageNumber - 1) * 5).Take(5).ToList();
            ViewBag.ks = query;


            //List<KidsStory> kss = entities.KidsStories.ToList();
            //List<KidsStoryType> ksts = entities.KidsStoryTypes.ToList();
            //var query1 = from k in kss
            //             join kt in ksts on k.StoryTypeId equals kt.KidsStoryTypeId into table1
            //             from kt in table1.DefaultIfEmpty()
            //             where k.statusId == 2
            //             orderby k.StoryId descending

            //             select new KidsStoryDetails { KS = k, KST = kt };
            //query1 = query1.Skip(0).Take(4).ToList();
            //ViewBag.kss = query1;

            return View();






        }

        public ActionResult Help()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<HelpCategory> list = db.HelpCategories.ToList();
            ViewBag.helpCategory = new SelectList(list, "CategoryId", "CategoryName ");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Help(tbl_helpValidation he)
        {
            try
            {
                List<HelpCategory> list = db.HelpCategories.ToList();
                ViewBag.helpCategory = new SelectList(list, "CategoryId", "CategoryName ");

                Helpe hecl = new Helpe();
                hecl.Name = he.Name;
                hecl.FatherName = he.FatherName;
                hecl.CategoryId = he.CategoryId;
                hecl.Description = he.Description;
                hecl.Email = he.Email;
                hecl.Nic = he.Nic;
                db.Helpes.Add(hecl);
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

        public ActionResult donar()
        {
            if (Session["Cliente"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<HelpCategory> list = db.HelpCategories.ToList();
            ViewBag.helpCategory = new SelectList(list, "CategoryId", "CategoryName ");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult donar(tbl_donarValidation he)
        {
            try
            {
                List<HelpCategory> list = db.HelpCategories.ToList();
                ViewBag.helpCategory = new SelectList(list, "CategoryId", "CategoryName ");

                Donar hecl = new Donar();
                hecl.Name = he.Name;
                hecl.FatherName = he.FatherName;
                hecl.CategoryId = he.CategoryId;
                hecl.Description = he.Description;
                hecl.Email = he.Email;
                hecl.Nic = he.Nic;
                db.Donars.Add(hecl);
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
            //int CourseId = Convert.ToInt32(Session["CourseId"]);

            if (Session["CadQuestionAnswer"] == null)
            {
                pageNumber = pageNumber + 1;
            }
            else
            {
                List<CandidateAnswer> canAnswer = Session["CadQuestionAnswer"] as List<CandidateAnswer>;
                pageNumber = canAnswer.Count + 1;

            }
            var listonlinetest = db.QuizContexts
                     .OrderByDescending(m => m.QuestionId)
                     .ToList();

            //listonlinetest = db.QuizContexts.Where(model => model.CourseId == CourseId).ToList();
            if (pageNumber == listonlinetest.Count)
            {
                IsLast = true;
            }
            int clientid = Convert.ToInt32(Session["Cliente"]);
            tbl_onlinetestAnswer objAnswer = new tbl_onlinetestAnswer();
            QuizContext objquestion = new QuizContext();
            objquestion = listonlinetest.Skip((pageNumber - 1) * pageSize).Take(pageSize).FirstOrDefault();
            var samequestion = db.QuizContextResults.Where(m => m.QuestionId == objquestion.QuestionId && m.UserId == clientid).SingleOrDefault();
            if (samequestion == null)
            {
                //objAnswer.IsLast = IsLast;
                objAnswer.QuestionId = objquestion.QuestionId;
                objAnswer.Questionname = objquestion.Question;
                objAnswer.ListOfQuizOption = (from obj in db.Options
                                              where obj.QuestionId == objquestion.QuestionId
                                              select new tbl_onlinetestoption()
                                              {
                                                  OptionName = obj.OptionName,
                                                  OptionId = obj.OptionId
                                              }).ToList();
                ViewBag.result = true;

                return PartialView("quizquestionoptions", objAnswer);
            }
            else
            {
                ViewBag.result = false;

                return PartialView("quizquestionoptions", objAnswer);
            }
        }

        public JsonResult SaveCandidateAnswer(CandidateAnswer objcandidateAnswer)
        {


            int clientid = Convert.ToInt32(Session["Cliente"]);
            //var dis = Convert.ToInt32(Session["QuestionAnswer"]);
            var ans = (from obj in db.Answers
                       where obj.QuestionId == objcandidateAnswer.QuestionId
                       select new
                       {
                           obj.AnswerText
                       }).FirstOrDefault();

            //foreach (var item in canAnswer)
            //{
            QuizContextResult result = new QuizContextResult();

            if (objcandidateAnswer.AnswerText == ans.AnswerText)
            {


                result.AnswerText = objcandidateAnswer.AnswerText;
                result.QuestionId = objcandidateAnswer.QuestionId;

                result.CreatedOn = DateTime.Now;

                result.UserId = clientid;
                result.status = true;
                db.QuizContextResults.Add(result);
                db.SaveChanges();
            }
            else
            {
                result.AnswerText = objcandidateAnswer.AnswerText;
                result.QuestionId = objcandidateAnswer.QuestionId;

                result.CreatedOn = DateTime.Now;
                //hardcode hai filhal abhi
                result.UserId = 0;
                result.status = false;
                db.QuizContextResults.Add(result);
                db.SaveChanges();
            }

            return Json(new { message = "Data Successfully Added", success = true }, JsonRequestBehavior.AllowGet);
        }
    }


}