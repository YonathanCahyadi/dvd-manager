using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace DVDManager {
    /// <summary>
    /// A Class that Contain a Collection Movie.
    /// The Movie is stored using Binary Tree Data Structure.
    /// </summary>
    class MovieCollection {

        /// <summary>
        /// The root node of the Movie Collection Binary Tree
        /// </summary>
        private MovieNode _root;

        /// <summary>
        /// To keep track the current number of registered DVD
        /// </summary>
        public int numberOfDVD = 0;

        /// <summary>
        /// Class Constructor, populate the Binary Tree root by null value
        /// </summary>
        public MovieCollection() {
            // populate the root node with null value
            _root = null;
        }

        /// <summary>
        /// Insert New Data into the Member Collection Binary Tree
        /// </summary>
        /// <param name="movie">The new Member</param>
        /// <returns>true if Successful, false if the same movie already exist</returns>
        public bool InsertMovie(Movie movie) {

            MovieNode before = null, after = this._root;

            // get the node to insert the new movie
            while (after != null) { 
                before = after;
                if (String.Compare(movie.title, after.data.title) == -1) {
                    after = after.left;
                } else if (String.Compare(movie.title, after.data.title) == 1) {
                    after = after.right;
                } else { // if the title is the same with the current node

                    // add the number of copy into the existing movie with the same title
                    after.data.numberOfAvailableCopy += movie.numberOfAvailableCopy;
                    return false;
                }

            }

            MovieNode newNode = new MovieNode();
            newNode.data = movie;


            if (this._root == null) { // if current tree is empty, put the new Movie into the root
                this._root = newNode;
                numberOfDVD++;
            } else { // if current tree is not empty
                // put the new movie into the left child of the current node if the title of the new Movie is 
                // alpabethically after before the current node
                if (String.Compare(movie.title, before.data.title) == -1) {
                    before.left = newNode;
                    numberOfDVD++;
                } else if (String.Compare(movie.title, before.data.title) == 1) { // otherwise, put it onn the right child of the current node
                    before.right = newNode;
                    numberOfDVD++;
                }
            }
            return true;

        }

        /// <summary>
        /// Delete Node in Binary Tree
        /// </summary>
        /// <param name="movie"></param>
        public void DeleteMovie(Movie movie) {
            MovieNode ptr = _root;
            MovieNode parent = null;

            // Look for the node with the same Movie title as the the provided parameters
            while ((ptr != null) && (movie != ptr.data)) {
                parent = ptr;
                if(String.Compare(movie.title, ptr.data.title) <= 0) { // if the current node is bigger than the param, go to the left Node
                    ptr = ptr.left;
                } else { // otherwise go to the right node
                    ptr = ptr.right;
                }
            }

            // if node with the same Movie title found
            if (ptr != null) {
                // Check the current node left and right child
                if ((ptr.left != null) && (ptr.right != null)) { // if both child is not null, connect both child of the current node with the data of the next node
                    if(ptr.left.right == null) {
                        ptr.data = ptr.left.data;
                        ptr.left = ptr.left.left;
                    } else {
                        MovieNode p = ptr.left;
                        MovieNode pp = ptr;

                        while(p.right != null) {
                            pp = p;
                            p = p.right;
                        }

                        ptr.data = p.data;
                        pp.right = p.left;
                    }


                } else { // otherwise, one of the child of the current node is null
                    MovieNode c;
                    if (ptr.left != null) { // if the left child is null
                        c = ptr.left;
                    } else { // if the right child is null
                        c = ptr.right;
                    }

                    if(ptr == _root) {
                        _root = c;
                       
                    } else {
                        if(ptr == parent.left) {
                            parent.left = c;
                          
                        } else {
                            parent.right = c;
                            
                        }
                    }

                }

                //decrease the number of DVD
                numberOfDVD--;
            }

        }
   
        /// <summary>
        /// Get all the Movie in the Binary Tree
        /// </summary>
        /// <returns>Array of all Movie Contained in the Binary Tree</returns>
        public Movie[] GetAllMovie() {

            if (_root == null) return null;

            Movie[] result = new Movie[numberOfDVD];


            AddToArray(_root, result, 0);
            return result;

        }

        /// <summary>
        /// Transfer Binary Tree Node into an array
        /// </summary>
        /// <param name="node">the node that will be traversed</param>
        /// <param name="array">Array to store the Node</param>
        /// <param name="i">current index of the array</param>
        /// <returns></returns>
        private int AddToArray(MovieNode node, Movie[] array, int i) { 
            if(node == null) {
                return i;
            }

            array[i] = node.data;
            i++;

            // traverse through all the Binary Tree Node and Put the Node into an array
            if (node.left != null)
                i = AddToArray(node.left, array, i);
            if (node.right != null)
                i = AddToArray(node.right, array, i);
            
            return i;
        
        }
      
        /// <summary>
        /// Check if the Movie is Available for Borrowing
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool isMovieAvailable(string title) {
            Movie[] temp = GetAllMovie();

            if (temp != null) {
                if (temp.Length != 0) {
                    for (int i = 0; i < temp.Length; i++) {
                        if (temp[i].title == title) {
                            if (temp[i].numberOfAvailableCopy != 0) {
                                return true;
                            }
                        }
                    }
                }
            }



            return false;
        }

        /// <summary>
        /// Get the Movie based on the title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Movie GetMovie(string title) {
            Movie[] temp = GetAllMovie();

            if (temp != null) {
                if (temp.Length != 0) {
                    for (int i = 0; i < temp.Length; i++) {
                        if (temp[i].title == title) {
                             return temp[i];
                        }
                    }
                }
            }


            return null;
        }

        /// <summary>
        /// Borrow the DVD
        /// </summary>
        /// <param name="title"></param>
        public void BorrowDVD(string title) {
            Movie[] temp = GetAllMovie();

            if (temp != null) {
                if (temp.Length != 0) {
                    for (int i = 0; i < temp.Length; i++) {
                        if (temp[i].title == title) {
                            if (temp[i].numberOfAvailableCopy != 0) {
                                temp[i].numberOfAvailableCopy = temp[i].numberOfAvailableCopy - 1;
                                temp[i].timesRented = temp[i].timesRented + 1;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Return the DVD
        /// </summary>
        /// <param name="title"></param>
        public void ReturnDVD(string title) {
            Movie[] temp = GetAllMovie();

            if (temp != null) {
                if (temp.Length != 0) {
                    for (int i = 0; i < temp.Length; i++) {
                        if (temp[i].title == title) {
                            temp[i].numberOfAvailableCopy = temp[i].numberOfAvailableCopy + 1;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get all DVD but sorted by how many time its been rented
        /// The sorting algorithm used is Merge Sort
        /// </summary>
        /// <param name="allAvailableMovie"></param>
        /// <returns>Sorted Movie Array, Sorted by borrowed Frequency</returns>
        public Movie[] GetSortedMovieByFrequencyOrder(Movie[] allAvailableMovie) {
            Movie[] result = new Movie[allAvailableMovie.Length]; // Create an array for the Merge Sorting Result

            // Made 2 sub array for later use
            Movie[] left;
            Movie[] right;

            // Check the length of the inputed array
            // if the length is 1 then return it back
            if (allAvailableMovie.Length <= 1) return allAvailableMovie;

            // get the mid point of our array
            int midPoint = allAvailableMovie.Length / 2;
            // initialize the left sub-array
            // so the number of array in the left sub-array is [0 - midPoint]
            left = new Movie[midPoint];

            // initialize the right sub-array
            // so the number of array in the right sub-array is [0 - midPoint] if even
            // [0 - midPoint + 1] if odd
            if(allAvailableMovie.Length % 2 == 0) {
                right = new Movie[midPoint];
            } else {
                right = new Movie[midPoint + 1];
            }

            // populate the left array using left side array param as the value
            // the element is from [0 - midPoint]
            for(int i = 0; i < midPoint; i++) {
                left[i] = allAvailableMovie[i];
            }
            //poulate the right array using right side array param as the value
            // the element is from [midPoint - allAvailableMovie length]
            int rightSubArrayIndex = 0;
            for(int i = midPoint; i < allAvailableMovie.Length; i++) {
                right[rightSubArrayIndex] = allAvailableMovie[i];
                rightSubArrayIndex++;
            }

            // recursively divide the array until there is only 1 element left in the sub-array 
            left = GetSortedMovieByFrequencyOrder(left);
            right = GetSortedMovieByFrequencyOrder(right);

            // merge the sub-array together into 1 sorted array
            result = MergeByFrequencyOrder(left, right);
            
            return result;
        }

        /// <summary>
        /// Merge to 2 sub-array together into 1 array by Frequency
        /// </summary>
        /// <param name="left">Left Array</param>
        /// <param name="right">Right Array</param>
        /// <returns>Return Sorted an Merged Array</returns>
        private Movie[] MergeByFrequencyOrder(Movie[] left, Movie[] right) {
            int resultLength = left.Length + right.Length; // get the length of the reulted array from the 2 sub-array
            Movie[] result = new Movie[resultLength]; // init the result array for later use

            int indexLeft = 0, indexRight = 0, indexResult = 0;

            // itterate all element in left and right array
            while(indexLeft < left.Length || indexRight < right.Length) {

                //if both array had element
                if(indexLeft < left.Length && indexRight < right.Length) {
                    // Sort the element by how many times it has been rented
                    // If the left Movie array is has been rented less than the right Movie array 
                    // Put the left array movie into the Result array
                    if (left[indexLeft].timesRented <= right[indexRight].timesRented) {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    } else { // otherwise, put the right side Movie array into the Result array
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                } else if (indexLeft < left.Length) { //if only the left array still has elements, add all its elements to the results array
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                } else if (indexRight < right.Length) { //if only the right array still has elements, add all its elements to the results array
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }

        /// <summary>
        /// Get all DVD but sorted by Alphabetical Order
        /// </summary>
        /// <param name="allAvailableMovie"></param>
        /// <returns></returns>
        public Movie[] GetSortedMovieByAlpabeticalOrder(Movie[] allAvailableMovie) {
            Movie[] result = new Movie[allAvailableMovie.Length]; // Create an array for the Merge Sorting Result

            // Made 2 sub array for later use
            Movie[] left;
            Movie[] right;

            // Check the length of the inputed array
            // if the length is 1 then return it back
            if (allAvailableMovie.Length <= 1) return allAvailableMovie;

            // get the mid point of our array
            int midPoint = allAvailableMovie.Length / 2;
            // initialize the left sub-array
            // so the number of array in the left sub-array is [0 - midPoint]
            left = new Movie[midPoint];

            // initialize the right sub-array
            // so the number of array in the right sub-array is [0 - midPoint] if even
            // [0 - midPoint + 1] if odd
            if (allAvailableMovie.Length % 2 == 0) {
                right = new Movie[midPoint];
            } else {
                right = new Movie[midPoint + 1];
            }

            // populate the left array using left side array param as the value
            // the element is from [0 - midPoint]
            for (int i = 0; i < midPoint; i++) {
                left[i] = allAvailableMovie[i];
            }
            //poulate the right array using right side array param as the value
            // the element is from [midPoint - allAvailableMovie length]
            int rightSubArrayIndex = 0;
            for (int i = midPoint; i < allAvailableMovie.Length; i++) {
                right[rightSubArrayIndex] = allAvailableMovie[i];
                rightSubArrayIndex++;
            }

            // recursively divide the array until there is only 1 element left in the sub-array 
            left = GetSortedMovieByAlpabeticalOrder(left);
            right = GetSortedMovieByAlpabeticalOrder(right);

            // merge the sub-array together into 1 sorted array
            result = MergeByAlpabethicalOrder(left, right);

            return result;
        }

        /// <summary>
        /// Merge 2 sub-array together into 1 array by Alphabethical Order
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private Movie[] MergeByAlpabethicalOrder(Movie[] left, Movie[] right) {
            int resultLength = left.Length + right.Length; // get the length of the reulted array from the 2 sub-array
            Movie[] result = new Movie[resultLength]; // init the result array for later use

            int indexLeft = 0, indexRight = 0, indexResult = 0;

            // itterate all element in left and right array
            while (indexLeft < left.Length || indexRight < right.Length) {

                //if both array had element
                if (indexLeft < left.Length && indexRight < right.Length) {
                    // Sort the element by alphabetical order
                    // If the left Movie array is has been rented less than the right Movie array 
                    // Put the left array movie into the Result array
                    if (String.Compare(left[indexLeft].title, right[indexRight].title) <= 0) {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    } else { // otherwise, put the right side Movie array into the Result array
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                } else if (indexLeft < left.Length) { //if only the left array still has elements, add all its elements to the results array
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                } else if (indexRight < right.Length) { //if only the right array still has elements, add all its elements to the results array
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }
        

    }
}
