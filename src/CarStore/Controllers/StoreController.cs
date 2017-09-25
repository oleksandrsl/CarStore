using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarStore.Models;
using CarStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Controllers
{
    [Route("api")]
    public class StoreController : Controller
    {
        private IStoreRepository _storeRepository;

        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }


        [HttpPost("order")]
        public ActionResult Order([FromBody]Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _storeRepository.OrderCar(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpGet("makes")]
        [ProducesResponseType(typeof(List<Make>), 200)]
        public async Task<ActionResult> Makes()
        {
            try
            {
                var makes = await _storeRepository.GetMakesAsync();
                return Ok(makes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("makes/{id}/models")]
        [ProducesResponseType(typeof(List<Model>), 200)]
        public async Task<ActionResult> Models(int id)
        {
            try
            {
                var makes = await _storeRepository.GetModelsAsync(id);
                return Ok(makes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("body")]
        [ProducesResponseType(typeof(List<BodyType>), 200)]
        public async Task<ActionResult> GetBodyTypes()
        {
            try
            {
                var bodyTypes = await _storeRepository.GetBodyTypesAsync();
                return Ok(bodyTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
