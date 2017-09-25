using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CarStore.Helpers;
using CarStore.Models;
using CarStore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarSCarStoreolution.Controllers
{
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Car>), 200)]
        public async Task<ActionResult> Cars([FromQuery]CarFilter carFilterParams)
        {
            try
            {
                var cars = await _carRepository.GetCarsAsync(carFilterParams);
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Car), 200)]
        public async Task<ActionResult> Car(int id)
        {
            try
            {
                var car = await _carRepository.GetCarAsync(id);
                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Car), 200)]
        public ActionResult AddNewCar([FromBody, Required]Car car, [FromQuery]int key)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!ManagementHelper.IsAdministrator(key))
                return Forbid();

            try
            {
                _carRepository.AddCar(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult EditCar(int id, [FromBody, Required]Car car, [FromQuery]int key)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!ManagementHelper.IsAdministrator(key))
                return Forbid();

            try
            {
                _carRepository.UpdateCar(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar(int id, [FromQuery]int key)
        {
            if (!ManagementHelper.IsAdministrator(key))
                return Forbid();

            try
            {
                _carRepository.DeleteCar(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
