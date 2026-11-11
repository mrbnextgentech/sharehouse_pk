using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class PropertyFilter
    {
        public List<string> Type { get; set; } = new List<string>();
        public List<string> Feature { get; set; } = new List<string>();
        public List<string> Rent { get; set; } = new List<string>();
        public List<string> Bed { get; set; } = new List<string>();
        // Add a property called "selection" to match the JSON structure
        public PropertyFilter selection { get; set; }
    }
}