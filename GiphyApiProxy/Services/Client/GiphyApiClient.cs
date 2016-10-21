using System;
using RestSharp;
using System.IO;
using System.Net;
using GiphyApiProxy.Models.Client;
using GiphyApiProxy.Models.Client.Results;
using System.Threading;
using System.Threading.Tasks;

namespace GiphyApiProxy.Services.Client
{
    public class GiphyApiClient : IGiphyApiClient
    {
        private readonly IRestClient RestClnt;

        public GiphyApiClient()
        {
            RestClnt = new RestClient(baseUrl: GIPHY_API_ROOT);
        }

        private const string GIPHY_API_ROOT = "http://api.giphy.com/v1/gifs/";    
        private const string GIPHY_PUBLIC_API_KEY = "dc6zaTOxFJmzC";

        public void Search(SearchParams searchInput, Action<IRestResponse<SearchResult>, RestRequestAsyncHandle> callback)
        {
            try
            {
                var restRequest = new RestRequest(resource: "search",
                    method: Method.GET);
                restRequest.AddParameter(new Parameter() 
                {
                    Name = "q",
                    Value = searchInput.q,
                    Type = ParameterType.QueryString
                });   

              
                if (searchInput.limit.HasValue)
                {
                    restRequest.AddParameter(new Parameter()
                    {
                        Name = "limit",
                        Value = searchInput.limit,
                        Type = ParameterType.QueryString
                    });
                }

                if (searchInput.offset.HasValue)
                {
                    restRequest.AddParameter(new Parameter()
                    {
                        Name = "offset", 
                        Value = searchInput.offset,
                        Type = ParameterType.QueryString
                    });
                }

                if (!string.IsNullOrEmpty(searchInput.rating))
                {
                    restRequest.AddParameter(new Parameter()
                    {
                        Name = "rating",
                        Value = searchInput.rating,
                        Type = ParameterType.QueryString
                    });
                }

                if (!string.IsNullOrEmpty(searchInput.lang))
                {
                    restRequest.AddParameter(new Parameter()
                    {
                        Name = "lang",
                        Value = searchInput.lang,
                        Type = ParameterType.QueryString
                    });
                }

            
                if (!string.IsNullOrEmpty(searchInput.fmt))
                {
                    restRequest.AddParameter(new Parameter()
                    {
                        Name = "fmt",
                        Value = searchInput.fmt,
                        Type = ParameterType.QueryString
                    });
                }

                restRequest.AddParameter(new Parameter()
                {
                    Name = "api_key", 
                    Value = GIPHY_PUBLIC_API_KEY,
                    Type = ParameterType.QueryString
                });

                RestClnt.GetAsync<SearchResult>(restRequest, callback);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Trending(TrendingParams input, Action<IRestResponse<SearchResult>, RestRequestAsyncHandle> callback)
        {
            try
            {
                var restRequest = new RestRequest(resource: "trending",
                    method: Method.GET);
                if (input.limit.HasValue)
                {
                    restRequest.AddParameter(new Parameter() 
                    {
                        Name = "limit",
                        Value = input.limit.Value,
                        Type = ParameterType.QueryString
                    });
                }

                if (!string.IsNullOrEmpty(input.rating))
                {
                    restRequest.AddParameter(new Parameter()
                    {
                        Name = "rating",
                        Value = input.rating,
                        Type = ParameterType.QueryString
                    });
                }

                if (!string.IsNullOrEmpty(input.fmt))
                {
                    restRequest.AddParameter(new Parameter()
                    {
                        Name = "fmt",
                        Value = input.fmt,
                        Type = ParameterType.QueryString
                    });
                }

                restRequest.AddParameter(new Parameter()
                {
                    Name = "api_key",
                    Value = GIPHY_PUBLIC_API_KEY,
                    Type = ParameterType.QueryString
                });

                RestClnt.GetAsync<SearchResult>(restRequest, callback);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public MemoryStream GetGif(string url)
        {
            try
            {
                var request = (HttpWebRequest) HttpWebRequest.Create(url);
                var responseTask = request.GetResponseAsync();
                //TODO: async or parallel foreach? 
         
                Task.WaitAll(responseTask);
                var response = responseTask.GetAwaiter().GetResult();
                return (MemoryStream) response.GetResponseStream();
            }
            catch (Exception)
            {
                throw;      
            }
        }
       
    }
}