using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FootballWebApp.Models;

namespace FootballWebApp.Controllers
{
    public class PostTagsController : Controller
    {
        private FootballDB db = new FootballDB();

        // GET: PostTags
        public ActionResult Index()
        {
            return View(db.PostTags.ToList());
        }

        //// GET: PostTags/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PostTag postTag = db.PostTags.Find(id);
        //    if (postTag == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(postTag);
        //}

        // GET: PostTags/Create
        public ActionResult Create()
        {
            ViewBag.tags = new SelectList(db.tags.ToList(), "id", "tag_title");
            ViewBag.posts = new SelectList(db.posts.ToList(), "id", "post_title");
            return View();
        }

        // POST: PostTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "")] PostTag postTag, int tags, int posts)
        {
            if (ModelState.IsValid)
            {
                postTag.postId = posts;
                postTag.tagId = tags;
                db.PostTags.Add(postTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postTag);
        }

        // GET: PostTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostTag postTag = db.PostTags.Find(id);
            if (postTag == null)
            {
                return HttpNotFound();
            }
            return View(postTag);
        }

        // POST: PostTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "postId,tagId")] PostTag postTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postTag);
        }

        // GET: PostTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostTag postTag = db.PostTags.Find(id);
            if (postTag == null)
            {
                return HttpNotFound();
            }
            return View(postTag);
        }

        // POST: PostTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostTag postTag = db.PostTags.Find(id);
            db.PostTags.Remove(postTag);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
