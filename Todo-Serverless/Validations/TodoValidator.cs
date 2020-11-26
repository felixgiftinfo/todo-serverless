using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Todo_Serverless.DTOs;
using Todo_Serverless.Models;

namespace Todo_Serverless.Validations
{
    public class TodoValidator : AbstractValidator<TodoDTO>
    {
        public TodoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Required!");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start Date is required!");
            RuleFor(x => x.StartTime).NotEmpty().WithMessage("Start Time Required!");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("End Date is required!");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("End Time Required!");

        }
    }
}
