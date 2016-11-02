using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PR3.view
{
   class Main1
    {
        static void Main(string[] args)
        {
            // Créer d'un contact à ajouter
            imagController imagC = new imagController();
            imagC.idimg = "jo";
            imagC.longueur = 3;
            imagC.hautueur = 2;

            // Création de l'objet Bdd pour l'intéraction avec la base de donnée MySQL
            ImagModel bdd = new ImagModel();
            bdd.AddContact(imagC);
        }
      

    }
}