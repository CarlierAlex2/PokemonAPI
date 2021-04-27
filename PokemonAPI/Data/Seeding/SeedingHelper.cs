using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace PokemonAPI.Data.Seeding
{
    public abstract class SeedingHelper
    {
        protected readonly ModelBuilder _modelBuilder;
        protected readonly IMapper _mapper;

        public SeedingHelper(ModelBuilder modelBuilder, IMapper mapper)
        {
            _modelBuilder = modelBuilder;
            _mapper = mapper;
        }

        protected abstract void DoSeeding();
        public void Seeding()
        {
            DoSeeding();
        }
    }
}
