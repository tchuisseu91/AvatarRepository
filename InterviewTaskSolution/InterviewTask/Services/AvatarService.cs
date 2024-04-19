using InterviewTask.Dto;
using InterviewTask.Services.Contracts;
using Microsoft.Data.Sqlite;

namespace InterviewTask.Services
{
    public class AvatarService : IAvatarService
    {
        public async Task<AvatarDto> GetAvatar(string userIdentifier)
        {
            AvatarDto avatarDto;

            try
            {
                var lastDigitOfUserIdentifier = int.Parse(userIdentifier.Last().ToString());

                switch (lastDigitOfUserIdentifier)
                {
                    case int n when n >= 6 && n <= 9:

                        avatarDto = new AvatarDto
                        {
                            Id = lastDigitOfUserIdentifier,
                            Url = $"https://my-jsonserver.typicode.com/ck-pacificdev/tech-test/images/{lastDigitOfUserIdentifier}"
                        };

                        break;

                    case int n when n >= 1 && n <= 5:

                        avatarDto = new AvatarDto
                        {
                            Id = lastDigitOfUserIdentifier,
                            Url = GetSQlLite(lastDigitOfUserIdentifier)
                        };

                        break;

                    case int n when userIdentifier.Any(c => "aeiouAEIOU".Contains(c)):

                        avatarDto = new AvatarDto
                        {
                            Id = n,
                            Url = "https://api.dicebear.com/8.x/pixel-art/png?seed=vowel&size=150"
                        };

                        break;

                    case int n when userIdentifier.Any(c => !Char.IsLetterOrDigit(c)):
                        Random random = new Random();
                        int randomNumber = random.Next(1, 5);

                        avatarDto = new AvatarDto
                        {
                            Id = lastDigitOfUserIdentifier,
                            Url = $"https://api.dicebear.com/8.x/pixel-art/png?seed={randomNumber}&size=150"
                        };

                        break;

                    default:
                        avatarDto = new AvatarDto
                        {
                            Id = lastDigitOfUserIdentifier,
                            Url = "https://api.dicebear.com/8.x/pixelart/png?seed=default&size=150"
                        };

                        break;
                }

            }catch (Exception) 
            {
                throw;
            }

            return await Task.FromResult(avatarDto);
        }
        

        private string GetSQlLite(int lastDigitOfUserIdentifier)
        {
            string url = string.Empty;

            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"SELECT Url FROM images WHERE id = $id";

                command.Parameters.AddWithValue("$id", lastDigitOfUserIdentifier);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        url = reader.GetString(0);
                    }
                }
            }
            return url;
        }
    }
}
