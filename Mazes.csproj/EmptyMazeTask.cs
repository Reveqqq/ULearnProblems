namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            for (int i = 0; i <= width - 4; i++)
                robot.MoveTo(Direction.Right);

            for (int j = 0; j <= height - 4; j++)
                robot.MoveTo(Direction.Down);
        }
	}
}