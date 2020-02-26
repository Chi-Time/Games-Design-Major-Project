using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[AttributeUsage (AttributeTargets.Class, AllowMultiple = true)]
public class DependsOn : Attribute
{
    public Type Component0 { get; }
    public Type Component1 { get; }
    public Type Component2 { get; }

    public DependsOn (Type component)
    {
        this.Component0 = component;
    }

    public DependsOn (Type component0, Type component1)
    {
        this.Component0 = component0;
        this.Component1 = component1;
    }

    public DependsOn (Type component0, Type component1, Type component2)
    {
        this.Component0 = component0;
        this.Component1 = component1;
        this.Component2 = component2;
    }
}

//namespace Assets.Code.Classes.Utilities
//{

//}
