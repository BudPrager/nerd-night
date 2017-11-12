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
    public class GroupController : Controller
    {
        private GroupContext db = new GroupContext();

        // GET: Group
        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        // GET: Group/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GroupName")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GroupName")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Group/Prefs/5
        [ActionName("Prefs")]
        public ActionResult EditAvailabilityPreferrences(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group group = db.Groups.Find(id);
            if(group == null)
            {
                return HttpNotFound();
            }

            var preferredDays = group.PreferredDays;
            foreach(DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                if(!preferredDays.Any(p => p.DayOfWeek == day))
                {
                    preferredDays.Add(
                        new PreferredDay()
                        {
                            GroupID = id.Value,
                            DayOfWeek = day
                        });
                }
            }
            preferredDays = preferredDays.OrderBy(p => p.DayOfWeek).ToList();

            AvailabilityViewModel viewModel = new AvailabilityViewModel()
            {
                GroupId = group.ID,
                DefaultAvailabilityRange = group.DefaultAvailabilityRange,
                AvailabilityUnit = group.AvailabilityUnit,
                MainCampaignID = group.CampaignID,
                PreferredDays = preferredDays
            };

            ViewBag.CampaignID = new SelectList(group.Campaigns, "ID", "CampaignName", viewModel.MainCampaignID);
            return View(viewModel);
        }

        [HttpPost, ActionName("Prefs")]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAvailabilityPreferrences(AvailabilityViewModel availabilityView)
        {
            if (ModelState.IsValid)
            {
                var group = db.Groups.Where(g => g.ID == availabilityView.GroupId).Include(g => g.PreferredDays).SingleOrDefault();

                if (group != null)
                {
                    //We don't need to save all the blank days, might aswell save some space
                    var blankDays = availabilityView.PreferredDays.Where(d => !(d.AllDay || d.Morning || d.Afternoon || d.Evening)).ToArray();
                    foreach (var day in blankDays)
                    {
                        availabilityView.PreferredDays.Remove(day);

                        //If we previously stored this day remove it from the database
                        var originalDay = group.PreferredDays.SingleOrDefault(d => d.ID == day.ID);
                        if (originalDay != null)
                        {
                            db.PreferredDays.Remove(originalDay);
                        }
                    }

                    //Set the values per items, entity framework doesn't know what to do with a whole collection
                    foreach (var day in availabilityView.PreferredDays)
                    {
                        var originalDay = group.PreferredDays.Where(d => d.ID == day.ID && d.ID != 0).SingleOrDefault();
                        if (originalDay != null)
                        {
                            var dayEntity = db.Entry(originalDay);
                            dayEntity.CurrentValues.SetValues(day);
                        }
                        else
                        {
                            day.ID = 0;
                            group.PreferredDays.Add(day);
                        }
                    }

                    group.DefaultAvailabilityRange = availabilityView.DefaultAvailabilityRange;
                    group.AvailabilityUnit = availabilityView.AvailabilityUnit;
                    group.CampaignID = availabilityView.MainCampaignID;

                    db.Entry(group).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Edit", new { group.ID });
                }            
            }
            return View(availabilityView);
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
