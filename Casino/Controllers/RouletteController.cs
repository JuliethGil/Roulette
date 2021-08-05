using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
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
        public async Task<IActionResult> NewRoulette()
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
    }
}
