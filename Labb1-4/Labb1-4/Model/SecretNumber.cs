using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labb1_4.Model
{
    public class SecretNumber
    {
        int secretNumber;
        List<int> previousGuesses;
        const int MaxNumberOfGuesses = 7;
        Outcome Outcome { get; set; }

        public SecretNumber()
        {
            Initialize();
        }

        public int SecretNumber
        {
            get
            {
                return secretNumber;
            }
            set
            {
                secretNumber = value;
                
            }
        }

        public IEnumerable<int> PreviousGuesses
        {
            get
            {
                return previousGuesses;
            }
        }

        public void Initialize()
        {
            secretNumber = GenerateRandomInteger(1, 100);
            previousGuesses.Clear();
            Outcome = Outcome.Indefinite;
        }

        public Outcome MakeGuess(int guess)
        {
            Outcome outcome = Outcome.Indefinite;

            if (previousGuesses.Count > MaxNumberOfGuesses)
            {
                outcome = Outcome.NoMoreGuesses;
            }
            else if (previousGuesses.Exists(pg => pg == guess))
            {
                outcome = Outcome.PreviousGuess;
            }
            else if (guess < secretNumber)
            {
                outcome = Outcome.Low;
            }
            else if (guess > secretNumber)
            {
                outcome = Outcome.High;
            }
            else if (guess == secretNumber)
            {
                outcome = Outcome.Correct;
            }

            return outcome;
        }

        

        /// <summary>
        /// Skapar ett nytt heltalsvärde.
        /// </summary>
        /// <param name="min">Det lägsta möjliga heltalsvärdet.</param>
        /// <param name="max">Det högsta möjliga heltalsvärdet.</param>
        /// <returns>Det genererade heltalet.</returns>
        private int GenerateRandomInteger(int min, int max)
        {
            return new Random().Next(min, max + 1);
        }
    }
}