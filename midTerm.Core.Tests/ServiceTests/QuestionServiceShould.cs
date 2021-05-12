using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using midTerm.Data;
using midTerm.Models.Models.Question;
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
    public class QuestionServiceShould : SqlLiteContext
    {
        private readonly IMapper _mapper;
        private readonly QuestionService _service;

        public QuestionServiceShould() : base(true)
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
            _service = new QuestionService(DbContext, _mapper);
        }

        [Fact]
        public async Task GetQuestiongById()
        {
            // Arrange
            var expected = 1;

            // Act
            var result = await _service.GetById(expected);

            // Assert

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<QuestionModelExtended>();
            result.Id.Should().Be(expected);
        }
        [Fact]
        public async Task GetAllQuestions()
        {
            // Arrange
            var expected = 3;
            // Act
            var result = await _service.Get();

            // Assert

            result.Should().NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo<IEnumerable<QuestionModelBase>>();
        }
        [Fact]
        public async Task GetAllQuestionsExtended()
        {
            // Arrange
            var expected = 3;

            // Act
            var result = await _service.Get();

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo<IEnumerable<QuestionModelBase>>();
        }

        [Fact]
        public async Task InsertNewQuestiong()
        {
            // Arrange
            var question = new QuestionCreateModel
            {
                Text = "New Question",
                Description = "New Description"
            };

            // Act
            var result = await _service.Insert(question);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<QuestionModelBase>();
            result.Id.Should().NotBe(0);
        }

        [Fact]
        public async Task UpdateQuestiong()
        {
            // Arrange
            var question = new QuestionUpdateModel
            {
                Id = 1,
                Text = "Updated text",
                Description = "Updated description"
            };

            // Act
            var result = await _service.Update(question);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<QuestionModelBase>();
            result.Id.Should().Be(question.Id);
            result.Text.Should().Be(question.Text);
            result.Description.Should().Be(question.Description);
        }

        [Fact]
        public async Task ThrowExceptionOnUpdateQuestion()
        {
            // Arrange
            var option = new QuestionUpdateModel
            {
                Id = 10,
                Text = "Should throw"
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.Update(option));
            Assert.Equal("Question not found", ex.Message);

        }

        [Fact]
        public async Task DeleteQuestion()
        {
            // Arrange
            var expected = 3;

            // Act
            var result = await _service.Delete(expected);
            var match = await _service.GetById(expected);

            // Assert
            result.Should().Be(true);
            match.Should().BeNull();
        }
    }

}
