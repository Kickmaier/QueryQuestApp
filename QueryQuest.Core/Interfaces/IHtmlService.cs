using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryQuest.Core.Interfaces
{
    public interface IHtmlService
    {
        string Decode(string htmlText);
    }
}
