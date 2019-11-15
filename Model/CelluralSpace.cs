using System;
using System.Collections.Generic;
using System.Drawing;

namespace Model{
    public class CelluralSpace
    {
        public Cell[,] currentState; //TODO: public for debug, may be changed as public property "Space" 
        public Cell[,] lastState; 

        public CelluralSpace(int size)
        {
            this.currentState = new Cell[size, size];
            this.lastState = new Cell[size, size];
            Clear();
        }   

        public void Clear()
        {
            //Grain emptyGrain = new Grain(); // fill with null or EmptyGrain ????

            FillCellsWithGrain(currentState, null);
            FillCellsWithGrain(lastState, null);
        }

        public Bitmap RenderCelluralSpace() // TODO: This method breaks single responsibility prinicpal, change !!
        {
            /*var bitmap = new Bitmap(500, 500); //TODO: Magic number
            for(int i = 0; i < bitmap.Width; i++)
            {
                for(int j = 0; j < bitmap.Height; j++)
                {
                    for(int k = 0; k < 10 && k + i < bitmap.Width; k++)// DEBUG!
                    {
                        bitmap.SetPixel(i, j, currentState[i, j].GrainMembership.Color);
                    }
                    //bitmap.SetPixel(i, j, currentState[i, j].GrainMembership.Color);

                }
            }
                
                return bitmap;*/
            return new Bitmap(@"C:\Users\mikim\Desktop\Multiscale Modeling\multiscale_modeling\MsmGrainGrowthGui\icons\excel.png");
        }


        private void FillCellsWithGrain(Cell[,] matrix, Grain grain)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = new Cell();
                    matrix[i, j].GrainMembership = grain;
                }
            }
        }

    }

    
}
