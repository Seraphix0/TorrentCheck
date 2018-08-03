using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.DAL;
using Microsoft.EntityFrameworkCore;
using TorrentCheck.Models;
using TorrentCheck.Models.HomeViewModels;

namespace TorrentCheck.Logic
{
    public class HomeLogic
    {
        private readonly HomeLogicHTTP logicHTTP;
        private readonly HomeLogicSQL logicSQL;

        public HomeLogic(TorrentContext context)
        {
            logicHTTP = new HomeLogicHTTP();
            logicSQL = new HomeLogicSQL(context);
        }

        /// <summary>
        /// Get list of results from HTTP source.
        /// </summary>
        /// <param name="query">Query conditions for which the search results have to qualify.</param>
        /// <returns>List of results from HTTP source.</returns>
        public List<Result> GetResultsHTTP (SearchViewModel query)
        {
            logicHTTP.RemoteSource = query.RemoteSource;

            if (query.Title != null)
            {
                string uriString = String.Format("{0}/s/?q={1}&page=0&orderby=99", query.RemoteSource, query.Title);
                List<Result> Results = logicHTTP.GetResults(uriString);

                try
                {
                    if (!query.IncludeUntrustedResults)
                    {
                        List<Result> FilteredResults = new List<Result>();
                        foreach (Result element in Results)
                        {
                            if (element.Trusted)
                            {
                                FilteredResults.Add(element);
                            }
                        }

                        Results = FilteredResults;
                    }

                    if (query.Category != Category.Undefined)
                    {
                        List<Result> FilteredResults = new List<Result>();
                        foreach (Result element in Results)
                        {
                            if (element.Category == query.Category)
                            {
                                FilteredResults.Add(element);
                            }
                        }

                        Results = FilteredResults;
                    }

                    if (Results.Any())
                    {
                        return Results;
                    }

                    return null;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
            else
            {
                if (query.Category == Category.Audio)
                {
                    string uriString = string.Format("{0}/browse/100", query.RemoteSource);
                    return logicHTTP.GetResults(uriString);
                }
                else if (query.Category == Category.Video)
                {
                    string uriString = string.Format("{0}/browse/200", query.RemoteSource);
                    return logicHTTP.GetResults(uriString);
                }
                else if (query.Category == Category.Applications)
                {
                    string uriString = string.Format("{0}/browse/300", query.RemoteSource);
                    return logicHTTP.GetResults(uriString);
                }
                else if (query.Category == Category.Games)
                {
                    string uriString = string.Format("{0}/browse/400", query.RemoteSource);
                    return logicHTTP.GetResults(uriString);
                }
                else if (query.Category == Category.Porn)
                {
                    string uriString = string.Format("{0}/browse/500", query.RemoteSource);
                    return logicHTTP.GetResults(uriString);
                }
                else if (query.Category == Category.Other)
                {
                    string uriString = string.Format("{0}/browse/600", query.RemoteSource);
                    return logicHTTP.GetResults(uriString);
                }

                throw new Exception("Incorrect query format.");
            }
        }

        /// <summary>
        /// Get list of results from SQL source.
        /// </summary>
        /// <param name="query">Query conditions for which the search results have to qualify.</param>
        /// <returns>List of results from SQL source.</returns>
        public List<Result> GetResultsSQL (SearchViewModel query)
        {
            List<Result> Results;
            try
            {
                Results = logicSQL.CompileList(logicSQL.GetAllTorrents());
            }
            catch (NullReferenceException)
            {
                return null;
            }

            List<Result> FilteredResults = new List<Result>();
            if (query.Title != null)
            {
                if (query.Category != Category.Undefined)
                {
                    foreach (Result element in Results)
                    {
                        string titleLower = element.Title.ToLower();
                        if (titleLower.Contains(query.Title.ToLower()) && element.Category == query.Category)
                        {
                            FilteredResults.Add(element);
                        }
                    }

                    if(FilteredResults.Any())
                    {
                        return FilteredResults;
                    }

                    return null;
                }
                else
                {
                    foreach (Result element in Results)
                    {
                        string titleLower = element.Title.ToLower();
                        if (titleLower.Contains(query.Title.ToLower()))
                        {
                            FilteredResults.Add(element);
                        }
                    }

                    if (FilteredResults.Any())
                    {
                        return FilteredResults;
                    }

                    return null;
                }
            }
            else
            {
                foreach (Result element in Results)
                {
                    if (element.Category == query.Category)
                    {
                        FilteredResults.Add(element);
                    }
                }

                if (FilteredResults.Any())
                {
                    return FilteredResults;
                }

                return null;
            }

            throw new Exception("Incorrect query format.");
        }

        public List<string> GetRemoteSources()
        {
            return logicHTTP.GetRemoteSources();
        }

        public string GetMagnetLink(string resultURL)
        {
            return logicHTTP.GetMagnetLink(resultURL);
        }
    }
}

