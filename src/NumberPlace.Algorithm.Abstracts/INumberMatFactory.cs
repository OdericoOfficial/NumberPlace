using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Algorithm.Abstracts
{
    public interface INumberMatFactory
    {
        /// <summary>
        /// 异步初始化数独，带CancellationToken
        /// </summary>
        /// <param name="initCount"></param>
        /// <param name="token"></param>
        Task<(int[,] Mat, UInt328 Condition)> GenerateMatAsync(int initCount, CancellationToken token);
        /// <summary>
        /// 异步初始化数独
        /// </summary>
        /// <param name="initCount"></param>
        Task<(int[,] Mat, UInt328 Condition)> GenerateMatAsync(int initCount);
        /// <summary>
        /// 初始化数独，使用栈分配、值类型、指针的方式减轻gc压力并加速数值读取
        /// </summary>
        /// <param name="initCount"></param>
        (int[,] Mat, UInt328 Condition) GenerateMat(int initCount);
    }
}
