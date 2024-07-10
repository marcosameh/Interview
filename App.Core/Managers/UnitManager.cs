using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Managers
{
    public class UnitManager
    {
        private readonly AppyInnovateContext _context;

        public UnitManager(AppyInnovateContext context)
        {
            _context = context;
        }
        public IQueryable<Unit> GetAll()
        {
            try
            {
                return _context.Units.AsNoTracking();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
