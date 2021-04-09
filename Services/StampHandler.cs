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
    public class StampHandler
    {
        private StampContext _context;
        public StampHandler(StampContext context)
        {
            _context = context;
        }
        #region CRUD
        public async Task<ActionResult> Get() => new JsonResult( await _context.Stamps.ToListAsync());
        public async Task<ActionResult> Get(int id)
        {
            var Stamp = await _context.Stamps.FindAsync(id);
            return Stamp != null? new JsonResult(Stamp) : new JsonResult("Not found");
        }

        public async Task<ActionResult> Post(StampModel model)
        {
            _context.Stamps.Add(model);
            await _context.SaveChangesAsync();
            return await Get(model.Id);
        }

        public async Task<ActionResult> Put(int id, StampModel model)
        {
            if (id != model.Id) return new JsonResult("Not found");
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await Get(model.Id);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var Stamp = await _context.Stamps.FindAsync(id);
            if (Stamp == null) return new JsonResult("Not found");
            _context.Stamps.Remove(Stamp);
            await _context.SaveChangesAsync();
            return new JsonResult("Successful delete");
        }
        #endregion

    }
}
