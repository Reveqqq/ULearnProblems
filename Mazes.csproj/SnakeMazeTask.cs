namespace Mazes
{
	public static class SnakeMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			while (true)
			{
				MoveRight(robot, width);
				MoveDown(robot, height);
				MoveLeft(robot, width);
				if (robot.Finished)
					break;
				else
					MoveDown(robot, height);
            }
		}

		private static void MoveDown(Robot robot, int height)
		{
			for (int j = 0; j < 2; j++)
				robot.MoveTo(Direction.Down);
		}

		private static void MoveRight(Robot robot, int width)
		{
			for (int i = 0; i <= width - 4; i++)
				robot.MoveTo(Direction.Right);
		}

        private static void MoveLeft(Robot robot, int width)
        {
            for (int i = 0; i <= width - 4; i++)
                robot.MoveTo(Direction.Left);
        }
    }
}