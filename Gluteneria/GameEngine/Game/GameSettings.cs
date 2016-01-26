using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GameEngine.Game.Interfaces;

namespace GameEngine.Game
{
    public class GameSettings:ISettnigs
    {
        public int HumanPlayers { get; set; }
        public int ComputerPlayers { get; set; }
        public int Lives { get; set; }
        public int HowManyApples { get; set; }
        public List<List<int>> WallsMap { get; set; }
        public double ArenaWidth { get; set; }
        public double ArenaHeight { get; set; }

        public List<List<Key>> PlayersControlKeys { get; set; }
    }
}
