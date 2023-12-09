﻿using System;
using Moq;
using TaxCalculator.Core.Interface.Repositories;
using TaxCalculator.Core.Model;

namespace TaxCalculator.Test.Cases
{
    [TestFixture]
    public class TaxTypeRepositoryTests
	{

        [Test]
        public async Task CreateTaxType_ShouldReturnCreatedTaxType()
        {
            // Arrange
            string type = "Progressiv";
            var taxType = new TaxTypeModel { Type = type };
            var mockTaxTypeRepository = new Mock<ITaxTypeRepositories>();
            mockTaxTypeRepository.Setup(repo => repo.Create(type))
                              .ReturnsAsync(new Tuple<bool, TaxTypeModel>(true, taxType));

            var taxTypeRepository = mockTaxTypeRepository.Object;

            // Act
            var result = await taxTypeRepository.Create(type);

            // Assert
            Assert.Equals(true, result.Item1);
            Assert.Equals("Progressive", result.Item2.Type);
        }

        [Test]
        public async Task UpdateTaxType_ShouldReturnUpdateTaxType()
        {
            // Arrange
            string type = "Progressiv";
            var taxType = new TaxTypeModel { Type = type };
            var mockTaxTypeRepository = new Mock<ITaxTypeRepositories>();
            mockTaxTypeRepository.Setup(repo => repo.Update(type, "Progressive"))
                              .ReturnsAsync(new Tuple<bool, TaxTypeModel>(true, taxType));

            var taxTypeRepository = mockTaxTypeRepository.Object;

            // Act
            var result = await taxTypeRepository.Create(type);

            // Assert
            Assert.Equals(true, result.Item1);
            Assert.Equals("Progressive", result.Item2.Type);
        }

        [Test]
        public async Task DeleteTaxType_ShouldReturnTrue()
        {
            // Arrange
            string type = "Progressive";
            var mockTaxTypeRepository = new Mock<ITaxTypeRepositories>();
            mockTaxTypeRepository.Setup(repo => repo.Delete(type))
                              .ReturnsAsync(new Tuple<bool, string>(true, type));

            var taxTypeRepository = mockTaxTypeRepository.Object;

            // Act
            var result = await taxTypeRepository.Delete(type);

            // Assert
            Assert.Equals(true, result.Item1);
        }

        [Test]
        public async Task GetByNameTaxType_ShouldReturnTrue()
        {
            // Arrange
            int typeId = 1;
            string type = "Progressive";
            var taxType = new TaxTypeModel { Type = type };
            var mockTaxTypeRepository = new Mock<ITaxTypeRepositories>();
            mockTaxTypeRepository.Setup(repo => repo.GetById(1))
                              .ReturnsAsync(new Tuple<bool, TaxTypeModel>(true, taxType));

            var taxTypeRepository = mockTaxTypeRepository.Object;

            // Act
            var result = await taxTypeRepository.GetById(typeId);

            // Assert
            Assert.Equals(true, result.Item1);
            Assert.Equals("Progressive", result.Item2.Type);
        }
        [Test]
        public async Task GetAllTaxType_ShouldReturnAllTaxType()
        {
            // Arrange
            var mockTaxTypeRepository = new Mock<ITaxTypeRepositories>();
            mockTaxTypeRepository.Setup(repo => repo.GetAll(1,2))
                              .ReturnsAsync(new Tuple<bool, TaxTypeModel[]>(true, new[]
                              {
                                 new TaxTypeModel { Type = "Progressive" },
                                 new TaxTypeModel { Type = "FlatRate" }

                              }));

            var taxTypeRepository = mockTaxTypeRepository.Object;

            // Act
            var result = await taxTypeRepository.GetAll(1,2);

            // Assert
            Assert.Equals(true, result.Item1);
            Assert.Equals("Progressive", result.Item2[0].Type);
        }
    }
}

