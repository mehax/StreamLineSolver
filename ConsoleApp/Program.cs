using ConsoleApp;

const int BOARD_ROW = 10;
const int BOARD_COL = 9;
const int PLAYER_ROW = 9;
const int PLAYER_COL = 0;
const int FINAL_ROW = 1;
const int FINAL_COL = 5;

Board board;
Player player;
Game game;
BoardPrinter printer;
GameSolver gameSolver;

board = new Board(BOARD_ROW, BOARD_COL);
player = new ConsolePlayer(new Point(PLAYER_ROW, PLAYER_COL));
game = new Game(board, player);
printer = new BoardPrinter(board, player);
gameSolver = new GameSolver(game);

board.WithCell(FINAL_ROW, FINAL_COL, CellType.Finish)
    .WithCell(0, 0, CellType.Block)
    .WithCell(0, 2, CellType.Block)
    .WithCell(1, 4, CellType.Block)
    .WithCell(5, 5, CellType.Block)
    .WithCell(6, 5, CellType.Block)
    .WithCell(7, 2, CellType.Block)
    .WithCell(9, 5, CellType.Block)
    .WithCell(1, 1, CellType.Pause)
    .WithCell(7, 8, CellType.Pause)
    .WithCell(0, 4, CellType.Key)
    .WithCell(3, 0, CellType.Locker)
    .WithCell(4, 0, CellType.Locker)
    .WithCell(1, 7, CellType.Locker)
    .WithCell(5, 8, CellType.Locker)
    .WithCell(9, 7, CellType.Locker)
    .WithCell(6, 2, CellType.DeadEnd)
    .WithCell(5, 6, CellType.DeadEnd)
    .WithCell(3, 1, CellType.Hole)
    ;

// while (true)
// {
//     printer.Print();
//     if (game.IsWin())
//     {
//         Console.WriteLine("GG");
//         return;
//     }
//     
//     var direction = player.GetNextMove();
//     switch (direction)
//     {
//         case MoveDirection.Rollback:
//             game.Rollback();
//             break;
//         
//         default:
//             game.Move(direction);
//             break;
//     }
// }

printer.Print();

var solution = gameSolver.Solve();

Console.WriteLine("COMPLETE:");
foreach (var moveDirection in solution)
{
    Console.WriteLine(moveDirection);
}
