using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATRafaelFront2.Infra;
using ATRafaelFront2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATRafaelFront2.Controllers
{

    public class TodoController : Controller
    {
        private readonly TaskRestClient trc;

        public TodoController() {
            this.trc = new TaskRestClient();
            
        }

        public ActionResult Index() {
            var model = this.trc.GetAll();
            return View(model);
        }

        
        public ActionResult Details(Guid id)
        {
            var model = this.trc.GetById(id);
            return View(model);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Create(TaskModel t)
        {
            try
            {
                this.trc.Save(t);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Edit(Guid id)
        {
            var model = this.trc.GetById(id);
            return View(model);
        }

        
        [HttpPost]
        public ActionResult Edit(Guid id, TaskModel t)
        {
            try
            {
                this.trc.Update(t);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(Guid id)
        {
            var model = this.trc.GetById(id);
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, TaskModel t)
        {
            try
            {
                this.trc.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
