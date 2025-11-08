using AiJobEx1.Domain.Entities;

namespace AiJobEx1.Domain.Events;

public record ChangeRequestApprovedEvent(JobDescriptionChangeRequest Request, JobDescription UpdatedJobDescription);
