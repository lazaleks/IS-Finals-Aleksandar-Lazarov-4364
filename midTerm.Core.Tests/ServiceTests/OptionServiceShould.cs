using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using midTerm.Data;
using midTerm.Data.Entities;
using midTerm.Infrastructure;
using midTerm.Models.Models.Option;
using midTerm.Models.Profiles;
using midTerm.Services.Services;
using Stats.Service.Test.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace midTerm.Core.Tests.ServiceTests
{
    public class OptionServiceShould : SqlLiteContext
    {
        private readonly IMapper _mapper;
        private readonly OptionService _service;

        public OptionServiceShould() : base(true)
        {
    
            if (_mapper == null)
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(BaseProfile));
                }).CreateMapper();
                _mapper = mapper;
            }
            //var dbOption = new DbContextOptionsBuilder<MidTermDbContext>()
            //    .UseSqlServer("Data Source=DESKTOP-5DKG8QI;Initial Catalog=finalsIS;Integrated Security=True")
            //    .Options;
            //var context = new MidTermDbContext(dbOption);
            _service = new OptionService(DbContext, _mapper);
        }

        [Fact]
        public async Task GetOptionById()
        {
            // Arrange
            var expected = 1;

            // Act
            var result = await _service.GetById(expected);

            // Assert

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OptionModelExtended>();
            result.Id.Should().Be(expected);
        }
        [Fact]
        public async Task GetOptionByQuestionId()
        {
            // Arrange
            var expected = 3;
            var questionId = 1;
            // Act
            var result = await _service.GetByQuestionId(questionId);

            // Assert

            result.Should().NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo<IEnumerable<OptionModelExtended>>();
        }
        [Fact]
        public async Task GetOptions()
        {
            // Arrange
            var expected = 6;

            // Act
            var result = await _service.Get();

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo<IEnumerable<OptionBaseModel>>();
        }

        [Fact]
        public async Task InsertNewOption()
        {
            // Arrange
            var option = new OptionCreateModel
            {
                QuestionId = 1,
                Text = "text 1"
            };

            // Act
            var result = await _service.Insert(option);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OptionBaseModel>();
            result.Id.Should().NotBe(0);
        }

        [Fact]
        public async Task UpdateOption()
        {
            // Arrange
            var option = new OptionUpdateModel
            {
                Id = 5,
                Text = "Updated text"
            };

            // Act
            var result = await _service.Update(option);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OptionBaseModel>();
            result.Id.Should().Be(option.Id);
            result.Text.Should().Be(option.Text);

        }

        [Fact]
        public async Task ThrowExceptionOnUpdateOption()
        {
            // Arrange
            var option = new OptionUpdateModel
            {
                Id = 10,
                Text = "Should throw"
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.Update(option));
            Assert.Equal("Option not found", ex.Message);

        }

        [Fact]
        public async Task DeleteOption()
        {
            // Arrange
            var expected = 6;

            // Act
            var result = await _service.Delete(expected);
            var match = await _service.GetById(expected);

            // Assert
            result.Should().Be(true);
            match.Should().BeNull();
        }
    }
}
