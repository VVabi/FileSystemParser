using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemParser
{
    interface TreeNodeInterface
    {
        void prettyPrint();
        long getSize();
    }
    class TreeNode
    {
        public TreeNode(TreeNodeInterface currentNode)
        {
            node     = currentNode;
            children = new  List<TreeNode>();
        }

        public TreeNode(TreeNodeInterface currentNode, List<TreeNode> childrenIn)
        {
            node = currentNode;
            children = childrenIn;
        }
        private List<TreeNode> children;
        TreeNodeInterface node;
        public void printTree(uint offset, int depth) { 
            if (depth < 0) {
                return;
            }

            for (uint ind = 0; ind < offset; ind++)
            {
                Console.Write("\t");
            }
            node.prettyPrint();
            foreach (var childNode in children)
            {
                childNode.printTree(offset + 1, depth - 1);
            }
        }

        public void addChild(TreeNode nextNode)
        {
            children.Add(nextNode);
        }

        public TreeNodeInterface getCurrentNode()
        {
            return node;
        }

    }
}
