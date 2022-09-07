using Agenda2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace Agenda2022.Controllers
{
    public class AprendizsController : Controller
    {
        //Crear un objeto que represente el contexto
        private Agenda2022Context db = new Agenda2022Context();
        // GET: Aprendizs
        public ActionResult Index()
        {
            //recuperar la relacion entre Aprendiz y ficha
            var aprendiz = db.Aprendizs.Include(a => a.ficha);
            return View(db.Aprendizs.ToList()); // SELECT * FROM table
        }

        [HttpGet] // Retorna una vista o infromacion al usuario 
        public ActionResult Create()
        {
            ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo");
            return View();
        }
        [HttpPost] // Recibe informaciion la valida y la lleva a la base de datos 
        public ActionResult Create(Aprendiz aprendiz)
        {
            try
            {
                db.Aprendizs.Add(aprendiz);  //INSERT INTO table values()
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null
                    && ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                {
                    ViewBag.Error = ("Ya existe un aprendiz con el mismo ducumento: " + aprendiz.Documento);
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
                return View(aprendiz);

            }
            return RedirectToAction("Index");

        }
        [HttpGet] // Retorna una vista o infromacion al usuario 
        public ActionResult Edit(int id)
        {

            Aprendiz aprendiz = db.Aprendizs.Find(id); //SELECT * FROM tabla WHERE FichaID = id
            if (aprendiz != null)
            {
                ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
                return View(aprendiz);
            }
            else
            {
                return HttpNotFound();
            }

        }
        [HttpPost] // Recibe informaciion la valida y la lleva a la base de datos 
        public ActionResult Edit(Aprendiz aprendiz)
        {
            try
            {
                db.Entry(aprendiz).State = EntityState.Modified;  //UPDATE Fichas SET programa= '', Codigo= '', .... WHERE FichaId = Ficha.Id
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null && ex.InnerException.InnerException != null
                    && ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                {
                    ViewBag.Error = ("Ya existe un aprendiz con el documento: " + aprendiz.Documento);
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
                return View(aprendiz);
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aprendiz aprendiz = db.Aprendizs.Find(id);
            if (aprendiz == null)
            {
                return HttpNotFound();
            }
            ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
            return View(aprendiz);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Aprendiz aprendiz = db.Aprendizs.Find(id);
            if (aprendiz == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    db.Aprendizs.Remove(aprendiz);
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    ViewBag.Error = e;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aprendiz aprendiz = db.Aprendizs.Find(id);
            if (aprendiz == null)
            {
                return HttpNotFound();
            }
            return View(aprendiz);
        }
        //Metodo para cerar la conexion con la base de datos 
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