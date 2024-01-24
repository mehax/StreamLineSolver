namespace ConsoleApp;

public abstract class Player
{
    public List<Point> Points { get; } = new();
    public int CurrentLayer { get; set; } = 1;
    public List<(List<Point>, MoveDirection direction)> History { get; } = new();
    public MoveDirection? LastDirection { get; set; }
    public Point InitHead { get; }

    public Player(Point startPos)
    {
        InitHead = startPos;
        Points.Add(startPos);
    }

    public bool Contains(Point pt, bool checkLayer = true)
    {
        return Points.Where(x => checkLayer || x.Layer == CurrentLayer).Contains(pt);
    }

    public Point Head => Points.Last();

    public void Add(Point pt)
    {
        Points.Add(pt);
    }

    public abstract MoveDirection GetNextMove();
}