using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class GetResponseFromServiceWithQueryDto
    {
        [Required]
        public float? latitude { get; set; }
        [Required]
        public float? longitude { get; set; }

        public string categoryQuery { get; set; }
    }
}
