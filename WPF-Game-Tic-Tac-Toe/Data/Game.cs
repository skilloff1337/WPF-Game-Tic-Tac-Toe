namespace WPF_Game_Tic_Tac_Toe.Data;

public class Game
{
    public int AllPlayedGames { get; set; } = 0;
    public int FirstPlayerWins { get; set; } = 0;
    public int SecondPlayerWins { get; set; } = 0;
    public int GameTurnNumber { get; set; } = 1;
}