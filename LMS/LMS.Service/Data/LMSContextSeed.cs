using LMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Data
{
    public class LMSContextSeed
    {
        public static async Task SeedAsync(LMSContext context,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {

                if (!await context.Author.AnyAsync())
                {
                    await context.Author.AddRangeAsync(
                        GetPreconfiguredAuthor());

                    await context.SaveChangesAsync();
                }
                if (!await context.Publisher.AnyAsync())
                {
                    await context.Publisher.AddRangeAsync(
                        GetPreconfiguredPublisher());

                    await context.SaveChangesAsync();
                }
                if (!await context.MediaType.AnyAsync())
                {
                    await context.MediaType.AddRangeAsync(
                        GetPreconfiguredMediaType());

                    await context.SaveChangesAsync();
                }

                if (!await context.Media.AnyAsync())
                {
                    await context.Media.AddRangeAsync(
                        GetPreconfiguredMedia());

                    await context.SaveChangesAsync();
                }

                
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<LMSContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(context, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        static IEnumerable<Author> GetPreconfiguredAuthor()
        {
            return new List<Author>()
            {
                new Author{Id = "Author 1", FirstName = "Resham", LastName= "Kafle"},
                new Author{Id = "Author 2", FirstName = "David", LastName= "Tiger"}
            };
        }

        static IEnumerable<Publisher> GetPreconfiguredPublisher()
        {
            return new List<Publisher>()
            {
                new Publisher{Id = "Publisher 1", Name ="Publisher A"},
                new Publisher{Id = "Publisher 2", Name ="Publisher B"}
            };
        }
        static IEnumerable<MediaType> GetPreconfiguredMediaType()
        {
            return new List<MediaType>()
            {
                new MediaType{Id = "Type 1", Name ="Book"},
                new MediaType{Id = "Type 2", Name ="CD-ROM"}
            };
        }

        static IEnumerable<Media> GetPreconfiguredMedia()
        {
            return new List<Media>()
            {
                new Media{Id = Guid.NewGuid().ToString(), Title ="Book 1", AuthorId = "Author 1", Cost = 23.0M, ISBN = "ISBN111111", PublisherId = "Publisher 1", MediaTypeId = "Type 1", Year= 2009, TotalQty = 200  },
                new Media{Id = Guid.NewGuid().ToString(), Title ="Book 2", AuthorId = "Author 2", Cost = 23.0M, ISBN = "ISBN222222", PublisherId = "Publisher 1", MediaTypeId = "Type 1", Year= 2020, TotalQty= 20  }

            }; 
        }
    }
}
