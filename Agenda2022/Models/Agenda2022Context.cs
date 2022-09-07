using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Agenda2022.Models
{
    public class Agenda2022Context:DbContext
    {
        //Crear el constructor por defecto
        public Agenda2022Context():base("DefaultConnection")
        {

        }
        //metodo para controlar la eliminacion en cascada 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        //Crear las representaciones de cada clase en la base de datos 

        public DbSet<Ficha> Fichas { get; set; }
        public DbSet<Aprendiz> Aprendizs { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<FichaInstructor> FichaInstructors { get; set; }

    }
}