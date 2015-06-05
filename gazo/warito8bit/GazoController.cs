using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace gazo
{
    public class GazoController
    {
        public static bool Process(string src, string dst)
        {
            if (!System.IO.File.Exists(src))
            {
                MessageBox.Show("そのようなファイルはございません");
                return false;
            }
            if (dst == "")
            {
                MessageBox.Show("保存先が空です");
                return false;
            }

            Bitmap gsrc = (Bitmap)Image.FromFile(src);
            Bitmap gdst = new Bitmap(gsrc.Width, gsrc.Height, PixelFormat.Format8bppIndexed);
//            BitmapData bitSrc = gsrc.LockBits(new Rectangle(0,0,gsrc.Width,gsrc.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitDst = gdst.LockBits(new Rectangle(0,0,gdst.Width,gdst.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            List<Color> detected = new List<Color>();
            for (int j = 0; j < gsrc.Height; j++)
            for (int i = 0; i < gsrc.Width; i++)
            {
                Color current = gsrc.GetPixel(i, j);
                if (!detected.Contains(current))
                {
                    detected.Add(current);

                    if (detected.Count > 256)
                    {
                        MessageBox.Show("色数が多すぎる。みたいだ。");
                        return false;
                    }
                }

                unsafe
                {
                    ((byte*)bitDst.Scan0.ToPointer())[j * bitDst.Stride + i] = (byte)detected.IndexOf(current);
                }
            }

            gdst.UnlockBits(bitDst);
            ColorPalette tmp = gdst.Palette;
            detected.CopyTo(tmp.Entries);
            gdst.Palette = tmp;

            gdst.Save(dst);
            gsrc.Dispose();
            gdst.Dispose();
            
            return true;
        }
    }
}
