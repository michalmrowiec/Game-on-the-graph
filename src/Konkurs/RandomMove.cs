using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konkurs
{
    public class RandomMove
    {
        public static int RandomLegalMove(int token, List<Edge> graph)
        {
            foreach (var node in graph)
            {
                if (token == node.Vertex1)
                    return node.Vertex2;
                if (token == node.Vertex2)
                     return node.Vertex1;
            }
            return 0;
        }
    }
}
