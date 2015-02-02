using System.Drawing;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace msdevshow_hero
{
    public class TwitterImage
    {
        public static async Task<Image> GetTwitterProfileImage(string twitterHandle)
        {
            //<img class="ProfileAvatar-image" src="(.*)"
            var client = new HttpClient();
            var html = await client.GetStringAsync("http://twitter.com/" + twitterHandle);

            var regex = new Regex("<img class=\"ProfileAvatar-image ?\" src=\"(.*?)\"");
            var match = regex.Match(html);
            if (match.Success)
            {
                var profileImageUrl = match.Groups[1].Value;
                var imageStream = await client.GetStreamAsync(profileImageUrl);
                return Image.FromStream(imageStream);
            }

            return null;
        }
    }
}