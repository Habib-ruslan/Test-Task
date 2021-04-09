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
    public class ListHandler
    {
        private StampContext _context;
        public ListHandler(StampContext context)
        {
            _context = context;
        }
        #region Start
        public async Task<ActionResult> ToGoDept(int DeptId, ListModel StampList = null)
        {
            if (StampList == null) StampList = await _context.Lists.FirstOrDefaultAsync();
            var Dept = await _context.Depts.FindAsync(DeptId);
            if (Dept == null) return new JsonResult($"Dept №{DeptId} is not found");

            if (isInfinityCycle(Dept, StampList)) return new JsonResult("Infinity cycle started");   //Предупреждение о бесконечном цикле
            if (Dept.RuleStamp == 0 || FindStamp(StampList, Dept.RuleStamp) != null)
            {
                await Rule(StampList, Dept, false);
            }
            else
            {
                await Rule(StampList, Dept, true);
            }
            return null;
        }
        private StampModel FindStamp(ListModel StampList, int id)
        {
            foreach (var k in StampList.Stamps)
            {
                if (k.Id == id) return k;
                else continue;
            }
            return null;
        }

        private bool isInfinityCycle(DeptModel Dept, ListModel StampList)
        {
            if (Dept.Lists.Count > 0)
            {
                foreach(var list in Dept.Lists)
                {
                    if (list.Stamps == StampList.Stamps) return true;
                }
            }
            return false;
        }

        private async Task Rule(ListModel StampList, DeptModel Dept, bool isAlt)
        {
            int OldStamp, NewStamp, NextDept;
            if (isAlt)
            {
                OldStamp = Dept.AltOldStamp;
                NewStamp = Dept.AltNewStamp;
                NextDept = Dept.AltNextDept;
            }
            else
            {
                OldStamp = Dept.OldStamp;
                NewStamp = Dept.NewStamp;
                NextDept = Dept.NextDept;
            }
            var _OldStamp = FindStamp(StampList, OldStamp);

            if (FindStamp(StampList, NewStamp) == null)
            {
                await Put(StampList.Id, NewStamp);
            }
            else if (_OldStamp != null)
            {
                StampList.Stamps.Remove(_OldStamp);
            }
            _context.Entry(StampList).State = EntityState.Modified;
            var NewStampList = new ListModel { Stamps = StampList.Stamps, DeptModelId = Dept.Id };
            _context.Lists.Add(NewStampList);
            Dept.Lists.Add(NewStampList);
            await _context.SaveChangesAsync();
            if (NextDept == 0) return;      //Завершение рекурсии
            await ToGoDept(NextDept, StampList);
        }
        #endregion
        #region Reset
        public async Task<ActionResult> Reset()
        {
            await RemoveLists();
            await RemoveDepts();
            await RecreateFirstList();
            return new JsonResult("Successful reset");
        }
        private async Task RecreateFirstList()
        {
            await Post(new ListModel { Stamps = new List<StampModel>(), DeptModelId = 1 });
        }

        private async Task RemoveLists()
        {
            var Lists = await _context.Lists.ToListAsync();
            if (Lists == null) return;
            for (int i= 0;i< Lists.Count; i++)
            {
                _context.Lists.Remove(Lists[i]);
                await _context.SaveChangesAsync();
            }
        }
        
        private async Task RemoveDepts()
        {
            var Depts = await _context.Depts.ToListAsync();
            for (int i = 0; i < Depts.Count; i++)
            {
                Depts[i].Lists = new List<ListModel>();
                _context.Entry(Depts[i]).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        #endregion
        #region CRUD
        public async Task<ActionResult> Get() => new JsonResult( await _context.Lists.Include(l => l.Stamps).ToListAsync());
        public async Task<ActionResult> Get(int id)
        {
            var StampList = await _context.Lists.Include(l => l.Stamps).Where(l => l.Id == id).ToListAsync();
            if (StampList == null)
            {
                if (id == 1) return new JsonResult(await _context.Lists.Include(l => l.Stamps).FirstOrDefaultAsync());
                return new JsonResult("Not Found");
            }
            return new JsonResult(StampList);
        }

        public async Task<ActionResult> Post(ListModel model)
        {
            _context.Lists.Add(model);
            var Dept = await _context.Depts.FindAsync(model.DeptModelId);
            if (Dept != null)
                Dept.Lists.Add(model);
            await _context.SaveChangesAsync();
            return await Get(model.Id);
        }
        public async Task<ActionResult> Put(ListModel model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new JsonResult(model);
        }
        public async Task<ActionResult> Put(int id, int StampId)
        {
            var StampList = await _context.Lists.FindAsync(id);
            var Stamp = await _context.Stamps.FindAsync(StampId);
            if (Stamp == null || StampList == null) return new JsonResult("Not Found");
            _context.Attach(Stamp);
            StampList.Stamps.Add(Stamp);
            await _context.SaveChangesAsync();
            return await Get(StampList.Id);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var StampList = await _context.Lists.FindAsync(id);
            if (StampList == null) return new JsonResult("List is not found");
            _context.Lists.Remove(StampList);
            await _context.SaveChangesAsync();
            return new JsonResult("Successful delete");
        }
        #endregion

    }
}
