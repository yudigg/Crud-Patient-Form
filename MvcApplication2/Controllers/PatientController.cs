using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Models;

namespace MvcApplication2.Controllers
{
    public class PatientController : Controller
    {
        private string conn = @"Data Source=.\sqlexpress;Initial Catalog='doctor patient data';Integrated Security=True";
        public ActionResult PatientList()
        {
            PatientsDb db = new PatientsDb(conn);
           var all = db.GetAllPatients();
            return View(all);
        }
        public ActionResult Form()
        {
            return View();
        }
        public ActionResult EditForm(int pid)
        {
            PatientsDb db = new PatientsDb(conn);
            Patient p = db.GetById(pid);
            return View(p);
        }
        [HttpPost]
        public ActionResult Add(string name, int age, string insurance)
        {
            PatientsDb db = new PatientsDb(conn);
            db.AddPatient(name, insurance, age);
            return RedirectToAction("PatientList");
        }
        [HttpPost]
        public ActionResult Edit(string name, int age, string insurance,int id)
        {
            PatientsDb db = new PatientsDb(conn);
          db.Update(name, age, insurance, id);
          return RedirectToAction("PatientList");
        }
        [HttpPost]
        public ActionResult DeleteForm(int id)
        {
            PatientsDb db = new PatientsDb(conn);
            db.Delete(id);
           return RedirectToAction("PatientList");
        }
    }
}
