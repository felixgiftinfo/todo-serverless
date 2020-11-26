using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Todo_Serverless.DTOs;

namespace Todo_Serverless.Validations
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Required!");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username Required!");
            RuleFor(x => x.Email)
                        .NotEmpty().WithMessage("Email Required!")
                        .EmailAddress().WithMessage("Invalid Email!");

        }
    }
 public class LoginUserValidator : AbstractValidator<LoginDTO>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email)
                        .NotEmpty().WithMessage("Email Required!")
                        .EmailAddress().WithMessage("Invalid Email!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password Required!");

        }
    }

}
