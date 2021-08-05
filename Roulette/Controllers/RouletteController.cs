using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Roulette.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Roulette.Controllers
{
    [ApiController]
    [Route("api/")]
    public class RouletteController : ControllerBase
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
        public async Task<IActionResult> Post()
        {
            try
            {
                int idRoulette = await _service.NewRoulette();
                return Ok(idRoulette);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, null);
            }
        }

        [Route("rouletteopening")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int idRoulette)
        {
            try
            {
                string rouletteStatus = await _service.RouletteOpening(idRoulette);

                return Ok(rouletteStatus);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, null);
            }
        }

        [Route("rouletteopening")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Bet([FromBody] RouletteModel request)
        {
            try
            {
                string rouletteStatus = string.Empty;

                return Ok(rouletteStatus);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, null);
            }
        }
    }
}
