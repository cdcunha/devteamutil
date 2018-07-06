using DevTeamUtils.Api.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace DevTeamUtils.Api.Tests
{
    public class ContatoControllerTest
    {
        private MongoDbContext GetContextWithData()
        {
            
            var options = new DbContextOptionsBuilder<MongoDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new MongoDbContext(options);

            /*var beerCategory = new Category { Id = 1, Name = "Beers" };
            var wineCategory = new Category { Id = 2, Name = "Wines" };
            context.Categories.Add(beerCategory);
            context.Categories.Add(wineCategory);

            context.Products.Add(new Product { Id = 1, Name = "La Trappe Isid'or", Category = beerCategory });
            context.Products.Add(new Product { Id = 2, Name = "St. Bernardus Abt 12", Category = beerCategory });
            context.Products.Add(new Product { Id = 3, Name = "Zundert", Category = beerCategory });
            context.Products.Add(new Product { Id = 4, Name = "La Trappe Blond", Category = beerCategory });
            context.Products.Add(new Product { Id = 5, Name = "La Trappe Bock", Category = beerCategory });
            context.Products.Add(new Product { Id = 6, Name = "St. Bernardus Tripel", Category = beerCategory });
            context.Products.Add(new Product { Id = 7, Name = "Grottenbier Bruin", Category = beerCategory });
            context.Products.Add(new Product { Id = 8, Name = "St. Bernardus Pater 6", Category = beerCategory });
            context.Products.Add(new Product { Id = 9, Name = "La Trappe Quadrupel", Category = beerCategory });
            context.Products.Add(new Product { Id = 10, Name = "Westvleteren 12", Category = beerCategory });
            context.Products.Add(new Product { Id = 11, Name = "Leffe Bruin", Category = beerCategory });
            context.Products.Add(new Product { Id = 12, Name = "Leffe Royale", Category = beerCategory });
            context.SaveChanges();
            */

            return context;
        }


        [Fact]
        public void ContatoTest()
        {

        }


        private readonly ContatoController _contatoController;

        public ContatoControllerTest()
        {
            //_contatoController = new ContatoController();
        }

        [Fact]
        public void ReturnFalseGivenValueOf1()
        {
            //var result = _primeService.IsPrime(1);

            //Assert.False(result, "1 should not be prime");
        }
    }
}
