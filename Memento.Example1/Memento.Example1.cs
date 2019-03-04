using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento.Example1
{
    /// <summary>
    /// Originator.
    /// </summary>
    class Browser
    {
        public void LoadHtml(string path)
        {
            var text = FileSystem.Load(path);
            _html = new HtmlDocument(text);

            // Display HTML in browser window
        }

        public void SaveHtml(string path)
        {
            // Save current window's contents as HTML object

            FileSystem.Save(path, _html.ToString());
        }

        HtmlDocument _html;
    }

    /// <summary>
    /// Memento, Originator side. Caretaker sees memento as System.String.
    /// </summary>
    class HtmlDocument
    {
        public HtmlDocument(string html)
        {
            // Convert to HTML tree representation
        }

        // More functionality...
    }

    /// <summary>
    /// Caretaker.
    /// </summary>
    public class FileSystem
    {
        public static void Save(string path, string contents)
        {
            _fileSystem[path] = contents;
        }

        public static string Load(string path)
        {
            _fileSystem.TryGetValue(path, out string contents);

            return contents;
        }

        /// <summary>
        /// "Saves" data using path + contents.
        /// </summary>
        private static Dictionary<string, string> _fileSystem = new Dictionary<string, string>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Load and save Web browser windows as HTML files
        }
    }
}
