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
            //snake.MakeBorder(false);
            //snake.MakeBorder(true);
            //snake.TestMovement();
            do
            {
                Console.Clear();
                snake.MakeBorder(false);
                snake.RunSnake();
            } while (snake.playAgain == true);
        }
    }
}