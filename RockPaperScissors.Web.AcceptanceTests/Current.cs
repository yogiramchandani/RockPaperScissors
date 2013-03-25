using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RockPaperScissors.Web.AcceptanceTests
{
    public static class Current<T> where T : class
    {
        internal static T Value
        {
            get { return ScenarioContext.Current.ContainsKey(typeof(T).FullName) ? ScenarioContext.Current[typeof(T).FullName] as T : null; }
            set { ScenarioContext.Current[typeof(T).FullName] = value; }
        }
    }
}
