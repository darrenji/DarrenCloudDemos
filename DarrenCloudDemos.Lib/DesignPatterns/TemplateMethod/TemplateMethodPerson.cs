using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns
{
    public class TemplateMethodPerson : IComparable<TemplateMethodPerson>
    {
        public TemplateMethodPerson(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public string Name { get; set; }
        public int Age { get; set; }

        /// <summary>
        /// 把比较的逻辑放在一个接口里，这里是IComparalble<T>，接口里定义的方法就是模板方法Template Method
        /// 模板方法Template Method只有到了子类中才明确定义
        /// 也就是把控制放在了接口中的Template Method, 把逻辑放在了子类
        /// 通常子类会定义一个抽象基类
        /// 站在需求的角度是为了让子类扩展
        /// 站在逻辑和控制分离的角度，接口中定义模板方法用来控制，子类实现逻辑
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo([AllowNull] TemplateMethodPerson other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return $"Name:{Name}\tAge:{Age}";
        }
    }
}
