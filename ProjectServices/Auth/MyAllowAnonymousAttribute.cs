using System;

namespace ProjectServices.Auth
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MyAllowAnonymousAttribute : Attribute
    { }
}