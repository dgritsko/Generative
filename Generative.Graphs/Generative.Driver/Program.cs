using Generative.Graphs;
using Generative.Generator;
using System.Collections.Generic;
using System;

namespace Generative.Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            var tests = Generator.Generator.Generate(new List<NodeBase> {
                new Node1(),
                new Node2(),
                new Node3()
            });

            foreach (var test in tests)
            {
                Console.WriteLine(test.Describe());
            }

            Console.ReadLine();
        }

        class Node1 : NodeBase
        {
            public override bool IsStartingPoint
            {
                get
                {
                    return true;
                }
            }
            public override int Id { get { return 1; } }
            public override Dictionary<int, System.Action> EntranceEdges
            {
                get { return new Dictionary<int,System.Action>(); }
            }
            public override Dictionary<int, System.Action> ExitEdges
            {
                get
                {
                    return new Dictionary<int, System.Action>()
                        {
                           { 2, () => { } },
                           { 3, () => { } }
                        };
                }
            }
        }

        class Node2 : NodeBase
        {
            public override int Id { get { return 2; } }
            public override Dictionary<int, System.Action> EntranceEdges
            {
                get
                {
                    return new Dictionary<int, System.Action>
                    {
                       { 1, () => { } },
                       { 3, null }
                    };
                }
            }
            public override Dictionary<int, System.Action> ExitEdges
            {
                get { return new Dictionary<int, System.Action>(); }
            }
        }

        class Node3 : NodeBase
        {
            public override int Id { get { return 3; } }
            public override Dictionary<int, System.Action> EntranceEdges
            {
                get
                {
                    return new Dictionary<int, System.Action>
                    {
                        { 1, null }
                    };
                }
            }
            public override Dictionary<int, System.Action> ExitEdges
            {
                get
                {
                    return new Dictionary<int, System.Action>
                        {
                            { 2, null }
                        };
                }
            }
        }
    }
}
