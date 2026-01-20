using System;
using System.Collections.Generic;
using System.Text;

namespace Nuggets01
{
    internal class IHM
    {
        public void AfficherMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayMenu()
        {
            Console.WriteLine("MENU");
            Console.WriteLine("1- Ajouter un Livre");
            Console.WriteLine("2- Afficher les Livres en DB");
            Console.WriteLine("3- Rechercher un Livre par son ID");
            Console.WriteLine("4- Mettre à jour un Livre");
            Console.WriteLine("5- Supprimer un Livre");
            Console.WriteLine("0- Quitter");
            Console.WriteLine("Votre choix :");
        }
        public void Menu()
        {
            while (true)
            {
                this.DisplayMenu();
                string choix = Console.ReadLine() ?? "";

                switch (choix)
                {
                    case "1":
                        LivreAsk.AjouterLivre();
                        break;
                    case "2":
                        LivreAsk.AfficherLivre();
                        break;
                    case "3":
                        LivreAsk.RechercherLivre();
                        break;
                    case "4":
                        LivreAsk.UpdateLivre();
                        break;
                    case "5":
                        LivreAsk.DeleteLivre();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Choix pas compris !!");
                        break;
                }
            }

        }

    }
}


