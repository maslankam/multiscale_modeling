using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace Model
{
    public class SpaceRenderingEngine
    {/*
        BitmapSource Render(CelluralSpace space) // BitmapSource is WPF class
        {
            PixelFormat pf = PixelFormats.Bgr32;
                int width = space.GetXLength();
                int height = space.GetYLength();
                int rawStride = (width * pf.BitsPerPixel + 7) / 8;
                byte[] rawImage = new byte[rawStride * height];
                int rawImageIndex = 0;

            for (int i = 0; i < space.GetXLength(); i++)
                {
                    for (int j = 0; j < space.GetYLength(); j++)
                    {
                        System.Drawing.Color pixelColor = 
                            space?.GetCell(i,j)?.MicroelementMembership?.Color ?? System.Drawing.Color.White;

                        // write byte[index] with pixelColor
                        //if (rawImageIndex >= rawImage.Length)
                        //{
                           // System.Diagnostics.Trace.WriteLine($"pixel [{i},{j}], rawIndex: {rawImageIndex}, outide of {rawImage.Length} bound");
                        //}
                        else
                        {
                            rawImage[rawImageIndex++] = pixelColor.R;
                            rawImage[rawImageIndex++] = pixelColor.G;
                            rawImage[rawImageIndex++] = pixelColor.B;
                            rawImage[rawImageIndex++] = 0;
                        }
                    }


                }
                BitmapSource bitmap = BitmapSource.Create(width, height,
                    this._spaceSize, this._spaceSize, pf, null,
                    rawImage, rawStride);

                return bitmap;
        }
    */}
}
