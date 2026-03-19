using System.Net;
using QueryQuest.Core.Interfaces;

namespace QueryQuest.Infrastructure.Repositories
{
        public class HtmlService : IHtmlService
        {
            public string Decode(string htmlText) => WebUtility.HtmlDecode(htmlText);
        }
}
