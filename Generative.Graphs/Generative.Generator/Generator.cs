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

            foreach (var node in nodes.Where(n => n.IsStartingPoint))
            {
                testDefinitions.AddRange(GetTestDefinitions(node, nodes, new List<NodeBase>()));
            }

            return testDefinitions;
        }

        private static IEnumerable<TestDefinition> GetTestDefinitions(NodeBase initial, IEnumerable<NodeBase> allNodes, List<NodeBase> currentNodeList)
        {
            var testDefinitions = new List<TestDefinition>();
            
            currentNodeList.Add(initial);
            
            if (initial.IsEndingPoint)
                testDefinitions.Add(new TestDefinition(currentNodeList));

            if (!initial.IsEndingPoint && initial.ExitEdges.Count == 0)
                throw new Exception("Node is not an Ending point, but has no exit edges defined");


            foreach (var nextNodeId in initial.ExitEdges)
            {
                var nextNode = allNodes.FirstOrDefault(n => n.Id == nextNodeId.Key && n.EntranceEdges.Any(ee => ee.Key == initial.Id));

                if (nextNode == null)
                    throw new Exception("Exit edge specified without matching entrance edge");

                testDefinitions.AddRange(GetTestDefinitions(nextNode, allNodes, new List<NodeBase>(currentNodeList)));
            }

            return testDefinitions;
        }
    }
}
