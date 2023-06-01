using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai7
{
    class TicTacToe
    {
        public void PlayGame()
        {
            char[,] board = new char[3, 3] { { '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };
            Node root = new Node { Board = board, Score = 0, Children = new List<Node>(), IsMax = true };
            CreateTree(root, 0);
            int[] move = FindBestMove(root);
            Console.WriteLine("Best move: ({0}, {1})", move[0], move[1]);
        }

        public void CreateTree(Node node, int depth)
        {
            if (depth >= 9) // the board is full, so no more moves can be made
                return;

            if (node.IsMax) // it's the maximizer's turn (i.e. X's turn)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (node.Board[i, j] == '-')
                        {
                            char[,] newBoard = (char[,])node.Board.Clone();
                            newBoard[i, j] = 'X';
                            Node child = new Node { Board = newBoard, Score = 0, Children = new List<Node>(), IsMax = false };
                            CreateTree(child, depth + 1);
                            int childScore = EvaluateBoard(child.Board);
                            child.Score = childScore;
                            node.Children.Add(child);
                            bestScore = Math.Max(bestScore, childScore);
                        }
                    }
                }
                node.Score = bestScore;
            }
            else // it's the minimizer's turn (i.e. O's turn)
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (node.Board[i, j] == '-')
                        {
                            char[,] newBoard = (char[,])node.Board.Clone();
                            newBoard[i, j] = 'O';
                            Node child = new Node { Board = newBoard, Score = 0, Children = new List<Node>(), IsMax = true };
                            CreateTree(child, depth + 1);
                            int childScore = EvaluateBoard(child.Board);
                            child.Score = childScore;
                            node.Children.Add(child);
                            bestScore = Math.Min(bestScore, childScore);
                        }
                    }
                }
                node.Score = bestScore;
            }
        }

        public int EvaluateBoard(char[,] board)
        {
            // This function evaluates the board and returns a score
            // for the given state of the game. The score is calculated
            // by counting the number of rows, columns, and diagonals
            // that have either X or O in them. If there are more X's
            // than O's, the score is positive. If there are more O's
            // than X's, the score is negative. Otherwise, the score is 0.

            int xCount = 0;
            int oCount = 0;

            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == 'X' && board[i, 1] == 'X' && board[i, 2] == 'X')
                {
                    xCount++;
                }
                else if (board[i, 0] == 'O' && board[i, 1] == 'O' && board[i, 2] == 'O')
                {
                    oCount++;
                }
            }
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == 'X' && board[1, j] == 'X' && board[2, j] == 'X')
                {
                    xCount++;
                }
                else if (board[0, j] == 'O' && board[1, j] == 'O' && board[2, j] == 'O')
                {
                    oCount++;
                }
            }

            // Check diagonals
            if (board[0, 0] == 'X' && board[1, 1] == 'X' && board[2, 2] == 'X')
            {
                xCount++;
            }
            else if (board[0, 0] == 'O' && board[1, 1] == 'O' && board[2, 2] == 'O')
            {
                oCount++;
            }

            if (board[0, 2] == 'X' && board[1, 1] == 'X' && board[2, 0] == 'X')
            {
                xCount++;
            }
            else if (board[0, 2] == 'O' && board[1, 1] == 'O' && board[2, 0] == 'O')
            {
                oCount++;
            }

            if (xCount > oCount)
            {
                return 1;
            }
            else if (oCount > xCount)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public int[] FindBestMove(Node root)
        {
            int bestScore = int.MinValue;
            int[] bestMove = new int[2];
            foreach (Node child in root.Children)
            {
                if (child.Score > bestScore)
                {
                    bestScore = child.Score;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (root.Board[i, j] != child.Board[i, j])
                            {
                                bestMove[0] = i;
                                bestMove[1] = j;
                            }
                        }
                    }
                }
            }
            return bestMove;
        }
    }
}

