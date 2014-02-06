using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labb1_4.Model
{
    public class SecretNumber
    {
        //privat instansvariabel: lista med gjorda gissningar
        List<int> previousGuesses;

        //privat konstant: maximalt antal tillåtna gissningar
        const int MaxNumberOfGuesses = 7;

        //egenskap: det hemliga talet
        public int SecretNum { get; private set; }

        //egenskap: utfallet av den senast gjorda gissningen
        public Outcome Outcome { get; set; }

        //egenskap: readonly-lista med gjorda gissningar
        public IEnumerable<int> PreviousGuesses
        {
            get { return previousGuesses; }
        }

        //egenskap: om gissning kan göras
        public bool CanMakeGuess
        {
            get { return (previousGuesses.Count < MaxNumberOfGuesses); }
        }

        //egenskap: antal gjorda gissningar
        public int NumberOfGuesses
        {
            get { return previousGuesses.Count; }
        }

        //konstruktor: skapar nytt objekt av klassen SecretNumber.
        public SecretNumber()
        {
            Initialize();
        }

        //Initierar gissningsobjektet: nollställer gissningar och skapar nytt hemligt tal.
        public void Initialize()
        {
            SecretNum = GenerateRandomInteger(1, 100);
            if (previousGuesses == null)
            {
                previousGuesses = new List<int>();
            }
            else
            {
                previousGuesses.Clear();                
            }

            Outcome = Outcome.Indefinite;
        }

        //kontrollerar och returnerar utfallet av gjord gissning och uppdaterar listan med gjorda gissningar.
        public Outcome MakeGuess(int guess)
        {
            Outcome outcome = Outcome.Indefinite;

            if (previousGuesses.Count == MaxNumberOfGuesses)
            {
                outcome = Outcome.NoMoreGuesses;
            }
            else if (previousGuesses.Exists(pg => pg == guess))
            {
                outcome = Outcome.PreviousGuess;
            }
            else if (guess < SecretNum)
            {
                outcome = Outcome.Low;
            }
            else if (guess > SecretNum)
            {
                outcome = Outcome.High;
            }
            else if (guess == SecretNum)
            {
                outcome = Outcome.Correct;
            }

            //lägg endast till gissningen till listan med gjorda gissningar
            //om gissningen på talet inte redan gjorts.
            if (outcome != Outcome.PreviousGuess)
            {
                previousGuesses.Add(guess);
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