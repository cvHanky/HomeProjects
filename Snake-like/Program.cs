namespace Snake_like
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.BufferHeight = 300;
            //Console.BufferWidth = 300;
            //Console.WindowHeight = 200;
            //Console.WindowWidth = 200;           this doesn't work yet.

            //    #### Initializing game ####

            Game game = new Game();
            game.CurrentHighScore = 0;

            //    #### Game loop ####

            do
            {
                Console.Clear();
                game.MakeBorder(false);
                game.RunSnake();
            } while (game.playAgain == true);

            //    #### End of game ####
            Console.ReadKey();
        }
    }
}