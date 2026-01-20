using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nuggets01
{
    internal class Commande
    {

        //Proprietes

        public int id { get; set; }
        public int ClientID { get; set; }
        public DateTime DateCommande { get; set; }
        public int total { get; set; }





        //Constructeur

        public Commande(int id, int clientid, string datecommande, int total)
        {
            this.id = id;
        }
    }
}
        


