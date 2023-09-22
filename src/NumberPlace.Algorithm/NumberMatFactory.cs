using NumberPlace.Algorithm.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NumberPlace.Algorithm
{
    public class NumberMatFactory : INumberMatFactory
    {
        /// <summary>
        /// 异步初始化数独，带CancellationToken
        /// </summary>
        /// <param name="initCount"></param>
        /// <param name="token"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<(int[,] Mat, UInt328 Condition)> GenerateMatAsync(int initCount, CancellationToken token)
            => Task.Run(() => GenerateMat(initCount), token);

        /// <summary>
        /// 异步初始化数独
        /// </summary>
        /// <param name="initCount"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<(int[,] Mat, UInt328 Condition)> GenerateMatAsync(int initCount)
            => Task.Run(() => GenerateMat(initCount));

        /// <summary>
        /// 初始化数独，使用值类型、指针的方式减轻gc压力，使用在栈上分配空间规避跨线程问题并加速数值读取
        /// </summary>
        /// <param name="initCount"></param>
        public unsafe (int[,] Mat, UInt328 Condition) GenerateMat(int initCount)
        {
            int[,] mat = new int[9, 9];    
            int cnt = 0;
            var random = new Random();

            UInt328 condition;
            byte* ptr = (byte*)&condition;
            byte* existPtr = stackalloc byte[81];
            byte* restPtr = stackalloc byte[9];

            while (cnt < initCount)
            {
                // 随机生成(i,j)
                var exist = random.Next(0, ResolveExistCondition(ptr, existPtr));
                var i = existPtr[exist] / 10;
                var j = existPtr[exist] % 10;

                // 随机生成k
                var k = restPtr[random.Next(0, ResolveRestCondition(ptr, restPtr, i, j))];

                // 设置集合位置条件为1
                int position = i * 9 + j;
                int col = i * 9 + 80 + k;
                int row = j * 9 + 161 + k;
                int part = (i / 3 * 3 + j / 3) * 9 + 242 + k;

                byte* positionPtr = ptr + position / 8;
                byte positionTemp = (byte)(1 << (position % 8));
                byte* colPtr = ptr + col / 8;
                byte colTemp = (byte)(1 << (col % 8));
                byte* rowPtr = ptr + row / 8;
                byte rowTemp = (byte)(1 << (row % 8));
                byte* partPtr = ptr + part / 8;
                byte partTemp = (byte)(1 << (part % 8));

                *positionPtr |= positionTemp;
                *colPtr |= colTemp;
                *rowPtr |= rowTemp;
                *partPtr |= partTemp;
                
                // 填入9x9数独
                mat[i, j] = k;
                cnt++;
            }

            return (mat, condition);
        }

        /// <summary>
        /// 从UInt328集合获取还有多少空每填，并把没填的空按照 ij 的形式填充到exist数组里，方便随机生成
        /// </summary>
        /// <param name="ptr">集合指针</param>
        /// <param name="existPtr">exist数组指针</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe int ResolveExistCondition(byte* ptr, byte* existPtr)
        {
            int x = 0, y = 0;
            int len = 0;
            for (int i = 0; i < 10; i++, ptr++)
            {
                byte temp = 1;
                for (int j = 0; j < 8; j++, x += y / 8, y = (y + 1) % 9, temp <<= 1)
                {
                    if ((*ptr & temp) == 0)
                        existPtr[len++] = (byte)(x * 10 + y);
                }
            }
            if ((*ptr & 1) == 0)
                existPtr[len++] = 88;

            return len;
        }

        /// <summary>
        /// 从UInt328集合获取随机生成的空白位置还有多少个数能填，并把能填的数存到rest数组中，方便随机生成
        /// </summary>
        /// <param name="ptr">集合指针</param>
        /// <param name="restPtr">rest数组指针</param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe int ResolveRestCondition(byte* ptr, byte* restPtr, int i, int j)
        {
            int len = 0;
            
            for (int iter = 1; iter <= 9; iter++)
            {
                int col = i * 9 + 80 + iter;
                int row = j * 9 + 161 + iter;
                int part = (i / 3 * 3 + j / 3) * 9 + 242 + iter;
                byte* colPtr = ptr + col / 8;
                byte colTemp = (byte)(1 << (col % 8));
                byte* rowPtr = ptr + row / 8;
                byte rowTemp = (byte)(1 << (row % 8));
                byte* partPtr = ptr + part / 8;
                byte partTemp = (byte)(1 << (part % 8));

                if ((*colPtr & colTemp) == 0
                    && (*rowPtr & rowTemp) == 0
                        && (*partPtr & partTemp) == 0)
                    restPtr[len++] = (byte)iter;
            }

            return len;
        }
    }
}
