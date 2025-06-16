using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models.CommonModels;
using Mission.Service.IServices;
using Mission.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Mission.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController(ICommonService commonService, IWebHostEnvironment hostingEnvironment) : ControllerBase
    {
        private readonly ICommonService _commonService = commonService;
        private readonly IWebHostEnvironment _hostingEnvironment = hostingEnvironment;
        ResponseResult result = new ResponseResult();

        [HttpGet]
        [Route("CountryList")]
        [Authorize]
        public ResponseResult CountryList()
        {
            try
            {
                result.Data = _commonService.CountryList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("CityList/{countryId}")]
        [Authorize]
        public ResponseResult CityList(int countryId)
        {
            try
            {
                result.Data = _commonService.CityList(countryId);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("MissionCountryList")]
        public ResponseResult MissionCountryList()
        {
            try
            {
                result.Data = _commonService.MissionCountryList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("MissionCityList")]
        public ResponseResult MissionCityList()
        {
            try
            {
                result.Data = _commonService.MissionCityList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("MissionThemeList")]
        public ResponseResult MissionThemeList()
        {
            try
            {
                result.Data = _commonService.MissionThemeList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("MissionSkillList")]
        public ResponseResult MissionSkillList()
        {
            try
            {
                result.Data = _commonService.MissionSkillList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("MissionTitleList")]
        public ResponseResult MissionTitleList()
        {
            try
            {
                result.Data = _commonService.MissionTitleList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<ResponseResult> UploadImage([FromForm] List<IFormFile> files)
        {
            List<string> fileList = new List<string>();
            if (files != null && files.Count > 0)
            {
                foreach (IFormFile file in files)
                {
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "UplodedImages", "MissionImages");

                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    var name = Path.GetFileNameWithoutExtension(file.FileName);
                    var ext = Path.GetExtension(file.FileName);

                    var unique = name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;

                    var filePath = Path.Combine(uploadFolder, unique);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var fPath = Path.Combine("UplodedImages", "MissionImages", unique);
                    fileList.Add(fPath);
                }
            }
            return new ResponseResult() { Data = fileList, Message = "Success", Result = ResponseStatus.Success };
        }

    }
}
