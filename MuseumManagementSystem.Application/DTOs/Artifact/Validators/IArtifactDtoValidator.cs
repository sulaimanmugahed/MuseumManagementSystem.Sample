using FluentValidation;
using MuseumManagementSystem.Application.DTOs.Artifact;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuseumManagementSystem.Application.DTOs.Artifact.Validators
{
    public class IArtifactDtoValidator : AbstractValidator<IArtifactDto>
    {
        public IArtifactDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(2).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

        }
    }
}
