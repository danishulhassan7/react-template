using edu_course.Gateway;
using edu_course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace edu_course.Areas.Superadmin.Controllers
{
    public class HomeController : Controller
    {
        Digital_LearningEntities db = new Digital_LearningEntities();

        // GET: Superadmin/Home

       
        public ActionResult dashboard()
        {
            if(Session["Ad"]==null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpGet]
        public ActionResult Profile()

        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            SuperAdminDBHandle sadb = new SuperAdminDBHandle();

            int userid = Convert.ToInt32((Session["Ad"]));
            var sab = db.SuperAdmins.Find(userid);
            Session["imgPath"] = sab.ad_imageurl;
            SuperAdminProfileDataModel sa = sadb.GetProfileData(userid);

            return View(sa);
        }
        [HttpPost]
        public ActionResult Profile(SuperAdminProfileDataModel sa)

        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            try
            {
                SuperAdmin superadmin = new SuperAdmin();
                int userid = Convert.ToInt32((Session["Ad"]));

                var l = db.loginTables.FirstOrDefault(t => t.UserId == userid);

                if (ModelState.IsValid)
                {
                    if (sa.UserImageFile != null)
                    {

                        if (l != null)
                        {
                            Session["name"] = sa.ad_name;
                            l.UserId = userid;
                            l.Name = sa.ad_name;
                            l.Password = sa.password;
                            l.Email = sa.ad_email;
                            l.RoleID = 1;
                            db.Entry(l).State = EntityState.Modified;

                            db.SaveChanges();
                        }
                        string filename = Path.GetFileNameWithoutExtension(sa.UserImageFile.FileName);
                        string extension = Path.GetExtension(sa.UserImageFile.FileName);
                        filename = DateTime.Now.ToString("yymmssff") + extension;




                        superadmin.ad_imageurl = "~/Content/img/users/" + filename;
                        superadmin.ad_name = sa.ad_name;
                        superadmin.ad_email = sa.ad_email;
                        superadmin.ad_password = sa.password;
                        superadmin.ad_Id = userid;





                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                        {
                            if (sa.UserImageFile.ContentLength <= 1000000)
                            {
                                db.Entry(superadmin).State = EntityState.Modified;



                                string oldImgPath = Request.MapPath(Session["imgPath"].ToString());

                                if (db.SaveChanges() > 0)
                                {
                                    filename = Path.Combine(Server.MapPath("~/Content/img/users/"), filename);
                                    sa.UserImageFile.SaveAs(filename);
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
                        superadmin.ad_imageurl = Session["imgPath"].ToString();
                        if (l != null)
                        {
                            Session["name"] = sa.ad_name;
                            l.UserId = userid;
                            l.Name = sa.ad_name;
                            l.Password = sa.password;
                            l.Email = sa.ad_email;
                            l.RoleID = 1;
                            db.Entry(l).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        superadmin.ad_name = sa.ad_name;
                        superadmin.ad_email = sa.ad_email;
                        superadmin.ad_password = sa.password;
                        superadmin.ad_Id = userid;
                        db.Entry(superadmin).State = EntityState.Modified;

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


        public ActionResult viewCertificate()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var data = db.ClientCertificates.ToList();


            return View(data);
        }
        [HttpPost]
        public ActionResult viewCertificate(int? searchTxt)


        {

            var data = db.ClientCertificates.ToList();



            if (searchTxt != null)
            {
                data = db.ClientCertificates.Where(x => x.Certificate_Id == searchTxt).ToList();
            }


            return View(data);

        }


        [HttpGet]
        public ActionResult addannouncement()
        {
            if (Session["Ad"] == null)
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


                int userid = Convert.ToInt32(Session["Ad"]);


                a.Announcement_Title = anouncement.Announcement_Title;
                a.Announcement_Body = anouncement.Announcement_Body;
                a.RoleId = 1;
                a.UserId = userid;


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
        public ActionResult addschool()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult addschool(tbl_schoolValidation school)
        {
            try
            {

                edu_course.Models.School a = new edu_course.Models.School();

                var userWithSameEmail = db.Schools.Where(m => m.School_Email == school.School_Email).SingleOrDefault(); //checking if the emailid already exits for any user
                if (userWithSameEmail == null)
                {
                    string filename = Path.GetFileNameWithoutExtension(school.UserImageFIle.FileName);
                    string extension = Path.GetExtension(school.UserImageFIle.FileName);
                    filename = DateTime.Now.ToString("yymmssff") + extension;

                    a.School_Image = "~/Content/img/users/" + filename;
                    //image ko folder me save krwanay ke leye
                    filename = Path.Combine(Server.MapPath("~/Content/img/users/"), filename);
                    school.UserImageFIle.SaveAs(filename);
                    a.School_Name = school.School_Name;
                    a.School_Address = school.School_Address;
                    a.School_Email = school.School_Email;
                    a.School_Contactno = school.School_Contactno;
                    a.Password = school.Password;

                    a.CreatedOn = DateTime.Now;

                    db.Schools.Add(a);

                    db.SaveChanges();
                }

                else
                {
                    ViewBag.Message = "User with this Email Already Exist";
                    return View();
                }


                int schoollatestid = a.School_Id;
                loginTable l = new loginTable();
                l.UserId = schoollatestid;
                l.Name = a.School_Name;
                l.Password = a.Password;
                l.RoleID = 2;
                l.Email = a.School_Email;
                db.loginTables.Add(l);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Data Submitted";

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
        }
        
        public ActionResult viewschool()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var data = db.Schools.ToList();


            return View(data);
        }

        //public JsonResult DeleteSchool(int SchoolId)
        //{

        //    bool resul = false;
        //    School sc = db.Schools.SingleOrDefault(x => x.School_Id == SchoolId);
        //    if(sc != null)
        //    {
        //        db.Schools.Remove(sc);
        //        db.SaveChanges();
        //        resul = true;
        //    }

        //    return Json(resul, JsonRequestBehavior.AllowGet);
        //}



        public ActionResult addsupportform()
        {
            return View();
        }
        public ActionResult viewsupportformclient()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult detailsupportformClient()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult detailsupportformClient(ClientComplainDetail sc)
        {
           
            return View();
        }
        public ActionResult viewsupportformSchool()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpGet]
        public ActionResult detailsupportformSchool()
        {

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult detailsupportformSchool(SchoolComplain sc)
        {
            return View();
        }
        [HttpGet]
        public ActionResult addarticle()
        {
            if (Session["Ad"] == null)
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
       
        [HttpGet]
        public ActionResult articletyped()
        {
            if (Session["Ad"] == null)
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

        public ActionResult viewarticel()
        {

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var data = (from complain in db.Articles
                        join sc in db.loginTables on complain.Role_Id equals sc.RoleID
                        where complain.Role_Id == 1

                        select new tbl_ArticleValidation
                        {

                            ArticleId = complain.ArticleId,
                            imgPath = complain.imgPath,
                            Title = complain.Title,
                            CreatedDate = complain.CreatedDate,
                            longDes = complain.longDes

                        }).ToList();



            return View(data);

        }
        [HttpGet]
        public ActionResult updatearticle(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<ArticleType> list = db.ArticleTypes.ToList();
            ViewBag.articletypelist = new SelectList(list, "Article_TypeId", "Article_Typename");


            var data = db.Articles.Where(x => x.ArticleId == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updatearticle(Article dat , int id)
        {

            try {
                List<ArticleType> list = db.ArticleTypes.ToList();
                ViewBag.articletypelist = new SelectList(list, "Article_TypeId", "Article_Typename");

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
            catch(Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
            return RedirectToAction("viewarticel", "Home");



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
        public ActionResult addblog()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<BlogType> list = db.BlogTypes.ToList();
            ViewBag.blogtypelist = new SelectList(list, "Blog_TypeId ", "Blog_Typename");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult addblog(tbl_BlogValidation blog)
        {
            try
            {
                //article.UserId = Convert.ToInt32(Session["Ad"]);
                List<BlogType> list = db.BlogTypes.ToList();
            ViewBag.blogtypelist = new SelectList(list, "Blog_TypeId", "Blog_Typename");
            Blog b = new Blog();
           
                string filename = Path.GetFileNameWithoutExtension(blog.UserImageFIle.FileName);
                string extension = Path.GetExtension(blog.UserImageFIle.FileName);
                filename = DateTime.Now.ToString("yymmssff") + extension;
                b.imgPath = "~/Content/img/" + filename;
                //image ko folder me save krwanay ke leye
                filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                blog.UserImageFIle.SaveAs(filename);
                b.Title = blog.Title;
                b.CreatedDate = DateTime.Now;
                b.longDes = blog.longDes;
                b.shortDes = blog.shortDes;
                b.Blog_TypeId = blog.Blog_TypeId;



                db.Blogs.Add(b);

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
        public ActionResult blogtype()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }

        [HttpPost]

        public ActionResult blogtype(tbl_blogTypeValidation blogty)
        {
            try
            {
                BlogType blog = new BlogType();
                blog.Blog_Typename = blogty.Blog_Typename;
                db.BlogTypes.Add(blog);
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
        public ActionResult viewBlog()
        {

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var data = db.Blogs.ToList();

            return View(data);

        }
        [HttpPost]
        public JsonResult DeleteBlog(int BlogId)
        {

            bool resul = false;
            Blog sc = db.Blogs.SingleOrDefault(x => x.BlogId == BlogId);
            if (sc != null)
            {
                db.Blogs.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }





        [HttpGet]
        public ActionResult updateBlog(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<BlogType> list = db.BlogTypes.ToList();
            ViewBag.blogtypelist = new SelectList(list, "Blog_TypeId", "Blog_Typename");


            var data = db.Blogs.Where(x => x.BlogId == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updateBlog(Blog dat, int id)
        {

            try
            {
                List<BlogType> list = db.BlogTypes.ToList();
                ViewBag.blogtypelist = new SelectList(list, "Blog_TypeId", "Blog_Typename");

                var data = db.Blogs.Where(x => x.BlogId == id).First();

                if (data != null)
                {
                    data.Blog_TypeId = dat.Blog_TypeId;
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
            return RedirectToAction("viewBlog", "Home");



        }




        [HttpGet]
        public ActionResult kidstore()
        {
            if (Session["Ad"] == null)
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
                kd.statusId = 2;
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
        [HttpGet]
        public ActionResult kidStoryType()
        {
            if (Session["Ad"] == null)
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
        [HttpGet]
        public ActionResult viewKidStory()
        {

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var data = db.KidsStories.ToList();

            return View(data);

            }
            [HttpPost]
            public JsonResult DeleteKidStory(int storyid)
            {

                bool resul = false;
                KidsStory sc = db.KidsStories.SingleOrDefault(x => x.StoryId == storyid);
                if (sc != null)
                {
                    db.KidsStories.Remove(sc);
                    db.SaveChanges();
                    resul = true;
                }

                return Json(resul, JsonRequestBehavior.AllowGet);
            }



        [HttpGet]
        public ActionResult updateKidStory(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<KidsStoryType> list = db.KidsStoryTypes.ToList();
            ViewBag.storytypelist = new SelectList(list, "KidsStoryTypeId", "KidsStoryName");

            var data = db.KidsStories.Where(x => x.StoryId == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updateKidStory(KidsStory da, int id)
        {

            try
            {
                List<KidsStoryType> list = db.KidsStoryTypes.ToList();
                ViewBag.storytypelist = new SelectList(list, "KidsStoryTypeId", "KidsStoryName");

                var data = db.KidsStories.Where(x => x.StoryId == id).First();

                if (data != null)
                {
                    
                    data.StoryTypeId = da.StoryTypeId;
                    data.StoryTitle = da.StoryTitle;
                    data.shortDes = da.shortDes;
                    data.longDes = da.longDes;

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
            return RedirectToAction("viewKidStory","Home");



        }















        public ActionResult clientapproval()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Client> clientes = entities.Clients.ToList();
            List<Status> status = entities.Status.ToList();
            var query = from c in clientes
                        join s in status on c.status equals s.statusId into table1
                        from s in table1.DefaultIfEmpty()
                        select new ViewStatus { CreateDate = c, statustype = s };
            return View(query);

        }

        [HttpGet]
        public ActionResult ApproveClient(int id)
        {
            //var Statustype = new SelectList(db.Status.ToList(), "statusId", "statustype");
            //ViewData["StatusType"] = Statustype;
            using (Digital_LearningEntities entities = new Digital_LearningEntities())
            {
                //Response.Write(submit);
                return View(entities.Clients.Single(x => x.UserId == id));
            }


        }
        [HttpPost]
        public ActionResult ApproveClient( Client cc,int id, string submit)
        {
            switch (submit)
            {
                case "Approve":
                    using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    {

                        var item = entities.Clients.Single(x => x.UserId == id);
                        //item.UserName = cc.UserName;
                        //item.FatherName = cc.FatherName;
                        //item.CreatedOn = cc.CreatedOn;
                        //item.Contact_No = cc.Contact_No;
                        //item.Email = cc.Email;
                        //item.Password = cc.Password;
                        //item.ConfirmPassword = cc.ConfirmPassword;
                        //item.Cnic = cc.Cnic;
                        //item.Image = cc.Image;

                      

                        item.status = 2;
                        entities.Clients.Attach(item);
                        entities.Entry(item).Property(X => X.status).IsModified = true;
                        entities.SaveChanges();
                        var userWithSameEmail = db.loginTables.Where(m => m.Email == cc.Email).SingleOrDefault();
                        if (userWithSameEmail == null)
                        {
                            loginTable lt = new loginTable();

                            int latestclientid = id;
                            lt.Name = cc.UserName;
                            lt.Email = cc.Email;
                            lt.Password = cc.Password;
                            lt.RoleID = 5;
                            lt.UserId = latestclientid;
                            db.loginTables.Add(lt);
                            db.SaveChanges();
                            return RedirectToAction("clientapproval");
                        }
                        else
                        {
                            return RedirectToAction("clientapproval");

                        }
                        break;
                    }

                case "Reject":
                    using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    {

                        var item = entities.Clients.Single(x => x.UserId == id);
                        //item.UserName = cc.UserName;
                        //item.FatherName = cc.FatherName;
                        //item.CreatedOn = cc.CreatedOn;
                        //item.Contact_No = cc.Contact_No;
                        //item.Email = cc.Email;
                        //item.Password = cc.Password;
                        //item.ConfirmPassword = cc.ConfirmPassword;
                        //item.Cnic = cc.Cnic;
                        //item.Image = cc.Image;



                        item.status = 3;
                  

                        //entities.SaveChanges();
                        //return View(item);

                        //entities.Entry(item).State = EntityState.Modified;    

                        entities.Clients.Attach(item);
                        entities.Entry(item).Property(X => X.status).IsModified = true;
                        entities.SaveChanges();
                        return RedirectToAction("clientapproval");

                        break;
                    }
                default:
                    throw new Exception();
                    break;
            }
            return View();
        }

        public ActionResult Clientapprovalarticle()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Article> article = entities.Articles.Where(x => x.SchoolId != null).ToList();
            List<Status> status = entities.Status.ToList();
            List<Client> client = entities.Clients.ToList();
            List<ArticleType> articletype = entities.ArticleTypes.ToList();
            var query = from c in article
                        join s in status on c.statusId equals s.statusId into table1
                        join t in articletype on c.Article_TypeId equals t.Article_TypeId into table2
                        join sc in client on c.UserId equals sc.UserId into table3
                        from s in table1.DefaultIfEmpty()
                        from t in table2.DefaultIfEmpty()
                        from sc in table3.DefaultIfEmpty()


                        select new ViewStatus { Clientarticle = c, statustype = s, Article_Typename = t, CreateDate = sc };
            return View(query);

        }

        [HttpGet]
        public ActionResult ApproveArticleClient(int id)
        {
            //var Statustype = new SelectList(db.Status.ToList(), "statusId", "statustype");
            //ViewData["StatusType"] = Statustype;
            using (Digital_LearningEntities entities = new Digital_LearningEntities())
            {
                //Response.Write(submit);
                return View(entities.Articles.Single(x => x.ArticleId == id));
            }


        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult ApproveArticleClient(int id, string submit)
        {
            switch (submit)
            {
                case "Approve":
                    using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    {

                        var item = entities.Articles.Single(x => x.ArticleId == id);



                        item.statusId = 2;

                        entities.Articles.Attach(item);
                        entities.Entry(item).Property(X => X.statusId).IsModified = true;
                        entities.SaveChanges();
                        return RedirectToAction("Clientapprovalarticle");
                        break;

                    }

                case "Reject":
                    using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    {

                        var item = entities.Articles.Single(x => x.ArticleId == id);



                        item.statusId = 3;

                        entities.Articles.Attach(item);
                        entities.Entry(item).Property(X => X.statusId).IsModified = true;
                        entities.SaveChanges();
                        return RedirectToAction("Clientapprovalarticle");
                        break;
                    }
                default:
                    throw new Exception();
                    break;
            }
            return View();
        }


        public ActionResult schoolapprovalarticle()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<Article> article = entities.Articles.Where(x => x.SchoolId != null).ToList();
            List<Status> status = entities.Status.ToList();
            List<School> school = entities.Schools.ToList();
            List<ArticleType> articletype = entities.ArticleTypes.ToList();
            var query = from c in article
                        join s in status on c.statusId equals s.statusId into table1
                        join t in articletype on c.Article_TypeId equals t.Article_TypeId into table2
                        join sc in school on c.SchoolId equals sc.School_Id into table3
                        from s in table1.DefaultIfEmpty()
                        from t in table2.DefaultIfEmpty()
                        from sc in table3.DefaultIfEmpty()


                        select new ViewStatus { schoolarticle = c, statustype = s, Article_Typename = t, School_Name = sc };
            return View(query);

        }


        [HttpGet]
        public ActionResult ApproveArticle(int id)
        {
            //var Statustype = new SelectList(db.Status.ToList(), "statusId", "statustype");
            //ViewData["StatusType"] = Statustype;
            using (Digital_LearningEntities entities = new Digital_LearningEntities())
            {
                //Response.Write(submit);
                return View(entities.Articles.Single(x => x.ArticleId == id));
            }


        }

       

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult ApproveArticle(int id, string submit)
        {
            switch (submit)
            {
                case "Approve":
                    using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    {

                        var item = entities.Articles.Single(x => x.ArticleId == id);



                        item.statusId = 2;

                        entities.Articles.Attach(item);
                        entities.Entry(item).Property(X => X.statusId).IsModified = true;
                        entities.SaveChanges();
                        return RedirectToAction("schoolapprovalarticle");
                        break;

                    }

                case "Reject":
                    using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    {

                        var item = entities.Articles.Single(x => x.ArticleId == id);



                        item.statusId = 3;

                        entities.Articles.Attach(item);
                        entities.Entry(item).Property(X => X.statusId).IsModified = true;
                        entities.SaveChanges();
                        return RedirectToAction("schoolapprovalarticle");
                        break;
                    }
                default:
                    throw new Exception();
                    break;
            }
            return View();
        }
        /*  [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult courseapproval( CourseAprov courseAprov,string submit)
          {
              if(submit)
              {
                  db.CourseAprovs.Add(courseAprov);
                  int AproveId = (int)Session["AproveId"]; // extract values from session to variable and use it in the query 

                  // Set the cartId ordered value to true for all the ordered items
                  var id = db.Courses.Where(c => c.courseId == AproveId).ToList();

                  foreach (var i in id)
                  {
                      i.Status = true;
                  }
                  return RedirectToAction("courseapproval");
              }

              return View(courseAprov);
          }

      */

       

        public ActionResult courseapproval()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });

            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<UserEnrollInCourse> clientes = entities.UserEnrollInCourses.ToList();
            List<Course> course = entities.Courses.ToList();

            List<Client> client = entities.Clients.ToList();

            List<Status> status = entities.Status.ToList();
            var query = from c in clientes
                        join ue in client on c.UserId equals ue.UserId into table1
                        join s in status on c.statusId equals s.statusId into table2
                        join cou in course on c.CourseId equals cou.courseId into table3
                        from ue in table1.DefaultIfEmpty()
                        from s in table2.DefaultIfEmpty()
                        from cou in table3.DefaultIfEmpty()
                        where c.statusId != null

                        select new ViewStatus { CreateDate = ue, statustype = s, usercourse = c, coursename = cou };
            return View(query);
           
        }
        [HttpGet]
        public ActionResult courseapproval(int id)
        {
            //var Statustype = new SelectList(db.Status.ToList(), "statusId", "statustype");
            //ViewData["StatusType"] = Statustype;
            using (Digital_LearningEntities entities = new Digital_LearningEntities())
            {
                //Response.Write(submit);
                return View(entities.Courses.Single(x => x.courseId == id));
            }


        }

        [HttpPost]
        [ValidateInput(false)]
        
        public ActionResult courseapproval(int id, string submit)
        {
            switch (submit)
            {
                case "Approve":
                    // using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    //    {



                    ViewBag.Message = "Data Updated";
                    return RedirectToAction("course");



                    break;

                // }

                case "Reject":
                    using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    {

                        var item = entities.UserEnrollInCourses.FirstOrDefault(x => x.EnrollmentId == id);

                        item.statusId = 3;

                        entities.UserEnrollInCourses.Attach(item);
                        entities.Entry(item).Property(X => X.statusId).IsModified = true;
                        entities.SaveChanges();
                        return RedirectToAction("courseapproval");
                        break;
                    }
                default:
                    return RedirectToAction("course");
                    throw new Exception();
                    break;
            }
           
            return View();
        }


/*
        [HttpGet]
        public ActionResult ApproveCourse(int id)
        {
            //var Statustype = new SelectList(db.Status.ToList(), "statusId", "statustype");
            //ViewData["StatusType"] = Statustype;
            using (Digital_LearningEntities entities = new Digital_LearningEntities())
            {
                //Response.Write(submit);
                return View(entities.Courses.FirstOrDefault(x => x.User_Id == id));
            }


        }
        
        
        [ValidateInput(false)]

        [HttpGet]
        public ActionResult ApproveCourse(int id, string coursename, int page = 1)
        {

            //var Statustype = new SelectList(db.Status.ToList(), "statusId", "statustype");
            //ViewData["StatusType"] = Statustype;
            Digital_LearningEntities entities = new Digital_LearningEntities();
            ViewBag.coursename = coursename;


            //Response.Write(submit);
            return View(entities.UserEnrollInCourses.FirstOrDefault(x => x.EnrollmentId == id));



        }

      */ 
        public ActionResult schoolapprovalkidstory()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            Digital_LearningEntities entities = new Digital_LearningEntities();
            List<KidsStory> kidstory = entities.KidsStories.Where(x => x.SchoolId != null).ToList();
           List<Status> status = entities.Status.ToList();
            List<School> school = entities.Schools.ToList();
            List<KidsStoryType> kidstorytype = entities.KidsStoryTypes.ToList();
            var query = from c in kidstory
                        join s in status on c.statusId equals s.statusId into table1
                        join t in kidstorytype on c.StoryTypeId equals t.KidsStoryTypeId into table2
                        join sc in school on c.SchoolId equals sc.School_Id into table3
                        from s in table1.DefaultIfEmpty()
                        from t in table2.DefaultIfEmpty()
                        from sc in table3.DefaultIfEmpty()


                        select new ViewStatus { schoolkidstory = c, statustype = s, KidsStoryName = t, School_Name = sc };
            return View(query);

        }


        [HttpGet]
        public ActionResult Approvekidstory(int id)
        {
            //var Statustype = new SelectList(db.Status.ToList(), "statusId", "statustype");
            //ViewData["StatusType"] = Statustype;
            using (Digital_LearningEntities entities = new Digital_LearningEntities())
            {
                //Response.Write(submit);
                return View(entities.KidsStories.Single(x => x.StoryId == id));
            }


        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Approvekidstory(int id, string submit)
        {
            switch (submit)
            {
                case "Approve":
                    using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    {

                        var item = entities.KidsStories.Single(x => x.StoryId == id);



                        item.statusId = 2;

                        entities.KidsStories.Attach(item);
                        entities.Entry(item).Property(X => X.statusId).IsModified = true;
                        entities.SaveChanges();
                        return RedirectToAction("schoolapprovalkidstory");
                        break;

                    }

                case "Reject":
                    using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    {

                        var item = entities.KidsStories.Single(x => x.StoryId == id);



                        item.statusId = 3;

                        entities.KidsStories.Attach(item);
                        entities.Entry(item).Property(X => X.statusId).IsModified = true;
                        entities.SaveChanges();
                        return RedirectToAction("schoolapprovalkidstory");
                        break;
                    }
                default:
                    throw new Exception();
                    break;
            }
            return View();
        }



        [HttpGet]
        public ActionResult courseapproval(int id)
        {
            //var Statustype = new SelectList(db.Status.ToList(), "statusId", "statustype");
            //ViewData["StatusType"] = Statustype;
            using (Digital_LearningEntities entities = new Digital_LearningEntities())
            {
                //Response.Write(submit);
                return View(entities.Courses.Single(x => x.courseId == id));
            }


        }
        
        [HttpPost]
        public ActionResult courseapproval(Course cc, int id, string submit)
        {
            switch (submit)
            {
                case "Approve":
                    using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    {

                        var item = entities.Courses.Single(x => x.User_Id == id);
                        //item.UserName = cc.UserName;
                        //item.FatherName = cc.FatherName;
                        //item.CreatedOn = cc.CreatedOn;
                        //item.Contact_No = cc.Contact_No;
                        //item.Email = cc.Email;
                        //item.Password = cc.Password;
                        //item.ConfirmPassword = cc.ConfirmPassword;
                        //item.Cnic = cc.Cnic;
                        //item.Image = cc.Image;

                       

                        entities.Courses.Attach(item);
                        entities.Entry(item).Property(X => X.Status).IsModified = true;

                        UserEnrollInCourse lt = new UserEnrollInCourse();

                            int latestcourseid = id;
                         
                            lt.Enrolldate = cc.CreatedDate;
                            lt.CourseId = cc.courseId;
                        lt.statusId = 1;
                        lt.RoleId = 5;
                            lt.IsUserActive = true;
                            db.UserEnrollInCourses.Add(lt);
                            db.SaveChanges();
                            return RedirectToAction("clientapproval");
                        
                        break;
                    }

                case "Reject":
                    using (Digital_LearningEntities entities = new Digital_LearningEntities())
                    {

                        //var item = entities.Courses.Single(x => x.UserId == id);
                        //item.UserName = cc.UserName;
                        //item.FatherName = cc.FatherName;
                        //item.CreatedOn = cc.CreatedOn;
                        //item.Contact_No = cc.Contact_No;
                        //item.Email = cc.Email;
                        //item.Password = cc.Password;
                        //item.ConfirmPassword = cc.ConfirmPassword;
                        //item.Cnic = cc.Cnic;
                        //item.Image = cc.Image;



                
                        return RedirectToAction("clientapproval");

                        break;
                    }
                default:
                    throw new Exception();
                    break;
            }
            return View();
        }
        //public ActionResult courseapproval()
        //{
        //    if (Session["Ad"] == null)
        //    {
        //        return RedirectToAction("login", "Home", new { area = "" });
        //    }
        //    return View();
        //}

        [HttpGet]
        public ActionResult addtest()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            //var Class = new SelectList(db.Classes.ToList(), "Class_Id", "Name");
            //ViewData["Classes"] = Class;

            //ViewData["Classes"] = Class;
            CourseDBHandle gc = new CourseDBHandle();
            //List<tbl_coursevalidation> lgc = gc.GetCourse();
            //ViewBag.course = new SelectList(lgc, "courseId", "courseName");

            List<tbl_OnlineTestValidation> list = gc.GetCourse();
            ViewBag.course = new SelectList(list, "courseId", "courseName");



            return View();
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
            objanswer.RoleId= Convert.ToInt32(Session["RoleId"]);
            objanswer.UserId = Convert.ToInt32(Session["Ad"]);
            objanswer.SchoolId = Convert.ToInt32(Session["Ad"]);



            objanswer.QuestionId = questionId;
            db.OnlineTestAnswers.Add(objanswer);
            db.SaveChanges();

            return Json(new { message = "Data Successfully Added", success = true } ,JsonRequestBehavior.AllowGet);
        }
        public ActionResult viewtest()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var data = db.OnlineTests.ToList();

            return View(data);

        }
        [HttpPost]

        public JsonResult Deletetest(int articleId)
        {

            bool resul = false;
            OnlineTest sc = db.OnlineTests.SingleOrDefault(x => x.QuestionId == articleId);
            if (sc != null)
            {
                db.OnlineTests.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult updatetest(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }


            var data = db.OnlineTests.Where(x => x.QuestionId == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updatetest(OnlineTest QuestionOption, int id)
        {

            try
            {

                var data = db.OnlineTests.Where(x => x.QuestionId == id).First();


                if (data != null)
                {
                    data.QuestionName = QuestionOption.QuestionName;

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
            return RedirectToAction("viewtest", "Home");



        }
        [HttpGet]
        public ActionResult course()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult course(tbl_coursevalidation cou)
        {
            try
            {
                Course cc = new Course();




                string filename = Path.GetFileNameWithoutExtension(cou.CourseImageFIle.FileName);
                string extension = Path.GetExtension(cou.CourseImageFIle.FileName);
                filename = DateTime.Now.ToString("yymmssff") + extension;
                cc.imageLink = "~/Content/img/" + filename;

                filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                cou.CourseImageFIle.SaveAs(filename);

                cou.Role_Id = Convert.ToInt32(Session["RoleId"]);
                cou.User_Id = Convert.ToInt32(Session["Ad"]);
                cc.User_Id = cou.User_Id;
                cc.Role_Id = cou.Role_Id;
                cc.CreatedDate = DateTime.Now;
                cc.courseDescription = cou.courseDescription;
                cc.courseName = cou.courseName;
                cc.courseType = cou.courseType;
                cc.Code = cou.Code;
                cc.VideoLink = cou.VideoLink;
                cc.longDes = cou.longDes;
                cc.duration = cou.duration;
               
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
        public ActionResult viewCourse()
        {

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var data = db.Courses.ToList();

            return View(data);

        }
        [HttpGet]
        public ActionResult updateCourse(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }


            var data = db.Courses.Where(x => x.courseId == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updateCourse(Course dat, int id)
        {

            try
            {

                var data = db.Courses.Where(x => x.courseId == id).First();

                if (data != null)
                {
                    data.courseType = dat.courseType;
                    data.courseName = dat.courseName;
                    data.Code = dat.Code;
                    data.courseDescription = dat.courseDescription;
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
            return RedirectToAction("viewCourse", "Home");



        }
        public ActionResult certificateissue()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpGet]
        public ActionResult events()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult events(tbl_eventValidation even)
        {
            try
            {
                Event ev = new Event();
                if (even.UserImageFIle == null)
                {
                    ModelState.AddModelError("NoFile", "Upload File");
                }
                else
                {
                    string filename = Path.GetFileNameWithoutExtension(even.UserImageFIle.FileName);
                    string extension = Path.GetExtension(even.UserImageFIle.FileName);
                    filename = DateTime.Now.ToString("yymmssff") + extension;
                    ev.Event_VenueImage = "~/Content/img/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                    even.UserImageFIle.SaveAs(filename);
                    ev.Title = even.Title;
                    ev.Event_Description = even.Event_Description;
                    ev.Event_End_Date = even.Event_End_Date;
                    ev.Event_Start_Date = even.Event_Start_Date;
                    ev.Event_End_Time = even.Event_End_Time;
                    ev.Event_Start_Time = even.Event_Start_Time;
                    ev.Event_Venue = even.Event_Venue;
                    ev.Event_VenueVideo = even.Event_VenueVideo;



                    db.Events.Add(ev);

                    db.SaveChanges();
                    ViewBag.Message = "Data Submitted";
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

        [HttpPost]

        public JsonResult DeleteEvent(int eventId)
        {

            bool resul = false;
            Event sc = db.Events.SingleOrDefault(x => x.EventId == eventId);
            if (sc != null)
            {
                db.Events.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }









        [HttpGet]
        public ActionResult collaboration()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult collaboration(tbl_collaborationValidation even)
        {
            try
            {
                Collaboration ev = new Collaboration();
                if (even.CollImageFIle == null)
                {
                    ModelState.AddModelError("NoFile", "Upload File");
                }
                else
                {
                    string filename = Path.GetFileNameWithoutExtension(even.CollImageFIle.FileName);
                    string extension = Path.GetExtension(even.CollImageFIle.FileName);
                    filename = DateTime.Now.ToString("yymmssff") + extension;
                    ev.Image = "~/Content/img/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                    even.CollImageFIle.SaveAs(filename);
                    ev.CallobrationName = even.CallobrationName;
                    ev.CallobrationTitle = even.CallobrationTitle;
                    ev.Date = DateTime.Now;
                    ev.Description = even.Description;




                    db.Collaborations.Add(ev);

                    db.SaveChanges();
                    ViewBag.Message = "Data Submitted";
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
        public ActionResult viewcollaboration()
        {

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var data = db.Collaborations.ToList();

            return View(data);

        }
        [HttpGet]
        public ActionResult updatecollaboration(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }


            var data = db.Collaborations.Where(x => x.CallobrationId == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updatecollaboration(Collaboration dat, int id)
        {

            try
            {

                var data = db.Collaborations.Where(x => x.CallobrationId == id).First();

                if (data != null)
                {

                    data.CallobrationName = dat.CallobrationName;
                    data.CallobrationTitle = dat.CallobrationTitle;
                    data.Date = dat.Date;
                    data.Description = dat.Description;
                    
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
            return RedirectToAction("viewcollaboration", "Home");



        }
        [HttpPost]

        public JsonResult Deletecollaboration(int articleId)
        {

            bool resul = false;
            Collaboration sc = db.Collaborations.SingleOrDefault(x => x.CallobrationId == articleId);
            if (sc != null)
            {
                db.Collaborations.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult viewsevent()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            else
            {
                Digital_LearningEntities entities = new Digital_LearningEntities();
                return View(from Event in entities.Events.Take(9)
                            select Event);
            }
        }
        [HttpGet]
        public ActionResult virewComplain()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            
            var data = (from complain in db.Client_SchoolComplain
                        join sc in db.Schools on complain.UserId equals sc.School_Id
                        where complain.RoleId == 2

                        
                        select new tblClientorSchoorComplainValidation
                        {

                            complain_Id = complain.complain_Id,
                           Name  =  sc.School_Name,
                           complain_description =complain.complain_description,
                           complain_date = complain.complain_date
                                
                        }).ToList();

            //var data = db.Client_SchoolComplain.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult virewComplainReply()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }


            return View();
        }
        [HttpPost]
        public ActionResult virewComplainReply(tblClientorSchoorComplainValidation sa , int id)
        {
            try
            {
                int userid = Convert.ToInt32((Session["Ad"]));

                var com = db.Client_SchoolComplain.FirstOrDefault(m => m.complain_Id == id);

                if (com != null)
                {
                    com.UserId = userid;
                    com.RoleId = 2;
                    com.complain_Id = id;
                    com.replymsg = sa.replymsg;

                    db.Entry(com).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Data Successfully Submitted";
                    ModelState.Clear();
                }

            }
            catch(Exception ex)
            {
                ViewBag.Message = "Not Submitted";
                return View();
            }
            return View();
        }


        [HttpGet]
        public ActionResult virewClientComplain()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var data = (from complain in db.Client_SchoolComplain
                        join sc in db.Clients on complain.UserId equals sc.UserId
                        where complain.RoleId == 5


                        select new tblClientorSchoorComplainValidation
                        {

                            complain_Id = complain.complain_Id,
                            Name = sc.UserName,
                            complain_description = complain.complain_description,
                            complain_date = complain.complain_date

                        }).ToList();

            //var data = db.Client_SchoolComplain.ToList();
            return View(data);
        }
        public ActionResult virewClientComplainReply()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }


            return View();
        }
        [HttpPost]
        public ActionResult virewClientComplainReply(tblClientorSchoorComplainValidation sa, int id)
        {
            try
            {
                int userid = Convert.ToInt32((Session["Ad"]));

                var com = db.Client_SchoolComplain.FirstOrDefault(m => m.complain_Id == id);

                if (com != null)
                {
                    com.UserId = userid;
                    com.RoleId = 5;
                    com.complain_Id = id;
                    com.replymsg = sa.replymsg;

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

        [HttpGet]
        public ActionResult addDeartment()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();

        }
        [HttpPost]
        public ActionResult addDeartment(tbl_addDepartment dep)
        {
            try
            {
                Department department = new Department();
                department.Department_Name = dep.Department_Name;
                db.Departments.Add(department);
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
        public ActionResult addTeam()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<Department> list = db.Departments.ToList();
            ViewBag.departmentList = new SelectList(list, "Department_ID", "Department_Name");


            return View();
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult addTeam(tbl_addTeam te)
        {

            List<Department> list = db.Departments.ToList();
            ViewBag.departmentList = new SelectList(list, "Department_ID", "Department_Name");

            Team tea = new Team();

                string filename = Path.GetFileNameWithoutExtension(te.UserImageFIle.FileName);
                string extension = Path.GetExtension(te.UserImageFIle.FileName);
                filename = DateTime.Now.ToString("yymmssff") + extension;
                tea.Image = "~/Content/img/" + filename;
                //image ko folder me save krwanay ke leye
                filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                te.UserImageFIle.SaveAs(filename);
                tea.Designation = te.Designation;
                tea.Long_Description = te.Long_Description;
                tea.Short_Description = te.Short_Description;
                tea.Department_ID = te.Department_ID;
                tea.Name = te.Name;



                db.Teams.Add(tea);

                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = "Data Submitted";

            
            return View();
        }
        public ActionResult viewTeam()
        {

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var data = db.Teams.ToList();

            return View(data);

        }
        [HttpGet]
        public ActionResult updateTeam(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<Department> list = db.Departments.ToList();
            ViewBag.articletypelist = new SelectList(list, "Department_ID", "Department_Name");


            var data = db.Teams.Where(x => x.Team_Id == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updateTeam(Team dat, int id)
        {

            try
            {
                List<Department> list = db.Departments.ToList();
                ViewBag.articletypelist = new SelectList(list, "Department_ID", "Department_Name");

                var data = db.Teams.Where(x => x.Team_Id == id).First();

                if (data != null)
                {
                    data.Department_ID = dat.Department_ID;
                    data.Name = dat.Name;
                    data.Designation = dat.Designation;
                    data.Short_Description = dat.Short_Description;
                    data.Long_Description = dat.Long_Description;

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
            return RedirectToAction("viewTeam", "Home");



        }
        [HttpPost]

        public JsonResult DeleteCourse(int articleId)
        {

            bool resul = false;
            Course sc = db.Courses.SingleOrDefault(x => x.courseId == articleId);
            if (sc != null)
            {
                db.Courses.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public JsonResult DeleteTeam(int articleId)
        {

            bool resul = false;
            BusinessOffer sc = db.BusinessOffers.SingleOrDefault(x => x.OfferId == articleId);
            if (sc != null)
            {
                db.BusinessOffers.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult addDeartmentboard()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();

        }
        [HttpPost]
        public ActionResult addDeartmentboard(tbl_addDepartment dep)
        {
            try
            {
                departmentBoard department = new departmentBoard();
                department.Department_Name = dep.Department_Name;
                db.departmentBoards.Add(department);
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
        public ActionResult advisoryBoard()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<departmentBoard> list = db.departmentBoards.ToList();
            ViewBag.departmentList = new SelectList(list, "Department_ID", "Department_Name");


            return View();
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult advisoryBoard(tbl_advisoryBoard te)
        {

            List<departmentBoard> list = db.departmentBoards.ToList();
            ViewBag.departmentList = new SelectList(list, "Department_ID", "Department_Name");

            AdvisoryBoard tea = new AdvisoryBoard();

            string filename = Path.GetFileNameWithoutExtension(te.UserImageFIle.FileName);
            string extension = Path.GetExtension(te.UserImageFIle.FileName);
            filename = DateTime.Now.ToString("yymmssff") + extension;
            tea.Image = "~/Content/img/" + filename;
            //image ko folder me save krwanay ke leye
            filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
            te.UserImageFIle.SaveAs(filename);
            tea.Designation = te.Designation;
            tea.Long_Description = te.Long_Description;
            tea.Short_Description = te.Short_Description;
            tea.Department_ID = te.Department_ID;
            tea.Name = te.Name;



            db.AdvisoryBoards.Add(tea);

            db.SaveChanges();

            ModelState.Clear();
            ViewBag.Message = "Data Submitted";


            return View();
        }
        public ActionResult viewadvisoryBoard()
        {

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var data = db.AdvisoryBoards.ToList();

            return View(data);

        }
        [HttpGet]
        public ActionResult updateadvisoryBoard(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<departmentBoard> list = db.departmentBoards.ToList();
            ViewBag.articletypelist = new SelectList(list, "Department_ID", "Department_Name");


            var data = db.AdvisoryBoards.Where(x => x.Board_Id == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updateadvisoryBoard(Team dat, int id)
        {

            try
            {
                List<departmentBoard> list = db.departmentBoards.ToList();
                ViewBag.articletypelist = new SelectList(list, "Department_ID", "Department_Name");


                var data = db.AdvisoryBoards.Where(x => x.Board_Id == id).First();

                if (data != null)
                {
                    data.Department_ID = dat.Department_ID;
                    data.Name = dat.Name;
                    data.Designation = dat.Designation;
                    data.Short_Description = dat.Short_Description;
                    data.Long_Description = dat.Long_Description;

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
            return RedirectToAction("viewadvisoryBoard", "Home");



        }
        [HttpPost]

        public JsonResult DeleteadvisoryBoard(int articleId)
        {

            bool resul = false;
            AdvisoryBoard sc = db.AdvisoryBoards.SingleOrDefault(x => x.Board_Id == articleId);
            if (sc != null)
            {
                db.AdvisoryBoards.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }


        public ActionResult viewAnnouncment()
        {
            


            var query = from sas in db.Announcements
                        join ss in db.Schools on sas.SchoolId equals ss.School_Id into table1
                        from ss in table1.DefaultIfEmpty()
                       
                        select new tbl_Announcementvalidation { SchoolName = ss.School_Name, Announcement_Title = sas.Announcement_Title, Announcement_Id = sas.Announcement_Id, Announcement_Body = sas.Announcement_Body };

            ViewBag.query = query;

            return View();
        }



        [HttpGet]
        public ActionResult virewClientFeedback()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var data = (from fedback in db.UserFeedbacks
                        join sc in db.Clients on fedback.UserId equals sc.UserId
                        where fedback.RoleId == 5


                        select new tbl_clienteFeedback
                        {
                            clientname = sc.UserName,
                            Description = fedback.Description,
                            Rating = fedback.Rating,
                            Date = fedback.CreatedDate,


                        }).ToList();

            //var data = db.Client_SchoolComplain.ToList();
            return View(data);
        }


        [HttpGet]
        public ActionResult TechnicalTips()
        {
            if (Session["Ad"] == null)
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
                tech.UserId = Convert.ToInt32(Session["Ad"]);
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

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var data = db.TechnicTips.ToList();
            return View(data);

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
            if (Session["Ad"] == null)
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
            return RedirectToAction("viewTechnicalTips", "Home");



        }










        [HttpGet]
        public ActionResult kidTalent()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult kidTalent(tbl_kidTalentValidation kidtal)
        {
            try
            {
                KidTalent kid = new KidTalent();



                kid.Role_Id = Convert.ToInt32(Session["RoleId"]);
                kid.Title = kidtal.Title;
                kid.VedioPath = kidtal.VedioPath;
                kid.shortDes = kidtal.shortDes;
                kid.statusId = 2;
                kid.LongDes = kidtal.LongDes;
                kid.UserId = Convert.ToInt32(Session["Ad"]);
                kid.CreatedDate = DateTime.Now;

                db.KidTalents.Add(kid);

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



        public ActionResult viewKidTalent()
        {

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var data = db.KidTalents.ToList();
            return View(data);

        }
        [HttpPost]

        public JsonResult DeletekidTalent(int telentid)
        {

            bool resul = false;
            KidTalent sc = db.KidTalents.SingleOrDefault(x => x.telentId == telentid);
            if (sc != null)
            {
                db.KidTalents.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult updatekidTalent(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            

            var data = db.KidTalents.Where(x => x.telentId == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updatekidTalent(KidTalent dat, int id)
        {

            try
            {
                
                var data = db.KidTalents.Where(x => x.telentId == id).First();

                if (data != null)
                {

                    data.Title = dat.Title;
                    data.VedioPath = dat.VedioPath;
                    data.shortDes = dat.shortDes;
                    data.LongDes = dat.LongDes;

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
            return RedirectToAction("viewKidTalent", "Home");



        }

        [HttpGet]
        public ActionResult addWebAdd()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addWebAdd(tbl_webaddValidation webadd)
        {
            try
            {
                
                webadd add = new webadd();

                string filename = Path.GetFileNameWithoutExtension(webadd.UserImageFIle.FileName);
                string extension = Path.GetExtension(webadd.UserImageFIle.FileName);
                filename = DateTime.Now.ToString("yymmssff") + extension;
                add.Image = "~/Content/img/" + filename;
                //image ko folder me save krwanay ke leye
                filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                webadd.UserImageFIle.SaveAs(filename);
                add.linq = webadd.linq;


                db.webadds.Add(add);

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
        public ActionResult viewWebAdd()
        {

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var data = db.webadds.ToList();
            return View(data);

        }
        [HttpPost]

        public JsonResult DeleteWebAdd(int telentid)
        {

            bool resul = false;
            webadd sc = db.webadds.SingleOrDefault(x => x.addId == telentid);
            if (sc != null)
            {
                db.webadds.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult updateWebAdd(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }


            var data = db.webadds.Where(x => x.addId == id).First();
            Session["Image"] = data.Image;

            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updateWebAdd(tbl_webaddValidation dat, int id)
        {

            try
            {
                webadd we = new webadd();
                //var data = db.webadds.Where(x => x.addId == id).First();

                if (dat.UserImageFIle != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(dat.UserImageFIle.FileName);
                    string extension = Path.GetExtension(dat.UserImageFIle.FileName);
                    filename = DateTime.Now.ToString("yymmssff") + extension;


                    we.Image = "~/Content/img/" + filename;
                    we.addId = id;
                    we.linq = dat.linq;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (dat.UserImageFIle.ContentLength <= 1000000)
                        {
                            db.Entry(we).State = EntityState.Modified;



                            string oldImgPath = Request.MapPath(Session["Image"].ToString());

                            if (db.SaveChanges() > 0)
                            {
                                filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                                dat.UserImageFIle.SaveAs(filename);
                                if (System.IO.File.Exists(oldImgPath))
                                {
                                    System.IO.File.Delete(oldImgPath);
                                }


                                ViewBag.Message = "Data Updated";
                                return RedirectToAction("viewWebAdd");
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

               
                else
                {
                    we.Image = Session["Image"].ToString();
                    
                    we.addId = id;
                    we.linq = dat.linq;
                    db.Entry(we).State = EntityState.Modified;

                    if (db.SaveChanges() > 0)
                    {


                        ViewBag.Message = "Data Updated";
                        return RedirectToAction("viewWebAdd");

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
        public ActionResult addquizcontext()
        {
            //if (Session["Ad"] == null)
            //{
            //    return RedirectToAction("login", "Home", new { area = "" });
            //}
            ////var Class = new SelectList(db.Classes.ToList(), "Class_Id", "Name");
            ////ViewData["Classes"] = Class;

            ////ViewData["Classes"] = Class;
            //CourseDBHandle gc = new CourseDBHandle();
            ////List<tbl_coursevalidation> lgc = gc.GetCourse();
            ////ViewBag.course = new SelectList(lgc, "courseId", "courseName");

            //List<tbl_OnlineTestValidation> list = gc.GetCourse();
            //ViewBag.course = new SelectList(list, "courseId", "courseName");



            return View();
        }
        [HttpPost]
        public JsonResult addquizcontext(QuestionOptionViewModel QuestionOption)
        {

            QuizContext q = new QuizContext();

            q.Question = QuestionOption.QuestionName;
            q.isActive = true;

            q.IsMultiple = false;
            db.QuizContexts.Add(q);
            db.SaveChanges();

            int questionId = q.QuestionId;
            foreach (var item in QuestionOption.ListOfOptions)
            {
                Option objoption = new Option();
                objoption.OptionName = item;
                objoption.QuestionId = questionId;
                db.Options.Add(objoption);
                db.SaveChanges();
            }
            Answer objanswer = new Answer();
            objanswer.AnswerText = QuestionOption.AnswerText;

            objanswer.QuestionId = questionId;
            db.Answers.Add(objanswer);
            db.SaveChanges();

            return Json(new { message = "Data Successfully Added", success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult viewquizcontext()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            var data = db.QuizContexts.ToList();

            return View(data);

        }
        [HttpPost]

        public JsonResult Deletequizcontext(int articleId)
        {

            bool resul = false;
            QuizContext sc = db.QuizContexts.SingleOrDefault(x => x.QuestionId == articleId);
            if (sc != null)
            {
                db.QuizContexts.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult updatequizcontext(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }


            var data = db.QuizContexts.Where(x => x.QuestionId == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updatequizcontext(QuizContext QuestionOption, int id)
        {

            try
            {

                var data = db.QuizContexts.Where(x => x.QuestionId == id).First();


                if (data != null)
                {
                    data.Question = QuestionOption.Question;

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
            return RedirectToAction("viewquizcontext", "Home");



        }
        [HttpGet]
        public ActionResult businessOffertyped()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            return View();
        }

        [HttpPost]

        public ActionResult businessOffertyped(tbl_articletypeValidation art)
        {
            try
            {
                BusinessOfferType arttype = new BusinessOfferType();
                arttype.Article_Typename = art.Article_Typename;
                db.BusinessOfferTypes.Add(arttype);
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
        public ActionResult addbusinessOffer()
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<BusinessOfferType> list = db.BusinessOfferTypes.ToList();
            ViewBag.articletypelist = new SelectList(list, "Article_TypeId", "Article_Typename ");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addbusinessOffer(tbl_ArticleValidation article)
        {
            try
            {
                BusinessOffer a = new BusinessOffer();

                List<BusinessOfferType> list = db.BusinessOfferTypes.ToList();
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

                db.BusinessOffers.Add(a);

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

        public ActionResult viewbusinessOffer()
        {

            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }

            var data = (from complain in db.BusinessOffers
                        join sc in db.loginTables on complain.Role_Id equals sc.RoleID
                        where complain.Role_Id == 1

                        select new tbl_ArticleValidation
                        {

                            ArticleId = complain.OfferId,
                            imgPath = complain.imgPath,
                            Title = complain.Title,
                            CreatedDate = complain.CreatedDate,
                            longDes = complain.longDes

                        }).ToList();



            return View(data);

        }
        [HttpGet]
        public ActionResult updatebusinessOffer(int id)
        {
            if (Session["Ad"] == null)
            {
                return RedirectToAction("login", "Home", new { area = "" });
            }
            List<BusinessOfferType> list = db.BusinessOfferTypes.ToList();
            ViewBag.articletypelist = new SelectList(list, "Article_TypeId", "Article_Typename");


            var data = db.BusinessOffers.Where(x => x.OfferId == id).First();


            return View(data);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult updatebusinessOffer(BusinessOffer dat, int id)
        {

            try
            {
                List<BusinessOfferType> list = db.BusinessOfferTypes.ToList();
                ViewBag.articletypelist = new SelectList(list, "Article_TypeId", "Article_Typename");

                var data = db.BusinessOffers.Where(x => x.OfferId == id).First();

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
            return RedirectToAction("viewbusinessOffer", "Home");



        }
        [HttpPost]

        public JsonResult DeletebusinessOffer(int articleId)
        {

            bool resul = false;
            BusinessOffer sc = db.BusinessOffers.SingleOrDefault(x => x.OfferId == articleId);
            if (sc != null)
            {
                db.BusinessOffers.Remove(sc);
                db.SaveChanges();
                resul = true;
            }

            return Json(resul, JsonRequestBehavior.AllowGet);
        }


    }
}