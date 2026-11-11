
using Microsoft.AspNetCore.Http;

namespace Gharshare.Models
{
    public class FormData
    {
        public Int64 propertyID { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
