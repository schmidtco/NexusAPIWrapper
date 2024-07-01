using System;
using System.Xml.Serialization;
using System.IO;

// Define a class to represent the structure of dagbogsnote XML element
[XmlRoot("dagbogsnote")]
public class Dagbogsnote
{
    [XmlElement("daxq:dato")]
    public string Dato { get; set; }

    [XmlElement("daxq:tidspunkt")]
    public string Tidspunkt { get; set; }

    [XmlElement("daxq:medarbejder")]
    public string Medarbejder { get; set; }

    [XmlElement("dag_overskrift")]
    public string DagOverskrift { get; set; }

    [XmlElement("dag_tekst")]
    public string DagTekst { get; set; }

    [XmlElement("fok_tekst")]
    public string FokTekst { get; set; }
}

public class XmlHandler
{
    public Dagbogsnote ParseXml(string filePath)
    {
        try
        {
            // Create XmlSerializer for Dagbogsnote class
            XmlSerializer serializer = new XmlSerializer(typeof(Dagbogsnote));

            // Read XML file into StreamReader
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Deserialize XML into Dagbogsnote object
                return (Dagbogsnote)serializer.Deserialize(reader);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing XML: {ex.Message}");
            return null;
        }
    }
}
