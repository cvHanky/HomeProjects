namespace Snake_like
{
    internal class MainGame
    {
        static void Main(string[] args)
        {
            //Console.BufferHeight = 300;
            //Console.BufferWidth = 300;
            //Console.WindowHeight = 200;
            //Console.WindowWidth = 200;

            Snake snake = new Snake();
            //snake.MakeBorder();
            snake.MakeBorder(true);
            snake.TestMovement();

            Console.ReadKey();
        }
    }
}