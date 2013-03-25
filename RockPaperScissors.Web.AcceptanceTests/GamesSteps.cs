using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Hosting;
using RockPaperScissors.Core;
using RockPaperScissors.Web.Controllers;
using System.Net.Http;
using TechTalk.SpecFlow;
using Xunit;

namespace RockPaperScissors.Web.AcceptanceTests
{
    [Binding]
    public class GamesSteps
    {
        private string gameId;
        private List<object> gameHistory;
        private GamesController gameController;
        private HttpResponseMessage responseMessage;

        [Given(@"I have entered ""(.*)"" into GetGame")]
        public void GivenIHaveEnteredIntoGetGame(string gameTypeString)
        {
            var game = new Game();
            var resultStore = new GameResultStore();
            this.gameController = new GamesController(game, resultStore) { Request = new HttpRequestMessage() };
            this.gameId = this.gameController.Get(gameTypeString);
            gameController.Request.Properties.Add(
                HttpPropertyKeys.HttpConfigurationKey,
                new HttpConfiguration());
        }
        
        [When(@"I send ""(.*)"" to play")]
        public void WhenISendToPlay(string move)
        {
            var response = this.gameController.Post(new GamesController.NewMove{Id = gameId, Move = move});
            response.Content.ReadAsAsync<ExpandoObject>().ContinueWith(
                task => {
                    gameHistory = ((dynamic)task.Result).ResultHistory;
                }).Wait();
        }
        
        [Then(@"the result should have ""(.*)"" item")]
        public void ThenTheResultShouldHaveItem(int expected)
        {
            Assert.Equal(expected, gameHistory.Count);
        }

        [When(@"I send invalid ""(.*)"" to play")]
        public void WhenISendInvalidToPlay(string move)
        {
            this.responseMessage = this.gameController.Post(new GamesController.NewMove { Id = gameId, Move = move });
        }

        [Then(@"expect bad request status code")]
        public void ThenExpectError()
        {
            Assert.Equal(HttpStatusCode.BadRequest, this.responseMessage.StatusCode);
        }
    }
}
