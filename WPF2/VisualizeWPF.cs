using Controller;
using Model;
using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WPF2
{
    public static class VisualizeWPF
    {

        #region graphics
        const string finish = ".\\Graphics\\finish.png";
        const string startgrid = ".\\Graphics\\startgrid.png";
        const string corner_left = ".\\Graphics\\corner_left.png";
        const string corner_right = ".\\Graphics\\corner_right.png";
        const string straight_hor = ".\\Graphics\\straigt_hor.png";
        const string straight_ver = ".\\Graphics\\straigt_ver.png";
        const string car_blue = ".\\Graphics\\car_blue.png";
        const string car_green = ".\\Graphics\\car_green.png";
        #endregion

        private static int _direction = 1;
        private static Point cursorpos = new Point(100, 200);
        public static BitmapSource DrawTrack(Track track)
        {
            Bitmap trackimg = new Bitmap(ImageHandler.GetBackground(400, 400));
            Graphics gr = Graphics.FromImage(trackimg);
            //1 is richting het oosten, 2 richting het zuiden, 3 het westen, en 0 het noorden 
            foreach (Section section in track.Sections)
            {
                
                if (section.SectionType.Equals(SectionTypes.StartGrid))
                {
                    gr.DrawImage(Image.FromFile(startgrid),cursorpos);
                    
                }
                if (section.SectionType.Equals(SectionTypes.Finish))
                {
                    gr.DrawImage(Image.FromFile(finish), cursorpos);

                }
                if (section.SectionType.Equals(SectionTypes.Straight))
                {
                    if(_direction == 1 || _direction == 3)
                    {
                        gr.DrawImage(Image.FromFile(straight_hor), cursorpos);     
                    }
                    else
                    {
                        gr.DrawImage(Image.FromFile(straight_ver), cursorpos);
                    }
                }
                //Bochten naar rechts
                if (section.SectionType.Equals(SectionTypes.RightCorner)){
                    _direction++;
                    var img = Image.FromFile(corner_left);
                    img.RotateFlip(Calcrotation());
                    gr.DrawImage(img, cursorpos);
                }
                //Bochten naar links
                if (section.SectionType.Equals(SectionTypes.LeftCorner)){
                    _direction--;
                    var img = Image.FromFile(corner_left);
                    img.RotateFlip(Calcrotation());
                    gr.DrawImage(img, cursorpos);
                }


                //Cursor en draaing bepalen
                if (_direction == 0 || _direction > 3)//Noorden
                {
                    
                    _direction = 0;
                    cursorpos.Y += 50;
                }
                else if (_direction == 1) //Oosten
                {
                    cursorpos.X += 50;
                }
                else if (_direction == 2) //Zuiden
                {
                    cursorpos.Y -= 50;
                }
                else if (_direction == 3 || _direction < 0)//Westen
                {
                    cursorpos.X -= 50;
                }
            }



            var Bitmapsource = ImageHandler.CreateBitmapSourceFromGdiBitmap(trackimg);

            return Bitmapsource;
        }

        public static RotateFlipType Calcrotation()
        {
            if(_direction == 3 || _direction < 0)
            {
                return RotateFlipType.RotateNoneFlipNone;
            }
            else if(_direction == 2)
            {
                return RotateFlipType.Rotate90FlipNone;
            }
            else if(_direction == 1)
            {
                return RotateFlipType.Rotate180FlipNone;
            } 
            else if(_direction == 0 || _direction > 3)
            {
                return RotateFlipType.Rotate270FlipNone;
            } else
            {
                return RotateFlipType.RotateNoneFlipNone;
            }

        }
    }
}
