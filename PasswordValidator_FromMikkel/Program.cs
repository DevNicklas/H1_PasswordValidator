using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator_FromMikkel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                StartController();
            } while (true);
        }

        #region Model
        private static string GetUserPassword()
        {
            return Console.ReadLine();
        }
        #endregion

        #region View
        /// <summary>
        /// Writes an given instruction to the standard output stream (console window)
        /// </summary>
        /// <param name="instruction"></param>
        private static void WriteGUIText()
        {
            Console.WriteLine("Welcome to the Password Validator\n");
            Console.WriteLine("Please read below, and enter a password");
            Console.Write("A password is ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("valid ");
            Console.ResetColor();
            Console.WriteLine("if:");
            Console.WriteLine("- it contains at least 12 characters and maximum 64 characters");
            Console.WriteLine("- it contains upper- and lowercase letters");
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

        private static void DrawResponseBar()
        {
        }

        #endregion

        #region Controller
        private static void StartController()
        {
            bool isValid = false;

            do
            {
                WriteGUIText();

                string userPassword = GetUserPassword();
                isValid = ValidatePassword(userPassword);
            } while (!isValid);
        }

        /// <summary>
        /// Validates the given password
        /// </summary>
        /// <param name="password">password to validate</param>
        /// <returns>A boolean. If the password is valid, then it returns true, but if not then return false</returns>
        private static bool ValidatePassword(string password)
        {
            if (password.Length < 12 || password.Length > 64)
            {
                return false;
            }

            if (!HasUpperAndLower(password))
            {
                return false;
            }

            if (!HasNumber(password))
            {
                return false;
            }

            if (!HasSpecialChar(password))
            {
                return false;
            }

            return true;
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
                if(!Char.IsLetterOrDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool HasRepeatedChars(string password)
        {
            
            return false;
        }

        private static bool HasSequentialChars()
        {
            return false;
        }
        #endregion
    }
}
