using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validator.Tests.Expenses.Register
{
    public class RegisterExpensevalidatorTests
    {
        [Fact]
        public void Success() 
        {
            // ARRANGE
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpensesJsonBuilder.Build();

            // ACT
            var result  = validator.Validate(request);

            // ASSERT
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void Error_Title_Empty(string title)
        {
            // ARRANGE
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpensesJsonBuilder.Build();
            request.Title = title;

            // ACT
            var result = validator.Validate(request);

            // ASSERT
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.TITLE_REQUIRED));
        }

        [Fact]
        public void Error_Date_Future()
        {
            // ARRANGE
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpensesJsonBuilder.Build();
            request.Date = DateTime.UtcNow.AddDays(1);

            // ACT
            var result = validator.Validate(request);

            // ASSERT
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSES_CANNOT_FOR_THE_FUTURE));
        }

        [Fact]
        public void Error_Payment_Type_Invalid()
        {
            // ARRANGE
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpensesJsonBuilder.Build();
            request.PaymentType = (PaymentType)700;

            // ACT
            var result = validator.Validate(request);

            // ASSERT
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.PAYMENT_TYPE_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-7)]
        public void Error_Amount_Invalid(decimal amount)
        {
            // ARRANGE
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpensesJsonBuilder.Build();
            request.Amount = amount;

            // ACT
            var result = validator.Validate(request);

            // ASSERT
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
        }
    }
}
