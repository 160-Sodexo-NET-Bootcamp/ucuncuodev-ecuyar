using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashManagementApi_Data.UoW;

namespace EnesCanUyar_Odev3_TrashManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrouppingController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public GrouppingController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //[HttpGet]
        //public async Task<IActionResult> ContainerGroups(long vehicleId, int groupCount)
        //{
        //    var containers = await unitOfWork.Container.GetAll();
        //    var vehicleContainers = containers.Where(x => x.VehicleId == vehicleId);

        //    var dataLength = vehicleContainers.Count();

        //    //if vehicle has no container send no content message
        //    if (dataLength < 1)
        //    {
        //        return NoContent();
        //    }

        //    // this decimal array will show that containers coordinates data[0] => container[12,563684][24,784521] etc.
        //    List<(decimal latitude, decimal longitude)> data = new();

        //    for (int i = 0; i < dataLength; i++)
        //    {
        //        data.Add((
        //            vehicleContainers.ElementAtOrDefault(i).Latitude,
        //            vehicleContainers.ElementAtOrDefault(i).Longitude));
        //    };

        //    //get coordinates ready for the method
        //    var coordinate = data.Select(x => new decimal[] { x.latitude, x.longitude }).ToArray();

        //    //send values to method to find means
        //    var result = KMeans(coordinate, groupCount);

        //    return Ok();
        //}


        //private static IList<decimal>[] KMeans(decimal[][] data, int groupCount)
        //{
        //    //this is the loop count that is used for "for loop" to find mean point
        //    var limit = 1000;

        //    //this is the final parameter to stop the algorithm
        //    var isUpdated = true;

            

        //    //send every coordinate to seperate groups
        //    var random = new Random();

        //    var randomGroups = Enumerable
        //        .Range(0, data.Length)
        //        .Select(i => (random.Next(0, groupCount), data[i]))
        //        .ToList();

        //    while (limit > 0)
        //    {
        //        var meanPoints = Enumerable.Range(0, groupCount).AsParallel().Select(groupNumber => (groupNumber, Enumerable.Range(0, data.Length)
        //        .Select(e => randomGroups.Where(s => s.);
        //    }


        //    return null;
        //}
    }
}
