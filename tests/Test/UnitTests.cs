using AutoFixture;
using Fruits.API;
using Fruits.Domain.Interfaces;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Test.Attributes;
using Xunit;
using BC = BCrypt.Net.BCrypt;

namespace Test
{
    [TestCaseOrderer("Test.Orderers.PriorityOrderer", "Test.Orderers")]
    public class UnitTests
    {
        private readonly DependencyResolverHelpercs _serviceProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFruitService _fruitService;
        private readonly IUserService _userService;

        public UnitTests()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("")
                .Build();

            _serviceProvider = new DependencyResolverHelpercs(webHost);
            _unitOfWork = _serviceProvider.GetService<IUnitOfWork>();
            _fruitService = _serviceProvider.GetService<IFruitService>();
            _userService = _serviceProvider.GetService<IUserService>();
        }

        [Fact, TestPriority(0)]
        public void ServiceInstanceTest()
        {
            var fruitService = _serviceProvider.GetService<IFruitService>();
            Assert.NotNull(fruitService);
        }

        [Fact, TestPriority(1)]
        public async Task ShouldInsertFruit()
        {
            var fixture = new Fixture();
            var fruit = fixture.Build<Fruit>()
                .With(c => c.Id, 0)
                .With(c => c.Name, "Banana")
                .With(c => c.AvailableQuantity, 10)
                .With(c => c.Price, decimal.Parse("1.87"))
                .With(c => c.Description, "Banana Description")
                .Create();

            _unitOfWork.GetContext().Add(fruit);
            await _unitOfWork.Commit();

            Assert.Contains(fruit, await _fruitService.GetAll());
        }

        [Fact, TestPriority(2)]
        public async Task ShouldInsertAndReturnFruit()
        {
            var fixture = new Fixture();
            var fruit = fixture.Build<Fruit>()
                .With(c => c.Id, 0)
                .With(c => c.Name, "Maçã")
                .With(c => c.AvailableQuantity, 10)
                .With(c => c.Price, Convert.ToDecimal(1.87))
                .With(c => c.Description, "Maçã Description")
                .Create();

            _unitOfWork.GetContext().Add(fruit);
            await _unitOfWork.Commit();

            var result = await _fruitService.GetByExpression(null, c => c.Name.ToLower().Contains("Maçã".ToLower()));
            Assert.Same(fruit, result.FirstOrDefault());
        }

        [Fact, TestPriority(3)]
        public async Task ShouldInsertAndUpdateFruit()
        {
            var fixture = new Fixture();
            var fruit = fixture.Build<Fruit>()
                .With(c => c.Id, 0)
                .With(c => c.Name, "Laranja")
                .With(c => c.AvailableQuantity, 20)
                .With(c => c.Price, Convert.ToDecimal(1.87))
                .With(c => c.Description, "Laranja Description")
                .Create();

            _unitOfWork.GetContext().Add(fruit);
            await _unitOfWork.Commit();

            var result = await _fruitService.FindByName("Laranja", null);
            var laranja = result.FirstOrDefault();

            laranja.Name = "Laranja Lima 123";
            laranja.Description = "Laranja Lima Description";
            laranja.Price = Convert.ToDecimal(1.50);

            _unitOfWork.GetContext().Set<Fruit>().Update(laranja);
            await _unitOfWork.Commit();

            result = await _fruitService.FindByName("Laranja Lima 123", null);

            Assert.Same(laranja, result.FirstOrDefault());
        }

        [Fact, TestPriority(4)]
        public async Task ShouldDeleteFruit()
        {
            var result = await _fruitService.FindByName("Laranja", null);
            var laranja = result.FirstOrDefault();

            _unitOfWork.GetContext().Set<Fruit>().Remove(laranja);
            await _unitOfWork.Commit();

            Assert.DoesNotContain(laranja, await _fruitService.GetAll());
        }

        [Fact, TestPriority(5)]
        public async Task ShouldInsertUser()
        {
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                .With(c => c.Id, 0)
                .With(c => c.Email, "cleverson85@gmail.com")
                .With(c => c.Senha, BC.HashPassword("123456"))
                .Create();

            _unitOfWork.GetContext().Add(user);
            await _unitOfWork.Commit();

            Assert.Contains(user, await _userService.GetAll());
        }
    }
}
