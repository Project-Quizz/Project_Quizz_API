using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;
using Project_Quizz_API.Services;
using Project_Quizz_API.Validations;

namespace Project_Quizz_API.Controllers
{
    /// <summary>
    /// Categorie workshop controller. It contains the endpoints to get, create and delete categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuthoriziation]
    public class CategorieWorkshopController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategorieWorkshopController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// To get a Categorie by Id
        /// </summary>
        /// <param name="id">Id of the Categorie to be returned</param>
        /// <returns>Returns the QuizCategorieDto</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [HttpGet]
        [Route("GetCategorie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategorie(int id)
        {
            try
            {
                var categorie = _context.Quiz_Categories.FirstOrDefault(x => x.Id == id);

                if (categorie == null)
                {
                    return NotFound($"Categorie with Id {id} not Found");
                }

                QuizCategorieDto quizCategorieDto = new QuizCategorieDto
                {
                    CategorieId = categorie.Id,
                    Name = categorie.Name,
                };

                return Ok(quizCategorieDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// To get all categories as list
        /// </summary>
        /// <returns>Return all categories as List</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categoriesInDb = _context.Quiz_Categories.ToList();

                var allCategories = new List<QuizCategorieDto>();

                foreach (var categorie in categoriesInDb)
                {
                    allCategories.Add(new QuizCategorieDto
                    {
                        CategorieId = categorie.Id,
                        Name = categorie.Name,
                    });
                };

                return Ok(allCategories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create categorie with given name
        /// </summary>
        /// <param name="name">Name of the categorie to be create</param>
        /// <returns>The created categorie id</returns>
        /// <response code="201"></response>
        /// <response code="400"></response>
        [HttpPost]
        [Route("CreateQuizCategorie")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateQuizCategorie(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Categorie name cant be null or empty");
            }

            bool categorieExists = _context.Quiz_Categories.Any(x => x.Name.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower());
            if (categorieExists)
            {
                return BadRequest("A categorie with the same name already exists");
            }

            var newCategorie = new Quiz_Categorie
            {
                Name = name,
                CreateDate = DateTime.Now
            };

            _context.Add(newCategorie);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategorie), new { categorieId = newCategorie.Id });
        }

        /// <summary>
        /// Delete categorie from db
        /// </summary>
        /// <param name="id">Id from categorie to be deleted</param>
        /// <returns>Return StatusCode</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [HttpDelete]
        [Route("DeleteQuizCategorie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteQuizCategorie(int id)
        {
            try
            {
                if (id == 1)
                {
                    return BadRequest($"Id {id} cannot be deleted due to development settings");
                }
                var categorieFromDb = _context.Quiz_Categories.FirstOrDefault(x => x.Id == id);

                if (categorieFromDb == null)
                {
                    return NotFound($"Categorie with Id {id} not Found");
                }

                if (CategogieWorkshopControllerValidators.CategorieIsInUse(id, _context))
                {
                    return BadRequest($"Categorie with Id {id} is in use");
                }

                _context.Remove(categorieFromDb);
                _context.SaveChanges();

                return Ok($"Categorie with Id {id} deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
