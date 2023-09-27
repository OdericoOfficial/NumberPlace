using NumberPlace.Application.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Application
{
    public static class MatExtensions
    {
        /// <summary>
        /// 更新空白格的值
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="i">行</param>
        /// <param name="j">列</param>
        /// <param name="k">值</param>
        /// <returns>如果该格为定死的则返回false</returns>
        public static bool TryAddOrUpdate(this Mat mat, int i, int j, byte k)
        {
            var exist = i * 9 + j;
            if (mat.Condition[exist] == 0)
            {
                if (!mat.Addition.TryAdd(exist, k))
                    mat.Addition[exist] = k;
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// 提交检查目前的解是否是正确的
        /// </summary>
        /// <param name="mat"></param>
        /// <returns>正解为true</returns>
        public static unsafe bool Submit(this Mat mat, out byte[]? finish)
        {
            finish = null;
            byte* additionCondition = stackalloc byte[324];
            
            foreach (var k in mat.Addition)
            {
                var i = k.Key / 9;
                var j = k.Key % 9;

                var row = i * 9 + 80 + k.Value;
                var col = j * 9 + 161 + k.Value;
                var part = (i / 3 * 3 + j / 3) * 9 + 242 + k.Value;

                additionCondition[k.Key] = 1;
                additionCondition[row] = 1;
                additionCondition[col] = 1;
                additionCondition[part] = 1;
            }

            for (int i = 0; i < 324; i++)
            {
                if ((additionCondition[i] ^ mat.Condition[i]) == 0)
                    return false;
            }

            finish = new byte[81];
            for (int i = 0; i < 81; i++)
            {
                if (mat.Matrix[i] != 0)
                    finish[i] = mat.Addition[i];
                else
                    finish[i] = mat.Matrix[i];
            }

            return true;
        }
    }
}
