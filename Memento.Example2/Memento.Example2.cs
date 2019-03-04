using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento.Example2
{
    /// <summary>
    /// Originator.
    /// </summary>
    class Browser
    {
        public void LoadHtml(string path)
        {
            // FileSystem is not aware of the memento's real nature. Need to cast it:
            _html = (HtmlDocument) FileSystem.Load(path);

            // Display HTML in browser window
        }

        public void SaveHtml(string path)
        {
            // Save current window's contents as HTML object

            // FileSystem is not aware of the memento's real nature - saves it as a System.Object:
            FileSystem.Save(path, _html);
        }

        HtmlDocument _html;
    }

    /// <summary>
    /// Memento.
    /// </summary>
    class HtmlDocument
    {
        // Functionality goes here
    }

    /// <summary>
    /// Caretaker.
    /// </summary>
    public class FileSystem
    {
        public static void Save(string path, object contents)
        {
            _fileSystem[path] = contents;
        }

        public static object Load(string path)
        {
            _fileSystem.TryGetValue(path, out object contents);

            return contents;
        }

        /// <summary>
        /// "Saves" data using path + contents.
        /// </summary>
        private static Dictionary<string, object> _fileSystem = new Dictionary<string, object>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Load and save Web browser windows as HTML files
        }
    }
}
