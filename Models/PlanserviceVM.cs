using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.Models
{
    public class PlanserviceVM
    {
        public Plan plan { get; set; }
        public List<SelectListItem>? services { get; set; }
        public Service? service { get; set; }
        public List<Service>? allservices { get; set; }
        public PlanService? planservice { get; set; }
        public List<Plan>? Landlordplans { get; set; }
        public List<PlanService>? Landlordplanservices { get; set; }
        public List<Plan>? Tenantplans { get; set; }
        public List<PlanService>? Tenantplanservices { get; set; }
        public List<UserPlan>? userPlans { get; set; }
        public string? userId { get; set; }
    }
}