using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAClient.ObjectModel
{
    /// <summary>
    /// فصل
    /// </summary>
    public class Subfield  
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// رشته
        /// </summary>
        public string Field { get; set; }

    }
}
