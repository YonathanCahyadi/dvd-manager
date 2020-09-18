using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DVDManager {
    /// <summary>
    /// The Class that contain all the Detail of Specified Member.
    /// </summary>
    class Member {

        /// <summary>
        /// To keep track the borrowed DVD, the size of this array is based on _BORROWED_DVD_LIMIT
        /// </summary>
        private Movie[] _borrowedDVD;

        /// <summary>
        /// Decide the Limit of borrowed DVD per Member
        /// </summary>
        private const int _BORROWED_DVD_LIMIT = 10;

        /// <summary>
        /// Keep track of the number of the current borrowed DVD this Member has.
        /// </summary>
        public int currentNumberOfBorrowedDVD = 0;
        /// <summary>
        /// The first name of this Member
        /// </summary>
        public string firstName { get; }
        /// <summary>
        /// The last name of this Member
        /// </summary>
        public string lastName { get; }
        /// <summary>
        /// The password credential of this Member
        /// </summary>
        public string password { get; }
        /// <summary>
        /// The full name of of this Member
        /// </summary>
        public string fullName { 
            get {
                return firstName + " " +lastName;
            }
         }

        /// <summary>
        /// The username credential of this Member
        /// </summary>
        public string username {
            get {
                return lastName + firstName;
            }
        }
        /// <summary>
        /// The phone number of this Member
        /// </summary>
        public string phoneNumber { get; }

        /// <summary>
        /// The address of this Member
        /// </summary>
        public string address { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="address"></param>
        public Member(string firstName, string lastName, string password, string phoneNumber, string address) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.address = address;

            // Check if the given password is valid
            // The password must be 4 digit Interger
            int o;
            bool isNumeric = int.TryParse(password, out o);

            if (isNumeric) {
                if (password.Length == 4) {
                    this.password = password;
                } else {
                    throw new MemberCollectionException("Password must be 4 digit interger");
                }
            } else {
                throw new MemberCollectionException("Password must be an interger");
            }

            this._borrowedDVD = new Movie[_BORROWED_DVD_LIMIT];

        }

        /// <summary>
        /// Borrow DVD
        /// </summary>
        /// <param name="dvd"></param>
        public void BorrowDVD(Movie dvd) {
            try {
                _borrowedDVD[currentNumberOfBorrowedDVD] = dvd;
                currentNumberOfBorrowedDVD++;
                
            } catch (Exception) {
               
            }
        }

        /// <summary>
        /// Return the borrowed DVD
        /// </summary>
        /// <param name="dvd"></param>
        public void ReturnDVD(Movie dvd) {
            try {
                for(int i = 0; i < currentNumberOfBorrowedDVD; i++) {
                    if(_borrowedDVD[i] == dvd) {
                        _borrowedDVD[i] = null;
                        currentNumberOfBorrowedDVD--;
                    }
                }
            } catch (Exception) {

            }
        }

        /// <summary>
        /// Get all the currently borrowed DVD
        /// </summary>
        /// <returns></returns>
        public Movie[] GetBorrowedDVD() {
            if (currentNumberOfBorrowedDVD > 0) {
                return _borrowedDVD;
            }
            return null;
        }

        /// <summary>
        /// Check if the DVD is currently in possesion
        /// </summary>
        /// <param name="dvd"></param>
        /// <returns></returns>
        public bool isDVDBorrowed(Movie dvd) {

            if (dvd == null) return false;
            if (currentNumberOfBorrowedDVD != 0) {
                for (int i = 0; i < currentNumberOfBorrowedDVD; i++) {
                    if (_borrowedDVD[i] != null) {
                        if (_borrowedDVD[i].title == dvd.title) return true;
                    }
                }
                
            }

            return false;
        }

        /// <summary>
        /// Check if current member is able to borrow more DVD
        /// </summary>
        /// <returns></returns>
        public bool isAbleToBorrow() {

            if (this.currentNumberOfBorrowedDVD < _borrowedDVD.Length) return true;
            
            return false;
        }

    }
}
