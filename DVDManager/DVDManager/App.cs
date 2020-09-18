using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVDManager {
    /// <summary>
    /// Contain all the UI and Function needed for the DVD Manager Console Application.
    /// </summary>
    class App {
        
        private static MovieCollection _movieCollection;
        private static MemberCollection _memberCollection;
        
        /// <summary>
        /// The current logged member
        /// </summary>
        private static Member _loggedMember;

        /// <summary>
        /// Initialize all the Essential component
        /// </summary>
        public static void Init() {
            _movieCollection = new MovieCollection();
            _memberCollection = new MemberCollection();
        }

        /// <summary>
        /// Display Main Menu
        /// </summary>
        public static void MainMenu() {

            string userInput;
            bool validInput = false;


            do {
                Console.Clear();
                Console.WriteLine("Welcome to the Community Library");
                Console.WriteLine("=========== Main Menu ==========");
                Console.WriteLine(" 1. Staff Login");
                Console.WriteLine(" 2. Member Login");
                Console.WriteLine(" 0. Exit");
                Console.WriteLine("================================\n\n\n");

                Question("Please make a selection (1-2, or 0 to exit):");

                // get user input
                userInput = Console.ReadLine();

                // validate user Input
                switch (userInput) {
                    case "1": // Staff Login Menu
                        StaffLogin();
                        StaffMenu();
                        validInput = true;
                        break;
                    case "2": // Memeber Login Menu
                        MemberLogin();
                        MemberMenu();
                        validInput = true;
                        break;
                    case "0": // Exit the Console
                        Environment.Exit(0);
                        break;
                    default: // if Input is non of the above
                        Alert("Invalid Input");
                        Console.ReadKey();
                        break;
                }


            } while (!validInput);

        }
        
        /// <summary>
        /// Handle Member Login
        /// </summary>
        /// <returns></returns>
        private static bool MemberLogin() {
            string username, password;

            bool validUser = false;

   
            Console.Clear();
            Console.WriteLine("=========== Member Login ========\n");
            Console.WriteLine("Username: ");
            Console.WriteLine("Password: ");
            Console.WriteLine("\n================================\n\n");

            // get the staff username and password
            Console.SetCursorPosition(10, 2);
            username = Console.ReadLine();
            Console.SetCursorPosition(10, 3);
            password = Console.ReadLine();

            // check if the username and password is valid
            try {
                _loggedMember = _memberCollection.CheckValidMember(username, password);
                validUser = true;

            }catch(Exception) {
                validUser = false;
                MainMenu();
            }
            


            return validUser;
        }

        /// <summary>
        /// Display the Member Menu
        /// </summary>
        private static void MemberMenu() {
            string userInput;
            bool validInput = false;


            do {
                Console.Clear();
                Console.WriteLine("=========== Member Menu ========");
                Console.WriteLine(" 1. Display all movie");
                Console.WriteLine(" 2. Borrow a movie DVD");
                Console.WriteLine(" 3. Return a movie DVD");
                Console.WriteLine(" 4. List current borrowed movie DVD");
                Console.WriteLine(" 5. Display top 10 most popular movies");
                Console.WriteLine(" 0. Return to main menu");
                Console.WriteLine("================================");

                Console.Write("Logged as: ");
                Info(_loggedMember.username);

                Question("\nPlease make a selection (1-5, or 0 to Return to Main Menu):");

                // get user input
                userInput = Console.ReadLine();

                // Validate user Input
                switch (userInput) {
                    case "1": // Display all movie
                        MemberDisplayAllMovie();
                        validInput = true;
                        break;
                    case "2": // Borrow a movie DVD
                        MemberBorrowDVD();
                        validInput = true;
                        break;
                    case "3": // Return a movie DVD
                        MemberReturnDVD();
                        validInput = true;
                        break;
                    case "4": // List current borrowed movie DVD
                        MemberSeeBorrowedDVD();
                        validInput = true;
                        break;
                    case "5": // Display top 10 most popular movies
                        MemberGetTop10();
                        validInput = true;
                        break;
                    case "0": // go back to main menu
                        MainMenu();
                        break;
                    default: // if Input is non of the above
                        Alert("Invalid Input");
                        Console.ReadKey();
                        break;
                }


            } while (!validInput);

        }

        /// <summary>
        /// Display all the Movie 
        /// </summary>
        private static void MemberDisplayAllMovie() {
            Console.Clear();

            Movie[] allMovie = _movieCollection.GetAllMovie();

            Console.WriteLine("============ Movies ===========\n");

            if (allMovie != null) { // Check is all Movie is null or not
                Movie[] sortedMovie = _movieCollection.GetSortedMovieByAlpabeticalOrder(allMovie); // get the sorted version of allMovie, sorted by alphabetical order
                if (sortedMovie != null && _movieCollection.numberOfDVD != 0) {
                    if (sortedMovie.Length != 0) {
                        for (int i = 0; i < sortedMovie.Length; i++) {
                            Numbering(i + 1);
                            Console.WriteLine("\tTitle          : " + sortedMovie[i].title);
                            Console.WriteLine("\tStarring       : " + sortedMovie[i].starring);
                            Console.WriteLine("\tDirector       : " + sortedMovie[i].director);
                            Console.WriteLine("\tGenre          : " + sortedMovie[i].genre);
                            Console.WriteLine("\tDuration       : " + sortedMovie[i].duration);
                            Console.WriteLine("\tRelease Date   : " + sortedMovie[i].releaseDate);
                            Console.WriteLine("\tClassification : " + sortedMovie[i].classification);
                            Console.WriteLine("\tAvailable Copy : " + sortedMovie[i].numberOfAvailableCopy);
                            Console.WriteLine("\tTimes Rented   : " + sortedMovie[i].timesRented);
                            Console.WriteLine();
                        }
                    }
                }
            } else { // if no Movie is registered
                Alert("No Movie available"); 
            }

            Console.WriteLine("\n==================================\n");

            Console.ReadKey();

            MemberMenu();
        }

        /// <summary>
        /// For member to borrow DVD
        /// </summary>
        private static void MemberBorrowDVD() {

            // getting the title of the DVD user wants to borrow
            bool validInput = false;
            string userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("========== Borrow DVD ===========\n");
                Console.WriteLine("DVD Title    : ");
                Console.WriteLine("\n================================\n\n");

                Console.SetCursorPosition(14, 2);
                userInput = Console.ReadLine();
                
                // Check user Input Make sure its not an empty string
                if (!String.IsNullOrWhiteSpace(userInput)) {
                    validInput = true;
                }
            } while (!validInput);

            // borrowing Procedure
            if (_movieCollection.isMovieAvailable(userInput)) { // if the movie is available for borrowing
                Movie inputMovie = _movieCollection.GetMovie(userInput); // get the Movie with the user inputed Title, from the movie collection
                if (inputMovie != null) { 
                    if (!(_loggedMember.isDVDBorrowed(inputMovie))) { // if DVD is not in current user Possesion
                        if (_loggedMember.isAbleToBorrow()) { // if user borrowed DVD number not beyond the limit 
                            _loggedMember.BorrowDVD(inputMovie); // add DVD to current user borrowed DVD array
                            _movieCollection.BorrowDVD(userInput); // Inform the movie collection that the DVD is borrowed by a User
                            MemberMenu(); // Go back to Member Menu
                        } else { // if current user borrowed DVD array already at its limit
                            Console.Clear();
                            Console.WriteLine("========== Borrow DVD ===========\n");
                            Alert("Sorry, but you cannot borrow more DVD.");
                            Console.WriteLine("\n================================\n\n");
                        }
                    } else { // if user already borrow the DVD
                        Console.Clear();
                        Console.WriteLine("========== Borrow DVD ===========\n");
                        Console.WriteLine("DVD Title    : " + userInput);
                        Alert("Sorry, this DVD is already in your possession");
                        Console.WriteLine("\n================================\n\n");   
                    }
                } else { // if there is some error in getting the Movie from the movie collection
                    Console.Clear();
                    Console.WriteLine("========== Borrow DVD ===========\n");
                    Alert("Sorry, there is some error while getting the movie titled" + userInput);
                    Console.WriteLine("\n================================\n\n");
                }
            } else { // if the DVD is not available for Borrowing
                Console.Clear();
                Console.WriteLine("========== Borrow DVD ===========\n");
                Console.WriteLine("DVD Title    : " + userInput);
                Alert("Sorry, this DVD is not Available at the Moment");
                Console.WriteLine("\n================================\n\n");
            }

            Console.ReadKey();

            MemberMenu();
        }

        /// <summary>
        /// For member to see their borrowed DVD
        /// </summary>
        private static void MemberSeeBorrowedDVD() {

            // get the array containing the borrowed DVD of current User
            Movie[] borrowedDVD = _loggedMember.GetBorrowedDVD();

            Console.Clear();
            Console.WriteLine("========== Borrowed DVD =========\n");
            if(borrowedDVD != null && _loggedMember.currentNumberOfBorrowedDVD != 0) { // if user have borrowed DVD in possesion
                if(borrowedDVD.Length != 0) {
                    // displaying the borrowed DVD in current user possesion
                    for(int i = 0; i < _loggedMember.currentNumberOfBorrowedDVD; i++) {
                        Numbering(i + 1);
                        Console.WriteLine("\tTitle          : " + borrowedDVD[i].title);
                        Console.WriteLine("\tStarring       : " + borrowedDVD[i].starring);
                        Console.WriteLine("\tDirector       : " + borrowedDVD[i].director);
                        Console.WriteLine("\tGenre          : " + borrowedDVD[i].genre);
                        Console.WriteLine("\tDuration       : " + borrowedDVD[i].duration);
                        Console.WriteLine("\tRelease Date   : " + borrowedDVD[i].releaseDate);
                        Console.WriteLine("\tClassification : " + borrowedDVD[i].classification);
                        Console.WriteLine();
                    }
                }
            } else { // if there is no DVD borrowed by the current user
                Alert("Currently there is no borrowed DVD");
            }
            Console.WriteLine("\n================================\n\n");

            Console.ReadKey();
            MemberMenu();
        }

        /// <summary>
        /// For member to return their borrowed DVD
        /// </summary>
        private static void MemberReturnDVD() {

            // Getting the title of the DVD, user want to return
            string userInput;
            bool validInput = false;
            do
            {
                Console.Clear();
                Console.WriteLine("========== Return DVD ===========\n");
                Console.WriteLine("Return DVD: ");
                Console.WriteLine("\n================================\n\n");

                Console.SetCursorPosition(11, 2);
                userInput = Console.ReadLine();

                // Check userInput, make sure its not empty string
                if (!String.IsNullOrWhiteSpace(userInput)) {
                    validInput = true;
                }

            } while (!validInput);

            // get the movie detail from the movie collection based on the Movie title
            Movie movie = _movieCollection.GetMovie(userInput);

            if (movie != null) { // if movie exist in movie collection
                if (_loggedMember.isDVDBorrowed(movie)) { // if the current user borrow the DVD with the inputed title
                    _loggedMember.ReturnDVD(movie); // return the DVD from the current user possesion
                    _movieCollection.ReturnDVD(userInput); // inform the Movie Collection that this DVD is already returned
                    MemberMenu(); // Go back to Member Menu
                } else { // if user didn't borrow this  movie
                    Console.Clear();
                    Console.WriteLine("========== Return DVD ===========\n");
                    Console.WriteLine("DVD Title    : " + userInput);
                    Alert("You didn't borrow this DVD");
                    Console.WriteLine("\n================================\n\n");
                }
            } else { // if borrowed movie doesn't exist in movie collection
                Console.Clear();
                Console.WriteLine("========== Return DVD ===========\n");
                Console.WriteLine("DVD Title    : " + userInput);  
                Alert("This DVD doesn't exist in Staff database");
                Console.WriteLine("\n================================\n\n");
            }

            Console.ReadKey();
            MemberMenu();
        }
           
        /// <summary>
        /// Show the top 10 most borrowed DVD, in descending order
        /// </summary>
        private static void MemberGetTop10() {
            // get all the registered movie
            Movie[] allMovie = _movieCollection.GetAllMovie(); 

            Console.Clear();
            Console.WriteLine("========== Top 10 DVD =========\n");

            if (allMovie != null) { // if there is some movie registered by the staff

                // get the sorted version of the registered movie, sorted my frequency
                Movie[] sortedMovieArray = _movieCollection.GetSortedMovieByFrequencyOrder(allMovie); 

                // Display the sorted array from the bottom up
                // this way we can get the array to show in descending order
                for (int i = sortedMovieArray.Length - 10; i < sortedMovieArray.Length; i++) {
                    // check if the i is bigger or equals 0, to avoid index out of bound.
                    if (i >= 0) {
                        Numbering((sortedMovieArray.Length - i));
                        Console.WriteLine("\tTitle          : " + sortedMovieArray[i].title);
                        Console.WriteLine("\tStarring       : " + sortedMovieArray[i].starring);
                        Console.WriteLine("\tDirector       : " + sortedMovieArray[i].director);
                        Console.WriteLine("\tGenre          : " + sortedMovieArray[i].genre);
                        Console.WriteLine("\tDuration       : " + sortedMovieArray[i].duration);
                        Console.WriteLine("\tRelease Date   : " + sortedMovieArray[i].releaseDate);
                        Console.WriteLine("\tClassification : " + sortedMovieArray[i].classification);
                        Console.WriteLine("\tTimes Rented   : " + sortedMovieArray[i].timesRented);
                        Console.WriteLine();

                    }
                }
                
            } else { // if there is no DVD registered by the staff
                Alert("No DVD Registered");
            }
            Console.WriteLine("\n================================\n\n");
            Console.ReadKey();

            MemberMenu();
        }

        /// <summary>
        /// Display the Staff Menu
        /// </summary>
        private static void StaffMenu() {
            
            string userInput;
            bool validInput = false;
            do {
                Console.Clear();
                Console.WriteLine("=========== Staff Menu =========");
                Console.WriteLine(" 1. Add a new movie DVD");
                Console.WriteLine(" 2. Remove a movie DVD");
                Console.WriteLine(" 3. Register a new Member");
                Console.WriteLine(" 4. Find a registered member's phone number");
                Console.WriteLine(" 0. Return to main menu");
                Console.WriteLine("================================\n\n");

                Question("Please make a selection (1-4, or 0 to Return to Main Menu):");

                // get user input
                userInput = Console.ReadLine();

                // 
                switch (userInput) {
                    case "1": // add new movie DVD
                        StaffAddNewDVD();
                        validInput = true;
                        break;
                    case "2": // remove a movie DVD
                        StaffRemoveMovie();
                        validInput = true;
                        break;
                    case "3": // register a new Member
                        StaffRegisterNewMember();
                        validInput = true;
                        break;
                    case "4": // find registered member's phone number
                        StaffFindMemberPhoneNumber();
                        validInput = true;
                        break;
                    case "0": // go back to main menu
                        MainMenu();
                        break;
                    default: // if Input is none of the above
                        Alert("Invalid Input");
                        Console.ReadKey();
                        break;
                }


            } while (!validInput);

        }

        /// <summary>
        /// Handle Staff Login
        /// </summary>
        /// <returns></returns>
        private static bool StaffLogin() {
            const string USERNAME = "staff";
            const string PASSWORD = "today123";

            string username, password;

            bool validUser = false;


           
            Console.Clear();
            Console.WriteLine("=========== Staff Login ========\n");
            Console.WriteLine("Username: ");
            Console.WriteLine("Password: ");
            Console.WriteLine("\n================================\n\n");

            // get the staff username and password
            Console.SetCursorPosition(10, 2);
            username = Console.ReadLine();
            Console.SetCursorPosition(10, 3);
            password = Console.ReadLine();

            // check if the username and password is valid
            if (username == USERNAME && password == PASSWORD) {
                validUser = true;
            } else {
                MainMenu();
            }


            return validUser;
        }

        /// <summary>
        /// Used by the Staff to remove DVD
        /// </summary>
        private static void StaffRemoveMovie() {
            Console.Clear();
            Console.WriteLine("========== Remove Movie =======\n");
            Console.WriteLine("Movie Title   : ");
            Console.WriteLine("\n================================\n\n");

            Console.SetCursorPosition(16, 2);
            string movieTitle = Console.ReadLine();

            Movie movie = _movieCollection.GetMovie(movieTitle);

            if(movie != null) {
                _movieCollection.DeleteMovie(movie);
                Console.Clear();
                Console.WriteLine("========== Remove Movie =======\n");
                Info("Movie Deleted.");
                Console.WriteLine("\n================================\n\n");


            } else {
                Console.Clear();
                Console.WriteLine("========== Remove Movie =======\n");
                Alert("Movie doesn't exist.");
                Console.WriteLine("\n================================\n\n");
            }

            Console.ReadKey();
            StaffMenu();

        }

        /// <summary>
        /// Used by the Staff to add new DVD
        /// </summary>
        private static void StaffAddNewDVD() {
            string title, director, starring, releaseDate, duration;

            Console.Clear();
            Console.WriteLine("=========== Add New DVD =========\n");
            Console.WriteLine("Title        : ");
            Console.WriteLine("Director     : ");
            Console.WriteLine("Starring     : ");
            Console.WriteLine("Release Date : ");
            Console.WriteLine("Duration     : ");
            Console.WriteLine("\n================================\n\n");

            // get the DVD basic Info
            Console.SetCursorPosition(14, 2);
            title = Console.ReadLine();
            Console.SetCursorPosition(14, 3);
            director = Console.ReadLine();
            Console.SetCursorPosition(14, 4);
            starring = Console.ReadLine();
            Console.SetCursorPosition(14, 5);
            releaseDate = Console.ReadLine();
            Console.SetCursorPosition(14, 6);
            duration = Console.ReadLine();

            // get the Genre of the DVD
            bool genreIsValid = false;
            int selectedGenre;
            do {
                Console.Clear();
                Console.WriteLine("=========== Add New DVD =========\n");
                Console.WriteLine("Select Genre Of DVD: ");
                Console.WriteLine("1. Action");
                Console.WriteLine("2. Adventure");
                Console.WriteLine("3. Animated");
                Console.WriteLine("4. Comedy");
                Console.WriteLine("5. Drama");
                Console.WriteLine("6. Family");
                Console.WriteLine("7. Sci-Fi");
                Console.WriteLine("8. Thriller");
                Console.WriteLine("9. Other");
                Console.WriteLine("\n================================\n\n");
                Question("Please Make Genre Selection From 1 - 9: ");

                string userSelectedGenreRaw = Console.ReadLine();
                
                //Check if Genre Selection is Valid
                if (int.TryParse(userSelectedGenreRaw, out selectedGenre)) {
                    if (selectedGenre > 0 && selectedGenre < 10) {
                        genreIsValid = true;
                    }
                }

            } while (!genreIsValid);

            // get the classification of the DVD
            bool classificationIsValid = false;
            int selectedClassification;
            do {
                Console.Clear();
                Console.WriteLine("=========== Add New DVD =========\n");
                Console.WriteLine("Select Classification Of DVD: ");
                Console.WriteLine("1. General (G)");
                Console.WriteLine("2. Parental Guidance (PG)");
                Console.WriteLine("3. Mature (M15+)");
                Console.WriteLine("4. Mature Acompanied (MA15+)");
                Console.WriteLine("\n================================\n\n");
                Question("Please Make Classification Selection From 1 - 4: ");

                string userSelectedClassificationRaw = Console.ReadLine();
                
                //Check if Classification Selection is valid
                if (int.TryParse(userSelectedClassificationRaw, out selectedClassification)) {
                    if (selectedClassification > 0 && selectedClassification < 5) {
                        classificationIsValid = true;
                    }
                }

            } while (!classificationIsValid);

            // get the number of Copy available
            bool numberIsValid = false;
            int numberOfAvailableCopy;
            do {
                Console.Clear();
                Console.WriteLine("=========== Add New DVD =========\n");
                Console.WriteLine("How Many Copy of this DVD: ");
                Console.WriteLine("\n================================\n\n");
               

                Console.SetCursorPosition(27, 2);
                string numberOfAvailableCopyRaw = Console.ReadLine();
                
                // Check if inputed number of available copy is valid
                if (int.TryParse(numberOfAvailableCopyRaw, out numberOfAvailableCopy)) {
                    if (numberOfAvailableCopy > 0) {
                        numberIsValid = true;
                    }
                }

            } while (!numberIsValid);

            // made the new movie
            Movie newMovie = new Movie(title, (Genre)selectedGenre, director, starring, releaseDate, (Classification)selectedClassification, duration, numberOfAvailableCopy);

            // Show the Info Inputed
            bool insertResult = false;
            bool validInput = false;
            do {
                Console.Clear();
                Console.WriteLine("=========== Add New DVD =========\n");
                Console.WriteLine("Title              : " + newMovie.title);
                Console.WriteLine("Director           : " + newMovie.director);
                Console.WriteLine("Starring           : " + newMovie.starring);
                Console.WriteLine("Genre              : " + newMovie.genre);
                Console.WriteLine("Release Date       : " + newMovie.releaseDate);
                Console.WriteLine("Duration           : " + newMovie.duration);
                Console.WriteLine("Classification     : " + newMovie.classification);
                Console.WriteLine("Number of Copy     : " + newMovie.numberOfAvailableCopy);
                Console.WriteLine("\n================================\n\n");
                Question("Put Movie into Movie Collection [y/n]: ");

                string userInput = Console.ReadLine();
                

                if (userInput.ToLower() == "y") {
                    insertResult =  _movieCollection.InsertMovie(newMovie);
                    validInput = true;

                } else if (userInput.ToLower() == "n") {
                    newMovie = null;
                    validInput = true;
                    StaffMenu();
                }

            } while (!validInput);

            if (insertResult) { // Movie didn't exist and successfuly added to the Movie Collection
                Console.Clear();
                Console.WriteLine("=========== Add New DVD =========\n");
                Info("Movie is Added into the Movie Collection");
                Console.WriteLine("\n================================\n\n");
            } else { // Movie exist and number of copies is added to the existing Movie
                Console.Clear();
                Console.WriteLine("=========== Add New DVD =========\n");
                Info(newMovie.numberOfAvailableCopy + " number of copy has been added to movie with title " + "\""+ newMovie.title + "\"");
                Console.WriteLine("\n================================\n\n");
            }
            Console.ReadKey();
            StaffMenu();

        }

        /// <summary>
        /// Used by staff to Register New Member
        /// </summary>
        private static void StaffRegisterNewMember() {

            string firstName, lastName, phoneNumber, address, password;
            Member newMember = null;


            bool validUser = false;
            do {

                Console.Clear();
                Console.WriteLine("========== Register Member =======\n");
                Console.WriteLine("First Name   : ");
                Console.WriteLine("Last Name    : ");
                Console.WriteLine("Phone Number : ");
                Console.WriteLine("Address      : ");
                Console.WriteLine("Password     : ");
                Console.WriteLine("\n================================\n\n");

                // get the staff username and password
                Console.SetCursorPosition(14, 2);
                firstName = Console.ReadLine();
                Console.SetCursorPosition(14, 3);
                lastName = Console.ReadLine();
                Console.SetCursorPosition(14, 4);
                phoneNumber = Console.ReadLine();
                Console.SetCursorPosition(14, 5);
                address = Console.ReadLine();
                Console.SetCursorPosition(14, 6);
                password = Console.ReadLine();




                // try to register a new member into the member collection
                try {
                    if (_memberCollection.isAbleToRegistered()) {// if the member collection array is not full
                        if (!_memberCollection.isRegistered(firstName, lastName)) { // if user have not already Registered
                            newMember = new Member(firstName, lastName, password, phoneNumber, address);
                            validUser = true;
                        } else { // if user already registered 
                            Console.SetCursorPosition(0, 10);
                            Alert("User is already Registered before"); ;
                            Console.ReadKey();
                            StaffMenu();
                        }
                    } else { // if the member collection array is already full
                        Console.SetCursorPosition(0, 10);
                        Alert("Sorry but the Member Collection is already full.");
                        Alert("Cannot Register More New Member");
                        Console.ReadKey();
                        StaffMenu();
                    }
                } catch (MemberCollectionException) { // the provided Member data is not up to specification
                    Console.SetCursorPosition(0, 10);
                    Alert("Password is Invalid -- Must be 4 digit Interger");;
                    Console.ReadKey();
                }

            } while (!validUser);

            // Confirm the detail of the will be registered new member
            bool validInput = false;
            do {
                Console.Clear();
                Console.WriteLine("========== Register Member =======\n");
                Console.WriteLine("First Name  : " + newMember.firstName);
                Console.WriteLine("Last Name   : " + newMember.lastName);
                Console.WriteLine("Phone Number: " + newMember.phoneNumber);
                Console.WriteLine("Address     : " + newMember.address);
                Console.WriteLine();
                Console.Write("Username    : ");
                Info(newMember.username);
                Console.Write("Password    : ");
                Info(newMember.password);
                Console.WriteLine("\n================================\n\n");
                Question("Register New Member [y/n]: ");


                string userInput = Console.ReadLine();
                if (userInput.ToLower() == "y") { // if the detail already correct
                    _memberCollection.InsertMember(newMember); // put the new member into the member collection
                    validInput = true;

                } else if (userInput.ToLower() == "n") { // if there is something wrong with the new member detail
                    validInput = true;
                    StaffMenu(); // go back to Staff Menu
                }

            } while (!validInput);

            // Give Confirmation if user successfuly registered into the member collection
            Console.Clear();
            Console.WriteLine("========== Register Member =======\n");
            Info("Member is successfuly registered");
            Console.WriteLine("\n================================\n\n");
            Console.ReadKey();

            StaffMenu(); // go back to Staff Menu
        }

        /// <summary>
        /// Used by staff to find Registered Member Phone Number
        /// </summary>
        private static void StaffFindMemberPhoneNumber() {

            // get the member Full Name
            Console.Clear();
            Console.WriteLine("========== Find Phone Number =======\n");
            Console.WriteLine("Full Name   : ");
            Console.WriteLine("\n================================\n\n");

            Console.SetCursorPosition(14, 2);
            string memberFullName = Console.ReadLine();

            // try to get member Phone number by Member Full Name
            try {
                string memberPhoneNumber = _memberCollection.GetMemberPhoneNumber(memberFullName);
                if (!String.IsNullOrWhiteSpace(memberPhoneNumber)) { // if user have a registered phone number
                    Console.Clear();
                    Console.WriteLine("========== Find Phone Number =======\n");
                    Console.WriteLine("Full Name   : " + memberFullName);
                    Console.Write("Phone Number: ");
                    Info(memberPhoneNumber);
                    Console.WriteLine("\n================================\n\n");
                    Console.ReadKey();
                } else { // if user didn't have phone number registered
                    Console.Clear();
                    Console.WriteLine("========== Find Phone Number =======\n");
                    Console.WriteLine("Full Name   : " + memberFullName);
                    Alert("This Member didn't have registered Phone Number");
                    Console.WriteLine("\n================================\n\n");
                    Console.ReadKey();
                }
            } catch (MemberCollectionException) { // member not found, therfor no Phone number
                Console.Clear();
                Console.WriteLine("========== Find Phone Number =======\n");
                Console.WriteLine("Full Name   : " + memberFullName);
                Alert("This Member is not Registered");
                Console.WriteLine("\n================================\n\n");
                Console.ReadKey();
            }

            StaffMenu();

        }

        /// <summary>
        /// For Displaying an alert message, the message color will be Red
        /// </summary>
        /// <param name="message">Displayed Message</param>
        private static void Alert(string message) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// For Displaying an Info message, the message color will be Green
        /// </summary>
        /// <param name="message">Displayed Message</param>
        private static void Info(string message) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// For Displaying a Number, the Number color will be Magenta
        /// </summary>
        /// <param name="number">The Number</param>
        private static void Numbering(int number) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(number.ToString() + " --- ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// For Displaying a Question, the message color will be Yellow
        /// </summary>
        /// <param name="message">Displayed Message </param>
        private static void Question(string message) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

}
