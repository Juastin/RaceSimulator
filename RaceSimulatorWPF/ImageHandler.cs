using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;

namespace RaceSimulatorWPF
{
    public static class ImageHandler
    {
        private static Dictionary<string, Bitmap> imageBitmaps = new Dictionary<string, Bitmap>();
        public static Bitmap GetBitmap(string url)
        {
            if (imageBitmaps.TryGetValue(url, out Bitmap bitmap))
                return bitmap;

            Bitmap newBitmap = new Bitmap(url);
            imageBitmaps.Add(url, newBitmap);
            return newBitmap;
        }
        public static Bitmap CreateEmptyBitmap(int x, int y)
        {
            imageBitmaps.TryGetValue("empty", out Bitmap bitmap);
            if (bitmap != null)
                return bitmap;
            Bitmap newEmptyBitmap = new Bitmap(x, y);
            using (Graphics gfx = Graphics.FromImage(newEmptyBitmap))
            using (SolidBrush brush = new SolidBrush(Color.OliveDrab))
            {
                gfx.FillRectangle(brush, 0, 0, x, y);
            }
            
            imageBitmaps.Add("empty", newEmptyBitmap);
            return (Bitmap)newEmptyBitmap.Clone();

        }
        public static void ClearCache()
        {
            imageBitmaps.Clear();
        }
        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
