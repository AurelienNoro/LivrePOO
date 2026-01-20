using System;
using System.Collections.Generic;
using System.Text;

namespace Nuggets01
{
   
    internal class Livre
    {
        // Propriétés
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public int AnneePublication { get; set; }
        public int Isbn { get; set; }

        // Constructeurs
   
        public Livre (string titre, string auteur, int annee, int isbn)
        {
            Titre = titre;
            Auteur = auteur;
            AnneePublication = annee;
            Isbn = isbn;
        }

        public Livre(int id, string titre, string auteur, int annee, int isbn) : this(titre, auteur, annee, isbn)
        {
            Id = id;
        }


        // Méthodes
        public override string ToString()
        {
            return $"Id {Id} : {Titre} de {Auteur} | Année : {AnneePublication} | N°ISBN : {Isbn}";
        }
    }
}
