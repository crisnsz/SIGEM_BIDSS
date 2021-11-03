using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class prie : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: prie
        public ActionResult Index()
        {
            var tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Include(t => t.tbAnticipoViatico).Include(t => t.tbEstado);
            return View(tbLiquidacionAnticipoViatico.ToList());
        }

        // GET: prie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Find(id);
            if (tbLiquidacionAnticipoViatico == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViatico);
        }

        // GET: prie/Create
        public ActionResult Create()
        {
            ViewBag.Anvi_Id = new SelectList(db.tbAnticipoViatico, "Anvi_Id", "Anvi_Correlativo");
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
            return View();
        }

        // POST: prie/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lianvi_Id,Lianvi_Correlativo,Anvi_Id,Lianvi_FechaLiquida,Lianvi_FechaInicioViaje,Lianvi_FechaFinViaje,Lianvi_Comentario,est_Id,Lianvi_RazonRechazo,Lianvi_UsuarioCrea,Lianvi_FechaCrea,Lianvi_UsuarioModifica,Lianvi_FechaModifica")] tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico)
        {
            if (ModelState.IsValid)
            {
                db.tbLiquidacionAnticipoViatico.Add(tbLiquidacionAnticipoViatico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Anvi_Id = new SelectList(db.tbAnticipoViatico, "Anvi_Id", "Anvi_Correlativo", tbLiquidacionAnticipoViatico.Anvi_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbLiquidacionAnticipoViatico.est_Id);
            return View(tbLiquidacionAnticipoViatico);
        }

        // GET: prie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Find(id);
            if (tbLiquidacionAnticipoViatico == null)
            {
                return HttpNotFound();
            }
            ViewBag.Anvi_Id = new SelectList(db.tbAnticipoViatico, "Anvi_Id", "Anvi_Correlativo", tbLiquidacionAnticipoViatico.Anvi_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbLiquidacionAnticipoViatico.est_Id);
            return View(tbLiquidacionAnticipoViatico);
        }

        // POST: prie/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lianvi_Id,Lianvi_Correlativo,Anvi_Id,Lianvi_FechaLiquida,Lianvi_FechaInicioViaje,Lianvi_FechaFinViaje,Lianvi_Comentario,est_Id,Lianvi_RazonRechazo,Lianvi_UsuarioCrea,Lianvi_FechaCrea,Lianvi_UsuarioModifica,Lianvi_FechaModifica")] tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbLiquidacionAnticipoViatico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Anvi_Id = new SelectList(db.tbAnticipoViatico, "Anvi_Id", "Anvi_Correlativo", tbLiquidacionAnticipoViatico.Anvi_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbLiquidacionAnticipoViatico.est_Id);
            return View(tbLiquidacionAnticipoViatico);
        }

        // GET: prie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Find(id);
            if (tbLiquidacionAnticipoViatico == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViatico);
        }

        // POST: prie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Find(id);
            db.tbLiquidacionAnticipoViatico.Remove(tbLiquidacionAnticipoViatico);
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
