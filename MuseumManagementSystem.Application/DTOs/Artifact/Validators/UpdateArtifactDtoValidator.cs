using FluentValidation;
using MuseumManagementSystem.Application.DTOs.Artifact;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuseumManagementSystem.Application.DTOs.Artifact.Validators
{
    public class UpdateArtifactDtoValidator : AbstractValidator<UpdateArtifactDto>
    {
        public UpdateArtifactDtoValidator()
        {
            Include(new IArtifactDtoValidator());

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
