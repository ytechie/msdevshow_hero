using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace msdevshow_hero.Controllers
{
    public class HomeController : Controller
    {
        public async Task<FileStreamResult> Image(string id)
        {
            var profileImage = await TwitterImage.GetTwitterProfileImage(id);
            var templateStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(typeof(MvcApplication), "Twitter-Cover-Photo.jpg");
            var template = System.Drawing.Image.FromStream(templateStream);
            var merged = Merge(template, profileImage);

            var ms = new MemoryStream();
            merged.Save(ms, ImageFormat.Png);
            ms.Position = 0;

            return new FileStreamResult(ms, "image/png");
        }

        private Image Merge(Image template, Image profileImage)
        {
            using (var canvas = Graphics.FromImage(template))
            {
                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                canvas.DrawImage(profileImage, 107, 225);
                canvas.Save();
            }

            return template;
        }
    }
}