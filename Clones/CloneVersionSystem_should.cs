﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Clones;

[TestFixture]
public class CloneVersionSystem_should
{
	[Test]
	public void Learn()
	{
		var clone1 = Execute("learn 1 45", "check 1").Single();
		Assert.AreEqual("45", clone1);
	}

	[Test]
	public void RollbackToBasic()
	{
		var clone1 = Execute("learn 1 45", "rollback 1", "check 1").Single();
		Assert.AreEqual("basic", clone1);
	}

	[Test]
	public void RollbackToPreviousProgram()
	{
		var clone1 = Execute("learn 1 45", "learn 1 100500", "rollback 1", "check 1").Single();
		Assert.AreEqual("45", clone1);
	}

	[Test]
	public void RelearnAfterRollback()
	{
		var clone1 = Execute("learn 1 45", "rollback 1", "relearn 1", "check 1").Single();
		Assert.AreEqual("45", clone1);
	}

	[Test]
	public void CloneBasic()
	{
		var clone2 = Execute("clone 1", "check 2").Single();
		Assert.AreEqual("basic", clone2);
	}

	[Test]
    public void DoubleCloneTest()
    {
        var clone2 = Execute("clone 3", "clone 3", "learn 5 50", "learn 5 10", "check 5" ).Single();
        Assert.AreEqual("10", clone2);
    }

    [Test]
	public void CloneLearned()
	{
		var clone2 = Execute("learn 1 42", "learn 1 50", "learn 1 100" , "clone 1", "check 2").Single();
		Assert.AreEqual("100", clone2);
	}

	[Test]
	public void LearnClone_DontChangeOriginal()
	{
		var res = Execute("learn 1 42", "clone 1", "learn 2 100500", "check 1", "check 2");
		Assert.AreEqual(new []{ "42", "100500"}, res);
	}

	[Test]
	public void RollbackClone_DontChangeOriginal()
	{
		var res = Execute("learn 1 42", "clone 1", "rollback 2", "check 1", "check 2");
		Assert.AreEqual(new[] { "42", "basic" }, res);
	}

	[Test]
	public void ExecuteSample()
	{
		var res = Execute("learn 1 5",
			"learn 1 7",
			"rollback 1",
			"check 1",
			"clone 1",
			"relearn 2",
			"check 2",
			"rollback 1",
			"check 1");
		Assert.AreEqual(new[] { "5", "7", "basic" }, res);
	}

    [Test]
    public void ExecuteSample2()
    {
        var res = Execute("learn 1 111",
            "learn 1 222",
            "learn 1 333",
            "rollback 1",
            "rollback 1",
            "clone 1",
            "relearn 2",
            "relearn 2",
            "check 1",
            "check 2");
        Assert.AreEqual(new[] { "111", "333" }, res);
    }

    [Test]
    public void ExecuteSample3()
    {
        var res = Execute("learn 1 1",
            "learn 1 2",
            "learn 1 3",
            "rollback 1",
            "rollback 1",
            "clone 1",
            "clone 2",
            "relearn 2",
            "relearn 2",
            "check 1",
            "check 2",
            "relearn 3",
            "check 3",
            "rollback 3",
            "check 3",
            "relearn 3",
            "relearn 3",
            "check 3");
        Assert.AreEqual(new[] { "1", "3", "2", "1", "3" }, res);
    }

    private List<string> Execute(params string[] queries)
	{
		var cvs = Factory.CreateCVS();
		var results = new List<string>();
		foreach (var command in queries)
		{
			var result = cvs.Execute(command);
			if (result != null) results.Add(result);
		}
		return results;
	}
}