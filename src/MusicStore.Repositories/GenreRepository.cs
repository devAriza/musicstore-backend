using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Persistence;

namespace MusicStore.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext context;

        public GenreRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<GenreResponseDto>> GetAsync()
        {

            // Without injection dependency
            //ApplicationDbContext context = new(null);
            //return context.Genres.ToList();

            //With injection dependency

            var items = await context.Set<Genre>()
                .AsNoTracking()
                .ToListAsync();

            // Mapping using LINQ
            var genresResponseDto = items.Select(x => new GenreResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status
            }).ToList();

            return genresResponseDto;

        }

        public async Task<GenreResponseDto?> GetAsync(int id)
        {

            var item = await context.Set<Genre>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item is not null)
            {
                var genreResponseDto = new GenreResponseDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.Status
                };
                return genreResponseDto;
            }
            else
                throw new InvalidOperationException($"Genre with id {id} not found.");

        }

        public async Task<int> AddAsync(GenreRequestDto genreRequestDto)
        {

            var genre = new Genre
            {
                Name = genreRequestDto.Name,
                Status = genreRequestDto.Status
            };

            context.Set<Genre>().Add(genre);
            await context.SaveChangesAsync();
            return genre.Id;

        }

        public async Task UpdateAsync(int id, GenreRequestDto genreRequestDto)
        {

            var item = await context.Set<Genre>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item is not null)
            {

                item.Name = genreRequestDto.Name;
                item.Status = genreRequestDto.Status;
                context.Update(item);
                await context.SaveChangesAsync();

            }
            else
            {
                throw new InvalidOperationException($"Genre with id {id} not found.");
            }

        }

        public async Task DeleteAsync(int id)
        {

            var item = await context.Set<Genre>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);


            if (item is not null)
            {
                context.Set<Genre>().Remove(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Genre with id {id} not found.");
            }
        }
    }
}
