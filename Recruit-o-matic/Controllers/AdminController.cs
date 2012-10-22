using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using Recruit_o_matic.Models;
using Recruit_o_matic.ViewModels.Admin;

namespace Recruit_o_matic.Controllers
{
    public class AdminController : BaseRavenController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            var viewModel = new IndexVM()
            {
                Positions = RavenSession.Query<Position>().ToList()
            };

            return View(viewModel);
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(string id)
        {
            return View();
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(Position position)
        {
            try
            {
                // TODO: Add insert logic here
                RavenSession.Store(position);                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(string id)
        {
            var position = RavenSession.Load<Position>(id);
            
            return View(position);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(Position position)
        {
            try
            {
                RavenSession.Store(position);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(string id)
        {
            var position = RavenSession.Load<Position>(id);
            return View(position);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult DeleteConfirm(string id)
        {
            try
            {
                var position = RavenSession.Load<Position>(id);
                RavenSession.Delete<Position>(position);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Publish(string id)
        {
            var position = RavenSession.Load<Position>(id);

            position.Published = position.Published ? false : true;

            return RedirectToAction("Index");

            
        }
    }
}
