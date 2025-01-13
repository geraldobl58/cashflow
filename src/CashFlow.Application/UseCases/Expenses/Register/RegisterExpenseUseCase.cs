using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponsesRegisteredExpenseJson Execute(RequestRegisterExpensesJson request)
        {
            //VALIDATIONS
            Validate(request);

            return new ResponsesRegisteredExpenseJson();
        }

        private void Validate(RequestRegisterExpensesJson request)
        { 
            var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);

            if (titleIsEmpty)
            {
                throw new ArgumentException("Required");
            }

            if (request.Amount <= 0)
            {
                throw new ArgumentException("The amount must be greater than zero");
            }

            var dateIsValid = DateTime.Compare(request.Date, DateTime.UtcNow);

            if (dateIsValid > 0)
            { 
                throw new ArgumentException("The date must be less than or equal to the current date");
            }

            var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);

            if (paymentTypeIsValid == false)
            {
                throw new ArgumentException("Invalid payment type");
            }
        }
    }
}
