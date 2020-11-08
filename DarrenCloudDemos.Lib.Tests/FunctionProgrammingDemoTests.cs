using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DarrenCloudDemos.Lib.Tests
{
    public class FunctionProgrammingDemoTests
    {

        /// <summary>
        /// 计算物业费
        /// </summary>
        /// <param name="price">单价</param>
        /// <param name="lengh">长</param>
        /// <param name="width">宽</param>
        /// <param name="expected">结果</param>
        [Theory]
        [InlineData(10, 2, 3, 60)]
        public void CalculatePropertyFee_ForPersonal(decimal price, int lengh, int width, decimal expected)
        {
            //Arrange
            var calculator = new FunctionProgrammingDemo();

            //Act
            var result = calculator.CalculatePropertyFee(price, lengh, width, calculator.CalculateForPersonal());

            //Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 计算物业费
        /// </summary>
        /// <param name="price">单价</param>
        /// <param name="lengh">长</param>
        /// <param name="width">宽</param>
        /// <param name="expected">结果</param>
        [Theory]
        [InlineData(10, 2, 3, 78)]
        public void CalculatePropertyFee_ForBusiness(decimal price, int lengh, int width, decimal expected)
        {
            //Arrange
            var calculator = new FunctionProgrammingDemo();

            //Act
            var result = calculator.CalculatePropertyFee(price, lengh, width, calculator.CalculateForBusiness());

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddTreeNumberTest()
        {
            //Arrange
            var demo = new FunctionProgrammingDemo();

            //Act
            var result1 = demo.AddThreeNumber()(1, 2, 3);
            var result2 = demo.AddThreeNumberCurring()(1);
            var result3 = result2(2);
            var result4 = result3(3);

            //Assert
            Assert.Equal(6, result1);
            Assert.Equal(6, result4);
        }
    }
}
