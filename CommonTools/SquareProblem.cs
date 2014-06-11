using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.CommonTools
{
    public class SquareProblem : Problem
    {
        public SquareProblem( uint width )
            : base(width, width*width)
        {
            int boxOffset = (int)Math.Round(Math.Sqrt(width),0);
            bool isSquare = boxOffset*boxOffset == nbrOfSymbols;
            // add row constraints
            // add col constraints
            // add "box" constraints
            for (int i = 0; i < width; i++)
            {
                List<int> rc = new List<int>();
                List<int> cc = new List<int>();
                List<int> bc = new List<int>();
                for (int j = 0; j < width; j++)
                {
                    rc.Add((int)(i * width + j));
                    cc.Add((int)(j * width + i));
                    bc.Add( (int)(
                        // get to the topleft for the current (i) box
                        width * boxOffset * (i / boxOffset) +
                        boxOffset * (i % boxOffset) +
                        // get to the right loc for the current (j) elt in that box
                        width * (j / boxOffset) +
                        (j % boxOffset)
                        ) );
                }
                constraints.Add(rc);
                constraints.Add(cc);
                if (isSquare)
                    constraints.Add(bc);
            }
        }
    }
}
