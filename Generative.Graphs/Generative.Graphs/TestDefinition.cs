using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Generative.Graphs
{
    public class TestDefinition
    {
        public List<NodeBase> Nodes { get; set; }

        public TestDefinition(IEnumerable<NodeBase> nodes)
        {
            Nodes = nodes.ToList();
        }

        public string Describe()
        {
            return string.Join(" -> ", Nodes.Select(n => n.Id));
        }
    }
}
