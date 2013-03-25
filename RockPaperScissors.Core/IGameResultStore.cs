using System.Collections.Generic;

namespace RockPaperScissors.Core
{
    public interface IGameResultStore
    {
        GameResults GetResult(string gameId);
        void SaveResult(string gameId, GameResults newResults);
    }

    public class GameResultStore : IGameResultStore
    {
        private Dictionary<string, GameResults> results;
        public GameResultStore()
        {
            results = new Dictionary<string, GameResults>();
        }

        public GameResults GetResult(string gameId)
        {
            GameResults value;
            results.TryGetValue(gameId, out value);
            return value;
        }

        public void SaveResult(string gameId, GameResults newResults)
        {
            if (this.results.ContainsKey(gameId))
            {
                this.results[gameId] = newResults;
                return;
            }
            this.results.Add(gameId, newResults);
        }
    }
}