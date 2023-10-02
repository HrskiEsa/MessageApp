using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;

namespace MessageApp
{
    /// <summary>
    /// Luokka jonka avulla kommunikoidaan SQL-palvelun kanssa
    /// </summary>
    public class DatabaseManager
    {
        private readonly string connectionString;

        public DatabaseManager()
        {
            // Haetaan connectionString asetus App.config tiedostosta
            connectionString = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        }

        /// <summary>
        /// Käyttäjän kirjautuminen
        /// </summary>
        /// <param name="userName">Käyttäjätunnus</param>
        /// <param name="password">Salasana</param>
        /// <returns></returns>
        public User LogIn(string userName, string password)
        {
            User user = null;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var sql = "SELECT * FROM users WHERE username='" + userName + "' AND password='" + password + "'";

                var cmd = new MySqlCommand(sql, conn);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    // Luodaan uusi käyttäjä tiedoilla
                    user = new User();
                    user.Username = dr.GetString("username");
                    user.IsAdmin = dr.GetBoolean("isadmin");
                }

            }

            // Palautetaan käyttä tai jos haku ei tuottanut tulosta niin tällöin user on arvossa null
            return user;
        }

        /// <summary>
        /// Palauttaa listan käyttäjistä
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            // Lista käyttäjistä
            var users = new List<User>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var sql = "SELECT username, isadmin FROM users";

                var cmd = new MySqlCommand(sql, conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    // Luodaan tiedoilla uusi käyttäjä
                    var user = new User();
                    user.Username = dr.GetString("username");
                    user.IsAdmin = dr.GetBoolean("isadmin");

                    // Lisätään käyttäjä listaan
                    users.Add(user);
                }

            }

            // Palautetaan hakua vastaavat käyttäjät
            return users;
        }

        /// <summary>
        /// Hakee käyttäjän viestit
        /// </summary>
        /// <param name="receiver">Viestin saaja</param>
        /// <param name="filter">Mahdollinen hakuehto</param>
        /// <returns></returns>
        public ObservableCollection<MailMessage> GetMessages(string receiver, string filter)
        {
            // Lista viestille
            var messages = new ObservableCollection<MailMessage>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // SQL haku käyttäjän viesteille
                var sql = "SELECT id, message, encrypted, receiver FROM messages WHERE receiver='" + receiver + "'";

                // jos hakuehto on annettu niin lisätään se LIKE tyyppisesti luodun SQL lauseen perään
                if (!string.IsNullOrEmpty(filter))
                {
                    sql += " AND message LIKE '" + filter + "'";
                }

                var cmd = new MySqlCommand(sql, conn);

                try
                {
                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        // Luodaan uusi viesti ja lisätään se viestit kokoelmaan
                        messages.Add(new MailMessage
                        {
                            ID = dr.GetInt32("id"),
                            Message = dr.GetString("message"),
                            Encrypted = dr.GetInt32("encrypted"),
                            Receiver = dr.GetString("receiver")
                        });
                    }
                }
                catch (Exception)
                {
                    var ex = new Exception("SQL VIRHE: " + sql);
                    throw ex;
                }

            }

            // Palautetaan luodut viestit
            return messages;

        }

        /// <summary>
        /// Lisää uuden viestin tietokantaan
        /// </summary>
        /// <param name="receiver">Viestin saaja</param>
        /// <param name="message">Viesti</param>
        /// <param name="isEncrypted">true jos viesti on salattu</param>
        public void AddMessage(string receiver, string message, int isEncrypted)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand("INSERT INTO messages (receiver, message, encrypted) VALUES (@receiver, @message, @encrypted)", conn);
                cmd.Parameters.AddWithValue("@receiver", receiver);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@encrypted", isEncrypted);

                cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Suorittaa SQL laiseen tietokantaan
        /// </summary>
        /// <param name="commandString">Suoritettava SQL lause</param>
        /// <returns>
        /// DataTable jossa kyselyn tulos
        /// </returns>
        public DataTable ExecuteCmd(string commandString)
        {

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(commandString, conn);

                try
                {
                    var dr = cmd.ExecuteReader();

                    // Luodaan .NET DataTable ja ladataan siihen datareaderin tiedot
                    var table = new DataTable();
                    table.Load(dr);

                    return table;
                }
                catch (Exception)
                {
                    return new DataTable();
                }
            }
        }

        /// <summary>
        /// Päivittää käyttäjän tiedot
        /// </summary>
        /// <param name="currentUser">Päivitettävä käyttäjä</param>
        public void UpdateUser(User currentUser)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand("UPDATE users SET PASSWORD = @password WHERE username = @username", conn);
                cmd.Parameters.AddWithValue("@password", currentUser.Password);
                cmd.Parameters.AddWithValue("@username", currentUser.Username);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Lisää uuden käyttäjän
        /// </summary>
        /// <param name="user">Lisättävä käyttäjä</param>
        /// <returns></returns>
        public bool AddUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand("INSERT INTO users (username, password) VALUES ('"+ user.Username + "', '" + user.Password + "')", conn);

                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }

}
