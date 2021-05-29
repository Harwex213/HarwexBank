using System;
using System.Windows;
using AngleSharp.Html.Parser;

namespace HarwexBank.Parser
{
    public class ParserWorker<T> where T : class
    {
        IParserSettings _parserSettings;
        HtmlLoader _loader;
        
        public IParser<T> Parser { get; set; }

        public IParserSettings Settings
        {
            get => _parserSettings;
            set
            {
                _parserSettings = value;
                _loader = new HtmlLoader(value);
            }
        }

        public bool IsActive { get; private set; }
        
        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public ParserWorker(IParser<T> parser)
        {
            Parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            _parserSettings = parserSettings;
        }

        public void Start()
        {
            IsActive = true;
            Worker();
        }

        public void Abort()
        {
            IsActive = false;
        }

        private async void Worker()
        {
            try
            {
                if (!IsActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await _loader.GetSource();
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);

                var result = Parser.Parse(document);

                OnNewData?.Invoke(this, result);

                OnCompleted?.Invoke(this);
                IsActive = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Курсы валют не были получены");
            }

        }

    }
}