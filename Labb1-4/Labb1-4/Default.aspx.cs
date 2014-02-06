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
        public SecretNumber SecretNumber
        {
            get 
            {
                return (SecretNumber)Session["SecretNum"];
            }
            private set 
            { 
                Session["SecretNum"] = value; 
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SecretNumber == null)
            {
                SecretNumber = new SecretNumber();
            }
        }

        protected void btnGuess_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (SecretNumber.CanMakeGuess)
                {
                    Outcome outcome = SecretNumber.MakeGuess(int.Parse(txtGuess.Text));

                    string output = "";
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
                            lblEndNumberOfGuesses.Text = String.Format(lblEndNumberOfGuesses.Text, SecretNumber.NumberOfGuesses);
                            lblEndNumberOfGuesses.Visible = true;
                            btnGuess.Enabled = false;
                            txtGuess.Text = "";
                            txtGuess.Enabled = false;
                            break;
                        case Outcome.PreviousGuess:
                            lblPreviousGuess.Text = String.Format(lblPreviousGuess.Text, txtGuess.Text);
                            lblPreviousGuess.Visible = true;
                            break;
                        default:
                            break;
                    }

                    phGuessResult.Controls.Add(new LiteralControl(output));
                    phGuessResult.Visible = true;

                    if (SecretNumber.NumberOfGuesses == 7 && outcome != Outcome.Correct)
                    {
                        lblFailed.Text = String.Format(lblFailed.Text, SecretNumber.SecretNum);
                        lblFailed.Visible = true;
                        btnGuess.Enabled = false;
                        txtGuess.Text = "";
                        txtGuess.Enabled = false;
                    }
                }
                else
                {
                    lblFailed.Text = String.Format(lblFailed.Text, SecretNumber.SecretNum);
                    lblFailed.Visible = true;
                }

                lblPreviousGuesses.Text = String.Format(lblPreviousGuesses.Text, string.Join(", ", SecretNumber.PreviousGuesses));
                lblPreviousGuesses.Visible = true;

            }
        }

        protected void btnNewNumber_Click(object sender, EventArgs e)
        {
            SecretNumber.Initialize();
        }
    }
}