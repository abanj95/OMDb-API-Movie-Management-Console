using System;
using Microsoft.EntityFrameworkCore;
using OmdbApiService.Models;

namespace OmdbApiService.Data
{

	public class MovieDbContext : DbContext
	{
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        // Add DbSets for your entities here
        public DbSet<Movie> Movies { get; set; }
    }
	
}

