// See https://aka.ms/new-console-template for more information

using MySql.Data.MySqlClient;
using Nuggets01;

String connectionString = "Server=localhost;Database=mardi;User ID=root;Password=root;";


// AJOUTER LIVRE

void AjouterLivre()
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


//Creation de l'objet Livre
Livre livre = new Livre(titre, auteur, annee, isbn);

//Console.WriteLine("Livre ajouté avec succès : ");

// Mise en place de notre object qui nous servira a interagir avec la bdd
MySqlConnection connection = new MySqlConnection(connectionString);

// nous allons essayer de nous connecter a la BDD
try
{
    // Ouverture de la connection
    connection.Open();

    // requete a effectue sur la BDD avec les valeurs passée via des parametre pour eviter le injections SQL
    string query = "INSERT INTO livre (Titre,Auteur,Annee,Isbn) VALUES (@Titre,@Auteur,@Annee,@Isbn)";

    // on va cree un object commande qui va conteneir la requete a effectue et la connection 
    MySqlCommand cmd = new MySqlCommand(query, connection);

    // On viens remplacer les differents parametres qui sont present dans notre requete
    cmd.Parameters.AddWithValue("@Titre", livre.Titre);
    cmd.Parameters.AddWithValue("@Auteur", livre.Auteur);
    cmd.Parameters.AddWithValue("@Annee", livre.AnneePublication);
    cmd.Parameters.AddWithValue("@Isbn", livre.Isbn);

    // On vas executer la requete sur la bdd et recuperer le nombres de lignes affecter
    int rowAffected = cmd.ExecuteNonQuery();
    if (rowAffected > 0)
    {
        Console.WriteLine("Livre ajouté avec succes");
    }


}
catch (Exception e)
{ // j'atterit dans le catch si une erreur est arrive dans le try
    Console.WriteLine("Erreur : " + e.Message);
}
finally
{
    connection.Close();
}

}


//AFFICHER LIVRES
void AfficherLivre()
{
    Console.WriteLine("--- Liste des Livres ---");
    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
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
                // Personne personne = new Personne(5, "toto","tata", 25, "toto@tata");
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
    finally
    {
        connection.Close();
    }
}


//RECHERCHER LIVRE PAR ID

void RechercherLivre()
{
    Console.WriteLine("--- Recherche Par Id ---");
    Console.WriteLine("Id du livre Recherché :");
    var id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
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
    finally
    {
        connection.Close();
    }
}

//MODIFIER UN LIVRE
void UpdateLivre()
{
    Console.WriteLine("--- Modifier un Livre ---");
    Console.WriteLine("Id du livre à modifier :");
    var id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();

        //Verification si la personne avec cet id existe bien
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
    finally
    {
        connection.Close();
    }
}

//SUPPRIMER UN LIVRE
void DeleteLivre()
{
    Console.WriteLine("--- Supprimer un Livre ---");
    Console.WriteLine("Id du livre à supprimer :");
    int id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
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
    finally
    {
        connection.Close();
    }
}

AjouterLivre();
AfficherLivre();
RechercherLivre();
UpdateLivre();
DeleteLivre();

