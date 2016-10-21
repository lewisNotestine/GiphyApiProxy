
using System;
using System.IO;
using GiphyApiProxy.Models.Client;
using GiphyApiProxy.Models.Client.Results;
using RestSharp;

namespace GiphyApiProxy.Services.Client
{
    public interface IGiphyApiClient
    {
        void Search(SearchParams searchInput, Action<IRestResponse<SearchResult>, RestRequestAsyncHandle> callback);
        void Trending(TrendingParams input, Action<IRestResponse<SearchResult>, RestRequestAsyncHandle> callback);
        MemoryStream GetGif(string url);
    }
}