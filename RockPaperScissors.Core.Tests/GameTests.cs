using System;
using Xunit;
using Xunit.Extensions;

namespace RockPaperScissors.Core.Tests
{
    public class GameTests
    {
        [Theory, GameTestConventions]
        public void CreateGame_ReturnsUniqueId(Game sut)
        {
            var first = sut.CreateGame(PlayerType.PlayerVsComputer).GameId;
            var second = sut.CreateGame(PlayerType.PlayerVsComputer).GameId;
            Assert.NotEqual(first, second);
        }

        [Theory, GameTestConventions]
        public void ComputerVsComputer_WithRPSPlayerTypeComputerVsPlayer_ReturnsException(Game sut)
        {
            var game = sut.CreateGame(PlayerType.PlayerVsComputer);
            Assert.Throws<ArgumentException>(() => sut.ComputerVsComputer(game));
        }

        [Theory, GameTestConventions]
        public void PlayerVsComputer_WithRPSPlayerTypeComputerVsComputer_ReturnsException(Game sut)
        {
            var game = sut.CreateGame(PlayerType.ComputerVsComputer);
            Assert.Throws<ArgumentException>(()=>sut.PlayerVsComputer(game, RPSMove.Rock));
        }

        [Theory, GameTestConventions]
        public void PlayerVsComputer_With1PlayerInput_Returns1Results(Game sut)
        {
            var game = sut.CreateGame(PlayerType.PlayerVsComputer);
            var results = sut.PlayerVsComputer(game, RPSMove.Rock);
            Assert.Equal(1, results.History.Count);
        }

        [Theory, GameTestConventions]
        public void PlayerVsComputer_With2PlayerInput_Returns2Results(Game sut)
        {
            var game = sut.CreateGame(PlayerType.PlayerVsComputer);
            game = sut.PlayerVsComputer(game, RPSMove.Rock);
            game = sut.PlayerVsComputer(game, RPSMove.Rock);
            Assert.Equal(2, game.History.Count);
        }

        [Theory, GameTestConventions]
        public void ComputerVsComputer_With1Call_Returns1Results(Game sut)
        {
            var game = sut.CreateGame(PlayerType.ComputerVsComputer);
            var results = sut.ComputerVsComputer(game);
            Assert.Equal(1, results.History.Count);
        }

        [Theory, GameTestConventions]
        public void ComputerVsComputer_With2Calls_Returns2Results(Game sut)
        {
            var game = sut.CreateGame(PlayerType.ComputerVsComputer);
            game = sut.ComputerVsComputer(game);
            game = sut.ComputerVsComputer(game);
            Assert.Equal(2, game.History.Count);
        }

        [Theory, GameTestConventions]
        public void GetResult_WherePaperVsPaper_ExpectDraw(Game sut)
        {
            var actual = sut.GetResult(new Tuple<RPSMove, RPSMove>(RPSMove.Paper, RPSMove.Paper));
            Assert.Equal(0, actual);
        }
        
        [Theory, GameTestConventions]
        public void GetResult_WherePaperVsRock_ExpectWin(Game sut)
        {
            var actual = sut.GetResult(new Tuple<RPSMove, RPSMove>(RPSMove.Paper, RPSMove.Rock));
            Assert.Equal(1, actual);
        }

        [Theory, GameTestConventions]
        public void GetResult_WherePaperVsScissors_ExpectLoss(Game sut)
        {
            var actual = sut.GetResult(new Tuple<RPSMove, RPSMove>(RPSMove.Paper, RPSMove.Scissors));
            Assert.Equal(-1, actual);
        }

        [Theory, GameTestConventions]
        public void GetResult_WhereRockVsRock_ExpectDraw(Game sut)
        {
            var actual = sut.GetResult(new Tuple<RPSMove, RPSMove>(RPSMove.Rock, RPSMove.Rock));
            Assert.Equal(0, actual);
        }

        [Theory, GameTestConventions]
        public void GetResult_WhereRockVsPaper_ExpectLoss(Game sut)
        {
            var actual = sut.GetResult(new Tuple<RPSMove, RPSMove>(RPSMove.Rock, RPSMove.Paper));
            Assert.Equal(-1, actual);
        }

        [Theory, GameTestConventions]
        public void GetResult_WhereRockVsScissors_ExpectWin(Game sut)
        {
            var actual = sut.GetResult(new Tuple<RPSMove, RPSMove>(RPSMove.Rock, RPSMove.Scissors));
            Assert.Equal(1, actual);
        }

        [Theory, GameTestConventions]
        public void GetResult_WhereScissorsVsScissors_ExpectDraw(Game sut)
        {
            var actual = sut.GetResult(new Tuple<RPSMove, RPSMove>(RPSMove.Scissors, RPSMove.Scissors));
            Assert.Equal(0, actual);
        }

        [Theory, GameTestConventions]
        public void GetResult_WhereScissorsVsPaper_ExpectWin(Game sut)
        {
            var actual = sut.GetResult(new Tuple<RPSMove, RPSMove>(RPSMove.Scissors, RPSMove.Paper));
            Assert.Equal(1, actual);
        }

        [Theory, GameTestConventions]
        public void GetResult_WhereScissorsVsRock_ExpectLoss(Game sut)
        {
            var actual = sut.GetResult(new Tuple<RPSMove, RPSMove>(RPSMove.Scissors, RPSMove.Rock));
            Assert.Equal(-1, actual);
        }
    }
}
