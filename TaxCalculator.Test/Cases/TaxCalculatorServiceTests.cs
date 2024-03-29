﻿using System;
using Moq;
using TaxCalculator.Core.Interface;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Data.Entities;
using TaxCalculator.Core.Interface.Manager;
using TaxCalculator.Core.Interface.Repositories;
using TaxCalculator.Infrastructure.Services;
using TaxCalculator.Data.Repositories;
using Xunit;
using Shouldly;

namespace TaxCalculator.Test.Cases
{
    public class TaxCalculatorServiceTests
	{
        private readonly Mock<ITaxRateManager> _taxRateManager;
        private readonly Mock<ITaxPostCodeManager> _taxPostCodeManager;
        private readonly Mock<ITaxTypeManager> _taxTypeManager;
        private readonly Mock<ICalculatedTaxeRepository> _calculatedTaxeRepository;
        private readonly TaxCalculatorService _taxCalculatorService;
        public TaxCalculatorServiceTests()
        {
            _taxRateManager = new Mock<ITaxRateManager>();
            _taxTypeManager = new Mock<ITaxTypeManager>();
            _taxPostCodeManager = new Mock<ITaxPostCodeManager>();
            _calculatedTaxeRepository = new Mock<ICalculatedTaxeRepository>();

            _taxCalculatorService = new TaxCalculatorService(_taxRateManager.Object,_taxPostCodeManager.Object,
                _calculatedTaxeRepository.Object, _taxTypeManager.Object);
        }

        [Fact]
        public async Task CalculateTax_ShouldReturnTax()
        {
            // Arrange
            decimal income = 100;
            string postCode = "00001";
            var dbOption = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase("taxtest").Options;
            using (var dbContext = new Context(dbOption))
            {
                dbContext.Add(new TaxType { Id = 1, Type = "Progresiive" });
                dbContext.Add(new TaxPostCode { Id = 1, TaxType = 1, PostalCode = "0000" });
                dbContext.Add(new TaxRate { Id = 1, From = 1000, To=0});
                await dbContext.SaveChangesAsync();
            }

            // Act
            var tax = await _taxCalculatorService.CalculateTax(income, postCode);

            // Assert
            tax.ShouldBe(299);
            //Should.That(tax, Is.EqualTo(299));

        }
    }
}
