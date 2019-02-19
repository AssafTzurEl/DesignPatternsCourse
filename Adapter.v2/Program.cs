using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adapter.v2
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

    class JsonTree : ITree
    {
        private readonly JObject _jObject;

        public JsonTree(string json)
        {
            _jObject = JObject.Parse(json);
        }

        // No requirement for root tree name
        public JsonTree()
        {
            _jObject = new JObject();
        }

        private JsonTree(JObject obj)
        {
            _jObject = obj;
        }

        public ITree CreateBranch(string name)
        {
            if (GetBranch(name) != null)
                throw new ArgumentException();

            var jObj = new JObject();

            _jObject.Add(name, jObj);

            return new JsonTree(jObj);
        }

        public ITree GetBranch(string name) =>
            _jObject.GetValue(name) is JObject jToken ? new JsonTree(jToken) : null;

        // Interface doesn't define what happens if key already exists
        public void CreateValue(string key, string value)
        {
            if (!_jObject.ContainsKey(key))
            {
                _jObject.Add(new JProperty(key, value));
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public string GetValue(string key)
        {
            return _jObject.GetValue(key)?.ToString();
        }

        public void SetValue(string key, string value)
        {
            if (!_jObject.ContainsKey(key))
                throw new ArgumentException();

            _jObject[key] = value;
        }

        public string ToString(bool prettyPrint)
        {
            var formatting = prettyPrint ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject(_jObject, formatting);
        }
    }

    // Multiple options: Tree names as element names (not using XML attributes),
    // or - as in the following implementation - reliance on XML attributes (with added
    // bonus of scheme capabilities)
    class XmlTree : ITree
    {
        private XElement _element;

        // Distinct "branch" and "root" elements - pros and cons
        public XmlTree() : this(new XElement("root"))
        { }

        private XmlTree(XElement element)
        {
            _element = element;
        }

        public ITree CreateBranch(string name)
        {
            XElement branch = new XElement("branch");
            branch.SetAttributeValue("name", name);
            _element.Add(branch);
            return new XmlTree(branch);
        }

        public void CreateValue(string key, string value)
        {
            if (key.Equals("name", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException("keys named 'name' are not supported");
            }

            _element.Add(new XAttribute(key, value));
        }

        public ITree GetBranch(string name)
        {
            XElement branch =
                _element
                .Elements("branch")
                .SingleOrDefault(child => child.Attribute("name").Value == name);

            return branch == null ? null : new XmlTree(branch);

        }

        public string GetValue(string key)
        {
            return _element.Attribute(key).Value;
        }

        public void SetValue(string key, string value)
        {
            _element.SetAttributeValue(key, value);
        }

        public string ToString(bool prettyPrint)
        {
            SaveOptions options
                = prettyPrint ? SaveOptions.None : SaveOptions.DisableFormatting;
            return _element.ToString(options);
        }
    }

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
            XmlTree xml = new XmlTree();
            ManipulateTree(xml);

            JsonTree json = new JsonTree();
            ManipulateTree(json);
        }
    }
}