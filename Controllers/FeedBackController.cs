using System;
using System.Threading.Tasks;
using FeedBack.Interfaces;
using FeedBack.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : Controller
    {
        private readonly IUserFeedBackRepository _userFeedBackRepository;

        public FeedBackController(IUserFeedBackRepository userFeedBackRepository)
        {
            _userFeedBackRepository = userFeedBackRepository;
        }
    
        [HttpPost]
        public async Task<IActionResult> AddFeedBack([FromBody]UserFeedback userFeedBack)
        {
            try
            {
                var feedBack = await _userFeedBackRepository.AddFeedBack(userFeedBack);

                if (feedBack == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    feedBack.Id,
                    feedBack.FeedbackType,
                    feedBack.Description,
                    feedBack.FirstName,
                    feedBack.LastName,
                    feedBack.Email
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}