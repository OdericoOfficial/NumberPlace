using NumberPlace.Algorithm.Abstracts;
using Serilog.Events;
using Serilog;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;

namespace NumberPlace.Algorithm.Test
{
    public class TestMat
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ConfigLog()
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
        }

        [Fact]
        public void TestMatWithLog()
        {
            ConfigLog();
            IMatFactory factory = new MatFactory();
            var mat = factory.GenerateMat(10, 0);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    builder.Append($"{mat[i, j]} ");
                }
                builder.AppendLine();
            }

            Log.Information(builder.ToString());
        }

        [Fact]
        public async Task TestNineMatWithLogAsync()
        {
            ConfigLog();
            IMatFactory factory = new MatFactory();
            var tasks = new Task<Mat>[9];
            for (int i = 0; i < 9; i++) 
                tasks[i] = factory.GenerateMatAsync(10, i);

            foreach (var task in await Task.WhenAll(tasks))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        builder.Append($"{task[i, j]} ");
                    }
                    builder.AppendLine();
                }

                Log.Information(builder.ToString());
            }
        }

        [Fact]
        public async Task TestNineMatAsync()
        {
            IMatFactory factory = new MatFactory();
            var tasks = new Task<Mat>[9];
            for (int i = 0; i < 9; i++)
                tasks[i] = factory.GenerateMatAsync(17, i);

            await Task.WhenAll(tasks);
        }
    }
}