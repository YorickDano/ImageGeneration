using System.Drawing;
using System.Text;

namespace ImageGeneration
{
    public class ImageGenerator
    {
        private readonly string Title;
        private Bitmap Image;
        private bool IsFolderCreated = false;

        public ImageGenerator(Bitmap image, string title)
        {
            Image = image;
            Title = title;
        }
        public async Task SetNoisedImage()
        {
            Image = new Bitmap(await GetNoisedImage());
        }
        public async Task SaveNoisedImage()
        {
            await CreateFolder();
            var image = await GetNoisedImage();
            var imagePath = Path.Combine(await GetImagePath(), "Noise.jpg");

            image.Save(imagePath);
        }

        public async Task<Image> GetNoisedImage()
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
        public async Task SetContrastedImage()
        {
            Image = new Bitmap(await GetContrastedImage());
        }
        public async Task SaveContrastedImage()
        {
            await CreateFolder();
            var image = await GetContrastedImage();
            var imagePath = Path.Combine(await GetImagePath(), "Contrast.jpg");

            image.Save(imagePath);
        }
        public async Task<Image> GetContrastedImage()
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
        public async Task SetReversedImage()
        {
            Image = new Bitmap(await GetReversedImage());
        }
        public async Task SaveReversedImage()
        {
            await CreateFolder();
            var image = await GetReversedImage();
            var imagePath = Path.Combine(await GetImagePath(), "Reverse.jpg");

            image.Save(imagePath);
        }
        public async Task<Image> GetReversedImage()
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
        public async Task SetBlackWhiteImage(int step = 1)
        {
            Image = new Bitmap(await GetBlackWhiteImage(step));
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
        public async Task SetColoredBaseImage(int r, int g, int b, int a = 255)
        {
            Image = new Bitmap(await GetColoredBaseImage(r, g, b, a));
        }
        public async Task SaveColoredBaseImage(int r, int g, int b, int a = 255)
        {
            await CreateFolder();
            var image = await GetColoredBaseImage(r, g, b);
            var imagePath = Path.Combine(await GetImagePath(), $"ColoredBase_{a}_{r}_{g}_{b}.jpg");

            image.Save(imagePath);
        }
        public async Task<Image> GetColoredBaseImage(int r, int g, int b, int a = 255)
        {
            var color = Color.FromArgb(a, r, g, b);
            var image = new Bitmap(Image);
            for (int i = 0; i < image.Width; ++i)
            {
                for (int j = 0; j < image.Height; ++j)
                {
                    var br = image.GetPixel(i, j).GetBrightness();
                    var argb = color.ToArgb();
                    var brColor = Convert.ToInt32(br * argb);
                    image.SetPixel(i, j, Color.FromArgb(brColor));
                }
            }

            return image;
        }
        public async Task SetColoredImage(int r, int g, int b, int a = 255)
        {
            Image = new Bitmap(await GetColoredImage(r, g, b, a));
        }
        public async Task SaveColoredImage(int r, int g, int b, int a = 255)
        {
            await CreateFolder();
            var image = await GetColoredImage(r, g, b);
            var imagePath = Path.Combine(await GetImagePath(), $"Colored_{a}_{r}_{g}_{b}.jpg");

            image.Save(imagePath);
        }
        public async Task<Image> GetColoredImage(int r, int g, int b, int a = 255)
        {
            var image = new Bitmap(Image);
            for (int i = 0; i < image.Width; ++i)
            {
                for (int j = 0; j < image.Height; ++j)
                {
                    var br = image.GetPixel(i, j).GetBrightness();
                    image.SetPixel(i, j, Color.FromArgb(a, (int)(r * br), (int)(g * br), (int)(b * br)));
                }
            }

            return image;
        }
        public async Task SetAddedColorImage(int r, int g, int b)
        {
            Image = new Bitmap(await GetAddedColorImage(r, g, b));
        }
        public async Task SaveAddedColorImage(int r, int g, int b)
        {
            await CreateFolder();
            var image = await GetAddedColorImage(r, g, b);
            var imagePath = Path.Combine(await GetImagePath(), $"AddedColor_{r}_{g}_{b}.jpg");

            image.Save(imagePath);
        }

        public async Task<Image> GetAddedColorImage(int r, int g, int b)
        {
            var image = new Bitmap(Image);
            for (int i = 0; i < image.Width; ++i)
            {
                for (int j = 0; j < image.Height; ++j)
                {
                    var color = image.GetPixel(i, j);
                    image.SetPixel(i, j, Color.FromArgb(Math.Min(color.R + r, 255),
                        Math.Min(color.G + g, 255), Math.Min(color.B + b, 255)));
                }
            }

            return image;
        }
        public async Task SetHeightMixImage(int count)
        {
            Image = new Bitmap(await GetHeightMixImage(count));
        }
        public async Task SaveHeightMixImage(int count)
        {
            await CreateFolder();
            var image = await GetHeightMixImage(count);
            var imagePath = Path.Combine(await GetImagePath(), $"HeightMix.jpg");

            image.Save(imagePath);
        }

        public async Task<Image> GetHeightMixImage(int numParts)
        {
            var image = new Bitmap(Image);
            int partHeight = image.Height / numParts;
            Bitmap[] mixedParts = new Bitmap[numParts];
            List<int> partOrder = Enumerable.Range(0, numParts).ToList();
            Random random = new Random();
            partOrder = partOrder.OrderBy(x => random.Next()).ToList();

            for (int i = 0; i < numParts; i++)
            {
                int sourceY = partOrder[i] * partHeight;
                Rectangle sourceRect = new Rectangle(0, sourceY, image.Width, partHeight);
                mixedParts[i] = new Bitmap(image.Width, partHeight);
                Graphics g = Graphics.FromImage(mixedParts[i]);
                g.DrawImage(image, 0, 0, sourceRect, GraphicsUnit.Pixel);
            }
            Bitmap mixedImage = new Bitmap(image.Width, image.Height);
            Graphics g2 = Graphics.FromImage(mixedImage);

            int y = 0;
            foreach (int partIndex in partOrder)
            {
                int destY = partIndex * partHeight;
                Rectangle destRect = new Rectangle(0, destY, mixedImage.Width, partHeight);
                g2.DrawImage(mixedParts[partIndex], destRect);
                y += partHeight;
            }

            return mixedImage;
        }
        public async Task SetWidthMixImage(int numParts)
        {
            Image = new Bitmap(await GetWidthMixImage(numParts));
        }
        public async Task SaveWidthMixImage(int numParts)
        {
            await CreateFolder();
            var image = await GetWidthMixImage(numParts);
            var imagePath = Path.Combine(await GetImagePath(), $"WidthMix.jpg");

            image.Save(imagePath);
        }

        public async Task<Image> GetWidthMixImage(int numParts)
        {
            var image = new Bitmap(Image);
            var partWidth = image.Width / numParts;
            var mixedParts = new Bitmap[numParts];
            var partOrder = Enumerable.Range(0, numParts).ToList();
            var random = new Random();
            partOrder = partOrder.OrderBy(x => random.Next()).ToList();

            for (int i = 0; i < numParts; i++)
            {
                int sourceX = partOrder[i] * partWidth;
                Rectangle sourceRect = new Rectangle(sourceX, 0, partWidth, image.Height);
                mixedParts[i] = new Bitmap(partWidth, image.Height);
                Graphics g = Graphics.FromImage(mixedParts[i]);
                g.DrawImage(image, 0, 0, sourceRect, GraphicsUnit.Pixel);
            }
            Bitmap mixedImage = new Bitmap(image.Width, image.Height);
            Graphics g2 = Graphics.FromImage(mixedImage);

            int x = 0;
            foreach (int partIndex in partOrder)
            {
                int destX = partIndex * partWidth;
                Rectangle destRect = new Rectangle(destX, 0, partWidth, mixedImage.Height);
                g2.DrawImage(mixedParts[partIndex], destRect);
                x += partWidth;
            }

            return mixedImage;
        }

        public async Task SetMixedImage(int numPartsX, int numPartsY)
        {
            Image = new Bitmap(await GetMixedImage(numPartsX, numPartsY));
        }


        public async Task SaveMixedImage(int numPartsX, int numPartsY)
        {
            await CreateFolder();
            var image = await GetMixedImage(numPartsX, numPartsY);
            var imagePath = Path.Combine(await GetImagePath(), $"Mixed.jpg");

            image.Save(imagePath);
        }

        public async Task<Image> GetMixedImage(int numPartsX, int numPartsY)
        {
            var image = new Bitmap(Image);
            int partWidth = image.Width / numPartsX;
            int partHeight = image.Height / numPartsY;
            Bitmap[,] mixedParts = new Bitmap[numPartsX, numPartsY];

            var partOrderX = Enumerable.Range(0, numPartsX).ToList();
            var random = new Random();
            partOrderX = partOrderX.OrderBy(x => random.Next()).ToList();

            var partOrderY = Enumerable.Range(0, numPartsY).ToList();
            partOrderY = partOrderY.OrderBy(x => random.Next()).ToList();

            for (int i = 0; i < numPartsX; i++)
            {
                int sourceX = partOrderX[i] * partWidth;
                for (int j = 0; j < numPartsY; j++)
                {
                    int sourceY = partOrderY[j] * partHeight;
                    Rectangle sourceRect = new Rectangle(sourceX, sourceY, partWidth, partHeight);
                    mixedParts[i, j] = new Bitmap(partWidth, partHeight);
                    Graphics g = Graphics.FromImage(mixedParts[i, j]);
                    g.DrawImage(image, 0, 0, sourceRect, GraphicsUnit.Pixel);
                }
            }

            Bitmap mixedImage = new Bitmap(image.Width, image.Height);
            Graphics g2 = Graphics.FromImage(mixedImage);

            for (int i = 0; i < numPartsX; i++)
            {
                int destX = partOrderX[i] * partWidth;
                for (int j = 0; j < numPartsY; j++)
                {
                    int destY = partOrderY[j] * partHeight;
                    var destRect = new Rectangle(partWidth * i, partHeight * j, partWidth, partHeight);
                    g2.DrawImage(mixedParts[i, j], destRect);
                }
            }

            return mixedImage;
        }

        public async Task SetHeightLinnerImage(int count, int size)
        {
            Image = new Bitmap(await GetHeightLinnerImage(count, size));
        }

        public async Task SaveHeightLinnerImage(int count, int size)
        {
            await CreateFolder();
            var image = await GetHeightLinnerImage(count, size);
            var imagePath = Path.Combine(await GetImagePath(), $"HeightLinner.jpg");

            image.Save(imagePath);
        }

        public async Task<Image> GetHeightLinnerImage(int count, int size)
        {
            var image = new Bitmap(Image);
            var brush = Brushes.Black;
            var step = image.Width / count;
            var g = Graphics.FromImage(image);
            for (var i = 1; i < count + 1; ++i)
            {
                var rect = new Rectangle(i * step, 0, size, image.Height);
                g.FillRectangle(brush, rect);
            }

            return image;
        }

        public async Task SetWidthLinnerImage(int count, int size)
        {
            Image = new Bitmap(await GetWidthLinnerImage(count, size));
        }

        public async Task SaveWidthLinnerImage(int count, int size)
        {
            await CreateFolder();
            var image = await GetWidthLinnerImage(count, size);
            var imagePath = Path.Combine(await GetImagePath(), $"WidthLinner.jpg");

            image.Save(imagePath);
        }

        public async Task<Image> GetWidthLinnerImage(int count, int size)
        {
            var image = new Bitmap(Image);
            var brush = Brushes.Black;
            var step = image.Height / count;
            var g = Graphics.FromImage(image);
            var to = (image.Height / step) + 1;
            for (var i = 0; i < to; ++i)
            {
                var rect = new Rectangle(0, i * step, image.Width, size);
                g.FillRectangle(brush, rect);
            }

            return image;
        }

        public async Task ShowConsoledColoredImage()
        {
            var consoleHeight = 62;
            var consoleWidth = 234;
            var image = new Bitmap(Image, consoleWidth, consoleHeight);
            var brightnessLevels = " .-+*wGHM#&%";

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    var pixelColor = image.GetPixel(j, i);
                    int dIndex = (int)(pixelColor.GetBrightness() * brightnessLevels.Length);
                    if (dIndex < 0)
                    {
                        dIndex = 0;
                    }
                    else if (dIndex >= brightnessLevels.Length)
                    {
                        dIndex = brightnessLevels.Length - 1;
                    }
                    Console.ForegroundColor = ToConsoleColor(pixelColor);
                    Console.Write(brightnessLevels[dIndex]);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public async Task ShowConsoledImage()
        {
            var consoleHeight = 62;
            var consoleWidth = 234;
            var image = new Bitmap(Image, consoleWidth, consoleHeight);
            var brightnessLevels = " .-+*wGHM#&%";

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    int dIndex = (int)(image.GetPixel(j, i).GetBrightness() * brightnessLevels.Length);
                    if (dIndex < 0)
                    {
                        dIndex = 0;
                    }
                    else if (dIndex >= brightnessLevels.Length)
                    {
                        dIndex = brightnessLevels.Length - 1;
                    }
                    Console.Write(brightnessLevels[dIndex]);
                }
                Console.WriteLine();
            }
        }

