using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GerenciamentoTarefaDB.Models;

namespace GerenciamentoTarefaDB.Controllers
{
    public class TarefaController : Controller
    {
        private GerenciamentoTarefaDBEntities db = new GerenciamentoTarefaDBEntities();

        // GET: Tarefa
        public ActionResult Index()
        {
            var tarefa = db.Tarefa.Include(t => t.Prioridade).Include(t => t.StatusTarefa).Include(t => t.Usuario);
            return View(tarefa.ToList());
        }

        // GET: Tarefa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarefa tarefa = db.Tarefa.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        // GET: Tarefa/Create
        public ActionResult Create()
        {
            ViewBag.IdPrioridade = new SelectList(db.Prioridade, "IdPrioridade", "Descricao");
            ViewBag.IdStatus = new SelectList(db.StatusTarefa, "IdStatus", "Descricao");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IDUsuario", "Nome");
            return View();
        }

        // POST: Tarefa/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTarefa,Descricao,NomeSetor,IdStatus,IdUsuario,IdPrioridade")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.DataCadastro = DateTime.Now;
                db.Tarefa.Add(tarefa);
                db.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro concluído com sucesso!";
                return RedirectToAction("Create");
            }

            ViewBag.IdPrioridade = new SelectList(db.Prioridade, "IdPrioridade", "Descricao", tarefa.IdPrioridade);
            ViewBag.IdStatus = new SelectList(db.StatusTarefa, "IdStatus", "Descricao", tarefa.IdStatus);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IDUsuario", "Nome", tarefa.IdUsuario);
            return View(tarefa);
        }

        // GET: Tarefa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarefa tarefa = db.Tarefa.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPrioridade = new SelectList(db.Prioridade, "IdPrioridade", "Descricao", tarefa.IdPrioridade);
            ViewBag.IdStatus = new SelectList(db.StatusTarefa, "IdStatus", "Descricao", tarefa.IdStatus);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IDUsuario", "Nome", tarefa.IdUsuario);
            return View(tarefa);
        }

        // POST: Tarefa/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTarefa,Descricao,NomeSetor,DataCadastro,IdStatus,IdUsuario,IdPrioridade")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarefa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPrioridade = new SelectList(db.Prioridade, "IdPrioridade", "Descricao", tarefa.IdPrioridade);
            ViewBag.IdStatus = new SelectList(db.StatusTarefa, "IdStatus", "Descricao", tarefa.IdStatus);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IDUsuario", "Nome", tarefa.IdUsuario);
            return View(tarefa);
        }

        // GET: Tarefa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarefa tarefa = db.Tarefa.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        // POST: Tarefa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tarefa tarefa = db.Tarefa.Find(id);
            db.Tarefa.Remove(tarefa);
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
