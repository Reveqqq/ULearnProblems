using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            if (Game.KeyPressed == System.Windows.Forms.Keys.Up && y - 1 >= 0
                && !(Game.Map[x, y - 1] is Sack))
                return new CreatureCommand() { DeltaX = 0, DeltaY = -1, TransformTo = null };
            else if (Game.KeyPressed == System.Windows.Forms.Keys.Right
                && x + 1 < Game.MapWidth && !(Game.Map[x + 1, y] is Sack))
                return new CreatureCommand() { DeltaX = 1, DeltaY = 0, TransformTo = null };
            else if (Game.KeyPressed == System.Windows.Forms.Keys.Down
                && y + 1 < Game.MapHeight && !(Game.Map[x, y + 1] is Sack))
                return new CreatureCommand() { DeltaX = 0, DeltaY = 1, TransformTo = null };
            else if (Game.KeyPressed == System.Windows.Forms.Keys.Left
                && x - 1 >= 0 && !(Game.Map[x - 1, y] is Sack))
                return new CreatureCommand() { DeltaX = -1, DeltaY = 0, TransformTo = null };
            else
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = null };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject.GetImageFileName() == "Sack.png" || conflictedObject.GetImageFileName() == "Monster.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Sack : ICreature
    {
        public int Counter;
        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight - 1)
            {
                var map = Game.Map[x, y + 1];
                if (map == null || (Counter > 0 && map.ToString() == "Digger.Player"))
                {
                    Counter++;
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                }
            }
            if (Counter > 1)
            {
                Counter = 0;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }
            Counter = 0;
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.GetImageFileName() == "Digger.png") ;
            Game.Scores += 10;
            return true;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject.GetImageFileName() == "Sack.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }
    }
}
