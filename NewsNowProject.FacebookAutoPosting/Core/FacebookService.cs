using System.Threading.Tasks;

namespace NewsNowProject.FacebookAutoPosting.Core
{
    public interface IFacebookService
    {
        Task PostOnWallAsync(string accessToken, string id, string link);
    }

    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task PostOnWallAsync(string accessToken, string id, string link)
            => await _facebookClient.PostAsync(accessToken, id + "/feed", new { link });
    }
}
