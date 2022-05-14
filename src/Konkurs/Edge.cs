using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konkurs
{
    /// <summary>
    /// Edge is the relationship between two vertices in an undirected graph.
    /// </summary>
    public struct Edge
    {
        public Edge(int v1, int v2)
        {
            Vertex1 = v1;
            Vertex2 = v2;
        }

        public int Vertex1 { get; }
        public int Vertex2 { get; }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Edge e1, Edge e2)
        {
            if((e1.Vertex1 == e2.Vertex1 && e1.Vertex2 == e2.Vertex2) || (e1.Vertex1 == e2.Vertex2 && e1.Vertex2 == e2.Vertex1))
                return true;
            return false;
        }

        public static bool operator !=(Edge e1, Edge e2)
        {
            if ((e1.Vertex1 == e2.Vertex1 && e1.Vertex2 == e2.Vertex2) || (e1.Vertex1 == e2.Vertex2 && e1.Vertex2 == e2.Vertex1))
                return false;
            return true;
        }
    }
}
