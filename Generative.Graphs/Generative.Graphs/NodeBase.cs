using System;
using System.Collections.Generic;

namespace Generative.Graphs
{
    public abstract class NodeBase
    {
        public abstract int Id { get; }

        public abstract Dictionary<int, Action> EntranceEdges;

        public abstract Dictionary<int, Action> ExitEdges;
    }
}
