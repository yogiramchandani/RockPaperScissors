using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace RockPaperScissors.Core.Tests
{
    public class GameResultStoreTests
    {
        [Theory, GameTestConventions]
        public void Get_WhenIdDoesNotExist_ExpectANullResult(GameResultStore sut)
        {
            var actual = sut.GetResult("InvalidId");
            Assert.Null(actual);
        }

        [Theory, GameTestConventions]
        public void Get_WithValid_ExpectNonNullResult(GameResultStore sut)
        {
            sut.SaveResult("ValidId", new GameResults());
            var actual = sut.GetResult("ValidId");
            Assert.NotNull(actual);
        }

        [Theory, GameTestConventions]
        public void Get_With2SavesForSameGame_ExpectResultUpdated(GameResultStore sut)
        {
            sut.SaveResult("ValidId", new GameResults{History = new Queue<GameHistory>()});
            var firstActual = sut.GetResult("ValidId");
            Assert.Equal(0, firstActual.History.Count);

            firstActual.History.Enqueue(new GameHistory());
            sut.SaveResult("ValidId", firstActual);
            var secondActual = sut.GetResult("ValidId");
            Assert.Equal(1, secondActual.History.Count);
        }
    }
}
