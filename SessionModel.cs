using MySql.Data.MySqlClient;
using System;

namespace PR3
{
    public static class SessionModel
    {
        public static string SessionID = "";
        public static bool Connecté = false;

        public static int numUser = 0;
        public static string Civilité = "";
        public static string NomUser = "";
        public static string PrénomUser = "";
        public static string Société = "";


        public static bool Set(MySqlConnection conn, MySqlDataReader reader) // faire reader.Read() avant l'appel de cette méthode!!!
        {
            numUser = Convert.ToInt32(reader["numUser"]) ;
            Civilité = reader["civiliteUser"].ToString();
            NomUser = reader["nomUser"].ToString();
            PrénomUser = reader["prenomUser"].ToString();
            Société = reader["numSociete"].ToString();

            if (reader["isActive"] is DBNull) goto manaraka ; // L'utilisateur n'est pas encore actif
            // test si false
            bool test = Convert.ToBoolean(reader["isActive"]) ;
            if (test) return false; // L'user est connecté en ce moment

        manaraka:
            // configurer la base pour que isActive == true... puis après:
            Connecté = true;
            return true;
        }


        public static void Reset()
        {
            numUser = 0;
            SessionID = Civilité = NomUser = PrénomUser = Société = "";
            Connecté = false;
        }
    }
}