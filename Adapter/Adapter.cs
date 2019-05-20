using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    /// <summary>
    /// Represents a tree of nodes, each one may contain other nodes (branches)
    /// or data (key-value pair).
    /// </summary>
    interface ITree
    {
        /// <summary>
        /// Creates a named branch (tree node) inside the current tree structure.
        /// </summary>
        /// <param name="name">The name of the new branch.</param>
        /// <returns>The new branch.</returns>
        ITree CreateBranch(string name);

        /// <summary>
        /// Retrieves a sub-branch (tree node) by its name from the current branch.
        /// </summary>
        /// <param name="name">The name of the requested branch.</param>
        /// <returns>The requested sub-branch.</returns>
        ITree GetBranch(string name);

        /// <summary>
        /// Creates a new key-value pair.
        /// </summary>
        /// <param name="key">The new key.</param>
        /// <param name="value">The new value.</param>
        void CreateValue(string key, string value);

        /// <summary>
        /// Retrieves a value by its key.
        /// </summary>
        /// <param name="key">The requested pair's key.</param>
        /// <returns>The requested value.</returns>
        string GetValue(string key);

        /// <summary>
        /// Sets a new value for an existing key.
        /// </summary>
        /// <param name="key">The requested pair's key.</param>
        /// <param name="newValue">The new value.</param>
        void SetValue(string key, string newValue);

        /// <summary>
        /// Serializes the tree into string representation.
        /// </summary>
        /// <param name="prettyPrint">
        /// Determines whether the string should be "beautified" with whitespaces,
        /// new lines, indentation, etc.
        /// </param>
        /// <returns>String representation of the tree object.</returns>
        string ToString(bool prettyPrint);
    }

    // ToDo: if (your age % 2 == 0) implement ITree using JSON, else implement ITree using XML.

    class Program
    {
        /// <summary>
        /// "Client" method: Uses ITree, doesn't know about JSON and XML.
        /// </summary>
        /// <param name="tree">Tree object used by the client.</param>
        static void ManipulateTree(ITree tree)
        {
            Console.WriteLine("Initial tree structure:{0}{1}", Environment.NewLine,
                tree.ToString(true));

            var branch1 = tree.GetBranch("branch1") ?? tree.CreateBranch("branch1");

            tree.CreateValue("key1", "val1");
            branch1.CreateValue("key2", "val2");

            Console.WriteLine("{0}Final tree structure (user-friendly):{0}{1}",
                Environment.NewLine, tree.ToString(true));
            Console.WriteLine("{0}Final tree structure (condensed):{0}{1}",
                Environment.NewLine, tree.ToString(false));
        }

        static void Main(string[] args)
        {
            // ToDo: Construct a JSON/XML object and pass it to ManipulateTree().
        }
    }
}