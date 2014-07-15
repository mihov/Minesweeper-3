using System;
using MinesweeperGame.Interfaces;
using MinesweeperGame.MineGenerator;
using MinesweeperGame.ScoresBoard;


namespace MinesweeperGame.Demo.MinesweeperFactory
{
    /// <summary>
    /// IMinesweeperFactory implementation.
    /// </summary>
    class MinesweeperFactory : IMinesweeperFactory
    {
        public IDrawer GetDrawer()
        {
            return new ConsoleDrawer.ConsoleDrawer();
        }

        public IUserInput GetCommandProvider()
        {
            return new ConsoleInput.ConsoleInput();
        }

        public IMinesGenerator GetMinesGenerator()
        {
            return new MinesGenerator();
        }

        public IScoreBoard GetScoreBoard()
        {
            return new ScoreBoard();
        }
    }
}
