using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinesweeperGame;

namespace MinesweeperGame.Demo
{
    public class MainDemo
    {
        public static void Main()
        {
            MinesInitializer igra = new MinesInitializer();
            igra.PlayMines();
        }
    }
}
