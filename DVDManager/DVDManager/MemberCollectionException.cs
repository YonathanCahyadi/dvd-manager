using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDManager {
    class MemberCollectionException : Exception {

        public MemberCollectionException(){

        }

        public MemberCollectionException(string message) : base(message) {

        }

        public MemberCollectionException(string message, Exception innerException) : base(message, innerException) {

        }

    }
}
