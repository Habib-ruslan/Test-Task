using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskEF.Models
{
    public class DeptModel
    {
        public int Id { get; set; }
        public int RuleStamp { get; set; }
        public int NewStamp { get; set; }
        public int OldStamp { get; set; }
        public int AltNewStamp { get; set; }
        public int AltOldStamp { get; set; }
        public int NextDept { get; set; }
        public int AltNextDept { get; set; }
        public List<ListModel> Lists { get; set; } = new List<ListModel>();
    }
}
