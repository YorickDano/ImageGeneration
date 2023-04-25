using ImageGeneration;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;

namespace StartUp
{
    public class Program
    {

        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Drag image here");
                var filePath = Console.ReadLine();
                var fileNameWithExt = filePath.Remove(0, filePath.LastIndexOf('\\') + 1);
                var fileName = fileNameWithExt.Remove(fileNameWithExt.LastIndexOf('.'));
                var bitmap = new Bitmap(filePath);
                var imgGenrator = new ImageGenerator(bitmap, fileName);
                Task.WaitAll(imgGenrator.SaveColoredImage(100,0,0), imgGenrator.SaveColoredBaseImage(255, 0, 0),
                    imgGenrator.SaveHeightMixImage(4), imgGenrator.SaveWidthMixImage(4),
                    imgGenrator.SaveMixedImage(6,6), imgGenrator.SaveColoredImage(100, 0, 0),
                    imgGenrator.SaveHeightLinnerImage(20, 1), imgGenrator.SaveWidthLinnerImage(20, 1),
                    imgGenrator.ShowConsoledColoredImage(), imgGenrator.ShowConsoledImage(),
                    imgGenrator.SaveColoredBaseImage(0, 0, 255),
                    imgGenrator.SetHeightLinnerImage(20,1), imgGenrator.SaveWidthLinnerImage(20,1));
              
                Console.WriteLine("Done");
            }
        }
    }
}
