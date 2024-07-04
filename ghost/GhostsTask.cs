using System;
using System.Text;

namespace hashes;

static class Extension
{
    public static void DoMagic(this byte[] x)
    {
        x[0]++;
    }
}

public class GhostsTask : 
	IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
	IMagic
{
    private static byte[] content = new byte[] { 1, 2, 3 };
    private Document document = new Document("SomeTitle", Encoding.ASCII, content);
    private static Vector vector1 = new Vector(10, 10);
    public Vector Vector = new Vector(1, 1);
    private Segment segment = new Segment(vector1, new Vector(1, 5));
    private static DateTime date = new DateTime(2010, 5, 15);
    private Cat cat = new Cat("Alex", "Blue", date);
    private Robot robot = new Robot("123");
    

    public void DoMagic()
	{
        content.DoMagic();
        Vector.Add(new Vector(2, 2));
        segment = new Segment (vector1.Add(new Vector(12, 22)), new Vector(2,2));
        Robot.BatteryCapacity++;
        cat.Rename("TomHardi");
    }

    Document IFactory<Document>.Create()
    {
        return document;
    }


    Vector IFactory<Vector>.Create()
	{
        return Vector;
	}

	Segment IFactory<Segment>.Create()
	{
        return segment;
	}

    Cat IFactory<Cat>.Create()
    {
        return cat;
    }

    Robot IFactory<Robot>.Create()
    {
        return robot;
    }
}