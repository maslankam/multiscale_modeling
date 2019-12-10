
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Model;

namespace GrainGrowthGui
{
    public class SpaceRenderingEngine
    {
        public BitmapSource Render(CelluralSpace space)
        {
            PixelFormat pf = PixelFormats.Bgr32;
            int spaceWidth = space.GetXLength();
            int spaceHeight = space.GetYLength();
            int rawStride = (spaceWidth * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[rawStride * spaceHeight];
            int rawImageIndex = 0;

            for (int i = 0; i < space.GetXLength(); i++)
            {
                for (int j = 0; j < space.GetYLength(); j++)
                {
                    System.Drawing.Color pixelColor = 
                        space.GetCell(i,j)?.MicroelementMembership?.Color ?? System.Drawing.Color.White;

                    //write byte[index] with pixelColor
                   
                        rawImage[rawImageIndex++] = pixelColor.R;
                        rawImage[rawImageIndex++] = pixelColor.G;
                        rawImage[rawImageIndex++] = pixelColor.B;
                        rawImage[rawImageIndex++] = 0;  
                }


            }
            BitmapSource bitmap = BitmapSource.Create(spaceWidth, spaceHeight,
                space.GetXLength(), space.GetYLength(), pf, null,
                rawImage, rawStride);
            return bitmap;
        }
    }
}
