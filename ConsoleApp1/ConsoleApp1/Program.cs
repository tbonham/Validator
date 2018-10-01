using System;
using System.Security.Cryptography;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main()
        {
            try
            {
                // Create a key and save it in a container.  
                GenKey_SaveInContainer("Test");

                // Retrieve the key from the container.  
                //GetKeyFromContainer("MyKeyContainer");

                // Delete the key from the container.  
                //DeleteKeyFromContainer("MyKeyContainer");

                // Create a key and save it in a container.  
                //GenKey_SaveInContainer("MyKeyContainer");

                // Delete the key from the container.  
                //DeleteKeyFromContainer("MyKeyContainer");

                Console.ReadLine();
            }
            catch (CryptographicException e)
            {
                
                Console.WriteLine(e.Message);
                Console.WriteLine(e.c)
            }

        }

        public static void GenKey_SaveInContainer(string ContainerName)
        {
            // Create the CspParameters object and set the key container   
            // name used to store the RSA key pair.  
            /*
            CspParameters cp = new CspParameters();
            cp.KeyNumber = (int)KeyNumber.Signature;
            Console.WriteLine(cp.KeyNumber.ToString());
            cp.KeyContainerName = ContainerName;

            // Create a new instance of RSACryptoServiceProvider that accesses  
            // the key container MyKeyContainerName.  
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.KeySize = 2048;
            
            // Display the key information to the console.  
            //Console.WriteLine("Key added to container: \n  {0}", rsa.ToXmlString(true));

            Console.WriteLine("Value in Save: {0}", rsa.ToString());*/


            System.Security.Cryptography.CspParameters csp = new CspParameters();
            // Set the key container name that has the RSA key pair
            csp.KeyContainerName = "test";
            //Set the CSP Provider Type PROV_RSA_FULL
            csp.ProviderType = 1;
            //Set the CSP Provider Name
            csp.ProviderName = "Microsoft Base Cryptographic Provider v1.0";
            //Create a DSA cypher and intialaise it with the CspParameters
            RSACryptoServiceProvider _cipher = new RSACryptoServiceProvider(2048,csp);
            
            Console.WriteLine("Cipher :" + _cipher.ToString());
            //Console.WriteLine("Key : " + _cipher.ToXmlString(true));
            Console.WriteLine("Key :" + RSAKeyExtensions.ToXmlString(_cipher, true));
            //Console.WriteLine("Key : " + _cipher.ToString());
            Console.WriteLine("Key Size : " + _cipher.KeySize);
            Console.WriteLine("KeyExchangeAlgorithm : " + _cipher.KeyExchangeAlgorithm);
            Console.WriteLine("SignatureAlgorithm : " + _cipher.SignatureAlgorithm);


        }
      
        public static void GetKeyFromContainer(string ContainerName)
        {
            // Create the CspParameters object and set the key container   
            // name used to store the RSA key pair.  
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;



            // Create a new instance of RSACryptoServiceProvider that accesses  
            // the key container MyKeyContainerName.  
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.KeySize = 2048;
            // Display the key information to the console.  
            //Console.WriteLine("Key retrieved from container : \n {0}", rsa.ToXmlString(false));
            //Console.WriteLine(rsa.ToString());
        }

        public static void DeleteKeyFromContainer(string ContainerName)
        {
            // Create the CspParameters object and set the key container   
            // name used to store the RSA key pair.  
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;

            // Create a new instance of RSACryptoServiceProvider that accesses  
            // the key container.  
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

            // Delete the key entry in the container.  
            rsa.PersistKeyInCsp = false;

            // Call Clear to release resources and delete the key from the container.  
            rsa.Clear();

            Console.WriteLine("Key deleted.");
        }
    }

    internal static class RSAKeyExtensions
    {
        /*
        #region JSON
        internal static void FromJsonString(this RSA rsa, string jsonString)
        {
            Check.Argument.IsNotEmpty(jsonString, nameof(jsonString));
            try
            {
                var paramsJson = JsonConvert.DeserializeObject<RSAParametersJson>(jsonString);

                RSAParameters parameters = new RSAParameters();

                parameters.Modulus = paramsJson.Modulus != null ? Convert.FromBase64String(paramsJson.Modulus) : null;
                parameters.Exponent = paramsJson.Exponent != null ? Convert.FromBase64String(paramsJson.Exponent) : null;
                parameters.P = paramsJson.P != null ? Convert.FromBase64String(paramsJson.P) : null;
                parameters.Q = paramsJson.Q != null ? Convert.FromBase64String(paramsJson.Q) : null;
                parameters.DP = paramsJson.DP != null ? Convert.FromBase64String(paramsJson.DP) : null;
                parameters.DQ = paramsJson.DQ != null ? Convert.FromBase64String(paramsJson.DQ) : null;
                parameters.InverseQ = paramsJson.InverseQ != null ? Convert.FromBase64String(paramsJson.InverseQ) : null;
                parameters.D = paramsJson.D != null ? Convert.FromBase64String(paramsJson.D) : null;
                rsa.ImportParameters(parameters);
            }
            catch
            {
                throw new Exception("Invalid JSON RSA key.");
            }
        }

        internal static string ToJsonString(this RSA rsa, bool includePrivateParameters)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);

            var parasJson = new RSAParametersJson()
            {
                Modulus = parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null,
                Exponent = parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null,
                P = parameters.P != null ? Convert.ToBase64String(parameters.P) : null,
                Q = parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null,
                DP = parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null,
                DQ = parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null,
                InverseQ = parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null,
                D = parameters.D != null ? Convert.ToBase64String(parameters.D) : null
            };

            return JsonConvert.SerializeObject(parasJson);
        }
        #endregion
        */
        #region XML

        public static void FromXmlString(this RSA rsa, string xmlString)
        {
            RSAParameters parameters = new RSAParameters();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "Exponent": parameters.Exponent = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "P": parameters.P = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "Q": parameters.Q = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "DP": parameters.DP = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "DQ": parameters.DQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "InverseQ": parameters.InverseQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "D": parameters.D = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

        public static string ToXmlString(this RSA rsa, bool includePrivateParameters)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);

            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                  parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null,
                  parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null,
                  parameters.P != null ? Convert.ToBase64String(parameters.P) : null,
                  parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null,
                  parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null,
                  parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null,
                  parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null,
                  parameters.D != null ? Convert.ToBase64String(parameters.D) : null);
        }

        #endregion
    }
}
