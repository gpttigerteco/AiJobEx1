using AiJobEx1.Application.Common.Interfaces;
using AiJobEx1.Application.JobDescriptions.Commands;
using AiJobEx1.Domain.Entities;
using AiJobEx1.Shared;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace AiJobEx1.UnitTests.Application.JobDescriptions;

public class RequestJobDescriptionChangeCommandTests
{
    [Fact]
    public async Task Handler_Should_Return_Failure_When_JobDescription_Not_Found()
    {
        var dbContext = new Mock<IApplicationDbContext>();
        dbContext.Setup(x => x.JobDescriptions.FindAsync(It.IsAny<object?[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((JobDescription?)null);

        var handler = new RequestJobDescriptionChangeCommandHandler(dbContext.Object, new FakeDateTimeProvider());

        var result = await handler.Handle(new RequestJobDescriptionChangeCommand(Guid.NewGuid(), Guid.NewGuid(), "markdown"), CancellationToken.None);

        Assert.False(result.Succeeded);
        Assert.Equal("Job description not found", result.Error);
    }

    [Fact]
    public void Validator_Should_Flag_Empty_Markdown()
    {
        var validator = new RequestJobDescriptionChangeCommandValidator();
        var result = validator.TestValidate(new RequestJobDescriptionChangeCommand(Guid.NewGuid(), Guid.NewGuid(), ""));
        result.ShouldHaveValidationErrorFor(x => x.ProposedMarkdown);
    }

    private sealed class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
