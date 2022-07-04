using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Models
{
    public class Issues
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public string Name { get; set; }
    }
}
