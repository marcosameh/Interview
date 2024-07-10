using App.Core.Entities;
using App.Core.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.UI.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceManager invoiceManager;
        private readonly UnitManager unitManager;

        public InvoiceController(InvoiceManager invoiceManager,UnitManager unitManager)
        {
            this.invoiceManager = invoiceManager;
            this.unitManager = unitManager;
        }
        // GET: InvoiceController
        public ActionResult Index()
        {
            var invoices = invoiceManager.GetAll();
            return View(invoices);
        }

        // GET: InvoiceController/Details/5
        public ActionResult Details(int lineNo)
        {
            var invoice = invoiceManager.Get(lineNo);
            return View(invoice);
        }

        // GET: InvoiceController/Create
        public ActionResult Create()
        {
            ViewBag.UnitNo = new SelectList(unitManager.GetAll(),"UnitNo","UnitName");
            return View();
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceDetail invoice)
        {
            try
            {
                invoice.Total = invoice.Quantity * invoice.Price;
                invoiceManager.Add(invoice);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Edit/5
        public ActionResult Edit(int lineNo)
        {
            ViewBag.UnitNo = new SelectList(unitManager.GetAll(), "UnitNo", "UnitName");
            var invoice = invoiceManager.Get(lineNo);
            return View(invoice);
        }

        // POST: InvoiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InvoiceDetail invoice)
        {
            try
            {
                invoiceManager.Edit(invoice);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Delete/5
        public ActionResult Delete(int lineNo)
        {
            var invoice = invoiceManager.Get(lineNo);
            return View(invoice);
        }

        // POST: InvoiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(InvoiceDetail invoice)
        {
            try
            {
               await invoiceManager.DeleteAsync(invoice.LineNo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
