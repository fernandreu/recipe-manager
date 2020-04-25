// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigrationTests.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 25/04/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using RecipeManager.Infrastructure.Data;
using SmartFormat;
using Xunit;

namespace RecipeManager.IntegrationTests.Infrastructure
{
    public sealed class MigrationTests : IDisposable
    {
        private readonly AppDbContext context;

        public MigrationTests()
        {
            context = new DesignDbContextFactory().CreateDbContext();
            context.Database.EnsureDeleted();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
        
        [Fact]
        public void AllMigrationsExist()
        {
            // Arrange
            context.Database.Migrate();
            var differ = context.Database.GetService<IMigrationsModelDiffer>();
            var assembly = context.Database.GetService<IMigrationsAssembly>();
            var snapshot = assembly.ModelSnapshot;
            snapshot.Should().NotBeNull("there should be at least one migration defined for the model snapshot to exist");

            // Act
            var differences = differ.GetDifferences(snapshot.Model, context.Model);

            // Assert
            differences.Should().BeEmpty($"a migration step is needed{Environment.NewLine}Missing steps are:{Environment.NewLine}{string.Join(Environment.NewLine, differences.Select(x => $"- {FormatMessage(x)}"))}");
        }

        [Fact]
        public void CanCreateFromScratch()
        {
            // Arrange

            // Act
            context.Database.Migrate();

            // Assert
            context.Database.GetPendingMigrations().Should().BeEmpty();
        }

        [Fact]
        public void CanMigrateStepByStep()
        {
            // Arrange
            var migrator = context.Database.GetService<IMigrator>();
            string previous = null;

            // Act

            foreach (var target in context.Database.GetPendingMigrations().ToList())
            {
                migrator.Invoking(x => x.Migrate(target))
                    .Should().NotThrow(previous != null ? $"Cannot migrate from '{previous}' to '{target}'" : $"Cannot perform initial migration '{target}'");

                previous = target;
            }

            // Assert
            context.Database.GetPendingMigrations().Should().BeEmpty();
        }

        private static string FormatMessage(object o)
        {
            var attribute = o.GetType().GetCustomAttribute<DebuggerDisplayAttribute>();
            return attribute == null ? o.ToString() : Smart.Format(attribute.Value, o);
        }
    }
}