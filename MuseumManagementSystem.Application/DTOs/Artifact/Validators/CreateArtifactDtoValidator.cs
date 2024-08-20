using FluentValidation;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.DTOs.Artifact;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuseumManagementSystem.Application.DTOs.Artifact.Validators
{
    public class CreateArtifactDtoValidator : AbstractValidator<CreateArtifactDto>
    {
        public CreateArtifactDtoValidator(IArtifactsRepository artifacts)
        {
            Include(new IArtifactDtoValidator());
        }
    }
}
