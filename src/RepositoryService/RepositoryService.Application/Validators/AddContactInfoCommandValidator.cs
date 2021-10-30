using FluentValidation;
using RepositoryService.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Application.Validators
{
    public class AddContactInfoCommandValidator : AbstractValidator<AddContactInfoCommand>
    {
        public AddContactInfoCommandValidator()
        {
            RuleFor(model => model.RecordId).NotNull().GreaterThanOrEqualTo(1);
            RuleFor(model => model.ContactInfo.Type).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(model => model.ContactInfo.Value).NotNull().NotEmpty();
        }
    }
}
