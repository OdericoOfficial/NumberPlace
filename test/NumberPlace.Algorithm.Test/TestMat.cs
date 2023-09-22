using NumberPlace.Algorithm.Abstracts;
using Serilog.Events;
using Serilog;
using System.Text;

namespace NumberPlace.Algorithm.Test
{
    public class TestMat
    {
        [Fact]
        public async Task TestNineMat()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

            int initCount = 63;
            INumberMatFactory factory = new NumberMatFactory();
            Task<(int[,] Mat, UInt328 Condition)>[] tasks = new Task<(int[,] Mat, UInt328 Condition)>[9];
            for (int i = 0; i < 9; i++)
                tasks[i] = factory.GenerateMatAsync(initCount);
            int cnt = 0;
            foreach (var task in await Task.WhenAll(tasks))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine($"-------{cnt}-------");
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                        builder.Append($"{task.Mat[i, j]} ");
                    builder.AppendLine();
                }
                builder.AppendLine("------------------------------------------------");

                Log.Information(builder.ToString());
                cnt++;
            }
        }
    }
}