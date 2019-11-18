using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Model{
    public class CelluralSpace : IEnumerable<Cell>, ICloneable
    {
        public int Size{
            get {return _space.GetLength(0); }
            private set{}
        }

        private Cell[,] _space; 

        public CelluralSpace(int size)
        {
            if(size < 2) throw new ArgumentException("Space size must be more than 1");
            this._space = new Cell[size, size];
        }  

        private CelluralSpace(Cell[,] cells)
        {
            this._space = cells;
        }   

        public void Initialize()
        {
            for (int i = 0; i < this._space.GetLength(0); i++)
            {
                for (int j = 0; j < _space.GetLength(1); j++)
                {
                    _space[i, j] = new Cell(i,j);
                }
            }
        }

        public int GetLength(int i){
            if(i < 0 || i > 1) throw new ArgumentException("Cellural space has only 2 dimentions.")
            return this._space.GetLength(i);
        }

        public Cell GetCell(int x, int y){
            return this._space[x, y];
        }

        public Cell GetCell(int index){
            return this._space[index / _space.Length, index % _space.Length ];
        }

        public CelluralSpace Clone(){
            var newCells = new Cell[this.Size,this.Size];
            for (int i = 0; i < this._space.GetLength(0); i++)
            {
                for (int j = 0; j < _space.GetLength(1); j++)
                {
                    newCells[i, j] = _space[i, j];
                }
            }
            return new CelluralSpace(newCells);
        }

        object ICloneable.Clone(){
            return (object)Clone();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        IEnumerator<Cell> IEnumerable<Cell>.GetEnumerator()
        {
            return (IEnumerator<Cell>)GetEnumerator();
        }
        
    
        public IEnumerator<Cell> GetEnumerator()
        {
            for (int i = 0; i < this._space.GetLength(0); i++)
            {
                for (int j = 0; j < _space.GetLength(1); j++)
                {
                    yield return _space[i, j];
                }
            }
        }
        
        
    }
}
