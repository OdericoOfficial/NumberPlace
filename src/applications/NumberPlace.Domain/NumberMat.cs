using Godot.Module.Repository.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Domain
{
#nullable disable
    public class NumberMat : Entity<Guid>
    {
        public DateTime CreateTime { get; set; }
        public string OriginMatrixJson { get; set; }
        public string CompleteMatrixJson { get; set; }
    }
}
