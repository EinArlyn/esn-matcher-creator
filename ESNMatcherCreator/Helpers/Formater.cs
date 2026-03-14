using ESNMatcherCreator.Models;
using System.Collections.ObjectModel;

namespace ESNMatcherCreator.Helpers
{
    public class Formater
    {
        #region Fields
        private readonly ObservableCollection<MatcherModel> matchers;
        #endregion

        #region Constructors
        public Formater(ObservableCollection<MatcherModel> items) => matchers = new ObservableCollection<MatcherModel>(items);
        #endregion

        #region Properties
        public string Get
        {
            get
            {
                string comment = $"\t\t\t<!-- {Ticker} matching from {LP} -->";
                string script = $"\t\t\t<Item MatcherSymbolId=\"{TickerId}\" LPSymbolIds=\"{LpId}\" />";
                return $"{comment}\n{script}";
            }
        }

        private string Ticker { get => matchers[0].Ticker; }

        private string TickerId { get => matchers[0].TickerId.ToString(); }

        private string LP
        {
            get
            {
                string lps = "";
                foreach (var item in matchers)
                {
                    lps += lps.Length == 0 ? item.LP : $", {item.LP}";
                }
                return lps;
            }
        }

        private string LpId
        {
            get
            {
                string ids = "";
                foreach (var item in matchers)
                {
                    ids += ids.Length == 0 ? item.LpId : $",{item.LpId}";
                }
                return ids;
            }
        }
        #endregion
    }
}
