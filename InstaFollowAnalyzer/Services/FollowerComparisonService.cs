
namespace InstaFollowAnalyzer.Services
{
    public class FollowerComparisonService
    {
        public List<string> GetUsersNotFollowingYouBack(
       List<string> following,
       List<string> followers)
        {
            return following
                .Except(followers, StringComparer.OrdinalIgnoreCase) 
                .OrderBy(username => username)
                .ToList();
        }

        public List<string> GetFollowersYouDoNotFollowBack(
            List<string> following,
            List<string> followers)
        {
            return followers
                .Except(following, StringComparer.OrdinalIgnoreCase)
                .OrderBy(username => username)
                .ToList();
        }

        public List<string> GetMutualFollowers(
            List<string> following,
            List<string> followers)
        {
            return following
                .Intersect(followers, StringComparer.OrdinalIgnoreCase)
                .OrderBy(username => username)
                .ToList();
        }

    }
}
