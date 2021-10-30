using FluentValidation;
using RepositoryService.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Application.Validators
{
    public class RemoveRecordCommandValidator : AbstractValidator<RemoveRecordCommand>
    {
        public RemoveRecordCommandValidator()
        {
            RuleFor(model => model.RecordId).NotNull().GreaterThanOrEqualTo(1);
        }
    }
}
