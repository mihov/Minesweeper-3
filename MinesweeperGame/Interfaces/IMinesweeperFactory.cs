using System;

namespace MinesweeperGame.Interfaces
{
    public interface IMinesweeperFactory
    {
        /// <summary>
        /// Returns UI drawer.
        /// </summary>
        /// <returns>IDrawer implementation.</returns>
        IDrawer GetDrawer();

        /// <summary>
        /// Returns random mines generator.
        /// </summary>
        /// <returns>IMinesGenerator implementation.</returns>
        IMinesGenerator GetMinesGenerator();

        /// <summary>
        /// Returns score board manager.
        /// </summary>
        /// <returns>IScoreBoard implementation.</returns>
        IScoreBoard GetScoreBoard();
    }
}
