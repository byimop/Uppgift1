namespace MovieService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using MovieService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MovieService.Models.MovieServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MovieService.Models.MovieServiceContext context)
        {
            context.Directors.AddOrUpdate(x => x.Id,
            new Director() { Id = 1, Name = "Tim Burton" },
            new Director() { Id = 2, Name = "Christopher Nolan" },
            new Director() { Id = 3, Name = "Michael Bay" }
            );
            context.Movies.AddOrUpdate(x => x.Id,
            new Movie()
            {
                Id = 1,
                Title = "Corpse Bride",
                Year = 2005,
                DirectorId = 1,
                Price = 18M,
                Genre = "Animation"
            },
            new Movie()
            {
                Id = 2,
                Title = "Batman Begins",
                Year = 1817,
                DirectorId = 2,
                Price = 100M,
                Genre = "Action"
            },
            new Movie()
            {
                Id = 3,
                Title = "Gravity",
                Year = 2014,
                DirectorId = 2,
                Price = 200M,
                Genre = "Space"
            },
            new Movie()
            {
                Id = 4,
                Title = "Transformers 3",
                Year = 2014,
                DirectorId = 3,
                Price = 200M,
                Genre = "Robots"
            }
            );
        }
    }
}
