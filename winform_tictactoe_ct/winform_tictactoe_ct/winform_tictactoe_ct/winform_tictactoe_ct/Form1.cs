using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winform_tictactoe_ct
{
    public partial class Form1 : Form
    {
        //global vars
        bool isPlayer2Turn = false;
        int turnCount = 0;
        string currentPlayer = "X";          

        public Form1()
        {
            InitializeComponent();
            resetBoard();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome to Tic Tac Toe, by Clinton Tyson", "About");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Text = (isPlayer2Turn) ? "O" : "X";
            currentPlayer = btn.Text;
            isPlayer2Turn = !isPlayer2Turn;
            turnCount++;
            btn.Enabled = false;           
            evaluateWinner();
        }       

        private void evaluateWinner()
        {
            if (turnCount <= 9)
            {
                if (weHaveAWinner())
                {
                    disableButtons();
                    string msg = ((isPlayer2Turn) ? txtPlayer1.Text : txtPlayer2.Text) + " is the Winner!";
                    MessageBox.Show(msg, "Winner!");
                    incrementWins();                    
                }
                else
                {
                    toggleTurn();                    
                }
            }
            else 
            {
                toggleTurn();
                lblDraws.Text = (Convert.ToInt32(lblDraws.Text) + 1).ToString();
                MessageBox.Show("We have a draw.", "Draw!");            
            }                  
        }

        private void toggleTurn()
        {
            imgP1.Visible = !isPlayer2Turn;
            imgP2.Visible = isPlayer2Turn;
        }

        private void incrementWins()
        {
            if (isPlayer2Turn)
            {
                lblPlayer1Wins.Text = (Convert.ToInt32(lblPlayer1Wins.Text) + 1).ToString();
            }
            else
            {
                lblPlayer2Wins.Text = (Convert.ToInt32(lblPlayer2Wins.Text) + 1).ToString();
            }
        }

        private bool weHaveAWinner()
        {
            return (checkHorizonalRows() || checkVerticalRows() || checkDiagonalRows());
        }        

        private bool checkHorizonalRows()
        {
            ArrayList btnList = new ArrayList();
            bool blnWinner = false;

            // Check for horizontal winners
            if (new[] { btnA1.Text, btnA2.Text, btnA3.Text }.All(x => x == currentPlayer))
            {
                btnList.Add(btnA1);
                btnList.Add(btnA2);
                btnList.Add(btnA3);
                blnWinner = true;
            }
            if (new[] { btnB1.Text, btnB2.Text, btnB3.Text }.All(x => x == currentPlayer))
            {
                btnList.Add(btnB1);
                btnList.Add(btnB2);
                btnList.Add(btnB3);
                blnWinner = true;
            }
            if (new[] { btnC1.Text, btnC2.Text, btnC3.Text }.All(x => x == currentPlayer))
            {
                btnList.Add(btnC1);
                btnList.Add(btnC2);
                btnList.Add(btnC3);                
                blnWinner = true;
            }

            if (blnWinner)
                highlightRow(btnList);

            return blnWinner;
        }

        private bool checkVerticalRows()
        {
            ArrayList btnList = new ArrayList();
            bool blnWinner = false;

            //// Check for veritical winners
            if (new[] { btnA1.Text, btnB1.Text, btnC1.Text }.All(x => x == currentPlayer))
            {
                btnList.Add(btnA1);
                btnList.Add(btnB1);
                btnList.Add(btnC1);
                blnWinner = true;
            }
            if (new[] { btnA2.Text, btnB2.Text, btnC2.Text }.All(x => x == currentPlayer))
            {
                btnList.Add(btnA2);
                btnList.Add(btnB2);
                btnList.Add(btnC2);
                blnWinner = true;
            }
            if (new[] { btnA3.Text, btnB3.Text, btnC3.Text }.All(x => x == currentPlayer))
            {
                btnList.Add(btnA3);
                btnList.Add(btnB3);
                btnList.Add(btnC3);
                blnWinner = true;
            }

            if (blnWinner)
                highlightRow(btnList);

            return blnWinner;
        }

        private bool checkDiagonalRows()
        {
            ArrayList btnList = new ArrayList();
            bool blnWinner = false;

            //// Check for diagonal winners
            if (new[] { btnA1.Text, btnB2.Text, btnC3.Text }.All(x => x == currentPlayer))
            {
                btnList.Add(btnA1);
                btnList.Add(btnB2);
                btnList.Add(btnC3);
                blnWinner = true;
            }
            if (new[] { btnA3.Text, btnB2.Text, btnC1.Text }.All(x => x == currentPlayer))
            {
                btnList.Add(btnA3);
                btnList.Add(btnB2);
                btnList.Add(btnC1);
                blnWinner = true;
            }

            if (blnWinner)
                highlightRow(btnList);

            return blnWinner;
        }

        private void disableButtons() 
        {
            foreach(Control ctrl in Controls) 
            {
                if(ctrl.GetType() == typeof(Button)) 
                {
                    Button btn = (Button)ctrl;
                    btn.Enabled = false;
                }
            }
        }

        private void highlightRow(ArrayList arrList)
        {
            foreach (Button item in arrList) 
            {
                item.BackColor = SystemColors.GradientActiveCaption;
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            resetBoard();
        }

        private void resetBoard()
        {
            isPlayer2Turn = false;
            turnCount = 0;
            currentPlayer = "X";
            toggleTurn();

            foreach (Control ctrl in Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    Button btn = (Button)ctrl;
                    btn.Enabled = true;
                    btn.Text = "";
                    btn.BackColor = SystemColors.Control;
                    btn.ForeColor = Color.Black;
                }
            }
        }
    }
}
