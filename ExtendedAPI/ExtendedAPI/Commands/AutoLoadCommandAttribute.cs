using System;

namespace ExtendedAPI.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AutoLoadCommandAttribute : Attribute
    {

    }
}
