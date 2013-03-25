using System.Collections.Generic;

namespace RockPaperScissors.Core
{
    public class GameResults
    {
        public string GameId { get; set; }
        public PlayerType GameType { get; set; }
        public Queue<GameHistory> History { get; set; }
    }
}