namespace MessageApp
{
    /// <summary>
    /// Käyttäjän tiedot
    /// </summary>
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        // True jos käyttäjä on administrator
        public bool IsAdmin { get; set; }
    }

}
