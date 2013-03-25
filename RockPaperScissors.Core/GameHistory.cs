using System;

namespace RockPaperScissors.Core
{
    public class GameHistory
    {
        public Tuple<RPSMove, RPSMove> Selection { get; set; }
        public int Result { get; set; }
    }
}