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
    public class SueldoHistoricoController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: SueldoHistorico
        public ActionResult Index()
        {
            return View(db.tbSueldoHistorico.ToList());
        }

        // GET: SueldoHistorico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSueldoHistorico tbSueldoHistorico = db.tbSueldoHistorico.Find(id);
            if (tbSueldoHistorico == null)
            {
                return HttpNotFound();
            }
            return View(tbSueldoHistorico);
        }

        // GET: SueldoHistorico/Creat

        // POST: SueldoHistorico/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
 
        // GET: SueldoHistorico/Edit/5
      

        // POST: SueldoHistorico/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
      

        // GET: SueldoHistorico/Delete/5
    

        // POST: SueldoHistorico/Delete/5

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
