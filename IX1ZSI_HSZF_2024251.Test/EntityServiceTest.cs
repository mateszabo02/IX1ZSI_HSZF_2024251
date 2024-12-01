using Castle.Core.Resource;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    [TestFixture]
    public class EntityServiceTest
    {
        [Test]
        public void CreateTest()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<RealEstateDbContext>();
                services.AddSingleton<IPropertyRepository, PropertyRepository>();
                services.AddSingleton<IPropertyService<Property>, PropertyService>();
            }).Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var propertyService = serviceProvider.GetService<IPropertyRepository>();
            int lastId = propertyService.ReadAll().OrderBy(x=>x.Id).Last().Id;
            Property property = new Property();
            property.District = 1;
            property.Address = "Minta utca 5.";
            property.Area = 120.3;
            property.Rooms = 3;
            property.SellingPrice = 100000;
            property.RentPrice = 10000;
            propertyService.Create(property);
            Assert.That((property.Id - lastId).Equals(1), Is.True);
        }
        [Test]
        public void ReadTest()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<RealEstateDbContext>();
                services.AddSingleton<IPropertyRepository, PropertyRepository>();
                services.AddSingleton<IPropertyService<Property>, PropertyService>();
            }).Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var propertyService = serviceProvider.GetService<IPropertyRepository>();
            Property property = new Property();
            property.District = 1;
            property.Address = "Minta utca 5.";
            property.Area = 120.3;
            property.Rooms = 3;
            property.SellingPrice = 100000;
            property.RentPrice = 10000;
            propertyService.Create(property);
            int lastId = propertyService.ReadAll().OrderBy(x => x.Id).Last().Id;
            Property lastProperty = propertyService.Read(lastId);
            Assert.That(property.Equals(lastProperty), Is.True);
        }
        [Test]
        public void UpdateTest()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<RealEstateDbContext>();
                services.AddSingleton<IPropertyRepository, PropertyRepository>();
                services.AddSingleton<IPropertyService<Property>, PropertyService>();
            }).Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var propertyService = serviceProvider.GetService<IPropertyRepository>();
            Property property = new Property();
            property.District = 1;
            property.Address = "Minta utca 5.";
            property.Area = 120.3;
            property.Rooms = 3;
            property.SellingPrice = 100000;
            property.RentPrice = 10000;
            propertyService.Create(property);
            int lastId = propertyService.ReadAll().OrderBy(x => x.Id).Last().Id;

            Property updatedProperty = new Property();
            updatedProperty.Id = lastId;
            updatedProperty.District = 1;
            updatedProperty.Address = "Minta utca 5.";
            updatedProperty.Area = 122;
            updatedProperty.Rooms = 4;
            updatedProperty.SellingPrice = 100000;
            updatedProperty.RentPrice = 10000;
            propertyService.Update(updatedProperty);

            Assert.That(propertyService.Read(lastId).Area.Equals(updatedProperty.Area), Is.True);
        }

        [Test]
        public void DeleteTest()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<RealEstateDbContext>();
                services.AddSingleton<IPropertyRepository, PropertyRepository>();
                services.AddSingleton<IPropertyService<Property>, PropertyService>();
            }).Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var propertyService = serviceProvider.GetService<IPropertyRepository>();
            Property property = new Property();
            property.District = 1;
            property.Address = "Minta utca 5.";
            property.Area = 120.3;
            property.Rooms = 3;
            property.SellingPrice = 100000;
            property.RentPrice = 10000;
            propertyService.Create(property);
            int propId = property.Id;
            propertyService.Delete(propId);
            int lastId = propertyService.ReadAll().OrderBy(x => x.Id).Last().Id;
            Assert.That((propId-lastId).Equals(1), Is.True);
        }

        [Test]
        public void PropertyToStringTest()
        {
            Property property = new Property();
            property.District = 1;
            property.Address = "Minta utca 5.";
            property.Area = 120.3;
            property.Rooms = 3;
            property.SellingPrice = 100000;
            property.RentPrice = 10000;
            string expected = $"{property.Id} District: 1. Address: Minta utca 5., Area: 120,3, Rooms: 3, SellingPrice: 100000, RentPrice: 10000";
            Assert.That(expected.Equals(property.ToString()), Is.True);
        }

        [Test]
        public void CustomerToStringTest()
        {
            Customer customer = new Customer();
            customer.MinRooms = 3;
            customer.MaxRooms = 4;
            customer.DistrictPreferences = new List<int> { 1, 2, 3 };
            customer.MinArea = 3;
            customer.MaxArea = 4;
            customer.MinPrice = 1000;
            customer.MaxPrice = 1500;
            customer.LookingForPurchase = true;
            customer.LookingForRent = true;
            string expected = $"({customer.Id})Rooms: 3-4;Area: 3-4;Preferred districts: 1, 2, 3, Price: 1000-1500;Looking for rent: True;Looking for purchase: True;";
            Assert.That(expected.Equals(customer.ToString()), Is.True);
        }

        [Test]
        public void ContractToStringTest()
        {
            Contract contract = new Contract();
            contract.PropertyId = 1;
            contract.SellerId = 1;
            contract.BuyerId = 1;
            contract.Price = 1000000;
            contract.SignDate = DateTime.Now;
            contract.ContractExpiration = DateTime.Now;

            string expected = $"({contract.Id}) PropertyId: 1; SellerId: 1; BuyerId: 1; Price: 1000000; SignDate: {DateTime.Now}; ContractExpiration: {DateTime.Now}";
            Assert.That(expected.Equals(contract.ToString()), Is.True);
        }

        [Test]
        public void CreateFalseTest()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<RealEstateDbContext>();
                services.AddSingleton<IPropertyRepository, PropertyRepository>();
                services.AddSingleton<IPropertyService<Property>, PropertyService>();
            }).Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var propertyService = serviceProvider.GetService<IPropertyRepository>();
            int lastId = propertyService.ReadAll().OrderBy(x => x.Id).Last().Id;
            Property property = new Property();
            property.District = 4;
            property.Address = "Bajza utca 3.";
            property.Area = 82;
            property.Rooms = 2;
            property.SellingPrice = 1000000;
            property.RentPrice = 20000;
            propertyService.Create(property);
            Assert.That((property.Id - lastId).Equals(0), Is.False);
        }

        [Test]
        public void ReadFalseTest()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<RealEstateDbContext>();
                services.AddSingleton<IPropertyRepository, PropertyRepository>();
                services.AddSingleton<IPropertyService<Property>, PropertyService>();
            }).Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var propertyService = serviceProvider.GetService<IPropertyRepository>();
            Property property = new Property();
            property.District = 4;
            property.Address = "Bajza utca 3.";
            property.Area = 82;
            property.Rooms = 2;
            property.SellingPrice = 1000000;
            property.RentPrice = 20000;
            propertyService.Create(property);
            int lastId = propertyService.ReadAll().OrderBy(x => x.Id).Last().Id;
            Property lastProperty = propertyService.Read(lastId-1);
            Assert.That(property.Equals(lastProperty), Is.False);
        }

        [Test]
        public void DeleteFalseTest()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<RealEstateDbContext>();
                services.AddSingleton<IPropertyRepository, PropertyRepository>();
                services.AddSingleton<IPropertyService<Property>, PropertyService>();
            }).Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var propertyService = serviceProvider.GetService<IPropertyRepository>();
            Property property = new Property();
            property.District = 1;
            property.Address = "Minta utca 5.";
            property.Area = 120.3;
            property.Rooms = 3;
            property.SellingPrice = 100000;
            property.RentPrice = 10000;
            propertyService.Create(property);
            int propId = property.Id;
            propertyService.Delete(propId);
            int lastId = propertyService.ReadAll().OrderBy(x => x.Id).Last().Id;
            Assert.That((propId - lastId).Equals(0), Is.False);
        }
    }
}
