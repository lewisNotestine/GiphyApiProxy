using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GiphyApiProxy.Models.Output;
using GiphyApiProxy.Services.Client;
using GiphyApiProxy.Models.Client;

namespace GiphyApiProxy.Controllers
{
    [Route("api/[controller]")]
    public class SpritesController
    {
        private readonly IGiphyApiClient GiphyClient;
        public SpritesController(IGiphyApiClient giphyClient)
        {
            GiphyClient = giphyClient;
        }

        //GET /api/sprites/currentsprites
        [HttpGet("currentsprites")]
        public SpriteListResult CurrentSprites()
        {            
            GiphyClient.Trending(new TrendingParams(), (rr, ah) => 
            {                
                var imgList = new Dictionary<string, string>();
                foreach (var img in rr.Data.data)
                {
                    var imgkey = img.id;                    
                    var imgVal = img.images.fixed_height_small.url;
                    imgList.Add(imgkey, imgVal);
                }                
            });
            
            //todo - get this as a separate service call.. this will be grabbing the stashed JPG files instead of creating one on the same call. 
            return new SpriteListResult(spriteSets: new Dictionary<string, IEnumerable<string>>() 
            {
                ["test"] = new List<string>() { "test1", "test2" }
            } );
        }
        
        [HttpGet]
        public FileResult Gif(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}