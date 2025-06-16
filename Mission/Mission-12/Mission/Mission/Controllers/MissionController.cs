using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services;
using Mission.Services.IServices;
using Mission.Services.Services;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController(IMissionService missionService) : ControllerBase
    {
        private readonly IMissionService _missionService = missionService;

        [HttpPost]
        [Route("AddMission")]
        public IActionResult AddMission(MissionRequestViewModel model)
        {
            var response = _missionService.AddMission(model);
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpGet]
        [Route("MissionList")]
        public async Task<IActionResult> GetAllMissionAsync()
        {
            var response = await _missionService.GetAllMissionAsync();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpPost]
        [Route("UpdateMission")]
        public async Task<ActionResult> UpdateMission([FromBody] MissionRequestViewModel requestModel)
        {
            try
            {
                var res = await _missionService.UpdateMission(requestModel);
                return Ok(new ResponseResult() { Data = "Data Updated!", Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to add user." });
            }
        }

        [HttpDelete]
        [Route("DeleteMission/{id}")]
        public ActionResult DeleteMission(int id)
        {
            try
            {
                var res = _missionService.MissionDelete(id);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "Mission Deleted Successfully" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to delete user" });
            }
        }

        [HttpGet]
        [Route("MissionDetailById/{id:int}")]
        public async Task<IActionResult> GetMissionById(int id)
        {
            var response = await _missionService.GetMissionById(id);
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpGet]
        [Route("MissionApplicationList")]
        public async Task<IActionResult> MissionApplicationList()
        {
            var response = await _missionService.GetMissionApplicationList();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpPost]
        [Route("MissionApplicationDelete")]
        public async Task<IActionResult> MissionApplicationDelete(UpdateMissionApplicationModel missionApp)
        {
            try
            {
                var res = await _missionService.MissionApplicationDelete(missionApp);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "Mission Application Deleted Successfully" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to delete user" });
            }
        }

        [HttpPost]
        [Route("MissionApplicationApprove")]
        public async Task<IActionResult> MissionApplicationApprove(UpdateMissionApplicationModel missionApp)
        {
            try
            {
                var ret = await _missionService.MissionApplicationApprove(missionApp);
                return Ok(new ResponseResult() { Data = ret, Message = "Mission Application Approved", Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Message = ex.Message, Result = ResponseStatus.Error });
            }
        }
    }
}
