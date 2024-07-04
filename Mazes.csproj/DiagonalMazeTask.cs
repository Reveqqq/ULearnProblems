namespace Mazes
{
	public static class DiagonalMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            while (true)
            {
                if (robot.Finished)
                    break;
                robot.MoveTo(Direction.Down);
                robot.MoveTo(Direction.Right);
            }
        }
	}
}