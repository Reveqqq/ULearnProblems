using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            foreach(var x in  Constants.FieldCounts)
            {
                var time1 = benchmark.MeasureDurationInMs(new StructArrayCreationTask(x), repetitionsCount);
                var time2 = benchmark.MeasureDurationInMs(new ClassArrayCreationTask(x), repetitionsCount);
                classesTimes.Add(new ExperimentResult(x, time2/x));
                structuresTimes.Add(new ExperimentResult(x, time1/x));
            }
            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var x in Constants.FieldCounts)
            {
                var time1 = benchmark.MeasureDurationInMs(new MethodCallWithStructArgumentTask(x), repetitionsCount);
                var time2 = benchmark.MeasureDurationInMs(new MethodCallWithClassArgumentTask(x), repetitionsCount);
                classesTimes.Add(new ExperimentResult(x, time2 / x));
                structuresTimes.Add(new ExperimentResult(x, time1 / x));
            }

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}