using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("KnowledgeRepresentationReasoning.Test")]
namespace KnowledgeRepresentationReasoning.Logic
{
    internal class Tree : ITree
    {
        private List<Vertex> _actualLevel;
        private List<Vertex> _allVertices;

        public Tree()
        {
            _actualLevel = new List<Vertex>();
            _allVertices = new List<Vertex>();
        }
    }
}
