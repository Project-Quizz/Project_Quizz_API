using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models.DTOs;
using Project_Quizz_API.Services;

namespace Project_Quizz_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuthoriziation]
    public class UserInformationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserInformationController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetUserProgressInformation")]
        public IActionResult GetUserProgressInformation(string userId)
        {
            if(userId.IsNullOrEmpty())
            {
                return BadRequest();
            }

            var userInformation = _context.Quiz_Match_Overview_Users.First(x => x.UserId == userId);

            if (userInformation == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<QuizMatchOverviewUserDto>(userInformation);

            return Ok(result);
        }
    }
}
