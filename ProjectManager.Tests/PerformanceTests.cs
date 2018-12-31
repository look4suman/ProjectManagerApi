using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NBench;
using ProjectManager.Api.Controllers;
using ProjectManager.Business;

namespace ProjectManagerApp.Test
{
    [ExcludeFromCodeCoverage]
    public class PerformanceTests
    {
        private Counter _counter;
        private ApplicationController _controller;
        private int TaskId;
        private int UserId;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
            _controller = new ApplicationController();
            TaskId = new Application().GetTasks().FirstOrDefault().Task_ID;
            UserId = new Application().GetUsers().FirstOrDefault().User_ID;
        }

        [PerfBenchmark(Description = "Get All tasks.",
        NumberOfIterations = 500, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void Getask()
        {
            _controller.GetTasks();
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Get All Projects.",
        NumberOfIterations = 500, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void GetProjects()
        {
            _controller.GetProject();
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Get All Users.",
        NumberOfIterations = 500, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void GetUsers()
        {
            _controller.GetUser();
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Get specific task.",
        NumberOfIterations = 500, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void GetSpecificTask()
        {
            _controller.GetSpecificTask(TaskId);
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Get specific user.",
         NumberOfIterations = 500, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void GetSpecificProject()
        {
            _controller.GetUser(UserId);
            _counter.Increment();
        }

        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }

    }

}
