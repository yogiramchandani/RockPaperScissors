using System;
using System.Collections.Generic;

namespace RockPaperScissors.Core
{
    public class Game : IGame
    {
        private Random generator;

        public Game()
        {
            generator = new Random();
        }

        public GameResults CreateGame(PlayerType type)
        {
            return new GameResults { GameId = Guid.NewGuid().ToString(), GameType = type, History = new Queue<GameHistory>()};
        }

        public GameResults PlayerVsComputer(GameResults game, RPSMove playerMove)
        {
            if (game.GameType != PlayerType.PlayerVsComputer)
            {
                throw new ArgumentException("Invalid game type", "game.GameType");
            }

            var computerMove = GetMove();
            return GetResultAndAddToHistory(game, playerMove, computerMove);
        }

        public GameResults ComputerVsComputer(GameResults game)
        {
            if (game.GameType != PlayerType.ComputerVsComputer)
            {
                throw new ArgumentException("Invalid game type", "game.GameType");
            }
            var computer1Move = GetMove();
            var computer2Move = GetMove();
            return GetResultAndAddToHistory(game, computer1Move, computer2Move);
        }

        public int GetResult(Tuple<RPSMove, RPSMove> moves)
        {
            if (moves.Item1 == moves.Item2)
            {
                return 0;
            }
            if ((moves.Item1 == RPSMove.Paper && moves.Item2 == RPSMove.Rock) ||
                (moves.Item1 == RPSMove.Rock && moves.Item2 == RPSMove.Scissors) ||
                (moves.Item1 == RPSMove.Scissors && moves.Item2 == RPSMove.Paper))
            {
                return 1;
            }

            return -1;
        }

        public RPSMove GetMove()
        {
            var next = this.generator.Next(1, 4);
            return (RPSMove) next;
        }

        private GameResults GetResultAndAddToHistory(GameResults game, RPSMove player1Move, RPSMove player2Move)
        {
            var selection = new Tuple<RPSMove, RPSMove>(player1Move, player2Move);
            var result = GetResult(selection);
            game.History.Enqueue(new GameHistory {Selection = selection, Result = result});
            return game;
        }
    }
}