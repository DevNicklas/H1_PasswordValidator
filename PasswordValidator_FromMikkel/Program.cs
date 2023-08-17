using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Runs until the user doesn't want to use it anymore
            do
            {
                StartController();
            }while(GetPressedKey() == ConsoleKey.Enter);
        }

        #region Model
        /// <summary>
        /// Gets password from user input
        /// </summary>
        /// <returns>Returns user input as an string</returns>
        private static string GetUserPassword()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Gets the pressed key
        /// </summary>
        /// <returns>Returns the key which the user pressed</returns>
        private static ConsoleKey GetPressedKey()
        {
            return Console.ReadKey().Key;
        }
        #endregion

        #region View
        /// <summary>
        /// Writes an given instruction to the standard output stream (console window)
        /// </summary>
        /// <param name="instruction"></param>
        private static void WriteGUIText()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Password Validator\n");
            Console.WriteLine("Please read below, and enter a password");
            Console.Write("A password is ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("valid ");
            Console.ResetColor();
            Console.WriteLine("if:");
            Console.WriteLine("- it contains at least 12 characters and maximum 64 characters");
            Console.WriteLine("- it contains upper-and lowercase letters");
            Console.WriteLine("- it contains at least 1 number");
            Console.WriteLine("- it contains at lest 1 special character\n");
            Console.WriteLine("When the password is valid it will be graded");
            Console.Write("A password is graded ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("weak ");
            Console.ResetColor();
            Console.WriteLine("when:");
            Console.WriteLine("- 4 letters or numbers of same type after eachother");
            Console.WriteLine("- a order of numbers or letters of 4 consecutive characters is presented\n");
            Console.WriteLine("Enter a password below:");
        }

        private static void DrawResponse(bool isValid, bool isWeak)
        {
            Console.Clear();
            if(isValid)
            {
                if(isWeak)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Password is valid but is considered weak");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Password is valid and is considered strong");
                }
                DrawResponseBar(isWeak);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Password isn't valid");
            }
            Console.ResetColor();
            Console.WriteLine("\nDo you want to try another password?");
            Console.WriteLine("Then press enter, or any other key to close the program");
        }

        /// <summary>
        /// Draws the bar which shows the password grade
        /// </summary>
        /// <param name="isWeak">a boolean value of the password grade</param>
        private static void DrawResponseBar(bool isWeak)
        {
            Console.ResetColor();
            Console.WriteLine('╔' + new string('═', 50) + '╗');
            for(int i = 0; i < 2; i++)
            {
                Console.SetCursorPosition(i * 51, 2);
                Console.Write('║');
            }
            Console.WriteLine("\n╚" + new string('═', 50) + '╝');

            Console.SetCursorPosition(1, 2);
            Console.BackgroundColor = ConsoleColor.DarkGray;

            if(isWeak)
            {
                Console.Write(new string(' ', 25));
            }
            else
            {
                Console.Write(new string(' ', 50));
            }
            Console.SetCursorPosition(0, 4);
        }

        #endregion

        #region Controller
        /// <summary>
        /// Starts the whole program, the first controller
        /// </summary>
        private static void StartController()
        {
            bool isValid = false;

            WriteGUIText();

            string userPassword = GetUserPassword();
            isValid = ValidatePassword(userPassword);
            bool isWeak = true;

            if (isValid)
            {
                isWeak = IsWeakGrade(userPassword);
            }

            // Respond with password grade and if the password is valid
            DrawResponse(isValid, isWeak);
        }

        /// <summary>
        /// Validates the given password
        /// </summary>
        /// <param name="password">password to validate</param>
        /// <returns>A boolean. If the password is valid, then it returns true, but if not then return false</returns>
        private static bool ValidatePassword(string password)
        {
            if (password.Length < 12 || password.Length > 64 || !HasUpperAndLower(password) || !HasNumber(password) || !HasSpecialChar(password))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether a password is weak or not
        /// </summary>
        /// <param name="password">password which is checked whether its weak or not</param>
        /// <returns>A boolean. If password contains repeated or sequential chars then true will be returned, if not then false will be returned</returns>
        private static bool IsWeakGrade(string password)
        {
            if (HasRepeatedChars(password) || HasSequentialChars(password))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a password contains upper- and lowercase
        /// </summary>
        /// <param name="password">password which is used to check for upper- and lowercase</param>
        /// <returns>A boolean. If password contains upper- and lowercase then true will be returned, if not then false will be returned</returns>
        private static bool HasUpperAndLower(string password)
        {
            bool hasUpper = false;
            bool hasLower = false;

            foreach (char c in password)
            {

                if (Char.IsUpper(c))
                {
                    hasUpper = true;
                    continue;
                }

                if (Char.IsLower(c))
                {
                    hasLower = true;
                }

                // Checks if the password contains upper-and lowercase and returns true if it does
                if (hasUpper && hasLower)
                {
                    return true;
                }
            }
            // Returns false if the password doesn't contain upper-and lowercase
            return false;
        }

        /// <summary>
        /// Checks whether a password contains a number or not
        /// </summary>
        /// <param name="password"></param>
        /// <returns>A boolean. If password contains a number then true will be returned, if not then false will be returned</returns>
        private static bool HasNumber(string password)
        {
            foreach(char c in password)
            {
                if(Char.IsNumber(c))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether a password contains a special character or not
        /// </summary>
        /// <param name="password"></param>
        /// <returns>A boolean. If password contains a special character then true will be returned, if not then false will be returned</returns>
        private static bool HasSpecialChar(string password)
        {
            foreach(char c in password)
            {
                // Returns true if the char isn't a letter or degit
                // since the only thing a char can be, if it isn't a letter or a digit, is an special char
                if(!Char.IsLetterOrDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check whether a password contains 4 repeated chars of same type after eachother or not
        /// </summary>
        /// <param name="password">password which is checked for 4 repeated chars of same type after eachother</param>
        /// <returns>A boolean. If the password contains 4 repeated chars of same type after eachother then true will be returned, else then false will be returned</returns>
        private static bool HasRepeatedChars(string password)
        {
            // I need to create a variable and put first character, so that I can check the next character
            // in the password is equal to the character that comes before
            char c = password.ToLower()[0];

            byte counter = 1;

            // Loop starts at 1 because the first character in the password is used to check if next char is the same type
            for (int i = 1; i < password.Length; i++)
            {
                if(c == password.ToLower()[i])
                {
                    counter++;

                    // When counter has found 4 repeating characters then return true
                    if (counter == 4)
                    {
                        return true;
                    }
                }

                // Resets the counter, and sets a new "char checker"
                else
                {
                    counter = 1;
                    c = password.ToLower()[i];
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether a password contains a series of numbers consecutive with at least 4 characters and is continous or not
        /// </summary>
        /// <param name="password">password which is checked for a series of numbers consecutive with at least 4 characters</param>
        /// <returns>A boolean. If the password contains a series of numbers consecutive with at least 4 characters and is continous then true will be returned, else then false will be returned</returns>
        private static bool HasSequentialChars(string password)
        {
            // I need to create a variable and put the ascii code of the first character,
            // so that I can check the next character in the password is equal to the character that comes before
            int asciiChar = (int)password.ToLower()[0];

            // Counts the amount of characters which is sequential
            // Example: 12 equals 2, 123 equals 3, 1234 equals 4
            byte counter = 1;

            // Loop starts at 1 because the first character in the password is already casted to an ascii code
            for(int i = 1; i < password.Length; i++)
            {

                // It has to only check for letters and numbers and this will make sure of that
                if (!Char.IsLetterOrDigit(password[i - 1]))
                {
                    break;
                }

                if (asciiChar == (int)password.ToLower()[i]-i)
                {
                    counter++;

                    // When counter has found 4 sequential characters then return true
                    if (counter == 4)
                    {
                        return true;
                    }
                }

                // Resets the counter, and sets a new "char checker"
                else
                {
                    counter = 1;
                    asciiChar = (int)password.ToLower()[i];
                }
            }
            return false;
        }
        #endregion
    }
}
