namespace ConsoleApp;

public class GameSolver
{
    private readonly Game _game;
    private readonly List<MoveDirection> _solution = new();
    private bool _isFinished = false;

    private readonly MoveDirection[] _allMoves =
    {
        MoveDirection.Up,
        MoveDirection.Down,
        MoveDirection.Left,
        MoveDirection.Right,
    };

    public GameSolver(Game game)
    {
        _game = game;
    }

    public List<MoveDirection> Solve()
    {
        Rec();

        return _solution;
    }

    private void Rec()
    {
        if (_isFinished)
        {
            return;
        }

        foreach (var move in _allMoves)
        {
            if (!_game.CanMove(move).canPosition)
            {
                continue;
            }

            _solution.Add(move);
            _game.Move(move);

            if (_game.IsWin())
            {
                _isFinished = true;
                return;
            }

            Rec();
            if (_isFinished)
            {
                return;
            }

            _game.Rollback();
            _solution.RemoveAt(_solution.Count - 1);
        }
    }
}