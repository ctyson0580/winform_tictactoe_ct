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
        bool blnTurn = false;
        int intTurnCount = 0;
        string strCurrentPlayer = "X";          

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
            btn.Text = (blnTurn) ? "O" : "X";
            strCurrentPlayer = btn.Text;
            blnTurn = !blnTurn;
            intTurnCount++;
            btn.Enabled = false;           
            evaluateWinner();
        }       

        private void evaluateWinner()
        {
            if (intTurnCount < 9)
            {
                if (weHaveAWinner())
                {
                    disableButtons();
                    string msg = ((blnTurn) ? txtPlayer1.Text : txtPlayer2.Text) + " is the Winner!";
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
            imgP1.Visible = !blnTurn;
            imgP2.Visible = blnTurn;
        }

        private void incrementWins()
        {
            if (blnTurn)
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
            ArrayList arrList = new ArrayList();
            bool blnWinner = false;

            // Check for horizontal winners
            if (new[] { btnA1.Text, btnA2.Text, btnA3.Text }.All(x => x == strCurrentPlayer))
            {
                arrList.Add(btnA1);
                arrList.Add(btnA2);
                arrList.Add(btnA3);
                blnWinner = true;
            }
            if (new[] { btnB1.Text, btnB2.Text, btnB3.Text }.All(x => x == strCurrentPlayer))
            {
                arrList.Add(btnB1);
                arrList.Add(btnB2);
                arrList.Add(btnB3);
                blnWinner = true;
            }
            if (new[] { btnC1.Text, btnC2.Text, btnC3.Text }.All(x => x == strCurrentPlayer))
            {
                arrList.Add(btnC1);
                arrList.Add(btnC2);
                arrList.Add(btnC3);                
                blnWinner = true;
            }

            if (blnWinner)
                highlightRow(arrList);

            return blnWinner;
        }

        private bool checkVerticalRows()
        {
            ArrayList arrList = new ArrayList();
            bool blnWinner = false;

            //// Check for veritical winners
            if (new[] { btnA1.Text, btnB1.Text, btnC1.Text }.All(x => x == strCurrentPlayer))
            {
                arrList.Add(btnA1);
                arrList.Add(btnB1);
                arrList.Add(btnC1);
                blnWinner = true;
            }
            if (new[] { btnA2.Text, btnB2.Text, btnC2.Text }.All(x => x == strCurrentPlayer))
            {
                arrList.Add(btnA2);
                arrList.Add(btnB2);
                arrList.Add(btnC2);
                blnWinner = true;
            }
            if (new[] { btnA3.Text, btnB3.Text, btnC3.Text }.All(x => x == strCurrentPlayer))
            {
                arrList.Add(btnA3);
                arrList.Add(btnB3);
                arrList.Add(btnC3);
                blnWinner = true;
            }

            if (blnWinner)
                highlightRow(arrList);

            return blnWinner;
        }

        private bool checkDiagonalRows()
        {
            ArrayList arrList = new ArrayList();
            bool blnWinner = false;

            //// Check for diagonal winners
            if (new[] { btnA1.Text, btnB2.Text, btnC3.Text }.All(x => x == strCurrentPlayer))
            {
                arrList.Add(btnA1);
                arrList.Add(btnB2);
                arrList.Add(btnC3);
                blnWinner = true;
            }
            if (new[] { btnA3.Text, btnB2.Text, btnC1.Text }.All(x => x == strCurrentPlayer))
            {
                arrList.Add(btnA3);
                arrList.Add(btnB2);
                arrList.Add(btnC1);
                blnWinner = true;
            }

            if (blnWinner)
                highlightRow(arrList);

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
            blnTurn = false;
            intTurnCount = 0;
            strCurrentPlayer = "X";
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
