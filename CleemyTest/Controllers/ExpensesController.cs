using Api.Controllers.Helper;
using Api.Controllers.Mapper;
using Api.Controllers.Models;
using Domain.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleemyTest.Controllers
{
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IUserExpensesRepository _repository;

        public ExpensesController(IUserExpensesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("users/{userId}/expenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExpenseResultItems>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListExpensesByUser([FromRoute] int userId,
                                                            [FromQuery] string? sortKey,
                                                            [FromQuery] string? sortDirection)
        {
            var user = await _repository.GetUserById(userId).ConfigureAwait(false);
            if (user == Users.Null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user.ToExpenseResultItems()
                              .ApplySort(sortKey, sortDirection));
            }
        }

        [HttpPost]
        [Route("users/{userId}/expenses")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExpenseResultItems))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNewExpenses([FromRoute] int userId,
                                                           [FromBody] ExpenseToCreate expense)
        {
            _ = await _repository.CreateNewExpenses(userId, expense.ToExpensesRecord());
            return StatusCode(201);
        }
    }
}