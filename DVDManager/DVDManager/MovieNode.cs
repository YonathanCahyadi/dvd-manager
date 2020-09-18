using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDManager {
    /// <summary>
    /// This Class is used by Movie Collection Binary Tree data structure.
    /// Every Movie Node containt a data, which is populated by either null value or Movie Class.
    /// The Left and Right side on this Movie Node, is populated by another Movie Node. 
    /// </summary>
    class MovieNode {

        /// <summary>
        /// Containing the Data of the Movie Node.
        /// This data is consisted of Movie Class
        /// </summary>
        public Movie data { get; set; }

        /// <summary>
        /// The Left child of this Movie Node.
        /// </summary>
        public MovieNode left { get; set; }

        /// <summary>
        /// The Right child of this Movie Node.
        /// </summary>
        public MovieNode right { get; set; }

        /// <summary>
        /// Constructor of Movie Node Class.
        /// </summary>
        public MovieNode() {
        }

    }
}
