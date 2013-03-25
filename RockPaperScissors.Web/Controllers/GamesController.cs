using System.Net;
using System.Net.Http;
using RockPaperScissors.Core;
using System;
using System.Web.Http;

namespace RockPaperScissors.Web.Controllers
{
    public class GamesController : ApiController
    {
        private IGame game { get; set; }
        private IGameResultStore resultStore { get; set; }

        public GamesController(IGame game, IGameResultStore resultStore)
        {
            this.game = game;
            this.resultStore = resultStore;
        }

        public string Get(string id)
        {
            PlayerType playerType;
            Enum.TryParse(id, true, out playerType);
            var gameResult = game.CreateGame(playerType);
            resultStore.SaveResult(gameResult.GameId, gameResult);
            return gameResult.GameId;
        }

        public HttpResponseMessage Post(NewMove newMove)
        {
            var result = this.resultStore.GetResult(newMove.Id);
            if (result.GameType == PlayerType.PlayerVsComputer)
            {
                RPSMove moveType;
                if (Enum.TryParse(newMove.Move, true, out moveType))
                {
                    result = game.PlayerVsComputer(result, moveType);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                result = game.ComputerVsComputer(result);
            }
            this.resultStore.SaveResult(result.GameId, result);
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                new
                {
                    result.GameId,
                    GameType = result.GameType.ToString(),
                    ResultHistory = result.History.ToArray()
                });
        }

        public class NewMove
        {
            public string Id { get; set; }
            public string Move { get; set; }
        }
    }
}