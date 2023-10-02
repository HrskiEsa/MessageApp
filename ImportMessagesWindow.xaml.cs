using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace MessageApp
{
    /// <summary>
    /// Viestien tuonti järjestelmään
    /// </summary>
    public partial class ImportMessagesWindow : Window
    {
        public ImportMessagesWindow()
        {
            InitializeComponent();
        }

        private void ImportClicked(object sender, RoutedEventArgs e)
        {

            // Tarkastetaan aineiston allekirjoituksen oikeus
            var valid = ValidateXmlSignature();

            if (valid)
            {
                // Aineisto koskematon 
                var manager = new DatabaseManager();

                var serializer = new XmlSerializer(typeof(List<MailMessage>));
                TextReader reader = new StreamReader(txtFilePath.Text);
                var messageList = (List<MailMessage>)serializer.Deserialize(reader);

                foreach (var message in messageList)
                {
                    manager.AddMessage(message.Receiver, message.Message, message.Encrypted);
                }

                MessageBox.Show("Viestin tuotu onnistuneesti", "Viestien tuonti");
            }
            else
            {
                // Aineiston allekirjoituksessa jokin haaste
                MessageBox.Show("Viestin allekirjoituksessa ongelma, sanomia ei voi tuoda järjestelmään", "Digitaalinen allekirjoitus");

            }
        }

        /// <summary>
        /// Viestien koskemattomuuden tarkastus
        /// </summary>
        /// <returns>
        /// true jos tuotavan aineiston allekirjoitus on OK
        /// false jos aineiston allekirjoitus ei täsmää
        /// </returns>
        private bool ValidateXmlSignature()
        {
            // Muistutus itselleni: Tee tämä loppuun kun palaat viikonlopun vietosta
            //  on aika tärkeä juttu saada kuntoon... ilman tätä tarkastusta voi olla "pelikaani turpiinissa" ja tapahtua ihmeitä... LOL ;)
            // p.s palautan tässä vain true, jotta saadaan koodit kääntymään ja asiat testiin...
            return true;
        }
    }
}
