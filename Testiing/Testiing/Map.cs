using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testiing
{
    public class Map
    {
        private int row;
        private int column;
        private string name;
        public int[,] map;
        public Map(string name, int row, int col) {
            this.name = name;
            this.row = row;
            this.column = col;
            this.map = new int[row, col];
        }
    }
}
