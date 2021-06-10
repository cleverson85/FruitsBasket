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
using Xunit;
using BC = BCrypt.Net.BCrypt;

namespace Test
{
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
                .UseEnvironment("Development")
                .Build();

            _serviceProvider = new DependencyResolverHelpercs(webHost);
            _unitOfWork = _serviceProvider.GetService<IUnitOfWork>();
            _fruitService = _serviceProvider.GetService<IFruitService>();
            _userService = _serviceProvider.GetService<IUserService>();
        }

        [Fact]
        public void A_ServiceInstanceTest()
        {
            var fruitService = _serviceProvider.GetService<IFruitService>();
            Assert.NotNull(fruitService);
        }

        [Fact]
        public async Task B_ShouldInsertFruit()
        {
            var fixture = new Fixture();
            var fruit = fixture.Build<Fruit>()
                .With(c => c.Name, "Banana")
                .With(c => c.AvailableQuantity, 10)
                .With(c => c.Price, Convert.ToDecimal(1.87))
                .With(c => c.Description, "Banana Description")
                .Create();

            _unitOfWork.GetContext().Add(fruit);
            await _unitOfWork.Commit();

            var result = await _fruitService.GetByExpression(null, c => c.Name == "Banana" && c.AvailableQuantity == 10);
            Assert.NotNull(result.FirstOrDefault());
        }

        [Fact]
        public async Task C_ShouldInsertAndReturnFruit()
        {
            var fixture = new Fixture();
            var fruit = fixture.Build<Fruit>()
                .With(c => c.Name, "Maçã")
                .With(c => c.AvailableQuantity, 25)
                .With(c => c.Price, Convert.ToDecimal(1.87))
                .With(c => c.Description, "Maçã Description")
                .Create();

            _unitOfWork.GetContext().Add(fruit);
            await _unitOfWork.Commit();

            var result = await _fruitService.GetByExpression(null, c => c.Name == "Maçã" && c.AvailableQuantity == 25);
            Assert.NotNull(result.FirstOrDefault());
        }

        [Fact]
        public async Task D_ShouldInsertAndUpdateFruit()
        {
            var fixture = new Fixture();
            var fruit = fixture.Build<Fruit>()
                .With(c => c.Name, "Laranja")
                .With(c => c.AvailableQuantity, 20)
                .With(c => c.Price, Convert.ToDecimal(1.87))
                .With(c => c.Description, "Laranja Description")
                .Create();

            _unitOfWork.GetContext().Add(fruit);
            await _unitOfWork.Commit();

            var result = _unitOfWork.GetContext().Set<Fruit>().Where(c => c.Name == "Laranja" && c.AvailableQuantity == 20);
            var laranja = result.FirstOrDefault();

            laranja.Name = "Laranja Lima";
            laranja.Description = "Laranja Lima Description";
            laranja.AvailableQuantity = 50;
            laranja.Price = Convert.ToDecimal(1.50);

            _unitOfWork.GetContext().Update(laranja);
            await _unitOfWork.Commit();

            result = _unitOfWork.GetContext().Set<Fruit>().Where(c => c.Name == "Laranja Lima" && c.AvailableQuantity == 50);
            Assert.NotNull(result.FirstOrDefault());
        }

        [Fact]
        public async Task E_ShouldDeleteFruit()
        {
            var fixture = new Fixture();
            var fruit = fixture.Build<Fruit>()
                .With(c => c.Name, "Laranja Lima")
                .With(c => c.AvailableQuantity, 20)
                .With(c => c.Price, Convert.ToDecimal(1.87))
                .With(c => c.Description, "Laranja Description")
                .Create();

            _unitOfWork.GetContext().Add(fruit);
            await _unitOfWork.Commit();

            var result = _unitOfWork.GetContext().Set<Fruit>().Where(c => c.Name == "Laranja Lima" && c.AvailableQuantity == 20);
            var laranja = result.FirstOrDefault();

            _unitOfWork.GetContext().Set<Fruit>().Remove(laranja);
            await _unitOfWork.Commit();

            var resultFinal = _unitOfWork.GetContext().Set<Fruit>().Where(c => c.Name == "Laranja Lima" && c.AvailableQuantity == 20).FirstOrDefault();
            Assert.Null(resultFinal);
        }

        [Fact]
        public async Task F_ShouldInsertUser()
        {
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                .With(c => c.Email, "cleverson85@gmail.com")
                .With(c => c.Senha, BC.HashPassword("123456"))
                .Create();

            _unitOfWork.GetContext().Add(user);
            await _unitOfWork.Commit();

            var result = await _userService.GetByExpression(null, c => c.Email == "cleverson85@gmail.com");
            Assert.NotNull(result.FirstOrDefault());
        }
    }
}
