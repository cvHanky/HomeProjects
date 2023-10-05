namespace Snake_like
{
    internal class MainGame
    {
        static void Main(string[] args)
        {
            Snake snake = new Snake();
            snake.MakeBorder();

            Console.ReadKey();
        }
    }
}