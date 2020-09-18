using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDManager {

    /// <summary>
    /// Available Genre for the Movie
    /// </summary>
    enum Genre {
        Action = 1,
        Adventure,
        Animated,
        Comedy,
        Drama,
        Family,
        SciFi,
        Thriller,
        Other
    }

    /// <summary>
    /// Available Classification for the Movie
    /// </summary>
    enum Classification { 
        General = 1,
        ParentalGuidance,
        Mature,
        MatureAccompanied
    }

}
