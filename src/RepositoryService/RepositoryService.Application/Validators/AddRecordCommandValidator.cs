using FluentValidation;
using RepositoryService.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Application.Validators
{
    public class AddRecordCommandValidator : AbstractValidator<AddRecordCommand>
    {
        public AddRecordCommandValidator()
        {
            RuleFor(model => model.Name).NotNull().NotEmpty();
            RuleFor(model => model.Surname).NotNull().NotEmpty();
        }
    }
}