        public async Task<string> GetConsoledImage()
        {
            var consoleHeight = 62;
            var consoleWidth = 234;
            var image = new Bitmap(Image, consoleWidth, consoleHeight);  
            var brightnessLevels = " .-+*wGHM#&%";
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < image.Height; i++)
            {              
                for (int j = 0; j < image.Width; j++)
                {
                    var pixelColor = image.GetPixel(j, i);
                    int dIndex = (int)(pixelColor.GetBrightness() * brightnessLevels.Length);

                    if (dIndex < 0)
                    {
                        dIndex = 0;
                    }
                    else if (dIndex >= brightnessLevels.Length)
                    {
                        dIndex = brightnessLevels.Length - 1;
                    }

                    stringBuilder.Append(brightnessLevels[dIndex]);
                }
                stringBuilder.Append('\n');
            }
            return stringBuilder.ToString();
        }

        private static ConsoleColor ToConsoleColor(Color color)
        {
            const double thresholdSaturation = 0.1;
            const double thresholdValue = 0.2;
            const double thresholdValue2 = 0.7;
            const double hueStep = 30.0;
            const double hueRed = 0.0;
            const double hueYellow = 60.0;
            const double hueGreen = 120.0;
            const double hueCyan = 180.0;
            const double hueBlue = 240.0;
            const double hueMagenta = 300.0;

            int r = color.R;
            int g = color.G;
            int b = color.B;

            double deltaR = (r / 255.0) - 0.5;
            double deltaG = (g / 255.0) - 0.5;
            double deltaB = (b / 255.0) - 0.5;

            double deltaMax = Math.Max(Math.Max(deltaR, deltaG), deltaB);
            double deltaMin = Math.Min(Math.Min(deltaR, deltaG), deltaB);

            double hue;
            if (deltaMax == deltaMin)
            {
                hue = 0.0;
            }
            else if (deltaMax == deltaR)
            {
                hue = ((60.0 * ((deltaG - deltaB) / (deltaMax - deltaMin))) + 360.0) % 360.0;
            }
            else if (deltaMax == deltaG)
            {
                hue = (60.0 * ((deltaB - deltaR) / (deltaMax - deltaMin))) + 120.0;
            }
            else
            {
                hue = (60.0 * ((deltaR - deltaG) / (deltaMax - deltaMin))) + 240.0;
            }

            double saturation = (deltaMax == 0.0) ? 0.0 : (deltaMax - deltaMin) / deltaMax;
            double value = deltaMax;

            if (value < thresholdValue)
            {
                return ConsoleColor.Black;
            }

            if (saturation < thresholdSaturation)
            {
                return (value < thresholdValue2) ? ConsoleColor.DarkGray : ConsoleColor.Gray;
            }

            double hueRound = Math.Floor(hue / hueStep + 0.5) * hueStep;
            switch (hueRound)
            {
                case hueRed:
                    return ConsoleColor.Red;
                case hueYellow:
                    return ConsoleColor.Yellow;
                case hueGreen:
                    return ConsoleColor.Green;
                case hueCyan:
                    return ConsoleColor.Cyan;
                case hueBlue:
                    return ConsoleColor.Blue;
                case hueMagenta:
                    return ConsoleColor.Magenta;
                default:
                    return ConsoleColor.White;
            }
        }

        public async Task<Image> GetImage() =>
            new Bitmap(Image);

        public async Task SaveImage()
        {
            await CreateFolder();
            var imagePath = Path.Combine(await GetImagePath(), $"CurrentImage.jpg");
            Image.Save(imagePath);
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
