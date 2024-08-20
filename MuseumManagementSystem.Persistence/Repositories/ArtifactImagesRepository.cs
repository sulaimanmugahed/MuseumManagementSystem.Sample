using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Application.Contracts.Persistence;

using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Persistence.Repositories.Base;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Persistence.Repositories
{
    public class ArtifactImagesRepository : GenericRepository<ArtifactImage>, IArtifactImagesRepository
    {
        private readonly ApplicationDbContext _context;


        public ArtifactImagesRepository(ApplicationDbContext context)
            : base(context)
        {
          
            _context = context;
        }

    

    }
}
