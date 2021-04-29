using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Pokemons.API.Data.Seeding
{
    public interface ISeedingHelper
    {
        void Seeding();
    }


    public abstract class SeedingHelper : ISeedingHelper
    {
        // Variables //-------------------------------------------------------------------------------------------------------------------------------
        protected readonly ModelBuilder _modelBuilder;
        protected readonly IMapper _mapper;


        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        public SeedingHelper(ModelBuilder modelBuilder, IMapper mapper)
        {
            _modelBuilder = modelBuilder;
            _mapper = mapper;
        }


        // Seeding functions //-------------------------------------------------------------------------------------------------------------------------------
        protected abstract void DoSeeding();
        public void Seeding()
        {
            DoSeeding();
        }
    }
}
