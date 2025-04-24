//--- https://meguminoken.blogspot.com/2011/07/c-imagetbyte-array.html ---

using System.Diagnostics;

namespace WinFormsApp1_Imaget
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // 先將圖檔讀到FileStream, 再轉換為byte array。
        private static void ToBinaryByFileStream(string imageFile)
        {
            Debug.WriteLine("ToBinaryByFileStream() start."); //Eric:for debug
            FileStream fs = new FileStream(imageFile, FileMode.Open);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
        }

        // 先將圖檔讀到Image object, 再轉換為byte array。
        private static void ToBinaryByImageObj(string imageFile)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(imageFile);
            MemoryStream mr = new MemoryStream();
            image.Save(mr, System.Drawing.Imaging.ImageFormat.Png);
            byte[] binaryImage = mr.ToArray();
            mr.Dispose();
            image.Dispose();
        }

        // 測試method。 
        public static void ConvertImageToBinary()
        {
            Debug.WriteLine("=== Convert the image file to the binary object. ===");

            string imageFolder = @"D:\temporary\imgs";
            // 測試的image數量。
            int sampleCount = 1; //100;

            string[] imagePaths = Directory.GetFiles(imageFolder, "*.png");
            if (0 == imagePaths.Length)
            {
                Console.WriteLine("No image found.");
                return;
            }

            Stopwatch sw = new Stopwatch();

            sw.Reset();
            sw.Start();
            for (int i = 0; i < sampleCount; i++)
            {
                ToBinaryByFileStream(imagePaths[i]);
            }
            sw.Stop();
            Console.WriteLine("start Console.WriteLine...");    //Eric:for debug
            Debug.WriteLine("By FileStream: {0} ms.",
                sw.ElapsedMilliseconds);

            sw.Reset();
            sw.Start();
            for (int i = 0; i < sampleCount; i++)
            {
                ToBinaryByImageObj(imagePaths[i]);
            }
            sw.Stop();
            Debug.WriteLine("By System.Drawing.Image: {0} ms.",
                sw.ElapsedMilliseconds);    //Console.WriteLine...
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConvertImageToBinary();
        }
    }
}
