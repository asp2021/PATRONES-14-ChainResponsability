using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainResponsability
{
    class MobilePremium : ISpecification<Mobile>
    {
        public bool IsSatisfied(Mobile item)
        {
            return item.type == Type.Premium;
        }
    }
}
