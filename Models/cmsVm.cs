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
    public class cmsVm
    {
        public StaticContent selectedContent { get; set; }         
        public List<StaticContent> contents { get; set; }
        public string pageTitle { get; set; }
        public StaticContent.Type contentType { get; set; }
        public string articleType { get; set; }
    }
}