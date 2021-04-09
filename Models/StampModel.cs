using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestTaskEF.Models
{
    public class StampModel
    {
        public int Id { get; set; }
        [JsonIgnore]
        public List<ListModel> Lists { get; set; } = new List<ListModel>();
    }
}
