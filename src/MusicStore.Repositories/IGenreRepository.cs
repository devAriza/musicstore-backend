using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;

namespace MusicStore.Repositories
{
    public interface IGenreRepository
    {
        Task<List<GenreResponseDto>> GetAsync();
        Task<GenreResponseDto?> GetAsync(int id);
        Task<int> AddAsync(GenreRequestDto genre);
        Task UpdateAsync(int id, GenreRequestDto genre);
        Task DeleteAsync(int id);
    }
}