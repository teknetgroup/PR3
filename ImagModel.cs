using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
// Librairie MySQL ajoutée dans les références.
using MySql.Data.MySqlClient;

namespace PR3.view
{
    public class ImagModel
    {

        private MySqlConnection connection;

        // Constructeur
        public ImagModel()
        {
            this.InitConnexion();
        }

        // Méthode pour initialiser la connexion
        private void InitConnexion()
        {
            // Création de la chaîne de connexion
            string connectionString = "SERVER=127.0.0.1; DATABASE=mli; UID=root; PASSWORD=";
            this.connection = new MySqlConnection(connectionString);
        }

        // Méthode pour ajouter un contact
        public void AddContact(imagController contact)
        {
            try
            {
                // Ouverture de la connexion SQL
                this.connection.Open();

                // Création d'une commande SQL en fonction de l'objet connection
                MySqlCommand cmd = this.connection.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO contact (idimg, longueu, hautueur) VALUES (@idimg, @longueur, @hautueur)";

                // utilisation de l'objet contact passé en paramètre
                cmd.Parameters.AddWithValue("@idimg", contact.idimg);
                cmd.Parameters.AddWithValue("@longueur", contact.longueur);
                cmd.Parameters.AddWithValue("@hautueur", contact.hautueur);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                // Fermeture de la connexion
                this.connection.Close();
            }
            catch
            {
                // Gestion des erreurs :
                // Possibilité de créer un Logger pour les exceptions SQL reçus
                // Possibilité de créer une méthode avec un booléan en retour pour savoir si le contact à été ajouté correctement.
            }
        }
    }
}
