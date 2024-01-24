namespace ConsoleApp;

public class Board
{
    private CellType[,] _board;
    public bool IsLocked { get; set; } = true;

    public Board(int rows, int cols)
    {
        _board = new CellType[rows, cols];
    }

    public Board WithCell(int row, int col, CellType type)
    {
        _board[row, col] = type;
        return this;
    }

    public CellType[,] GetBoard => _board;

    public IEnumerable<Point> Teleports
    {
        get
        {
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    if (_board[row, col] == CellType.Teleport)
                    {
                        yield return new Point(row, col);
                    }
                }
            }
        }
    }
    
    public Point Finish
    {
        get
        {
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    if (_board[row, col] == CellType.Finish)
                    {
                        return new Point(row, col);
                    }
                }
            }

            throw new Exception();
        }
    }
}