namespace ConsoleApp;

public class ConsolePlayer : Player
{
    public ConsolePlayer(Point startPos) : base(startPos)
    {
    }

    public override MoveDirection GetNextMove()
    {
        while (true)
        {
            Console.WriteLine("\n\n NextMove: ");
            var key = Console.ReadKey().Key;
            var direction = key switch
            {
                ConsoleKey.UpArrow => MoveDirection.Up,
                ConsoleKey.DownArrow => MoveDirection.Down,
                ConsoleKey.LeftArrow => MoveDirection.Left,
                ConsoleKey.RightArrow => MoveDirection.Right,
                ConsoleKey.R => MoveDirection.Rollback,
                _ => MoveDirection.None,
            };

            if (direction != MoveDirection.None)
            {
                return direction;
            }
        }
    }
}