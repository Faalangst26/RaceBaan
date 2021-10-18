using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

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
                var graphics = Graphics.FromImage(background);
                graphics.FillRectangle(Brush, 0, 0, len, width);
            }
        }
    }
}
