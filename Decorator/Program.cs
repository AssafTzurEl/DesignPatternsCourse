using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    interface IHtmlElement
    {
        string Compute();
    }

    class TextElement : IHtmlElement
    {
        public TextElement(string text)
        {
            _text = text.Replace("<", "&lt;").Replace(">", "&gt;");
        }

        public string Compute()
        {
            return _text;
        }

        private readonly string _text;
    }

    class NewLineElement : IHtmlElement
    {
        public string Compute()
        {
            return "<br/>";
        }
    }

    class ImageElement : IHtmlElement
    {
        public ImageElement(string imagePath, string altText = null)
        {
            var altTag = string.IsNullOrWhiteSpace(altText) ?
                string.Empty : $"alt=\"{altText}\" ";

            _imgElement = $"<img src=\"{imagePath}\" {altTag}/>";
        }

        public string Compute()
        {
            return _imgElement;
        }

        private readonly string _imgElement;
    }

    abstract class DecoratorElement : IHtmlElement
    {
        public DecoratorElement(IHtmlElement htmlElement)
        {
            _htmlElement = htmlElement;
        }

        public abstract string Compute();

        protected IHtmlElement _htmlElement;
    }

    class BoldDecorator : DecoratorElement
    {
        public BoldDecorator(IHtmlElement htmlElement)
            : base(htmlElement)
        { }

        public override string Compute()
        {
            return $"<b>{_htmlElement.Compute()}</b>";
        }
    }

    // Also: ItalicDecorator, UnderlinedDecorator (ins tag), ParagraphDecorator, etc.

    class LinkDecorator : DecoratorElement
    {
        public LinkDecorator(IHtmlElement htmlElement, Uri url)
            : base(htmlElement)
        {
            _url = url;
        }

        public override string Compute()
        {
            return $"<a href=\"{_url}>{_htmlElement.Compute()}</a>";
        }

        private Uri _url;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var text = new TextElement("Hello, CodeValue!");
            var bold = new BoldDecorator(text);
            IHtmlElement htmlElement = new LinkDecorator(bold, new Uri("http://codevalue.net"));

            Console.WriteLine(htmlElement.Compute());
        }
    }
}
