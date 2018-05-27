
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;


namespace WindowsForms
{
    class CircleBL
    {

        public static void SerializeList(List<Circle> circles, string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Circle>));
            circles.ForEach(p => p.RgbaColor = p.Color.ToArgb());
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, circles);
            }
            
        }

        public static List<Circle> DeserializeList(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Circle>));
            List<Circle> circles = null;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                circles = (List<Circle>)formatter.Deserialize(fs);
            }
            circles.ForEach(t => t.Color = Color.FromArgb(t.RgbaColor));
            if (circles == null)
            {
                throw new ApplicationException(string.Format("cannot deserialize file {0}", path));
            }

            return circles;
        }

        
    }
}
