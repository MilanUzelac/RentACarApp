using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TVPProject
{
    class RadSaDatotekom
    {
        public static List<T> Procitaj<T>(string imeFajla)
        {
            //koristice kao povratna vrednost iz fje
            List<T> result; 
            if (!File.Exists(imeFajla))
            {
                //Ukoliko ne postoji fajl, funkcija vraca praznu listu.
                return new List<T>();
            }
            // pravimo BinnaryFormater
            BinaryFormatter formatter = new BinaryFormatter();
            // pravimo fajl strim i otvaramo datoteku
            using (FileStream myFileStream = new FileStream(imeFajla, FileMode.Open)) 
            {
                // deserijalizacija upisanih podataka, kastovanje u listu<T> i smestanje u result 
                result = (List<T>)formatter.Deserialize(myFileStream);
                //obavezno zatvaranje strima
                myFileStream.Close(); 
            }
            //vracanje procitanih vrednosti
            return result;
        }

        public static void Upisi<T>(List<T> list, string imeFajla)
        {
            //ukoliko fajl ne postoji, pravimo novi
            if (!File.Exists(imeFajla))
            {
                // pravimo BinnaryFormater
                BinaryFormatter serializer = new BinaryFormatter();
                // inicijalizacija file strima i kreiranje fajla
                using (FileStream fs = new FileStream(imeFajla, FileMode.Create)) 
                {
                    //upis liste
                    serializer.Serialize(fs, list);
                    //zatvaranje streama
                    fs.Close(); 
                    fs.Dispose();
                }
            }
            else //ukoliko fajl postoji
            {
                BinaryFormatter serializer = new BinaryFormatter();
                //samo se otvara za citanje
                using (FileStream fs = new FileStream(imeFajla, FileMode.Open)) 
                {
                    // upis u fajl
                    serializer.Serialize(fs, list);
                    //zatvaranje streama
                    fs.Close(); 
                    fs.Dispose();
                }
            }

        }
        
    }
}
