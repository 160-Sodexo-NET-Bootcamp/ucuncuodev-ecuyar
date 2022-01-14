using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class VehicleDto
    {
        public long Id { get; set; }

        [Required]
        public string VehicleName { get; set; }

        [Required]
        [MinLength(7)]
        [MaxLength(7)]
        public string VehiclePlate { get; set; }
    }
}
