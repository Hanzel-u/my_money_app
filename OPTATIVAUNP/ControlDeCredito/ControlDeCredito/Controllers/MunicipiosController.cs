﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlDeCredito.Models;

namespace ControlDeCredito.Controllers
{
    public class MunicipiosController : Controller
    {
        private ControlDeCreditoContainer db = new ControlDeCreditoContainer();

        // GET: Municipios
        public ActionResult Index()
        {
            var municipio = db.Municipio.Include(m => m.Departamento);
            return View(municipio.ToList());
        }

        // GET: Municipios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        // GET: Municipios/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentoId = new SelectList(db.Departamento, "Id", "DescripcionDeDepartamento");
            return View();
        }

        // POST: Municipios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodigoDeMunicipio,DescripcionDeMunicipio,DepartamentoId")] Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                db.Municipio.Add(municipio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartamentoId = new SelectList(db.Departamento, "Id", "CodigoDeDepartamento", municipio.DepartamentoId);
            return View(municipio);
        }

        // GET: Municipios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartamentoId = new SelectList(db.Departamento, "Id", "CodigoDeDepartamento", municipio.DepartamentoId);
            return View(municipio);
        }

        // POST: Municipios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodigoDeMunicipio,DescripcionDeMunicipio,DepartamentoId")] Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(municipio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartamentoId = new SelectList(db.Departamento, "Id", "CodigoDeDepartamento", municipio.DepartamentoId);
            return View(municipio);
        }

        // GET: Municipios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Municipio municipio = db.Municipio.Find(id);
            db.Municipio.Remove(municipio);
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
