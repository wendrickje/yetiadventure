using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Reflection;

namespace LevelManager
{
    [DataContract]
    public class LevelManager 
    {

        public LevelManager()
        {

        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public static LevelManager Create(string name)
        //{
        //    var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(LevelManager));

        //    var assembly = typeof(LevelManager).GetTypeInfo().Assembly;
        //    Stream stream = assembly.GetManifestResourceStream("YetiAdventure.Levels.Layouts."+ name+ ".lev.xml");

        //    //var file = String.Format("levels/layouts/{0}.lev.xml", name);
        //    //var stream = TitleContainer.OpenStream(file);
            
        //    var manager = serializer.ReadObject(stream) as LevelManager;

        //    return manager;
        //}

        //public static LevelManager Create(byte[] data)
        //{
        //    var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(LevelManager));
            
        //    Stream stream = new MemoryStream(data);
            
        //    var manager = serializer.ReadObject(stream) as LevelManager;
        //    stream.Dispose();
        //    return manager;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public string LevelLayout { get { return String.Format("YetiAdventure.Levels.Layouts.{0}.lev", Name); } }//levels/layouts/


        [DataMember(Order = 0)]
        public string Name { get; set; }

        [DataMember(Order = 1)]
        public TileSet TileSet { get; set; }

        [DataMember(Order = 2)]
        public List<TileMap> TileMaps { get; set; }

        [DataMember(Order = 3)]
        public List<LegendKey> Legend { get; set; }

    }
}
