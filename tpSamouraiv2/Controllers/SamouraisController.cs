using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using tpSamouraiv2.Data;
using tpSamouraiv2.Models;

namespace tpSamouraiv2.Controllers
{
    public class SamouraisController : Controller
    {
        private tpSamouraiv2Context db = new tpSamouraiv2Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }



        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           //recuperation du samourai et de son arme attachée (LazyLoading)
            Samourai samourai = db.Samourais.Include(a => a.Arme).FirstOrDefault(s => s.Id ==id);


            
            if (samourai == null)
            {
                return HttpNotFound();
            }
            int degats = 0;
            //calcul du potentiel du samourai
            int force = samourai.Force;
            int nbArtMatiaux = samourai.ArtsMartiaux.Count();
            if (samourai.Arme.Degats != null)
            {
                 degats = (int)samourai.Arme.Degats;
            }
            else
            {
                 degats = 0;
            }
            samourai.Potentiel = (force + degats) *(nbArtMatiaux +1);
                       
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraiVM vm = new SamouraiVM();

            //selectionne toute les armes n'ayant pas de liens avec les samourais 
            vm.Arme = db.Armes.Include(x => x.Samourai).Where(x => x.Samourai == null).Select(

                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })

                .ToList();

            //selection de la liste des Arts matieaux existants
            vm.ArtMartiaux = db.ArtMartial.Select(

                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })

                .ToList();
            return View(vm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SamouraiVM vm)
        {
            Samourai samourai = new Samourai();

            if (ModelState.IsValid)
            {
                samourai.Nom = vm.Samourai.Nom;
                samourai.Force = vm.Samourai.Force;

                if (vm.IdArme != null)
                {
                    samourai.Arme = db.Armes.Find(vm.IdArme);

                }
                //ajout des Arts matiaux selectionnés a notre samourai
                if (vm.IdArtMartial.Count > 0)

                {

                    samourai.ArtsMartiaux = db.ArtMartial.Where(a => vm.IdArtMartial.Contains(a.Id)).ToList();

                }
                db.Entry(samourai).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Samourais/Edit/{id}
        public ActionResult Edit(int? id)
        {
            SamouraiVM vm = new SamouraiVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Samourai samourai = db.Samourais.Include(a => a.Arme).FirstOrDefault(s => s.Id == id);


            vm.Samourai = samourai;
            //selectionne toute les armes n'ayant pas de liens avec les samourais 

            vm.Arme =db.Armes.Include(x => x.Samourai).Where(x => x.Samourai ==null || x.Samourai.Id == vm.Samourai.Id).Select(

                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })

                .ToList();
           
            vm.ArtMartiaux = db.ArtMartial.Select(

               x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })

               .ToList();

            if (samourai.Arme!= null){
                vm.IdArme = samourai.Arme.Id;
            }

            //inectiion des Artmatiaux pratiqué par le samourai (permet la subrillance dans la vue)
            if (samourai.ArtsMartiaux != null)
            {
                vm.IdArtMartial = samourai.ArtsMartiaux.Select(a => a.Id).ToList();

            }

            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: Samourais/Edit/{id}
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit(SamouraiVM vm)
        {
            if (ModelState.IsValid)
            {
                

                Samourai samourai = db.Samourais.Include(a => a.Arme).FirstOrDefault(s => s.Id==vm.Samourai.Id);
                samourai.Arme = (vm.IdArme != null) ? db.Armes.FirstOrDefault(a => a.Id == vm.IdArme) : null;

                samourai.Nom = vm.Samourai.Nom;
                samourai.Force = vm.Samourai.Force;
                //on vide la liste des art martiaux du samourai
                samourai.ArtsMartiaux.Clear();
                // on ajoute les art martiaux selectionné au samourai
               foreach(var item in vm.IdArtMartial) {

                    samourai.ArtsMartiaux.Add(db.ArtMartial.FirstOrDefault(a => a.Id == item));
                }
                db.Entry(samourai).State = EntityState.Modified;

                  db.SaveChanges();


                   return RedirectToAction("Index");

                


            }
            return View(vm);
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
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
