namespace MessageApp
{
    /// <summary>
    /// Yhden viestin tiedot
    /// </summary>
    public class MailMessage
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public string Receiver { get; set; }
        public int Encrypted { get; set; }
    }

}
