using FoodAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;
        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        [HttpGet]
        public IActionResult GetAllFoods()
        {
            return Ok(_foodRepository.GetAllFoods());
        }
        [HttpGet("{id}")]
        public IActionResult GetFoodById(int id)
        {
            if (_foodRepository.GetFoodById(id) == null)
            {
                return NotFound();
            }
            return Ok(_foodRepository.GetFoodById(id));
        }
        [HttpPost]
        public IActionResult AddFood([FromBody] Foods food)
        {
            _foodRepository.AddFood(food);
            return Ok("Product added successfull");
        }
        [HttpPut("{id}")]
        public IActionResult UpadteFood([FromBody]Foods food,int id) 
        { 
           _foodRepository.UpdateFood(food,id);
            return Ok("updated successfully");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteFood(int id)
        {
            
            return Ok(_foodRepository.DeleteFood(id));
        }
    }
}
