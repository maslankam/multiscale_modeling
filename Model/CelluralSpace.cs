using System;
using System.Collections;
using System.Collections.Generic;
using Model.Microelements;

namespace Model{
    public class CelluralSpace : IEnumerable<Cell>, ICloneable
    {
        public int Size => _space.GetLength(0);

        private readonly Cell[,] _space; 

        public CelluralSpace(int size)
        {
            if(size < 2) throw new ArgumentException("Space size must be more than 1");
            _space = new Cell[size, size];
            Initialize();
        }  

        public CelluralSpace(Cell[,] cells)
        {
            _space = cells;
        }

        private void Initialize()
        {
            for (int i = 0; i < _space.GetLength(0); i++)
            {
                for (int j = 0; j < _space.GetLength(1); j++)
                {
                    _space[i, j] = new Cell();
                }
            }
        }

        public int GetXLength(){
            return _space.GetLength(0);
        }

        public int GetYLength()
        {
            return _space.GetLength(1);
        }

        public Cell GetCell(int x, int y){
            return _space[x, y];
        }

        public void SetCellMembership(Microelement element,int x, int y)
        {
            _space[x, y].MicroelementMembership = element;
            if(element != null)
            {
                element.Members.Add(_space[x, y]);
            }
                
            
        }

        public CelluralSpace Clone() //copy membership reference
        {
            var newCells = new Cell[Size, Size];
            for (int i = 0; i < _space.GetLength(0); i++)
            {
                for (int j = 0; j < _space.GetLength(1); j++)
                {
                    newCells[i,j] = new Cell(_space[i,j].MicroelementMembership);
                }
            }
            return new CelluralSpace(newCells);
        }

        object ICloneable.Clone(){
            return Clone();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<Cell> IEnumerable<Cell>.GetEnumerator()
        {
            return GetEnumerator();
        }
        
    
        public IEnumerator<Cell> GetEnumerator()
        {
            for (int i = 0; i < _space.GetLength(0); i++)
            {
                for (int j = 0; j < _space.GetLength(1); j++)
                {
                    yield return _space[i, j];
                }
            }
        }
        
        
    }
}
