using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Application.Abstracts
{
    public delegate void MatTimerUpdatedEventHandler(Guid guid, DateTime updateTime);

    public interface INotifyMatTimerUpdated
    {
        event MatTimerUpdatedEventHandler? OnTimerUpdated;
    }
}
