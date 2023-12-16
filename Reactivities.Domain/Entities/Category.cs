using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactivities.Domain.Entities
{
    public class Category
    {
        public string Name { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
