

namespace NumberPlace.Application.Abstracts
{
    public interface INumberMatService : INotifyMatGenerated, INotifyMatTimerUpdated, IDisposable
    {
        Mat this[Guid guid] { get; }
        Task<Guid> GenerateMatAsync(int initCount);
        Task<bool> SubmitAsync(Guid guid);
    }
}
