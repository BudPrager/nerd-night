using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NerdNight.DAL;
using NerdNight.Models;

namespace NerdNight.Controllers
{
    public class CampaignsController : Controller
    {
        private GroupContext db = new GroupContext();

        // GET: Campaigns
        //public ActionResult Index()
        //{
        //    var campaigns = db.Campaigns.Include(c => c.Group);
        //    return View(campaigns.ToList());
        //}

        // GET : Campaigns/5
        //public ActionResult Index(int? groupId)
        //{
        //    var campaigns = db.Campaigns.Include(c => c.Group);

        //    if (groupId.HasValue)
        //    {
        //        campaigns = campaigns.Where(c => c.GroupID == groupId.Value);
        //    }
             
        //    if(campaigns.Count() == 0)
        //    {
        //        return HttpNotFound(); //TODO - should we really return a 404 for this?
        //    }

        //    return View(campaigns);
        //}

        // GET: Campaigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // GET: Campaigns/Create(/5)
        public ActionResult Create(int? groupID)
        {
            if(groupID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (groupID.HasValue)
            {
                Group group = db.Groups.SingleOrDefault(g => g.ID == groupID.Value);
                if (group != null)
                {
                    var campaign = new Campaign()
                    {
                        GroupID = group.ID,
                        Group = group
                    };

                    return View(campaign);
                }
            }

            return HttpNotFound();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CampaignName,GroupID")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                db.Campaigns.Add(campaign);
                db.SaveChanges();
                return RedirectToAction("Details", "Group", new { id = campaign.GroupID });
            }

            ViewBag.GroupID = new SelectList(db.Groups, "ID", "GroupName", campaign.GroupID);
            return View(campaign);
        }

        // GET: Campaigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "GroupName", campaign.GroupID);
            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CampaignName,GroupID")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campaign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Group", new { id = campaign.GroupID });
            }
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "GroupName", campaign.GroupID);
            return View(campaign);
        }

        // GET: Campaigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campaign campaign = db.Campaigns.Find(id);
            db.Campaigns.Remove(campaign);
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
