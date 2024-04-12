using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;
using Project_Quizz_API.Services;
using Project_Quizz_API.Validations;

namespace Project_Quizz_API.Controllers
{
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
        /// Return categorie with given Id
        /// </summary>
        /// <param name="id">Id of the Categorie to be returned</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCategorie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategorie(int id)
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

        /// <summary>
        /// Return all categories in DB
        /// </summary>
        /// <returns>Return all categories as List</returns>
        [HttpGet]
        [Route("GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllCategories()
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

        /// <summary>
        /// Create categorie with given name
        /// </summary>
        /// <param name="name">Name of the categorie to be create</param>
        /// <returns></returns>
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
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteQuizCategorie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteQuizCategorie(int id)
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


    }
}
