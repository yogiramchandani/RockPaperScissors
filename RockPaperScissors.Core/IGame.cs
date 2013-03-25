namespace RockPaperScissors.Core
{
    public interface IGame
    {
        GameResults CreateGame(PlayerType type);
        GameResults PlayerVsComputer(GameResults game, RPSMove playerMove);
        GameResults ComputerVsComputer(GameResults game);
    }
}