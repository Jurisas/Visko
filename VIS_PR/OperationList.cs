using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace VIS_PR
{
    [XmlRoot]
    public class OperationList
    {
        [XmlArray]
        public List<Operations> OperationsList { get; set; }


        public void Serializer()
        {
            if (OperationsList.Count > 0)
            {
                try
                {
                    var filename = OperationsList[0].Patient.Firstname + OperationsList[0].Patient.Lastname +
                                   "Operations.xml";
                    var serializer = new DataContractSerializer(OperationsList.GetType());
                    using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        serializer.WriteObject(stream, OperationsList);
                    }
                }
                catch (SerializationException se)
                {
                    Console.WriteLine(se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw;
                }
            }
        }
        
    }
}