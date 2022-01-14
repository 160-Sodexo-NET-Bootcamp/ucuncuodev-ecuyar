using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ContainerGroup
    {
        public int ContainerGroupId { get; set; }
        public long VehicleId { get; set; }
        public List<ContainerDto> Containers { get; set; }

        public ContainerGroup()
        {
            Containers = new List<ContainerDto>();
        }
    }
}
