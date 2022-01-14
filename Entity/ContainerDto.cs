using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ContainerDto
    {
        public long Id { get; set; }

        public string ContainerName { get; set; }

        [Column(TypeName = "decimal(10, 6)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(10, 6)")]
        public decimal Longitude { get; set; }

        // I didnt use this prop when updating model so I didnt use auto mapper while updating
        public long VehicleId { get; set; }
    }
}
