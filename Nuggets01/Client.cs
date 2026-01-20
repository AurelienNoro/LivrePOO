using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Policy;
using System.Text;

namespace Nuggets01;

public class Client
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Adresse { get; set; }
    public string CodePostal { get; set; }
    public string Ville { get; set; }
    public string Telephone { get; set; }

    public override string ToString()
    {
        return $"{Id} | {Nom} {Prenom} | {Ville} | {Telephone}";
    }

    public Client(int id, string nom, string prenom, string adresse, string codePostal, string ville, string telephone)
    {
        Id = id;
        Nom = nom;
        Prenom = prenom;
        Adresse = adresse;
        CodePostal = codePostal;
        Ville = ville;
        Telephone = telephone;
    }



    public static Client CreerClient()
    {
        Console.WriteLine("--- Création d'un nouveau client ---");
        Console.Write("Nom : ");
        string nom = Console.ReadLine();
        Console.Write("Prénom : ");
        string prenom = Console.ReadLine();
        Console.Write("Adresse : ");
        string adresse = Console.ReadLine();
        Console.Write("Code Postal : ");
        string codePostal = Console.ReadLine();
        Console.Write("Ville : ");
        string ville = Console.ReadLine();
        Console.Write("Téléphone : ");
        string telephone = Console.ReadLine();
        return new Client(0, nom, prenom, adresse, codePostal, ville, telephone);

    }
    public static void AjouterClientDB()
    {
        Console.WriteLine("--- Ajout d'un nouveau client dans la base de donnée ---");
        Client nouveauClient = CreerClient();
        try
        {
            using MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString);
            connection.Open();
            //Mise en place de la requete d'insertion
            string query = "INSERT INTO client (nom, prenom, adresse, code_postal, ville, telephone) " +
                           "VALUES (@Nom, @Prenom, @Adresse, @CodePostal, @Ville, @Telephone)";
            //Creation de la commande pour interagir avec la base de donnée
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Ajout des parametres a la requete
            cmd.Parameters.AddWithValue("@Nom", nouveauClient.Nom);
            cmd.Parameters.AddWithValue("@Prenom", nouveauClient.Prenom);
            cmd.Parameters.AddWithValue("@Adresse", nouveauClient.Adresse);
            cmd.Parameters.AddWithValue("@CodePostal", nouveauClient.CodePostal);
            cmd.Parameters.AddWithValue("@Ville", nouveauClient.Ville);
            cmd.Parameters.AddWithValue("@Telephone", nouveauClient.Telephone);
            //Execution de la requete d'insertion
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
    


    public static void AfficherClient()
    {
        Console.WriteLine("--- Liste des Clients ---");

        try
        {
           using MySqlConnection connection =
           new MySqlConnection(DatabaseConfig.ConnectionString);
            connection.Open();

            
            string query = "SELECT * FROM client";
            MySqlCommand cmd = new MySqlCommand(query, connection);

      
            MySqlDataReader reader = cmd.ExecuteReader();

          
            if (reader.HasRows)
            {

              
                while (reader.Read())
                {

                    Client p = new Client(

                        reader.GetInt32("id"),
                        reader.GetString("nom"),
                        reader.GetString("prenom"),
                        reader.GetString("adresse"),
                        reader.GetString("code_postal"),
                        reader.GetString("ville"),
                        reader.GetString("telephone")

                        );

                    Console.WriteLine(p);

                }
            }
            else
            {
                Console.WriteLine("Aucun client dans la base de donnée");
            }
            reader.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur : " + ex.Message);
        }
    }


        public static void AjouterClient()
    {
        Console.WriteLine("--- Ajout d'un nouveau client ---");
        Client nouveauClient = CreerClient();

    }

    public static void UpdateClient()
    {
        Console.WriteLine("--- Modifier un Client ---");
        Console.WriteLine("Id du client à modifier :");
        var id = int.Parse(Console.ReadLine());


        try
        {
            using MySqlConnection connection =
            new MySqlConnection(DatabaseConfig.ConnectionString);
            connection.Open();

            //Verification si le livre avec cet id existe bien
            string queryCheck = "SELECT COUNT(*) FROM client WHERE id = @id";
            MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
            cmdCheck.Parameters.AddWithValue("@id", id);
            int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

            if (count == 0)
            {
                Console.WriteLine("Aucun client trouvée avec cet Id");
                return;
            }

            Console.WriteLine("Nouveau Prenom:");
            var prenom = Console.ReadLine();
            Console.WriteLine("Nouveau nom:");
            var nom = Console.ReadLine();
            Console.WriteLine("Nouvelle Adresse:");
            var adresse = (Console.ReadLine());
            Console.WriteLine("Nouveau Code Postal:");
            var codePostal = (Console.ReadLine());
            Console.WriteLine("Nouvelle ville:");
            var ville = Console.ReadLine();
            Console.WriteLine("Nouveau telephone");
            var telephone = Console.ReadLine();


            string query = "UPDATE Client SET nom = @nom , prenom = @prenom , adresse = @adresse ,code_postal = @code_postal, ville = @ville, telephone = @telephone WHERE Id = @id";


            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nom", nom);
            cmd.Parameters.AddWithValue("@prenom", prenom);
            cmd.Parameters.AddWithValue("@adresse", adresse);
            cmd.Parameters.AddWithValue("@code_postal", codePostal);
            cmd.Parameters.AddWithValue("@ville", ville);
            cmd.Parameters.AddWithValue("@telephone", telephone);


            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Client modifié avec succès");
            }

        }
        catch (Exception ex)
        {

            Console.WriteLine("Erreur : " + ex.Message);
        }
    }




    public static void DeleteClient()
    {
        Console.WriteLine("--- Supprimer un Client ---");
        Console.WriteLine("Id du Client à supprimer :");
        int id = int.Parse(Console.ReadLine());

        try
        {
            using MySqlConnection connection =
            new MySqlConnection(DatabaseConfig.ConnectionString);
            connection.Open();

            string query = "DELETE FROM client WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Client supprimé avec succès");
            }
            else
            {
                Console.WriteLine("Aucun Client trouvée avec cet ID");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur :" + ex.Message);
        }
    }
    public static void AfficherClientCommande()
    {
        Console.WriteLine("--- Liste des Clients et Commandes ---");

        try
        {
            using MySqlConnection connection =
                new MySqlConnection(DatabaseConfig.ConnectionString);
            connection.Open();

            string query = @"
        SELECT 
            c.id AS client_id,
            c.nom,
            c.prenom,
            c.adresse,
            c.code_postal,
            c.ville,
            c.telephone,
            co.id AS commande_id,
            co.date_commande,
            co.total
        FROM client c
        LEFT JOIN commandes co ON co.client_id = c.id;
        ";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            using MySqlDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("Aucun client dans la base de données");
                return;
            }

            while (reader.Read())
            {
                Client client = new Client(
                    reader.GetInt32("client_id"),
                    reader.GetString("nom"),
                    reader.GetString("prenom"),
                    reader.GetString("adresse"),
                    reader.GetString("code_postal"),
                    reader.GetString("ville"),
                    reader.GetString("telephone")
                );

                Console.WriteLine(client);

                if (!reader.IsDBNull("commande_id"))
                {
                  
                    Commande commande = new Commande(
                        reader.GetInt32("commande_id"),
                        reader.GetInt32("client_id"),
                        reader.GetDateTime("date_commande").ToString(), 
                        reader.GetInt32("total")
                    );

                    Console.WriteLine("   " + commande);
                }
                else
                {
                    Console.WriteLine("   Aucune commande");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur : " + ex.Message);
        }
    }
    public static void AjouterCommande(int clientId, DateTime dateCommande, int total)
{
    try
    {
        using MySqlConnection connection =
        new MySqlConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        string query = @"
        INSERT INTO commandes (client_id, date_commande, total)
        VALUES (@clientId, @dateCommande, @total);
        ";

        using MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@clientId", clientId);
        cmd.Parameters.AddWithValue("@dateCommande", dateCommande);
        cmd.Parameters.AddWithValue("@total", total);

        int rows = cmd.ExecuteNonQuery();

        if (rows > 0)
        {
            Console.WriteLine("Commande ajoutée avec succès !");
        }
        else
        {
            Console.WriteLine("Échec de l'ajout de la commande.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur lors de l'ajout de la commande : " + ex.Message);
    }
}


}







