using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            var watch = new Stopwatch();
            task.Run();
            watch.Restart();
            for (int i = 0; i < repetitionCount; i++)
            {
                task.Run();
            }
            watch.Stop();
            return watch.Elapsed.TotalMilliseconds / repetitionCount;
        }
    }

    public class StringCreationTask1 : ITask
    {
        public void Run()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < 10000; i++)
                builder.Append("a");
            builder.ToString();
        }
    }

    public class StringCreationTask2 : ITask
    {
        public void Run()
        {
            new string('a', 10000);
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            int count = 10000;
            Benchmark benchmark1 = new Benchmark();
            Benchmark benchmark2 = new Benchmark();
            var time1 = benchmark1.MeasureDurationInMs(new StringCreationTask1(), count);
            var time2 = benchmark2.MeasureDurationInMs(new StringCreationTask2(), count);
            Assert.Less(time2, time1);
        }
    }
}