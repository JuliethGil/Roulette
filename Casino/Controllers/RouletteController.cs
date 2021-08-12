using AutoMapper;
using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using Casino.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;

namespace Casino.Controllers
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
        [Produces(MediaTypeNames.Application.Json, Type = typeof(RouletteCreationModel))]
        public IActionResult NewRoulette([FromBody] RouletteCreationModel request)
        {
            try
            {
                var objRequest = Mapper.Map<Roulette>(request);
                int idRoulette = _service.NewRoulette(objRequest);

                return Ok(idRoulette);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, null);
            }

        }

        [Route("rouletteopening")]
        [HttpPut]
        [DisableRequestSizeLimit]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(RouletteUpdateModel))]
        public IActionResult RouletteOpening([FromBody] RouletteUpdateModel request)
        {
            try
            {
                var objRequest = Mapper.Map<Roulette>(request);
                string state = _service.RouletteOpening(objRequest);

                return Ok(state);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, null);
            }
        }

        [Route("bet")]
        [HttpPost]
        [DisableRequestSizeLimit]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(BetModel))]
        public IActionResult Bet([FromHeader, Required] int idUser, [FromBody] BetModel request)
        {
            try
            {
                var objRequest = Mapper.Map<Bet>(request);
                string state = _service.Bet(objRequest);

                return Ok(state);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, null);
            }
        }

        [Route("closebets")]
        [HttpPut]
        [DisableRequestSizeLimit]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(RouletteUpdateModel))]
        public IActionResult RouletteClose([FromBody] RouletteUpdateModel request)
        {
            try
            {
                var objRequest = Mapper.Map<Roulette>(request);
                ResultOfBetDto resultOfBet = _service.RouletteClose(objRequest);

                return Ok(resultOfBet);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
            }
        }

        [Route("roullettes")]
        [HttpGet]
        [DisableRequestSizeLimit]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public IActionResult GetAllRoulettes()
        {
            try
            {
                List<RoulettesDto> roulettes = _service.GetAllRoulettes();

                return Ok(roulettes);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
            }
        }
    }
}
