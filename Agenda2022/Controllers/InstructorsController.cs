using Agenda2022.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Agenda2022.Controllers
{
    public class InstructorsController : Controller
    {
        //Crear un objeto que represente el context
        private Agenda2022Context db = new Agenda2022Context();
        // GET: Fichas
        public ActionResult Index()
        {
            return View(db.Instructors.ToList()); // SELECT * FROM table
        }
        //Crear un registro de tipo ficha
        [HttpGet] // Retorna una vista o infromacion al usuario 
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost] // Recibe informaciion la valida y la lleva a la base de datos 
        public ActionResult Create(Instructor instructor)
        {
            try
            {
                db.Instructors.Add(instructor);  //INSERT INTO table values()
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null
                    && ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                {
                    ViewBag.Error = ("Ya existe una ficha con este codigo: " + instructor.Documento);
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                return View(instructor);

            }
            return RedirectToAction("Index");

        }


        [HttpGet] // Retorna una vista o infromacion al usuario 
        public ActionResult Edit(int id)
        {

            Instructor instructor = db.Instructors.Find(id); //SELECT * FROM tabla WHERE FichaID = id
            if (instructor != null)
            {
                return View(instructor);
            }
            else
            {
                return HttpNotFound();
            }

        }
        [HttpPost] // Recibe informaciion la valida y la lleva a la base de datos 
        public ActionResult Edit(Instructor instructor)
        {
            try
            {
                db.Entry(instructor).State = EntityState.Modified;  //UPDATE Fichas SET programa= '', Codigo= '', .... WHERE FichaId = Ficha.Id
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null && ex.InnerException.InnerException != null
                    && ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                {
                    ViewBag.Error = ("Ya existe una ficha con este codigo: " + instructor.Documento);
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                return View(instructor);
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
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    db.Instructors.Remove(instructor);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                         ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                    {
                        ViewBag.Error = "No s epermite eliminar registros con integrida referencial";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(instructor);

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
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
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