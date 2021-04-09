using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskEF.Context;
using TestTaskEF.Models;

namespace TestTaskEF.Services
{
    public class DeptHandler
    {
        private StampContext _context;
        public DeptHandler(StampContext context)
        {
            _context = context;
        }

        #region CRUD
        public async Task<ActionResult> Get(int id)
        {
            var Dept = await _context.Depts.Include(d => d.Lists).ThenInclude(l => l.Stamps).Where(d => d.Id == id).ToListAsync();
            return Dept != null ? new JsonResult(Dept) : new JsonResult("Not found");
        }
        public async Task<ActionResult> Get() => new JsonResult(await _context.Depts.Include(d => d.Lists).ThenInclude(l => l.Stamps).ToListAsync());

        public async Task<ActionResult> Post(DeptModel model)
        {
            _context.Depts.Add(model);
            await _context.SaveChangesAsync();
            return await Get(model.Id);
        }
        public async Task<ActionResult> Put(DeptModel model)
        {
            if (await _context.Depts.FindAsync(model.Id) == null) return new JsonResult("Not found");
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await Get(model.Id);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var Dept = await _context.Depts.FindAsync(id);
            if (Dept == null) return new JsonResult("Not found");
            _context.Depts.Remove(Dept);
            await _context.SaveChangesAsync();
            return new JsonResult("Successful delete");
        }

        #endregion
    }
}
