using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using QueryQuest.Core.Interfaces;

namespace QueryQuest.Infrastructure.Repositories
{
        public class HtmlService : IHtmlService
        {
            public string Decode(string htmlText) => WebUtility.HtmlDecode(htmlText);
        }
}
