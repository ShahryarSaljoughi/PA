using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Model
{
    public class PAIndex
    {
        public Guid Id { get; set; }
        public virtual double Value { get; set; }
        public virtual Guid TimeBoxId { get; set; }
        public virtual TimeBox TimeBox { get; set; }
        public virtual Guid SubfieldId { get; set; }
        public virtual Subfield Subfield { get; set; }
        public PAIndex(TimeBox timebox, Subfield subfield)
        {
            TimeBox = timebox;
            Subfield = subfield;
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// DONT USE THIS CTOR.
        /// 
        /// Only be used by ef core. see:
        /// https://stackoverflow.com/questions/55285417/no-suitable-constructor-found-for-entity-type-myimage
        /// </summary>
        public PAIndex()
        {
            TimeBox = new TimeBox();
            Subfield = new Subfield();
        }

    }
}
