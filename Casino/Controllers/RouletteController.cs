using AutoMapper;
using BusinessLayer.Interfaces;
using Casino.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Casino.Controllers
{
    [ApiController]
    [Route("api/")]
    public class RouletteController: ControllerBase
    {
        private readonly IRouletteLogic _service;
        public RouletteController(IRouletteLogic service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Route("newroulette")]
        [HttpPost]
        [DisableRequestSizeLimit]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(RouletteModel))]
        public IActionResult NewRoulette([FromBody] RouletteModel request)
        {
            try
            {
                var objRequest = Mapper.Map<Roulette>(request);
                int idRoulette = _service.NewRoulette(objRequest);
                    
                return Ok(idRoulette);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, null);
            }

        }
    }
}
