using NetArchTest.Rules;
using FluentAssertions;

namespace BookingSystem.Architecture.Tests;

public class LayerTests
{
    private const string DomainNamespace = "BookingSystem.Domain";
    private const string ApplicationNamespace = "BookingSystem.Application";
    private const string InfrastructureNamespace = "BookingSystem.Infrastructure";
    private const string ApiNamespace = "BookingSystem.Api";
    private const string WorkerNamespace = "BookingSystem.Worker";

    [Fact]
    public void Domain_Should_Not_HaveDependencyOn_OtherProjects()
    {
        // Arrange
        var domainAssembly = typeof(Domain.Class1).Assembly;

        // Act
        var result = Types
            .InAssembly(domainAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(
                ApplicationNamespace,
                InfrastructureNamespace,
                ApiNamespace,
                WorkerNamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}