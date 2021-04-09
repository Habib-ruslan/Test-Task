using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskEF.Models
{
    public class ListModel
    {
        public int Id { get; set; }
        public List<StampModel> Stamps { get; set; } = new List<StampModel>();
        public int DeptModelId { get; set; }
    }
}
