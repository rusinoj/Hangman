using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hangman
{
    public class Program
    {
        //***************************************
        //Title: Hangman
        //Application Type: Application that facilitates a game of hangman for the user
        //Author: Jack Rusinowski
        //Date Created: 7/15/2021
        //Last Modified: 7/24/2021
        //***************************************

        public static List<char> pickedWord;
        public static List<char> wordGuessed;
        public static List<char> incorrectGuesses;
        public static void Main(string[] args)
        {
            SetTheme();
            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        public static void DisplayMenuScreen()
        {
            Console.Clear();            
            Console.CursorVisible = true;
            bool quitApplication = false;
            string menuChoice;
            do
            {
                DisplayScreenHeader("**Hangman Main Menu!**");
                Console.WriteLine("\ta) Instructions");
                Console.WriteLine("\tb) Pick Word");
                Console.WriteLine("\tc) Play Hangman");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayInstructions();
                        break;

                    case "b":
                        PickWord();
                        break;

                    case "c":
                        PlayHangman();
                        break;

                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);




            DisplayContinuePrompt();
        }
        
        public static void PlayHangman()
        {
            Console.Clear();
            string currentGuess;
            string guessed1 = "";
            string wrong1 = "";
            int lives = 0;
            List<char> incorrectGuesses = new List<char>() { };
            while (wordGuessed.Contains('_'))
            {
                var guessed = new string(wordGuessed.ToArray());
                var wrong = new string(incorrectGuesses.ToArray());
                DisplayScreenHeader("**Hangman!**");
                DisplayHangman(lives);
                Console.WriteLine();
                Console.WriteLine("\t" + guessed);
                Console.WriteLine("      Guessed Letters:" + wrong);
                Console.WriteLine("Guess a letter:");
                Console.WriteLine();
                currentGuess = Console.ReadLine();
                if (Regex.IsMatch(currentGuess, @"^[a-zA-Z]+$"))
                {
                    char guess = Convert.ToChar(currentGuess.ToUpper());

                    if (!wordGuessed.Contains(guess) && !incorrectGuesses.Contains(guess))
                    {
                        if (pickedWord.Contains(guess))
                        {
                            for (int x = 0; x < pickedWord.Count; x++)
                            {
                                if (pickedWord[x] == guess)
                                    wordGuessed[x] = guess;
                            }
                            Console.WriteLine();
                            Console.WriteLine("Correct guess!");
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Incorrect guess.");
                            incorrectGuesses.Add(guess);
                            if (incorrectGuesses.Count >= 6)
                            {
                                Console.WriteLine("Oh no! The man has been hanged!");
                                DisplayContinuePrompt();
                                break;
                            }
                            DisplayContinuePrompt();
                        }
                    } else
                    {
                        Console.WriteLine("That letter has already been guessed. Try again.");
                        DisplayContinuePrompt();
                    }

                } 
                else 
                {
                    Console.WriteLine("Invalid guess. Please guess a letter.");
                    DisplayContinuePrompt();
                }
                lives = incorrectGuesses.Count;
                guessed1 = guessed;
                wrong1 = wrong;
            }
            if (incorrectGuesses.Count >= 6)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\t\t**Hangman!**");
                Console.WriteLine("\tYou're out of guesses! You Lose!");
                lives = 6;
            }
            else
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\t\t**Hangman!**");
                Console.WriteLine("\tCongratulations, you won!");
                guessed1 = new string(pickedWord.ToArray());
            }
            DisplayHangman(lives);
            Console.WriteLine();
            Console.WriteLine("\t" + guessed1);
            Console.WriteLine("      Guessed Letters:" + wrong1);
            Console.WriteLine("\tGame Over. Thank you for playing.");
            Console.WriteLine("\tThe word was:");
            var myString = new string(pickedWord.ToArray());
            Console.WriteLine("\t\t\t**" + myString + "**");
            DisplayContinuePrompt();
        }

        public static void DisplayInstructions()
        {
            Console.Clear();
            DisplayScreenHeader("**Instructions!**");
            Console.WriteLine("\tHave a friend input a secret word.");
            Console.WriteLine("\tThen, guess the secret word letter by letter before the man's body completes.");
            Console.WriteLine("\tIf the word is not revealed in ample number of guesses, the man hangs and the game is lost.");
            DisplayContinuePrompt();
        }

        public static void PickWord()
        {
            Console.Clear();
            string word;
            wordGuessed = new List<char>();
            DisplayScreenHeader("**Pick Word!**");
            Console.WriteLine("Enter Word to Use:");
            word = Console.ReadLine().ToUpper();
            Program.pickedWord = word.ToList();
            for (var x = 0; x < pickedWord.Count; x++)
                wordGuessed.Add('_');
            var word2 = new string(pickedWord.ToArray());
            Console.WriteLine("You chose the word {0}.", word2);
            DisplayContinuePrompt();
        }
        public static void DisplayHangman(int lives)
        {
            if (lives <= 0)
            {
                Console.WriteLine(" ______");
                Console.WriteLine("|      |");
                Console.WriteLine("|       ");
                Console.WriteLine("|         ");
                Console.WriteLine("|         ");
                Console.WriteLine("|_______  ");
                Console.WriteLine("|******|");
            }
            else if (lives == 1)
            {


                Console.WriteLine(" ______");
                Console.WriteLine("|      |");
                Console.WriteLine("|      o ");
                Console.WriteLine("|         ");
                Console.WriteLine("|         ");
                Console.WriteLine("|_______  ");
                Console.WriteLine("|******|");
            }
            else if (lives == 2)
            {
                Console.WriteLine(" ______");
                Console.WriteLine("|      |");
                Console.WriteLine("|      o ");
                Console.WriteLine("|      |  ");
                Console.WriteLine("|         ");
                Console.WriteLine("|_______  ");
                Console.WriteLine("|******|");
            }
            else if (lives == 3)
            {
                Console.WriteLine(" ______");
                Console.WriteLine("|      |");
                Console.WriteLine("|      o ");
                Console.WriteLine(@"|     \|  ");
                Console.WriteLine("|         ");
                Console.WriteLine("|_______  ");
                Console.WriteLine("|******|");
            }
            else if (lives == 4)
            {
                Console.WriteLine(" ______");
                Console.WriteLine("|      |");
                Console.WriteLine("|      o ");
                Console.WriteLine(@"|     \|/ ");
                Console.WriteLine("|         ");
                Console.WriteLine("|_______  ");
                Console.WriteLine("|******|");
            }
            else if (lives == 5)
            {
                Console.WriteLine(" ______");
                Console.WriteLine("|      |");
                Console.WriteLine("|      o ");
                Console.WriteLine(@"|     \|/ ");
                Console.WriteLine("|     /  ");
                Console.WriteLine("|_______  ");
                Console.WriteLine("|******|");
            }
            else if (lives >= 6)
            {
                Console.WriteLine(" ______");
                Console.WriteLine("|      |");
                Console.WriteLine("|      o ");
                Console.WriteLine(@"|     \|/ ");
                Console.WriteLine(@"|     / \ ");
                Console.WriteLine("|_______  ");
                Console.WriteLine("|******|");
            }


        }

        
        #region USER INTERFACE
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            DisplayScreenHeader("**Hangman!**");
            Console.WriteLine();
            Console.WriteLine("\tThank you for playing Hangman.");
            Console.WriteLine("\tPlay again any time and have a good day.");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;
            Console.Clear();
            DisplayScreenHeader("**Hangman!**");
            Console.WriteLine();
            Console.WriteLine("\tWelcome to the Hangman application.");
            Console.WriteLine("\tPlay a round of this classic word guessing game.");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        static void SetTheme()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }
        #endregion
    }
}
