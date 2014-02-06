using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labb1_4.Model;

namespace Labb1_4
{
    public partial class Default : System.Web.UI.Page
    {
        //egenskap: instans av klassen SecretNumber
        public SecretNumber SecretNumber
        {
            get 
            {
                //istället för (SecretNumber)Session["SecretNum"]; för att undvika risk för InvalidCastException.
                return Session["SecretNum"] as SecretNumber;
            }
            private set 
            { 
                Session["SecretNum"] = value; 
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //vid första sidladdningen skapas en instans av klassen SecretNumber
            //som lagras i egenskapen SecretNumber
            if (SecretNumber == null)
            {
                SecretNumber = new SecretNumber();
            }
        }

        //händelsehanterare för gissnings-knappen.
        protected void btnGuess_Click(object sender, EventArgs e)
        {
            //kontroll att validering lyckats
            if (IsValid)
            {
                //kontroll att fler gissningar är tillåtna att göra
                if (SecretNumber.CanMakeGuess)
                {
                    //gissar ett tal. resultatet lagras i outcome
                    Outcome outcome = SecretNumber.MakeGuess(int.Parse(txtGuess.Text));

                    string output = "";

                    //ger feedback till användaren beroende på resultatet av gissningen
                    switch (outcome)
                    {
                        case Outcome.High:
                            output = "<img src=\"Images/toohigh.png\" />För högt!";
                            break;
                        case Outcome.Low:
                            output = "<img src=\"Images/toolow.png\" />För lågt!";
                            break;
                        case Outcome.Correct:
                            output = "<img src=\"Images/correct.png\" />Korrekt!";
                            //informerar användaren att han/hon klarade att gissa
                            //rätt tal på x antal gissningar.
                            lblEndNumberOfGuesses.Text = String.Format(lblEndNumberOfGuesses.Text, SecretNumber.NumberOfGuesses);
                            lblEndNumberOfGuesses.Visible = true;
                            //förhindrar ytterligare gissningar
                            btnGuess.Enabled = false;
                            txtGuess.Text = "";
                            txtGuess.Enabled = false;
                            break;
                        case Outcome.PreviousGuess:
                            //informerar användaren att gissning på detta tal redan gjorts
                            lblPreviousGuess.Text = String.Format(lblPreviousGuess.Text, txtGuess.Text);
                            lblPreviousGuess.Visible = true;
                            break;
                        default:
                            break;
                    }

                    //visar användaren resultatet av gissningen (för högt/för lågt/korrekt/inget)
                    phGuessResult.Controls.Add(new LiteralControl(output));
                    phGuessResult.Visible = true;

                    //om gissningen var felaktig och antalet gissningar uppgått till 7
                    //informeras användaren att inga fler gissningar får göras
                    //och vilket det hemliga talet var. Ytterligare gissningar tillåts inte.
                    if (SecretNumber.NumberOfGuesses == 7 && outcome != Outcome.Correct)
                    {
                        lblFailed.Text = String.Format(lblFailed.Text, SecretNumber.SecretNum);
                        lblFailed.Visible = true;
                        btnGuess.Enabled = false;
                        txtGuess.Text = "";
                        txtGuess.Enabled = false;
                    }
                }
                //egentligen kommer denna kod inte köras.
                //om inga fler gissningar får göras
                else
                {
                    //användaren informeras att max antal gissningar har gjorts och
                    //vilket det hemliga talet var.
                    lblFailed.Text = String.Format(lblFailed.Text, SecretNumber.SecretNum);
                    lblFailed.Visible = true;
                }

                //oavsett resultat listas gjorda gissningar
                lblPreviousGuesses.Text = String.Format(lblPreviousGuesses.Text, string.Join(", ", SecretNumber.PreviousGuesses));
                lblPreviousGuesses.Visible = true;

            }
        }

        //initierar en ny omgång av spelet, med nytt slumpat hemligt tal.
        protected void btnNewNumber_Click(object sender, EventArgs e)
        {
            SecretNumber.Initialize();
        }
    }
}