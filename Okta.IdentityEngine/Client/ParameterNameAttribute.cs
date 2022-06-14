using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ParameterNameAttribute : Attribute
    {
        public ParameterNameAttribute() { }
        public ParameterNameAttribute(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }
}
