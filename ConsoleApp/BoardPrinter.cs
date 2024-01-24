namespace ConsoleApp;

public class BoardPrinter
{
    private readonly Board _board;
    private readonly Player _player;

    public BoardPrinter(Board board, Player player)
    {
        _board = board;
        _player = player;
    }

    public void Print()
    {
        Console.Clear();
        for (var col = 0; col < _board.GetBoard.GetLength(1) + 2; col++)
        {
            Console.Write("=");
        }

        Console.WriteLine();
        
        for (var row = 0; row < _board.GetBoard.GetLength(0); row++)
        {
            Console.Write("|");
            for (var col = 0; col < _board.GetBoard.GetLength(1); col++)
            {
                var c = _board.GetBoard[row, col] switch
                {
                    CellType.Block => 'B',
                    CellType.Pause => 'P',
                    CellType.Hole => 'H',
                    CellType.DeadEnd => 'X',
                    CellType.Finish => 'F',
                    CellType.Teleport => 'T',
                    CellType.Key => 'K',
                    CellType.Locker => 'L',
                    CellType.ShiftUp => '\u02c4',
                    CellType.ShiftDown => '\u02c5',
                    CellType.ShiftLeft => '\u02c2',
                    CellType.ShiftRight => '\u02c3',
                    _ => '·',
                };

                if (c == '·')
                {
                    var pt = new Point(row, col);
                    if (_player.Head with {Layer = 1} == pt)
                    {
                        c = 'O';
                    }
                    else if (_player.Contains(pt, false))
                    {
                        c = 'o';
                    }
                }
                
                Console.Write($"{c}");
            }
            
            Console.WriteLine("|");
        }
        
        for (var col = 0; col < _board.GetBoard.GetLength(1) + 2; col++)
        {
            Console.Write("=");
        }

        Console.WriteLine();
    }
}