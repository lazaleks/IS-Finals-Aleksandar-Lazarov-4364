using System;
using System.Collections.Generic;

using Bogus;
using Bogus.DataSets;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using midTerm.Data;
using midTerm.Data.Entities;

namespace Stats.Service.Test.Internal
{
    public abstract class SqlLiteContext
        : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;
        protected readonly MidTermDbContext DbContext;

        protected DbContextOptions<MidTermDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<MidTermDbContext>()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseSqlite(_connection)
                .Options;
        }
        protected SqlLiteContext(bool withData = false)
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            DbContext = new MidTermDbContext(CreateOptions());
            _connection.Open();
            DbContext.Database.EnsureCreated();
            if (withData)
                SeedData(DbContext);
        }

        private void SeedData(MidTermDbContext context)
        {
            var questions = new List<Question>
            {
                new Question
                {
                    Id = 1,
                    Text = "Question 1",
                    Description = "Description 1",
                    Options = new List<Option>()
                }, new Question
                {
                    Id = 2,
                    Text = "Question 2",
                    Description = "Description 2",
                    Options = new List<Option>()
                }, new Question
                {
                    Id = 3,
                    Text = "Question 3",
                    Description = "Description 3",
                    Options = new List<Option>()
                }
            };
            var options = new List<Option>
            {
                new Option
                {
                    Id = 1,
                    Text = "Option text 1",
                    QuestionId = 1
                },
                new Option
                {
                    Id = 2,
                    Text = "Option text 2",
                    QuestionId = 1
                },
                new Option
                {
                    Id = 3,
                    Text = "Option text 3",
                    QuestionId = 1
                },
                new Option
                {
                    Id = 4,
                    Text = "Option text 4",
                    QuestionId = 2
                },
                new Option
                {
                    Id = 5,
                    Text = "Option text 5",
                    QuestionId = 2
                },
                new Option
                {
                    Id = 6,
                    Text = "Option text 6",
                    QuestionId = 2
                }
            };
            context.Questions.AddRange(questions);
            context.Options.AddRange(options);
            context.SaveChanges();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
