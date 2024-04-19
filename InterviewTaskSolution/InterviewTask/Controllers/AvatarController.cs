using InterviewTask.Dto;
using InterviewTask.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly IAvatarService _avatarService;
        private readonly ILogger<AvatarController> _logger;

        public AvatarController(IAvatarService avatarService, ILogger<AvatarController> logger)
        {
            _avatarService = avatarService;
            _logger = logger;
        }
        [HttpGet(Name = "GetAvatar")]
        public async Task<ActionResult<AvatarDto>> GetAvatar(string userIdentifier)
        {
            _logger.LogInformation($"{nameof(GetAvatar)} is called with the input {userIdentifier}");

            var avatarDto = await _avatarService.GetAvatar(userIdentifier);

            return Ok(avatarDto);
        }

    }
}
