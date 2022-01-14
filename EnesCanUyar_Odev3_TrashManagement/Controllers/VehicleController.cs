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
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        // Custom messages
        private static readonly string NotRightObjectMessage = "Not right object.";
        private static readonly string ProcessSuccessfulMessage = "Process is successful.";
        private static readonly string VehicleIsNotFoundMessage = "Vehicle is not found.";
        private static readonly string ProcessErrorMessage = "Process is not successfull.";

        //I am using dto to not to expose the data models
        public VehicleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var allVehicles = await unitOfWork.Vehicle.GetAll();

            //convert model to dto
            List<VehicleDto> vehicles = mapper.Map<IEnumerable<Vehicle_DataModel>, List<VehicleDto>>(allVehicles);

            //if we have no vehicle show nocontent message otherwise show them
            if (vehicles.Count == 0)
            {
                return NoContent();
            }

            return Ok(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleDto vehicleDto)
        {
            // i will show badrequest when vehicle is null.
            if (vehicleDto != null)
            {
                var vehicle_dm = mapper.Map<Vehicle_DataModel>(vehicleDto);

                await unitOfWork.Vehicle.Add(vehicle_dm);
                unitOfWork.Complete();

                return Ok(ProcessSuccessfulMessage);
            }
            return BadRequest(NotRightObjectMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVehicle([FromBody] VehicleDto vehicleDto)
        {
            //convert dto to real data model
            var vehicleDataModel = await unitOfWork.Vehicle.GetById(vehicleDto.Id);

            if (vehicleDto == null)
            {
                return BadRequest(NotRightObjectMessage);
            }

            if (vehicleDataModel == null)
            {
                return NotFound(VehicleIsNotFoundMessage);
            }

            vehicleDataModel.VehicleName = vehicleDto.VehicleName;
            vehicleDataModel.VehiclePlate = vehicleDto.VehiclePlate;

            //update method returns tru or false based on if update is successfull
            var isChanged = unitOfWork.Vehicle.Update(vehicleDataModel);

            if (isChanged == true)
            {
                unitOfWork.Complete();
                return Ok(ProcessSuccessfulMessage);
            }

            return NotFound(ProcessErrorMessage);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVehicle([FromQuery] long id)
        {
            var vehicleDM = await unitOfWork.Vehicle.GetById(id);

            if (vehicleDM == null)
            {
                return NotFound(VehicleIsNotFoundMessage);
            }

            unitOfWork.Vehicle.DeleteWithContainerInfo(vehicleDM.Id);
            unitOfWork.Complete();

            return Ok(ProcessSuccessfulMessage);
        }


        //this method can be seperated to Vehicle/Container Repository but I created in the controller
        [HttpGet]
        [Route("containerGroup")]
        public async Task<IActionResult> GetAllByVehicleId([FromQuery] long vehicleId, int groupCount)
        {
            List<ContainerGroup> containerGroups = new();

            var containers = await unitOfWork.Container.GetAll();
            var chosenContainers = containers.Where(x => x.VehicleId == vehicleId);
            int chosenContainersCount = chosenContainers.Count();

            //this shows how many containers does a group have
            int containerGroupItemCount = chosenContainersCount / groupCount;

            //if can not be divided equally create a one more group
            if (chosenContainersCount % groupCount != 0)
            {
                containerGroupItemCount += 1;
            }

            //keep track of where should start to get elements
            int pointer = 0;


            for (int i = 0; i < groupCount; i++)
            {
                ContainerGroup c = new()
                {
                    ContainerGroupId = i + 1,
                    VehicleId = vehicleId
                };

                for (int j = 0; j < containerGroupItemCount; j++)
                {
                    Container_DataModel nextContainerDataModel = chosenContainers.Skip(pointer).Take(1).FirstOrDefault();

                    //when group size is not equal, nextContainerDataModel will be null so we will exit from loop
                    if (nextContainerDataModel == null)
                    {
                        break;
                    }

                    //convert model to dto
                    var nextContainerDto = mapper.Map<ContainerDto>(nextContainerDataModel);

                    pointer++;

                    //send container into container group
                    c.Containers.Add(nextContainerDto);
                }

                //send container group into list of container groups
                containerGroups.Add(c);
            }

            return Ok(containerGroups);
        }

        [HttpGet]
        [Route("getVehicleById")]
        public async Task<IActionResult> GetById([FromQuery] long vehicleId)
        {
            var vehicle = await unitOfWork.Vehicle.GetById(vehicleId);

            if (vehicleId == 0)
            {
                return BadRequest(ProcessErrorMessage);
            }

            if (vehicle == null)
            {
                return NoContent();
            }

            VehicleDto vehicleDto = mapper.Map<VehicleDto>(vehicle);

            return Ok(vehicleDto);
        }
    }
}
