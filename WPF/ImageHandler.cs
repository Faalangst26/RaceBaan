using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing.Imaging;

namespace WPF
{
    public static class ImageHandler
    {
        private static Dictionary<string, Bitmap> _ImageCache = new Dictionary<string, Bitmap>();

        public static Bitmap GetImage(string imgurl)
        {
            if (_ImageCache.ContainsKey(imgurl))
            {
                return _ImageCache[imgurl];
            }
            else
            {
                Bitmap bm = new Bitmap(imgurl);
                _ImageCache[imgurl] = bm;
                return bm;
            }
        }

        public static void ClearCache()
        {
            _ImageCache.Clear();
        }

        public static Bitmap GetBackground(int len, int width)
        {
            if (_ImageCache.ContainsKey("empty"))
            {
                return _ImageCache["empty"];
            }
            else //Er is nog geen empty Bitmap in de cache, maak een nieuwe aan.
            {
                Bitmap background = new Bitmap(len, width);
                _ImageCache["empty"] = (Bitmap)background.Clone();
                var graphics = Graphics.FromImage(background);
                graphics.FillRectangle(new SolidBrush(System.Drawing.Color.LightGreen), 0, 0, len, width);
                graphics.DrawImage(background, 0, 0);
                
                return background;

            }
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
