using FluentValidation;
using RepositoryService.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Application.Validators
{
    public class RemoveContactInfoCommandValidator : AbstractValidator<RemoveContactInfoCommand>
    {
        public RemoveContactInfoCommandValidator()
        {
            RuleFor(model => model.ContactInfoId).NotNull().GreaterThanOrEqualTo(1);
        }
    }
}
