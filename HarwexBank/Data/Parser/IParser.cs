using AngleSharp.Html.Dom;

namespace HarwexBank.Parser
{
    public interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}