using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemParser
{
    class FileNode : TreeNodeInterface
    {
        String filename;
        long size;
        public FileNode(String fname, long sz)
        {
            filename    = fname;
            size        = sz;
        }

        public long getSize()
        {
            return size;
        }
        public void prettyPrint()
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(filename + " " + size);
            Console.ForegroundColor = original;
        }
    }

    class DirectoryNode : TreeNodeInterface
    {
        String filename;
        long size;
        public DirectoryNode(String fname)
        {
            filename = fname;
            size = 0;
        }
        public void prettyPrint()
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(filename + " " + size);
            Console.ForegroundColor = original;
        }
        public long getSize()
        {
            return size;
        }

        public void updateSize(List<TreeNode> children)
        {
            size = 0;
            foreach (var c in children)
            {
                size += c.getCurrentNode().getSize();
            }
        }
    }
    class Program
    {
        static TreeNode parseFileTree(String filePath, String directoryName)
        {
            var nodeContent = new DirectoryNode(directoryName);
            var children = new List<TreeNode>();

            var info = new DirectoryInfo(filePath);
            var files = info.GetFiles();
            

            foreach (var f in files)
            {
                if (File.Exists(f.FullName))
                {
                    try
                    {
                        TreeNode leaveNode = new TreeNode(new FileNode(f.Name, f.Length));
                        children.Add(leaveNode);
                    } catch (Exception e)
                    {
                        
                    }
                }
            }
           

            var dirs = info.GetDirectories();

            foreach (var d in dirs) {
                if (Directory.Exists(d.FullName))
                {
                    try
                    {
                        children.Add(parseFileTree(d.FullName, d.Name));
                    } catch (Exception e)
                    {

                    }
                }
            }
            nodeContent.updateSize(children);
            return new TreeNode(nodeContent, children);
        }
        static void Main(string[] args)
        {
            TreeNode s = parseFileTree("C:\\Users", "C:\\Users");
            s.printTree(0, 3);
        }
    }
}
