using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace Nuggets01
{


    public class LivreAsk
    {
        // Propriétés
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public int AnneePublication { get; set; }
        public int Isbn { get; set; }


        // Constructeurs

        public LivreAsk(string titre, string auteur, int annee, int isbn)
        {
            Titre = titre;
            Auteur = auteur;
            AnneePublication = annee;
            Isbn = isbn;
        }

        public LivreAsk(int id, string titre, string auteur, int annee, int isbn)
        {
            Id = id;
            Titre = titre;
            Auteur = auteur;
            AnneePublication = annee;
            Isbn = isbn;
        }




        // Méthodes
        private static LivreAsk CreerLivre()
        {
            Console.WriteLine("Ajout d'un nouveau livre");
            Console.Write("Titre : ");
            string titre = Console.ReadLine();
            Console.Write("Auteur : ");
            string auteur = Console.ReadLine();
            Console.Write("Année de publication : ");
            int annee = int.Parse(Console.ReadLine());
            Console.Write("Numéro ISBN : ");
            int isbn = int.Parse(Console.ReadLine());

            LivreAsk livreAsk = new LivreAsk(titre, auteur, annee, isbn);
            return livreAsk;
        }

        public static void AjouterLivreDB(LivreAsk livre)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString);
                connection.Open();

                string query = "INSERT INTO livre (Titre, Auteur, Annee, Isbn) VALUES (@Titre, @Auteur, @Annee, @Isbn)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Titre", livre.Titre);
                cmd.Parameters.AddWithValue("@Auteur", livre.Auteur);
                cmd.Parameters.AddWithValue("@Annee", livre.AnneePublication);
                cmd.Parameters.AddWithValue("@Isbn", livre.Isbn);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine("Livre ajouté avec succès !");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }
        }

        public static void AjouterLivre()
        {
            LivreAsk nouveauLivre = CreerLivre();
            AjouterLivreDB(nouveauLivre);
        }

        //AFFICHER LIVRES
        public static void AfficherLivre()
        {
            Console.WriteLine("--- Liste des Livres ---");

            try
            {
                using MySqlConnection connection =
               new MySqlConnection(DatabaseConfig.ConnectionString);
                connection.Open();

                //Mise en place de la requete de recuperation des livres
                string query = "SELECT * FROM livre";
                //Creation de la commande pour interagir avec la base de donnée
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execution de la requete de recuperation des livres et stockage des donnée dans l'object DataReader
                MySqlDataReader reader = cmd.ExecuteReader();

                //verification si on a bien au moins une ligne recuperé
                if (reader.HasRows)
                {

                    //Pour chaque ligne recuperé on va donc crée un object personne
                    while (reader.Read())
                    {

                        Livre p = new Livre(
                            //recuperation de la valeur a la colone "id" dans une variable de type int
                            reader.GetInt32("Id"),
                            //recuperation de la valeur a la colone "nom" dans une variable de type String
                            reader.GetString("Titre"),
                            //recuperation de la valeur a la colone "prenom" dans une variable de type String
                            reader.GetString("Auteur"),
                            //recuperation de la valeur a la colone "age" dans une variable de type int
                            reader.GetInt32("Annee"),
                            //recuperation de la valeur a la colone "email" dans une variable de type String
                            reader.GetInt32("Isbn")
                            );

                        Console.WriteLine(p);

                    }
                }
                else
                {
                    Console.WriteLine("Aucun livre dans la base de donnée");
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
            //Utilisation du finaly (block au passage obligatoire pour fermer la connection a la bdd

        }


        //RECHERCHER LIVRE PAR ID

        public static void RechercherLivre()

        {
            Console.WriteLine("--- Recherche Par Id ---");
            Console.WriteLine("Id du livre Recherché :");
            var id = int.Parse(Console.ReadLine());

            try
            {
                using MySqlConnection connection =
               new MySqlConnection(DatabaseConfig.ConnectionString);
                connection.Open();

                string query = "SELECT * FROM livre WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Livre p = new Livre(
                            //recuperation de la valeur a la colone "id" dans une variable de type int
                            reader.GetInt32("id"),
                            //recuperation de la valeur a la colone "nom" dans une variable de type String
                            reader.GetString("Titre"),
                            //recuperation de la valeur a la colone "prenom" dans une variable de type String
                            reader.GetString("Auteur"),
                            //recuperation de la valeur a la colone "age" dans une variable de type int
                            reader.GetInt32("Annee"),
                            //recuperation de la valeur a la colone "email" dans une variable de type String
                            reader.GetInt32("Isbn")
                            );

                    Console.WriteLine("Livre trouvé : " + p);
                }
                else
                {
                    Console.WriteLine("Aucun livre trouvée avec cet ID ");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
          }

        //MODIFIER UN LIVRE
        public static void UpdateLivre()
        {
            Console.WriteLine("--- Modifier un Livre ---");
            Console.WriteLine("Id du livre à modifier :");
            var id = int.Parse(Console.ReadLine());


            try
            {
                using MySqlConnection connection =
                new MySqlConnection(DatabaseConfig.ConnectionString);
                connection.Open();

                //Verification si le livre avec cet id existe bien
                string queryCheck = "SELECT COUNT(*) FROM Livre WHERE Id = @id";
                MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
                cmdCheck.Parameters.AddWithValue("@Id", id);
                int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                if (count == 0)
                {
                    Console.WriteLine("Aucun livre trouvée avec cet Id");
                    return;
                }

                Console.WriteLine("Nouveau Titre :");
                var titre = Console.ReadLine();
                Console.WriteLine("Nouvel Auteur :");
                var auteur = Console.ReadLine();
                Console.WriteLine("Nouvel Annee:");
                var annee = int.Parse(Console.ReadLine());
                Console.WriteLine("Nouvel Isbn:");
                var isbn = int.Parse(Console.ReadLine());


                string query = "UPDATE Livre SET titre = @titre , auteur = @auteur , annee = @annee , isbn = @isbn WHERE Id = @id";


                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@titre", titre);
                cmd.Parameters.AddWithValue("@auteur", auteur);
                cmd.Parameters.AddWithValue("@annee", annee);
                cmd.Parameters.AddWithValue("@isbn", isbn);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Livre modifié avec succès");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Erreur : " + ex.Message);
            }

        }

        //SUPPRIMER UN LIVRE

        public static void DeleteLivre()
        {
            Console.WriteLine("--- Supprimer un Livre ---");
            Console.WriteLine("Id du livre à supprimer :");
            int id = int.Parse(Console.ReadLine());

            try
            {
               using MySqlConnection connection =
               new MySqlConnection(DatabaseConfig.ConnectionString);
                connection.Open();

                string query = "DELETE FROM Livre WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Livre supprimé avec succès");
                }
                else
                {
                    Console.WriteLine("Aucun Livre trouvée a cet ID");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur :" + ex.Message);
            }

        }
    }
}









