using System.Linq;
using System.Collections.Generic;
using System;
using Generative.Graphs;

namespace Generative.Generator
{
    public static class Generator
    {
        public static IEnumerable<TestDefinition> Generate(IEnumerable<NodeBase> nodes)
        {
            if (nodes == null)
                return null;

            var nodeIds = nodes.Select(n => n.Id);
            var distinctNodeIds = nodeIds.Distinct();
            
            if (nodeIds.Count() != distinctNodeIds.Count())
                throw new ArgumentException("Duplicate Node IDs detected");

            if (nodes.Any(n => n.EntranceEdges.Count == 0 && n.ExitEdges.Count == 0))
            {
                // TODO: Log warning
            }

            if (nodes.Any(n => n.EntranceEdges.Any(ee => !nodes.Any(n2 => n2.Id == ee.Key))))
                throw new ArgumentException("Entrance edge found with ID, but no node with matching ID found");

            if (nodes.Any(n => n.ExitEdges.Any(ee => !nodes.Any(n2 => n2.Id == ee.Key))))
                throw new ArgumentException("Exit edge found with ID, but no node with matching ID found");

            var testDefinitions = new List<TestDefinition>();

            foreach (var node in nodes)
            {
                testDefinitions.AddRange(GetTestDefinitions(node, nodes.Except(new [] { node })));
            }

            return testDefinitions;
        }

        private static IEnumerable<TestDefinition> GetTestDefinitions(NodeBase startingNodes, IEnumerable<NodeBase> otherNodes)
        {
            throw new NotImplementedException();
        }
    }
}
