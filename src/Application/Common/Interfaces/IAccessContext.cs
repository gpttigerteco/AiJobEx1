namespace AiJobEx1.Application.Common.Interfaces;

public interface IAccessContext
{
    Guid? GetCurrentUserId();
}
