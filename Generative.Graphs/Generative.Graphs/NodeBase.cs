using System;
using System.Collections.Generic;

namespace Generative.Graphs
{
    public abstract class NodeBase
    {
        public abstract int Id { get; }

        public abstract Dictionary<int, Action> EntranceEdges { get; }

        public abstract Dictionary<int, Action> ExitEdges { get; }

        public virtual bool IsStartingPoint { get { return false; } }

        public virtual bool IsEndingPoint { get { return true; } }
    }
}
