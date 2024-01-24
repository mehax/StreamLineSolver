namespace ConsoleApp;

public class Game
{
    private Board _board;
    private Player _player;

    public Game(Board board, Player player)
    {
        _board = board;
        _player = player;
    }

    public void Move(MoveDirection direction)
    {
        var session = new List<Point>();
        var oldDirection = direction;

        while (true)
        {
            var ptDir = direction switch
            {
                MoveDirection.Up => new Point(-1, 0),
                MoveDirection.Down => new Point(1, 0),
                MoveDirection.Left => new Point(0, -1),
                MoveDirection.Right => new Point(0, 1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            
            var pt = new Point(_player.Head.Row + ptDir.Row, _player.Head.Col + ptDir.Col);
            var (canPosition, canAdvance) = CanMove(direction);
            if (!canPosition)
            {
                break;
            }
            
            var cell = _board.GetBoard[pt.Row, pt.Col];
            if (cell == CellType.Hole)
            {
                _player.CurrentLayer++;
            }

            var newPt = pt with { Layer = _player.CurrentLayer };
            _player.Add(newPt);
            session.Add(newPt);

            if (cell == CellType.ShiftUp)
            {
                direction = MoveDirection.Up;
            }

            if (cell == CellType.ShiftDown)
            {
                direction = MoveDirection.Down;
            }

            if (cell == CellType.ShiftLeft)
            {
                direction = MoveDirection.Left;
            }

            if (cell == CellType.ShiftRight)
            {
                direction = MoveDirection.Right;
            }

            if (cell == CellType.Teleport)
            {
                var nextPt = _board.Teleports.First(t => t != pt)! with { Layer = _player.CurrentLayer};
                _player.Add(nextPt);
                session.Add(nextPt);
            }

            if (cell == CellType.Key)
            {
                _board.IsLocked = false;
            }

            _player.LastDirection = direction;
            
            if (!canAdvance)
            {
                break;
            }
        }

        if (session.Any())
        {
            _player.History.Add((session, oldDirection));
        }
    }

    public void Rollback()
    {
        if (!_player.History.Any())
        {
            return;
        }

        var lastSession = _player.History.Last();
        _player.History.RemoveAt(_player.History.Count - 1);
        foreach (var pt in lastSession.Item1)
        {
            _player.Points.Remove(pt);

            if (_board.GetBoard[pt.Row, pt.Col] == CellType.Key)
            {
                _board.IsLocked = true;
            }
        }

        _player.LastDirection = _player.History.Any() ? _player.History.Last().direction : null;
        _player.CurrentLayer = _player.History.Any() ? _player.History.Last().Item1.Last().Layer : 1;
    }

    public (bool canPosition, bool canAdvance) CanMove(MoveDirection direction)
    {
        var opposites = new Dictionary<MoveDirection, MoveDirection>()
        {
            { MoveDirection.Up, MoveDirection.Down },
            { MoveDirection.Down, MoveDirection.Up },
            { MoveDirection.Left, MoveDirection.Right },
            { MoveDirection.Right, MoveDirection.Left },
        };
        
        if (opposites[direction] == _player.LastDirection)
        {
            return (false, false);
        }
        
        var currentCell = _board.GetBoard[_player.Head.Row, _player.Head.Col];

        if (currentCell == CellType.DeadEnd)
        {
            return (false, false);
        }
        
        var ptDir = direction switch
        {
            MoveDirection.Up => new Point(-1, 0),
            MoveDirection.Down => new Point(1, 0),
            MoveDirection.Left => new Point(0, -1),
            MoveDirection.Right => new Point(0, 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
        
        var pt = new Point(_player.Head.Row + ptDir.Row, _player.Head.Col + ptDir.Col, _player.CurrentLayer);

        if (pt.Row < 0 || pt.Row >= _board.GetBoard.GetLength(0))
        {
            return (false, false);
        }
        
        if (pt.Col < 0 || pt.Col >= _board.GetBoard.GetLength(1))
        {
            return (false, false);
        }

        if (_player.Contains(pt))
        {
            return (false, false);
        }
        
        var cell = _board.GetBoard[pt.Row, pt.Col];
        
        if (cell == CellType.Block)
        {
            return (false, false);
        }

        if (cell == CellType.Pause)
        {
            return (true, false);
        }

        if (cell == CellType.Finish)
        {
            return (true, false);
        }

        if (cell == CellType.DeadEnd)
        {
            return (true, false);
        }

        if (cell == CellType.Hole)
        {
            return (true, true);
        }

        if (cell == CellType.Locker && _board.IsLocked)
        {
            return (false, false);
        }
        
        return (true, true);
    }

    public bool IsWin()
    {
        return new Point(_player.Head.Row, _player.Head.Col) == _board.Finish;
    }
}