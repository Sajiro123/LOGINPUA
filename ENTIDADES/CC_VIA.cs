using System;
using Dapper.Contrib.Extensions;

namespace ENTIDADES
{
    public class CC_VIA
    {
        [ExplicitKey]
        public int ID_VIA { get; set; }
        public string NOMBRE { get; set; }
    }
}