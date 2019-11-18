using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Model{
    public class CelluralSpace : IEnumerable
    {
        private Cell[,] space; 

        public CelluralSpace(int size)
        {
            this.space = new Cell[size, size];
        }   

        public void Initialize()
        {
            for (int i = 0; i < this.space.GetLength(0); i++)
            {
                for (int j = 0; j < space.GetLength(1); j++)
                {
                    space[i, j] = new Cell(i,j);
                }
            }
        }
 

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            for (int i = 0; i < this.space.GetLength(0); i++)
            {
                for (int j = 0; j < space.GetLength(1); j++)
                {
                    yield return space[i, j];
                }
            }
        }

        
    }

    public class CelluralSpaceEnumerator : IEnumerator
    {
        public object Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }


}
