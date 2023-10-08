namespace Snake_like
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.BufferHeight = 300;
            //Console.BufferWidth = 300;
            //Console.WindowHeight = 200;
            //Console.WindowWidth = 200;

            Game game = new Game();
            //snake.MakeBorder(false);
            //snake.MakeBorder(true);
            //snake.TestMovement();
            do
            {
                Console.Clear();
                game.MakeBorder(true);
                game.RunSnake();
            } while (game.playAgain == true);
        }
    }
}