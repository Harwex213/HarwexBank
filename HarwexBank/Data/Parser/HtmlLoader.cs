using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HarwexBank.Parser
{
    public class HtmlLoader
    {
        readonly HttpClient _client;
        readonly string _url;

        public HtmlLoader(IParserSettings settings)
        {
            _client = new HttpClient();
            _url = $"{settings.BaseUrl}";
        }

        public async Task<string> GetSource()
        {
            var response = await _client.GetAsync(_url);
            string source = null;

            if(response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}