using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register(RequestRegisterExpensesJson request)
        {
            try
            {
                var useCase = new RegisterExpenseUseCase();

                var response = useCase.Execute(request);

                return Created(string.Empty, response);

            } catch (ArgumentException ex)
            {
                var error = new ResponseErrorJson(ex.Message);

                return BadRequest(error);
            }
            catch
            {
                var error = new ResponseErrorJson("Internal server error");

                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
    }
}
