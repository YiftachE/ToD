using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodDataApp
{
    class Program
    {
        private static System.Timers.Timer printScreenTimer = null;
        private static string outputDir;
        private static int interval;

        static void Main(string[] args)
        {
            outputDir = args[0];

            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            interval = int.Parse(args[1]);

            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();

            Console.ReadLine();            
        }

        static void Run()
        {
            printScreenTimer = new System.Timers.Timer(interval);

            printScreenTimer.Elapsed += printScreenTimer_Elapsed;

            printScreenTimer.Enabled = true;
            printScreenTimer.Start();
        }

        static void printScreenTimer_Elapsed(object sender, EventArgs e)
        {
            string fileName = DateTime.Now.ToString("dd_MM_yy hh_mm_ss") + ".png";

            Bitmap screen = PrintScreen();

            screen.Save(Path.Combine(outputDir, fileName), ImageFormat.Png);
        }

        public static Bitmap PrintScreen()
        {
            Bitmap screenShotBMP = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            
            Graphics screenShotGraphics = Graphics.FromImage(screenShotBMP);
            screenShotGraphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, 
                Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

            screenShotGraphics.Dispose();

            return screenShotBMP;
        }
    }
}
