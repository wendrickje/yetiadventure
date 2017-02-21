using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Shared.Icons
{
    public class Icon 
    {
        IconType _iconType;

        public Icon(IconType type)
        {
            _iconType = type;
        }

        public byte[] Raw { get { return ExtractResource(); } }
        

        private byte[] ExtractResource()
        {
            var assembly = typeof(Icon).GetTypeInfo().Assembly;
            var ns = assembly.FullName.Split(',')[0];
            var filename = String.Format("{0}.Resources.{1}.png", ns, _iconType.ToString());
            using (Stream resFilestream = assembly.GetManifestResourceStream(filename))
            {
                if (resFilestream == null) return null;
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
            
        }
        private static Dictionary<IconType, Icon> _icons;
        public static Dictionary<IconType, Icon> Icons {  get { return _icons ?? (_icons = GenerateIcons());  } }
        private static Dictionary<IconType, Icon> GenerateIcons()
        {
            var value = new Dictionary<IconType, Icon>();
            value.Add(IconType.Polygon, new Icon(IconType.Polygon));
            value.Add(IconType.Cursor, new Icon(IconType.Cursor));
            value.Add(IconType.Transform, new Icon(IconType.Transform));
            return value;
        }
    }

    public enum IconType
    {

        // Name must match embedded file name
        Cursor,
        Polygon,
        Transform

    }
}
