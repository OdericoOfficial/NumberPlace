using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Algorithm.Abstracts
{
    public interface IMatFactory : INotifyMatGeneratedFailed
    {
        /// <summary>
        /// 异步初始化数独，带CancellationToken
        /// </summary>
        /// <param name="initCount">数独上初始的数字个数</param>
        /// <param name="matIndex">数独编号</param>
        /// <param name="token"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Task<Mat> GenerateMatAsync(int initCount, int matIndex, CancellationToken token);
        /// <summary>
        /// 异步初始化数独
        /// </summary>
        /// <param name="initCount"></param>
        /// <param name="matIndex">数独编号</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Task<Mat> GenerateMatAsync(int initCount, int matIndex);
        /// <summary>
        /// 初始化数独，使用栈分配、值类型、指针的方式减轻gc压力并加速数值读取
        /// </summary>
        /// <param name="initCount"></param>
        /// <param name="matIndex">数独编号</param>
        Mat GenerateMat(int initCount, int matIndex);
    }
}
