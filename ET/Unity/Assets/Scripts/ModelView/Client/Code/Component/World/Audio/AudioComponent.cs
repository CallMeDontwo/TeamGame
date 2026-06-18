using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class AudioComponent : Entity, IAwake,IDestroy
    {
        public bool isBGM { get; set; } = true;
        public bool isEffect { get; set; } = true;
    }
}
