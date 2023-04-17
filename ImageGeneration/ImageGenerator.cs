using System.Drawing;

namespace ImageGeneration
{
    public class ImageGenerator
    {
        private readonly string Title;
        private readonly Bitmap Image;
        private bool IsFolderCreated = false;

        public ImageGenerator(Bitmap image, string title)
        {
            Image = image;
            Title = title;
        }

        public async Task SaveNoisedImage()
        {
            await CreateFolder();
            var image = await GetNoiseImage();
            var imagePath = Path.Combine(await GetImagePath(), "Noise.jpg");

            image.Save(imagePath);
        }

        public async Task<Image> GetNoiseImage()
        {
            var rand = new Random();
            var image = new Bitmap(Image);
            var maxX = (int)(image.Width * 0.05);
            var maxY = (int)(image.Height * 0.05);
            for (int i = 0; i < 666; ++i)
            {
                var x = rand.Next(0, image.Width);
                var y = rand.Next(0, image.Height);

                var randX = rand.Next(0, maxX);
                var randY = rand.Next(0, maxY);
                if (x + randX > image.Width || y + randY > image.Height)
                {
                    continue;
                }
                for (int j = x; j < x + randX; ++j)
                {
                    for (int c = y; c < y + randY; ++c)
                    {
                        image.SetPixel(j, c, Color.Black);
                    }
                }
            }

            return image;
        }

        public async Task SaveContrastedImage()
        {
            await CreateFolder();
            var image = await GetContrastImage();
            var imagePath = Path.Combine(await GetImagePath(), "Contrast.jpg");

            image.Save(imagePath);
        }
        public async Task<Image> GetContrastImage()
        {
            var image = new Bitmap(Image);
            for (int i = 0; i < image.Width; ++i)
            {
                for (int j = 0; j < image.Height; ++j)
                {
                    var color = image.GetPixel(i, j);
                    var r = (int)(color.R * 1.1); var g = (int)(color.G * 1.1); var b = (int)(color.B * 1.1);
                    var newColor = Color.FromArgb(r > 255 ? 255 : r, g > 255 ? 255 : g, b > 255 ? 255 : b);
                    image.SetPixel(i, j, newColor);
                }
            }

            return image;
        }

        public async Task SaveReversedImage()
        {
            await CreateFolder();
            var image = await GetReverseImage();
            var imagePath = Path.Combine(await GetImagePath(), "Reverse.jpg");

            image.Save(imagePath);
        }
        public async Task<Image> GetReverseImage()
        {
            var image = new Bitmap(Image);
            for (int i = 0; i < image.Width; ++i)
            {
                for (int j = 0; j < image.Height; ++j)
                {
                    var color = image.GetPixel(i, j);
                    image.SetPixel(i, j, Color.FromArgb(color.ToArgb() ^ 0xffffff));
                }
            }

            return image;
        }

        public async Task SaveBlackWhiteImage(int step = 1)
        {
            await CreateFolder();
            var image = await GetBlackWhiteImage(step);
            var imagePath = Path.Combine(await GetImagePath(), $"BlackWhite{step}.jpg");

            image.Save(imagePath);
        }
        public async Task<Image> GetBlackWhiteImage(int step = 1)
        {
            var image = new Bitmap(Image);
            for (int i = 0; i < image.Width; i += step)
            {
                for (int j = 0; j < image.Height; j += step)
                {
                    var br = image.GetPixel(i, j).GetBrightness();
                    var brColor = Convert.ToInt32(br * 255);
                    image.SetPixel(i, j, Color.FromArgb(brColor, brColor, brColor));
                }
            }

            return image;
        }

        public async Task SaveReddishedImage()
        {
            await CreateFolder();
            var image = await GetReddishImage();
            var imagePath = Path.Combine(await GetImagePath(), $"Reddish.jpg");

            image.Save(imagePath);
        }
        public async Task<Image> GetReddishImage()
        {
            var image = new Bitmap(Image);
            for (int i = 0; i < image.Width; ++i)
            {
                for (int j = 0; j < image.Height; ++j)
                {
                    var br = image.GetPixel(i, j).GetBrightness();
                    var brColor = Convert.ToInt32(br * 255);
                    image.SetPixel(i, j, Color.FromArgb(brColor, 0, 0));
                }
            }

            return image;
        }

        public async Task SaveBluishedImage()
        {
            await CreateFolder();
            var image = await GetBluishImage();
            var imagePath = Path.Combine(await GetImagePath(), $"Bluish.jpg");

            image.Save(imagePath);
        }
        public async Task<Image> GetBluishImage()
        {
            var image = new Bitmap(Image);
            for (int i = 0; i < image.Width; ++i)
            {
                for (int j = 0; j < image.Height; ++j)
                {
                    var br = image.GetPixel(i, j).GetBrightness();
                    var brColor = Convert.ToInt32(br * 255);
                    image.SetPixel(i, j, Color.FromArgb(0, 0, brColor));
                }
            }

            return image;
        }

        public async Task SaveGreenishedImage()
        {
            await CreateFolder();
            var image = await GetGreenishImage();
            var imagePath = Path.Combine(await GetImagePath(), $"Greenish.jpg");

            image.Save(imagePath);
        }
        public async Task<Image> GetGreenishImage()
        {
            var image = new Bitmap(Image);
            for (int i = 0; i < image.Width; ++i)
            {
                for (int j = 0; j < image.Height; ++j)
                {
                    var br = image.GetPixel(i, j).GetBrightness();
                    var brColor = Convert.ToInt32(br * 255);
                    image.SetPixel(i, j, Color.FromArgb(0, brColor, 0));
                }
            }

            return image;
        }

        private async Task<string> GetImagePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Title);
        }

        private async Task CreateFolder()
        {
            if (!IsFolderCreated)
            {
                Directory.CreateDirectory(await GetImagePath());
                IsFolderCreated = true;
            }
        }
    }
}
