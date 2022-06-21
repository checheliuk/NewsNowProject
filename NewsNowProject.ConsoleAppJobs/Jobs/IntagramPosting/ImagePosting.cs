using System;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace NewsNowProject.ConsoleAppJobs.Jobs.IntagramPosting
{
    public static class ImagePosting
    {
        public static string GetPath(string message, DateTime date)
        {
            string imageFilePath = InstagramPostingSettings.PathToMockUp;
            var bitmap = (Bitmap)Image.FromFile(imageFilePath);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawString(message,
                                    new Font("Cuprum", 60),
                                    Brushes.White,
                                    new Rectangle(400, 400, 3700, 3700),
                                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });

                graphics.DrawString(date.ToString("dd MMMM", CultureInfo.CreateSpecificCulture("uk-UA")),
                                  new Font("Cuprum", 48),
                                  Brushes.White,
                                  new Rectangle(750, 500, 3000, 3000),
                                  new StringFormat() { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Center });

                string path = InstagramPostingSettings.PathToImages + Guid.NewGuid().ToString() + ".jpg";
                bitmap.Save(path);
                return path;
            }
        }

        public static void DeleteImage(string path)
        {
            File.Delete(path);
        }
    }
}
