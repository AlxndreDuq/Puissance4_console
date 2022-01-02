using System;

namespace Puissance4_console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables + tableau
            bool turn = true;
            bool victoire = false;
            int rejouer = 1;
            string[,] Puissance4 = new string[7, 7];

            //Jeu
            while (rejouer == 1)
            {
                ClearTableau(ref Puissance4);
                victoire = false;

                while (!victoire)
                {
                    Console.Clear();
                    AfficherTableau(Puissance4);
                    Jouer(ref turn, ref Puissance4, ref victoire);
                }

                AfficherTableau(Puissance4);

                rejouer = SaisieInt("Voulez-vous rejouer (0 : Non / 1 : Oui)", 0, 1);
            }

            
        }

        //Fonction qui remet le tableau à zéro
        static void ClearTableau(ref string[,] tableau)
        {
            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                for (int a = 0; a < tableau.GetLength(1); a++)
                {
                    tableau[i, a] = " ";
                }

            }
        }
        
        //Fonction qui affiche le tableau
        static void AfficherTableau(string[,] tableau)
        {
            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                for (int a = 0; a < tableau.GetLength(1); a++)
                {
                    if (tableau[i, a] == "X")
                    {
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{tableau[i, a]}");
                        Console.ResetColor();
                    }
                    else if (tableau[i, a] == "O")
                    {
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{tableau[i, a]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write($"|{tableau[i, a]}");
                    }
                    
                }
                Console.Write("|");

                Console.WriteLine();
            }

            for (int i = 0; i < (tableau.GetLength(0) * 2) + 1; i++)
            {
                Console.Write("_");
            }

            Console.WriteLine();

            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                Console.Write($"|{i + 1}");
            }
            Console.Write("|");

            Console.WriteLine("\n");
        }

        //Fonction qui sécurise la saisie de chiffres
        static int SaisieInt(string question, int min, int max)
        {
            string saisie;
            int saisie_convertie;

            Console.WriteLine(question);
            saisie = Console.ReadLine();

            while(!Int32.TryParse(saisie, out saisie_convertie) || saisie_convertie < min || saisie_convertie > max)
            {
                Console.WriteLine($"Erreur, saisissez un chiffre entre {min} et {max}");
                saisie = Console.ReadLine();
            }

            return saisie_convertie;
        }

        //Fonction du jeu
        static void Jouer(ref bool turn, ref string[,] tableau, ref bool victoire)
        {
            //Le joueur saisie la colonne manuellement
            //int colonne = SaisieInt("Entrez le numéro de la colonne", 1, 7) - 1;

            //Le joueur choisi la colonne avec les flèches
            int colonne = 0;
            choixcolonnefleche(ref colonne);
            
            //Verifie si la colonne est pleine
            while (ColonnePleine(tableau, colonne))
            {
                Console.WriteLine("La colonne est pleine, choisissez en une autre");
                //colonne = SaisieInt("Entrez le numéro de la colonne", 1, 7) - 1;
                choixcolonnefleche(ref colonne);
            }

            //Change le X et O à chaque tour
            string XouO;

            if (turn)
            {
                XouO = "X";
            }
            else
            {
                XouO = "O";
            }

            //Met le X ou O le plus bas possible dans la colonne
            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                if (i == (tableau.GetLength(0)-1) || tableau[i + 1, colonne] != " ")
                {
                    tableau[i, colonne] = XouO;
                    victoire = Win(i, colonne, tableau);
                    turn = !turn;
                    break;
                }
                
            }
        }

        //Fonction qui verifie si une colonne est pleine
        static bool ColonnePleine(string[,] tableau, int colonne)
        {
            if (tableau[0, colonne] == " ")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Fonction qui verifie si il y a une victoire
        static bool Win(int x, int y, string[,] tableau)
        {

            int score = 1;

            //victoire verticale

            for (int i = 1; i < 4; i++)
            {
                if (!(x + i > 6))
                {
                    if (tableau[x + i, y] == tableau[x, y])
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }

                }
                else
                {
                    break;
                }
            }
            
            if (score_win(ref score, tableau, x, y))
            {
                return true;
            }

            //victoire horizontale
            for (int i = 1; i < 4; i++)
            {
                if (!(y + i > 6))
                {
                    if (tableau[x, y + i] == tableau[x, y])
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 4; i++)
            {

                if (!(y - i < 0))
                {
                    if (tableau[x, y - i] == tableau[x, y])
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }

            }

            if (score_win(ref score, tableau, x, y))
            {
                return true;
            }

            // diagonale \
            for (int i = 1; i < 4; i++)
            {
                if (!(x + i > 6 || y + i > 6))
                {
                    if (tableau[x + i, y + i] == tableau[x, y])
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 4; i++)
            {
                if (!(x - i < 0 || y - i < 0))
                {
                    if (tableau[x - i, y - i] == tableau[x, y])
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            if (score_win(ref score, tableau, x, y))
            {
                return true;
            }

            // diagonale /

            for (int i = 1; i < 4; i++)
            {
                if (!(x + i > 6 || y - i < 0))
                {
                    if (tableau[x + i, y - i] == tableau[x, y])
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 4; i++)
            {
                if (!(x - i < 0 || y + i > 6))
                {
                    if (tableau[x - i, y + i] == tableau[x, y])
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            
            if (score_win(ref score, tableau, x, y))
            {
                return true;
            }


            return false;
            
        }

        //Fonction qui verifie si il y a une victoire grâce au score dans la fonction win
        static bool score_win(ref int score, string[,] tableau, int x, int y)
        {
            if (score >= 4)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Victoire des {tableau[x, y]}");
                Console.ResetColor();
                return true;
            }
            else
            {
                score = 1;
                return false;
            }
        }

        //Fonction qui permet aux joueurs de choisir la colonne avec les fleches
        static void choixcolonnefleche(ref int colonne)
        {
            int cursor_x = 1;
            ConsoleKeyInfo key;
            Console.SetCursorPosition(cursor_x, 7);

            do
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.LeftArrow && colonne > 0)
                {
                    colonne--;
                    cursor_x -= 2;
                    Console.SetCursorPosition(cursor_x, 7);

                }

                if (key.Key == ConsoleKey.RightArrow && colonne < 6)
                {
                    colonne++;
                    cursor_x += 2;
                    Console.SetCursorPosition(cursor_x, 7);
                }

            } while (key.Key != ConsoleKey.Enter);
        }
    }
}
