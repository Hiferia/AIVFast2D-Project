using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    abstract class Component : IUpdatable
    {
        protected GameObject owner;

        public bool IsActive;

        public Component(GameObject owner)
        {
            this.owner = owner;
        }

        public virtual void Update()
        {

        }
    }
}
