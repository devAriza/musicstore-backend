using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Entities;

namespace MusicStore.Repositories
{
    public class GenreRepository
    {

        private readonly List<Genre> genresList;

        public GenreRepository()
        {
            genresList = new List<Genre>();
            genresList.Add(new Genre { Id = 1, Name = "Salsa" });
            genresList.Add(new Genre { Id = 2, Name = "Cumbia" });
            genresList.Add(new Genre { Id = 3, Name = "Balada" });

        }

        public List<Genre> Get()
        {
            return genresList;
        }

        public Genre? Get(int id)
        {
            return genresList.FirstOrDefault(g => g.Id == id);
        }

        public void Add(Genre genre)
        {
            var lastItem = genresList.MaxBy(g => g.Id);
            genre.Id = lastItem is null ? 1 : lastItem.Id + 1;
            genresList.Add(genre);
        }

        public void Update(int id, Genre genre)
        {
            var item = Get(id);
            if (item is not null)
            {
                item.Name = genre.Name;
                item.Status = genre.Status;
            }
        }

        public void Delete(int id)
        {
            var item = Get(id);
            if (item is not null)
            {
                genresList.Remove(item);
            }

        }
    }
}
