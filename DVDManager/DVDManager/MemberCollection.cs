using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDManager {
    /// <summary>
    /// A Class that Contain a Collection Member.
    /// The Member is stored using array.
    /// </summary>
    class MemberCollection {

        /// <summary>
        /// Array to store the registered Member.
        /// </summary>
        private Member[] _collection;
        /// <summary>
        /// To keep track of the current number of registered Member
        /// </summary>
        private int _currentNumOfMember;

        /// <summary>
        /// The limit of stored member
        /// </summary>
        private const int _LIMIT = 2;

        /// <summary>
        /// Member Collection Class Constructor. 
        /// This will initialize the Member collection array using _LIMIT as it's size
        /// This will also assign the current number of member in this collection to 0
        /// </summary>
        public MemberCollection() {
            this._collection = new Member[_LIMIT];
            this._currentNumOfMember = 0;
        }

        /// <summary>
        /// Insert new Member into the array
        /// </summary>
        /// <param name="newMember"></param>
        public void InsertMember(Member newMember) {
            try {
                _collection[_currentNumOfMember] = newMember;
                _currentNumOfMember++;
               
            }catch(Exception) {
                throw new MemberCollectionException("Member Collection is Full");
            }
        }

        /// <summary>
        /// Check if Member is a valid registered Member
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Member CheckValidMember(string username, string password) {

            for(int i = 0; i < _currentNumOfMember; i++) {
                if (_collection[i].username == username && _collection[i].password == password) {
                    return _collection[i];
                }
            }

            throw new MemberCollectionException("Not a member");
        }
        
        /// <summary>
        /// Check if member is already registered
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public bool isRegistered(string firstName, string lastName) {
           
            if (_currentNumOfMember != 0) {
                for (int i = 0; i < _currentNumOfMember; i++) {
                    if (_collection[i].firstName == firstName && _collection[i].lastName == lastName) return true;
                }
            }
            

            return false;
        }

        /// <summary>
        /// Get member Phone Number by using user full name
        /// </summary>
        /// <param name="fullName">Member Full Name</param>
        /// <returns>Member Phone Number</returns>
        public string GetMemberPhoneNumber(string fullName) {
            for (int i = 0; i < _currentNumOfMember; i++) { 
                if(_collection[i].fullName == fullName) {
                    return _collection[i].phoneNumber;
                }
            }

            throw new MemberCollectionException("User Not Found!!");
        }

        /// <summary>
        /// Check if current member collection is able to register more new member
        /// </summary>
        /// <returns></returns>
        public bool isAbleToRegistered() {
            if (_currentNumOfMember < _LIMIT) return true;
            return false;
        }



    }
}
