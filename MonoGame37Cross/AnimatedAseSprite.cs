using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace MonoGame37Cross
{
    public class AseSpriteAnimation
    {
        public AseSpriteAnimation(string aseAnimationName)
        {
            using (var stream = TitleContainer.OpenStream("Content/assets/asesprites/" + aseAnimationName + ".json"))
            using (var sr = new StreamReader((stream)))
            {
                JObject content = JObject.Parse(sr.ReadToEnd());
                JArray frames = (JArray)content["frames"];
                System.Console.WriteLine(frames[0]["frame"]["x"]);
            }
        }
    }
}
