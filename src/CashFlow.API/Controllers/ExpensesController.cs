using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;

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

            } catch (ErrorOnValidationException ex)
            {
                var error = new ResponseErrorJson(ex.Errors);

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
