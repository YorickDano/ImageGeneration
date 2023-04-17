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
            while (true) { 
            Console.WriteLine("Drag image here");
            var filePath = Console.ReadLine();
            var fileNameWithExt = filePath.Remove(0, filePath.LastIndexOf('\\') + 1);
            var fileName = fileNameWithExt.Remove(fileNameWithExt.LastIndexOf('.'));
            var bitmap = new Bitmap(filePath);
            var imgGenrator = new ImageGenerator(bitmap, fileName);
            Task.WaitAll(imgGenrator.SaveGreenishedImage(), imgGenrator.SaveReddishedImage(),
                imgGenrator.SaveBluishedImage(), imgGenrator.SaveContrastedImage(), imgGenrator.SaveNoisedImage(),
                imgGenrator.SaveReversedImage(), imgGenrator.SaveBlackWhiteImage());
            Console.WriteLine("Done");
            }
        }
    }
}
