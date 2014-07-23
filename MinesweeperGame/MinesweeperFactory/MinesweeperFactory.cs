using System;
using MinesweeperGame.Interfaces;

namespace MinesweeperGame
{
    /// <summary>
    /// IMinesweeperFactory implementation.
    /// </summary>
    public class MinesweeperFactory : IMinesweeperFactory
    {
        public IDrawer GetDrawer()
        {
            return new ConsoleDrawer();
        }

        public IUserInput GetCommandProvider()
        {
            return new ConsoleInput();
        }

        public IMinesGenerator GetMinesGenerator()
        {
            return new MinesGenerator();
        }

        public IScoreBoard GetScoreBoard()
        {
            return new ScoreBoard(MediatorExtensions.MAIN_DATAFILE_PATH);
        }
    }
}
