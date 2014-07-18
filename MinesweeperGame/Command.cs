using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperGame
{ 
    abstract class Command  // http://www.codeproject.com/Articles/15207/Design-Patterns-Command-Pattern
    {
        abstract public void Redo();
        abstract public void Undo();
    }
}
