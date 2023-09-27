using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Application.Abstracts
{
    public delegate void MatGeneratedEventHandler(Mat mat);
    public interface INotifyMatGenerated
    {
        event MatGeneratedEventHandler? MatGenerated;
    }
}
