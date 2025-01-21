using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponsesRegisteredExpenseJson Execute(RequestRegisterExpensesJson request)
        {
            Validate(request);


            var entity = new Expense
            {
                Amount = request.Amount,
                Date = request.Date,
                Description = request.Description,
                Title = request.Title,
                PaymentType = (Domain.Enums.PaymentType)request.PaymentType
            };

            return new ResponsesRegisteredExpenseJson();
        }

        private void Validate(RequestRegisterExpensesJson request)
        {
            var validator = new RegisterExpenseValidator();

            var result  = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorsMessage = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorsMessage);
            }
        }
    }
}
