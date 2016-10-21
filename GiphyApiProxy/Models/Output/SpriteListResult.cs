using System;
using System.Collections.Generic;

namespace GiphyApiProxy.Models.Output
{
    
    public class SpriteListResult
    {
        public IDictionary<string, IEnumerable<string>> SpriteSets;
        
        [Obsolete("For deserialization only")]
        public SpriteListResult()
        {}

        public SpriteListResult(IDictionary<string, IEnumerable<string>> spriteSets)
        {
            SpriteSets = spriteSets;
        }
    }
}