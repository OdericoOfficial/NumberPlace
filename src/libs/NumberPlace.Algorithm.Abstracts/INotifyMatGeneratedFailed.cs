using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Algorithm.Abstracts
{
    public delegate void MatGeneratedFailedEventHandler(Exception ex, int matIndex);

    public interface INotifyMatGeneratedFailed
    {
        event MatGeneratedFailedEventHandler? OnMatGeneratedFailed;
    }
}
