using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Model
{
    /// <summary>
    /// فصل
    /// </summary>
    public class Subfield
    {
        public virtual Guid Id { get; set; }
        /// <summary>
        /// see this: https://github.com/ShahryarSaljoughi/PA/wiki/%D8%AA%D8%B5%D9%85%DB%8C%D9%85%D8%A7%D8%AA-%D8%B7%D8%B1%D8%A7%D8%AD%DB%8C-%D9%86%D8%B1%D9%85-%D8%A7%D9%81%D8%B2%D8%A7%D8%B1
        /// </summary>
        public virtual string Number { get; set; }
        public virtual string Title { get; set; }
        /// <summary>
        /// رشته
        /// </summary>
        public virtual string Field { get; set; }
        public string Text => $"فصل {Number} - {Title}";
        public override string ToString()
        {
            return Text;
        }

    }
}
