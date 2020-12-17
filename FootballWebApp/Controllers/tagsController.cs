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
    public class tagsController : Controller
    {
        private FootballDB db = new FootballDB();

        // GET: tags
        public ActionResult Index()
        {
            var tags = db.tags.Include(t => t.post);
            return View(tags.ToList());
        }

        // GET: tags/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tag tag = db.tags.Find(id);
        //    if (tag == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tag);
        //}

        // GET: tags/Create
        public ActionResult Create(int id)
        {
            ViewBag.post_title = db.posts.Single(p=>p.id==id).post_title;
            ViewBag.p_id = id;
            return View();
        }

        // POST: tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tag_title")] tag tag,int id)
        {
            if (ModelState.IsValid)
            {
                tag.post_id = id;
                db.tags.Add(tag);
                db.SaveChanges();
                return RedirectToAction("Index","posts");
            }

            ViewBag.post_id = new SelectList(db.posts, "id", "post_title", tag.post_id);
            return View(tag);
        }

        // GET: tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tag tag = db.tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            ViewBag.post_id = new SelectList(db.posts, "id", "post_title", tag.post_id);
            return View(tag);
        }

        // POST: tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tag_title,post_id")] tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.post_id = new SelectList(db.posts, "id", "post_title", tag.post_id);
            return View(tag);
        }

        // GET: tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tag tag = db.tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tag tag = db.tags.Find(id);
            db.tags.Remove(tag);
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
