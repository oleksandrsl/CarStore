using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CarStore.Models;
using CarStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Controllers
{
    [Route("api/[controller]")]
    public class StatisticController : Controller
    {
        IStatisticRepository _statisticRepository;
        public StatisticController(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        [HttpGet("sales/{year}")]
        [ProducesResponseType(typeof(List<Sales>), 200)]
        public ActionResult GetSalesStatistic(int year)
        {
            try
            {
                var statistic = _statisticRepository.GetSalesStatistic(year);
                return Ok(statistic);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sales/top/{count}")]
        [ProducesResponseType(typeof(List<PopularCar>), 200)]
        public ActionResult GetPopularCars(int count)
        {
            try
            {
                var popularCars = _statisticRepository.GetPopularCars(count);
                return Ok(popularCars);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
