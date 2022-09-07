using Agenda2022.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;

namespace Agenda2022.Controllers
{
    public class FichasController : Controller
    {
        //Crear un objeto que represente el context
        private Agenda2022Context db = new Agenda2022Context();
        // GET: Fichas
        public ActionResult Index()
        {
            return View(db.Fichas.ToList()); // SELECT * FROM table
        }
        //Crear un registro de tipo ficha
        [HttpGet] // Retorna una vista o infromacion al usuario 
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost] // Recibe informaciion la valida y la lleva a la base de datos 
        public ActionResult Create(Ficha ficha)
        {
            try
            {
                db.Fichas.Add(ficha);  //INSERT INTO table values()
                db.SaveChanges();  
            }
            catch (Exception ex)
            {
                if(ex.InnerException != null && ex.InnerException.InnerException != null
                    && ex.InnerException.InnerException.Message.Contains("IndexCodigo"))
                {
                   ViewBag.Error = ("Ya existe una ficha con este codigo: " + ficha.Codigo);
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                return View(ficha);

            }
            return RedirectToAction("Index");

        }


        [HttpGet] // Retorna una vista o infromacion al usuario 
        public ActionResult Edit(int id)
        {

            Ficha ficha = db.Fichas.Find(id); //SELECT * FROM tabla WHERE FichaID = id
            if(ficha != null)
            {
                return View(ficha);
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        [HttpPost] // Recibe informaciion la valida y la lleva a la base de datos 
        public ActionResult Edit(Ficha ficha)
        {
            try
            {
                db.Entry(ficha).State = EntityState.Modified;  //UPDATE Fichas SET programa= '', Codigo= '', .... WHERE FichaId = Ficha.Id
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null && ex.InnerException.InnerException != null
                    && ex.InnerException.InnerException.Message.Contains("IndexCodigo"))
                {
                    ViewBag.Error = ("Ya existe una ficha con este codigo: " + ficha.Codigo);
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                return View(ficha);
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
            Ficha ficha = db.Fichas.Find(id);
            if (ficha == null)
            {
                return HttpNotFound();
            }
            return View(ficha);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Ficha ficha = db.Fichas.Find(id);
            if(ficha ==  null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    db.Fichas.Remove(ficha);
                    db.SaveChanges();
                    
                }
                catch (Exception ex)
                {
                    if(ex.InnerException!= null && ex.InnerException.InnerException != null &&
                         ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                    {
                        ViewBag.Error = "No s epermite eliminar registros con integrida referencial";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(ficha);
                    
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
            Ficha ficha = db.Fichas.Find(id);
            if (ficha == null)
            {
                return HttpNotFound();
            }

            var fichaDet = new FichaDetailsView
            {
                FichaId = ficha.FichaId,
                Codigo = ficha.Codigo,
                Programa = ficha.Programa,
                FechaInicio = ficha.FechaInicio,
                FechaFin = ficha.FechaFin,
                FichaInstructors = ficha.FichaInstructors.ToList()
            };

            return View(fichaDet);
        }
        

        [HttpGet]
        public ActionResult AddInstructor(int fichaId)
        {
            ViewBag.InstructorId = new SelectList(db.Instructors.OrderBy(i => i.Nombres)
                .ThenBy(i => i.Apellidos), "InstructorId", "FullName");

            var FichaInst = new FichaInstructorView
                {
                   FichaId = fichaId,
            };
            return View(FichaInst);

        }

        [HttpPost]
        public ActionResult AddInstructor(FichaInstructorView fichaInst)
        {
            if (ModelState.IsValid)
            {
                ViewBag.InstructorId = new SelectList(db.Instructors.OrderBy(i => i.Nombres)
                    .ThenBy(i => i.Apellidos), "InstructorId", "FullName");

                var instFicha = db.FichaInstructors.Where(fi => fi.FichaId == fichaInst.FichaId &&
                fi.InstructorId == fichaInst.InstructorId).FirstOrDefault();

                if(instFicha != null)
                {
                    ViewBag.Error = ("El Instructor, ya esta vinculado a la ficha ");
                    return View(fichaInst);
                }
                else
                {
                    instFicha = new FichaInstructor
                    {
                        FichaId = fichaInst.FichaId,
                        InstructorId = fichaInst.InstructorId,
                    };
                    db.FichaInstructors.Add(instFicha);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(fichaInst);
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