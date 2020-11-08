using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib
{
    /// <summary>
    /// 函数式编程Demo
    /// </summary>
   public  class FunctionProgrammingDemo
    {
        #region 高阶函数
        /// <summary>
        /// 计算物业费
        /// 这里是高阶函数，因为函数或方法中使用了函数或方法作为参数
        /// </summary>
        /// <param name="price">单价</param>
        /// <param name="length">长</param>
        /// <param name="width">宽</param>
        /// <param name="calculateArea">计算面积的方法</param>
        /// <returns></returns>
        public decimal CalculatePropertyFee(decimal price, int length, int width, Func<int, int, decimal> calculateArea)
        {
            return price * calculateArea(length, width);
        }

        /// <summary>
        /// 计算面积，为个人
        /// </summary>
        /// <returns></returns>
        public Func<int, int, decimal> CalculateForPersonal()
        {
            return (length, width) => length * width;
        }

        /// <summary>
        /// 计算面积，为商用
        /// </summary>
        /// <returns></returns>
        public Func<int, int, decimal> CalculateForBusiness()
        {
            return (length, width) => length * width * 1.3m;
        }
        #endregion

        #region 惰性求值

        /// <summary>
        /// 模拟剩余的内存
        /// </summary>
        /// <returns></returns>
        public double AvailableMemory()
        {
            return 0.8;
        }

        /// <summary>
        /// 模拟剩余的数量
        /// </summary>
        /// <returns></returns>
        public int AvailableCount()
        {
            return 10;
        }

        /// <summary>
        /// 是否进行下一步
        /// 根据上面两个方法的结果
        /// 当使用AvailableMemory方法和AvailableCount方法返回值，肯定会执行方法，这是严格执行策略
        /// </summary>
        /// <param name="availableMemory">剩余的内存</param>
        /// <param name="availbleCount">剩余的数量</param>
        /// <returns></returns>
        public bool IsNextStepGo(double availableMemory, int availbleCount)
        {
            if(availableMemory  < 0.7 && availbleCount < 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 如果参数是函数就变成了惰性求值
        /// 如果一个函数没有满足条件，另一个函数就不会执行
        /// </summary>
        /// <param name="availableMemory"></param>
        /// <param name="availableCount"></param>
        /// <returns></returns>
        public bool IsNextStepGoLazy(Func<double> availableMemory, Func<int> availableCount)
        {
            if(availableMemory() < 0.7 && availableCount() < 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 函数柯里化 Curry,让多个函数参数变成一个
        public Func<int, int , int ,int> AddThreeNumber()
        {
            return (x, y, z) => x + y + z;
        }

        public Func<int, Func<int, Func<int, int>>> AddThreeNumberCurring()
        {
            Func<int, Func<int, Func<int, int>>> addCurring = x => y => z => x + y + z;
            return addCurring;
        }
        #endregion

    }
}
