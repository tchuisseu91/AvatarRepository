using InterviewTask.Dto;

namespace InterviewTask.Services.Contracts
{
    public interface IAvatarService
    {
        Task<AvatarDto> GetAvatar(string userIdentifier);
    }
}
