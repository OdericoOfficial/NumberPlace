using Godot.Module.DependencyInjection;
using Godot.Module.Repository.Abstracts;
using NumberPlace.Application.Abstracts;
using NumberPlace.Domain;
using System.Text.Json;

namespace NumberPlace.Application
{
    [Singleton<INumberMatService>]
    internal class NumberMatService : INumberMatService
    {
        internal class MatTimer
        {
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }
            public CancellationTokenSource Source { get; } = new CancellationTokenSource();    
        }

        private readonly IMatFactory _factory;
        private readonly IRepository<NumberMat, Guid> _repository;
        private readonly IDictionary<Guid, Mat> _mats
            = new Dictionary<Guid, Mat>();  
        private readonly IDictionary<Guid, MatTimer> _timer
            = new Dictionary<Guid, MatTimer>();
        private bool disposedValue;

        public event MatGeneratedEventHandler? MatGenerated;
        public event MatTimerUpdatedEventHandler? OnTimerUpdated;

        public Mat this[Guid guid]
            => _mats[guid];

        public NumberMatService(IMatFactory factory, IRepository<NumberMat, Guid> repository)
        {
            _factory = factory;
            _repository = repository;
        }

        public async Task<Guid> GenerateMatAsync(int initCount)
        {
            Mat mat = await _factory.GenerateMatAsync(initCount);
            MatGenerated?.Invoke(mat);
            Guid guid = Guid.NewGuid();
            _mats.Add(guid, mat);
            return guid;
        }

        public bool StartTimer(Guid guid)
        {
            if (_mats.TryGetValue(guid, out Mat? mat) 
                && !_timer.ContainsKey(guid))
            {
                var timer = new MatTimer();
                var token = timer.Source.Token;
                token.Register(() => timer.FinishTime = DateTime.UtcNow);

                timer.StartTime = DateTime.UtcNow;
                Task.Run(() =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        OnTimerUpdated?.Invoke(guid, DateTime.UtcNow);
                        Thread.Sleep(1000);
                    }
                }, token);
                _timer.Add(guid, timer);
                return true;
            }

            return false;
        }

        public bool EarlyStopTime(Guid guid)
        {
            if (_mats.TryGetValue(guid, out Mat? mat)
                && _timer.TryGetValue(guid, out MatTimer? timer))
            {
                timer.Source.Cancel();
                timer.Source.Dispose();
                _timer.Remove(guid);
                return true;
            }
            
            return false;
        }

        public async Task<bool> SubmitAsync(Guid guid)
        {
            if (_mats.TryGetValue(guid, out Mat? mat)
                && mat.Submit(out byte[]? finish)
                    && _timer.TryGetValue(guid, out MatTimer? source))
            {
                source.Source.Cancel();
                source.Source.Dispose();

                await _repository.AddAsync(new NumberMat
                {
                    Id = guid,
                    OriginMatrixJson = JsonSerializer.Serialize(mat.Matrix),
                    CompleteMatrixJson = JsonSerializer.Serialize(finish),
                    StartTime = source.StartTime,
                    FinishTime = source.FinishTime
                });

                await _repository.EnsureChangesAsync();
            
                return true;
            }

            return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var source in _timer.Values)
                        source.Source.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
