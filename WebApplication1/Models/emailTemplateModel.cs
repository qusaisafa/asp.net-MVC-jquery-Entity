using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class emailTemplateModel
    {
        public int TemplateId { get; set; }
        public string TemplateType { get; set; }
        public string TemplateTitle { get; set; }
        public string TemplateName { get; set; }
        public string TemplateBody { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}