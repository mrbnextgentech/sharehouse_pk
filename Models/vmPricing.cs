using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.Models
{
    public class vmPricing
    {
        public List<Plan> Plans { get; set; }
        public List<Service> Services { get; set; }
        public List<Service> Match { get; set; }
        public List<Service> Manage { get; set; }
        public List<Service> Maintain { get; set; }
        public List<Testimonial> Testimonials { get; set; }
    }
}
