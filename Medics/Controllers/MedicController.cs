using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Medics.Models;
using Medics.Models.ViewModels;
using Medics.Models.Entities;
using Medics.Controllers;
using System.Data;
using System.Data.SqlClient;

namespace Medics.Controllers
{
    public class MedicController : Controller
    {
        string connectionString = @"Data Source=DESKTOP-9VS1P1T\SQLEXPRESS;Initial Catalog=Medicos;Integrated Security=True";
        

        // GET: Medic
        public ActionResult Index()
        {
            List<MedicViewModel> list;
            using (MedicsEntities bd = new MedicsEntities())
            {
                list = (from m in bd.Medicos
                        select new MedicViewModel
                        {
                            IdMedic = m.IdMedico,
                            Nombre = m.Nombre,
                            Apellido = m.Apellido,
                            Especialidad = m.Especialidad,
                            Matricula = m.Matricula

                        }).ToList();
            }
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string Name, string Lastname, string Specialties, int? Enrollment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MedicsEntities bd = new MedicsEntities())
                    {
                        var oMedic = new Medicos();
                        oMedic.Nombre = Request["Name"];
                        oMedic.Apellido = Request["LastName"];
                        oMedic.Especialidad = Request["Specialties"];
                        oMedic.Matricula = Convert.ToInt32(Request["Enrollment"]);
                        bd.Medicos.Add(oMedic);

                        bd.SaveChanges();
                    }
                    return Redirect("/Medic");
                }
                return View();
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);

            }
        }

        
        public ActionResult Edit(int id)
        {
            MedicViewModel model = new MedicViewModel();
            using (MedicsEntities bd = new MedicsEntities())
            {
                var oMedic = bd.Medicos.Find(id);
                model.Nombre = oMedic.Nombre;
                model.Apellido = oMedic.Apellido;
                model.Especialidad = oMedic.Especialidad;
                model.Matricula = Convert.ToInt32(oMedic.Matricula);
                model.IdMedic = oMedic.IdMedico;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MedicViewModel model)
        {
           try { 
                if (ModelState.IsValid) { 
            using (MedicsEntities bd = new MedicsEntities())
            {

                var oMedic = bd.Medicos.Find(model.IdMedic);
                oMedic.Nombre = model.Nombre;
                oMedic.Apellido = model.Apellido;
                oMedic.Especialidad = model.Especialidad;
                oMedic.Matricula = model.Matricula;

                bd.Entry(oMedic).State = System.Data.Entity.EntityState.Modified;
                bd.SaveChanges();


            }
                    return Redirect("/Medic");
                }
                return View(model);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        [HttpGet]
        public ActionResult Erase(int Id)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM Medicos Where IdMedico = @IdMedico";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@IdMedico", Id);
                sqlCmd.ExecuteNonQuery();
                
                //var oMedic = bd.Medicos.Find(Id);
                //bd.Medicos.Attach(oMedic);
                //bd.Medicos.Remove(oMedic);
                //bd.SaveChanges();
               
            }
            return Redirect("/Medic");
        }
    
    }
}