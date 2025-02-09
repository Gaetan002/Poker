﻿using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Poker
{
    class Program
    {
        // -----------------------
        // DECLARATION DES DONNEES
        // -----------------------
        // Importation des DL (librairies de code) permettant de gérer les couleurs en mode console
        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, int wAttributes);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetStdHandle(uint nStdHandle);
        static uint STD_OUTPUT_HANDLE = 0xfffffff5;
        static IntPtr hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
        // Pour utiliser la fonction C 'getchar()' : saisie d'un caractère
        [DllImport("msvcrt")]
        static extern int _getche();

        //-------------------
        // TYPES DE DONNEES
        //-------------------

        // Fin du jeu
        public static bool fin = false;

        // Codes COULEUR
        public enum Couleur { VERT = 10, ROUGE = 12, JAUNE = 14, BLANC = 15, NOIRE = 0, ROUGESURBLANC = 252, NOIRESURBLANC = 240 };

        // Coordonnées pour l'affichage
        public struct Coordonnees
        {
            public int x;
            public int y;
        }

        // Une carte
        public struct Carte
        {
            public char valeur;
            public int famille;
        };

        // Liste des combinaisons possibles
        public enum Combinaison { RIEN, PAIRE, DOUBLE_PAIRE, BRELAN, QUINTE, FULL, COULEUR, CARRE, QUINTE_FLUSH };

        // Valeurs des cartes : As, Roi,...
        public static char[] valeurs = { 'A', 'R', 'D', 'V', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

        // Codes ASCII (3 : coeur, 4 : carreau, 5 : trèfle, 6 : pique)
        public static int[] familles = { 3, 4, 5, 6 };

        // Numéros des cartes à échanger
        public static int[] echange = { 0, 0, 0, 0 };

        // Jeu de 5 cartes
        public static Carte[] MonJeu = new Carte[5];

        //----------
        // FONCTIONS
        //----------

        // Génère aléatoirement une carte : {valeur;famille}
        // Retourne une expression de type "structure carte"
        public static Carte tirage()
        {
            Random random = new Random();
            Carte uneCarte = default(Carte);
            uneCarte.valeur = valeurs[random.Next(0, valeurs.Length)];
            uneCarte.famille = familles[random.Next(0, familles.Length)];
            return uneCarte;
        }

        // Indique si une carte est déjà présente dans le jeu
        // Paramètres : une carte, le jeu 5 cartes, le numéro de la carte dans le jeu
        // Retourne un entier (booléen)
        public static bool carteUnique(Carte uneCarte, Carte[] unJeu, int numero)
        {
            numero = uneCarte.valeur;

            if (numero == unJeu[0].valeur && numero== unJeu[1].valeur && numero == unJeu[2].valeur && numero == unJeu[3].valeur && numero == unJeu[4].valeur)
            {

                Console.WriteLine("La carte est déjà présente dans le jeu.");
                return false;
            }
            else
            {
                Console.WriteLine("La carte est actuellement unique au jeu présent.");
                return true;
            }
        }

        // Calcule et retourne la COMBINAISON (paire, double-paire... , quinte-flush)
        // pour un jeu complet de 5 cartes.
        // La valeur retournée est un élement de l'énumération 'combinaison' (=constante)
        public static Combinaison chercheCombinaison(Carte[] unJeu)
        {
            // Combinaison d'une famille
            for (int i = 0; i < unJeu.Length; i++)
            {
                for (int j = 1; j < unJeu.Length; i++)
                {
                    if (unJeu[i].famille == unJeu[j].famille)
                    {

                    }
                }
            }

            // Combinaison d'une valeur
            int nbpaire = 0;
            for(int i = 0; i < unJeu.Length; i++)
            {
                for (int j = 1; j < unJeu.Length; i++)
                {
                    if (unJeu[i].valeur == unJeu[j].valeur)
                    {
                        nbpaire += 1;
                    } 
                }
            }
            if (nbpaire == 2)
            {
                return Combinaison.DOUBLE_PAIRE;
            }
        }

        // Echange des cartes
        // Paramètres : le tableau de 5 cartes et le tableau des numéros des cartes à échanger
        private static void echangeCarte(Carte[] unJeu, int[] e)
        {


        }

        // Pour afficher le Menu pricipale
        private static void afficheMenu()
        {
            Console.WriteLine(
                "\n|==============|\n" +
                "|     MENU     |\n" +
                "|==============|\n" +
                "|  1 - Jouer   |\n" +
                "|==============|\n" +
                "|  2 - Score   |\n" +
                "|==============|\n" +
                "|  3 - Fin     |\n" +
                "|==============|\n"
            );
        }

        // Jouer au Poker
        // Ici que vous appellez toutes les fonction permettant de joueur au poker
        private static void jouerAuPoker()
        {
            tirage();
            affichageCarte();
            carteUnique();
            chercheCombinaison();
        }

        // Tirage d'un jeu de 5 cartes
        // Paramètre : le tableau de 5 cartes à remplir
        private static void tirageDuJeu(Carte[] unJeu)
        {

        }

        // Affiche à l'écran une carte {valeur;famille} 
        private static void affichageCarte(Carte uneCarte)
        {
            //----------------------------
            // TIRAGE D'UN JEU DE 5 CARTES
            //----------------------------
            int left = 0;
            int c = 1;
            // Tirage aléatoire de 5 cartes
            for (int i = 0; i < 5; i++)
            {
                // Tirage de la carte n°i (le jeu doit être sans doublons !)

                // Affichage de la carte
                if (MonJeu[i].famille == 3 || MonJeu[i].famille == 4)
                {
                    SetConsoleTextAttribute(hConsole, 252);
                }
                else
                {
                    SetConsoleTextAttribute(hConsole, 240);
                }

                Console.SetCursorPosition(left, 5);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", '*', '-', '-', '-', '-', '-', '-', '-', '-', '-', '*');
                Console.SetCursorPosition(left, 6);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", '|', (char)MonJeu[i].famille, ' ', (char)MonJeu[i].famille, ' ', (char)MonJeu[i].famille, ' ', (char)MonJeu[i].famille, ' ', (char)MonJeu[i].famille, '|');
                Console.SetCursorPosition(left, 7);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|');
                Console.SetCursorPosition(left, 8);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", '|', (char)MonJeu[i].famille, ' ', ' ', ' ', ' ', ' ', ' ', ' ', (char)MonJeu[i].famille, '|');
                Console.SetCursorPosition(left, 9);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", '|', ' ', ' ', ' ', (char)MonJeu[i].valeur, (char)MonJeu[i].valeur, (char)MonJeu[i].valeur, ' ', ' ', ' ', '|');
                Console.SetCursorPosition(left, 10);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", '|', (char)MonJeu[i].famille, ' ', ' ', (char)MonJeu[i].valeur, (char)MonJeu[i].valeur, (char)MonJeu[i].valeur, ' ', ' ', (char)MonJeu[i].famille, '|');
                Console.SetCursorPosition(left, 11);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", '|', ' ', ' ', ' ', (char)MonJeu[i].valeur, (char)MonJeu[i].valeur, (char)MonJeu[i].valeur, ' ', ' ', ' ', '|');
                Console.SetCursorPosition(left, 12);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", '|', (char)MonJeu[i].famille, ' ', ' ', ' ', ' ', ' ', ' ', ' ', (char)MonJeu[i].famille, '|');
                Console.SetCursorPosition(left, 13);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|');
                Console.SetCursorPosition(left, 14);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", '|', (char)MonJeu[i].famille, ' ', (char)MonJeu[i].famille, ' ', (char)MonJeu[i].famille, ' ', (char)MonJeu[i].famille, ' ', (char)MonJeu[i].famille, '|');
                Console.SetCursorPosition(left, 15);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", '*', '-', '-', '-', '-', '-', '-', '-', '-', '-', '*');
                Console.SetCursorPosition(left, 16);
                SetConsoleTextAttribute(hConsole, 10);
                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\n", ' ', ' ', ' ', ' ', ' ', c, ' ', ' ', ' ', ' ', ' ');
                left = left + 15;
                c++;
            }

        }

        // Enregistre le score dans le txt
        private static void enregistrerJeu(Carte[] unJeu)
        {

        }

        // Affiche le Scores
        private static void voirScores()
        {

        }

        // Affiche résultat
        private static void afficheResultat(Carte[] unJeu)
        {
            SetConsoleTextAttribute(hConsole, 012);
            Console.Write("RESULTAT - Vous avez : ");
            try
            {
                // Test de la combinaison
                switch (chercheCombinaison(ref MonJeu))
                {
                    case Combinaison.RIEN:
                        Console.WriteLine("rien du tout... desole!"); break;
                    case Combinaison.PAIRE:
                        Console.WriteLine("une simple paire..."); break;
                    case Combinaison.DOUBLE_PAIRE:
                        Console.WriteLine("une double paire; on peut esperer..."); break;
                    case Combinaison.BRELAN:
                        Console.WriteLine("un brelan; pas mal..."); break;
                    case Combinaison.QUINTE:
                        Console.WriteLine("une quinte; bien!"); break;
                    case Combinaison.FULL:
                        Console.WriteLine("un full; ouahh!"); break;
                    case Combinaison.COULEUR:
                        Console.WriteLine("une couleur; bravo!"); break;
                    case Combinaison.CARRE:
                        Console.WriteLine("un carre; champion!"); break;
                    case Combinaison.QUINTE_FLUSH:
                        Console.WriteLine("une quinte-flush; royal!"); break;
                };
            }
            catch
            { 

            }
        }


        //--------------------
        // Fonction PRINCIPALE
        //--------------------
        static void Main(string[] args)
        {
            //---------------
            // BOUCLE DU JEU
            //---------------
            char reponse;
            while (true)
            {
                afficheMenu();
                reponse = (char)_getche();
                if (reponse != '1' && reponse != '2' && reponse != '3')
                {
                    Console.Clear();
                    afficheMenu();
                }
                else
                {
                    SetConsoleTextAttribute(hConsole, 015);
                    // Jouer au Poker
                    if (reponse == '1')
                    {
                        int i = 0;
                        jouerAuPoker();
                    }
                    if (reponse == '2')
                    {
                        voirScores();
                    }
                    if (reponse == '3')
                    {
                        Console.WriteLine("Vous allez quitter le programme.");
                        Thread.Sleep(3000);
                        break;
                    }
                }
            }
            Console.Clear();
        }
    }
}

