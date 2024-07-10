using App.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Managers
{
    public class InvoiceManager
    {
        private readonly AppyInnovateContext _context;

        public InvoiceManager(AppyInnovateContext context)
        {
            _context = context;
        }
        public IQueryable<InvoiceDetail> GetAll()
        {
            try
            {
                return _context.InvoiceDetails.Include(x => x.UnitNoNavigation);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public InvoiceDetail Get(int lineNo)
        {
            try
            {
                return _context.InvoiceDetails.Include(x => x.UnitNoNavigation).FirstOrDefault(x => x.LineNo == lineNo);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async void Add(InvoiceDetail invoice)
        {
            try
            {
                invoice.Total = invoice.Price * invoice.Quantity;
                await _context.InvoiceDetails.AddAsync(invoice);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.InnerException?.Message);
            }
        }

        public async void Edit(InvoiceDetail invoice)
        {
            try
            {
                var existingInvoice = _context.InvoiceDetails.Include(x => x.UnitNoNavigation).FirstOrDefault(x => x.LineNo == invoice.LineNo);
                existingInvoice.LineNo = invoice.LineNo;
                existingInvoice.ProductName = invoice.ProductName;
                existingInvoice.ExpiryDate = invoice.ExpiryDate;
                existingInvoice.Price = invoice.Price;
                existingInvoice.UnitNo = invoice.UnitNo;
                existingInvoice.Quantity = invoice.Quantity;
                existingInvoice.Total = invoice.Price * invoice.Quantity;
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.InnerException?.Message);
            }
        }
        public async Task DeleteAsync(int lineNo)
        {
            var existingInvoice = _context.InvoiceDetails.Include(x => x.UnitNoNavigation).FirstOrDefault(x => x.LineNo == lineNo);
            _context.InvoiceDetails.Remove(existingInvoice);
            await _context.SaveChangesAsync();

        }
    }
}
