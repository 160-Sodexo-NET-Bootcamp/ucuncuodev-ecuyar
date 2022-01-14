using AutoMapper;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashManagementApi_Data;
using TrashManagementApi_Data.UoW;

namespace EnesCanUyar_Odev3_TrashManagement.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        // Custom messages
        private static readonly string NotRightObjectMessage = "Not right object.";
        private static readonly string ProcessSuccessfulMessage = "Process is successful.";
        private static readonly string ContainerIsNotFoundMessage = "Container is not found.";
        private static readonly string ProcessErrorMessage = "Process is not successfull.";

        //I am using dto to not to expose the data models
        public ContainerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContainers()
        {
            var allContainers = await unitOfWork.Container.GetAll();

            //convert model to dto
            List<ContainerDto> containers = mapper.Map<IEnumerable<Container_DataModel>, List<ContainerDto>>(allContainers);

            // i used vehicleId = -1 to unbounded containers
            return Ok(containers);
        }

        [HttpPost]
        public async Task<IActionResult> AddContainer([FromBody] ContainerDto containerDto)
        {
            // i will show badrequest when container is null.
            if (containerDto != null)
            {
                //convert dto to model
                var containerDataModel = mapper.Map<Container_DataModel>(containerDto);

                await unitOfWork.Container.Add(containerDataModel);
                unitOfWork.Complete();

                return Ok(ProcessSuccessfulMessage);
            }
            return BadRequest(NotRightObjectMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContainer([FromBody] ContainerDto containerDto)
        {
            //convert dto to real data model
            var containerDataModel = await unitOfWork.Container.GetById(containerDto.Id);

            if (containerDto == null)
            {
                return BadRequest(NotRightObjectMessage);
            }

            if (containerDataModel == null)
            {
                return NotFound(ContainerIsNotFoundMessage);
            }

            //even if user send vehicleId, I will not update the id
            //I didnt use auto mapper because dto has vehicleid property so didnt want to update it. manually mapped
            containerDataModel.ContainerName = containerDto.ContainerName;
            containerDataModel.Latitude = containerDto.Latitude;
            containerDataModel.Longitude = containerDto.Longitude;

            //update method returns true or false based on if update is successfull
            var isChanged = unitOfWork.Container.Update(containerDataModel);

            if (isChanged == true)
            {
                unitOfWork.Complete();
                return Ok(ProcessSuccessfulMessage);
            }

            return NotFound(ProcessErrorMessage);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContainer([FromQuery] long id)
        {
            var containerDM = await unitOfWork.Container.GetById(id);

            if (containerDM == null)
            {
                return NotFound(ContainerIsNotFoundMessage);
            }

            unitOfWork.Container.Delete(containerDM.Id);
            unitOfWork.Complete();

            return Ok(ProcessSuccessfulMessage);
        }

        [HttpGet]
        [Route("byVehicle")]
        public async Task<IActionResult> GetAllByVehicleId([FromQuery] long id)
        {

            var containers = await unitOfWork.Container.GetAll();
            var chosenContainers = containers.Where(x => x.VehicleId == id);

            if (chosenContainers == null)
            {
                return NoContent();
            }

            return Ok(chosenContainers);
        }
    }
}
