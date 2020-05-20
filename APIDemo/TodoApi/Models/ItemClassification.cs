using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class ItemClassification
    {
        public long Id { get; set; }
        public string Classification { get; set; }
        public ICollection<TodoItemDTO> TodoItems { get; set; }
    }
}
