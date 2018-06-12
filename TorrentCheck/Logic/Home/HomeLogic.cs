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
            if (query.Title != null)
            {
                string uriString = String.Format("https://proxyfl.info/s/?q={0}&page=0&orderby=99", query.Title);
                List<Result> Results = logicHTTP.GetResults(uriString);
                
                if (query.Category != Category.Undefined)
                {
                    List<Result> FilteredResults = new List<Result>();
                    foreach (Result element in Results)
                    {
                        if (element.Category == query.Category)
                        {
                            FilteredResults.Add(element);
                        }
                        return FilteredResults;
                    }
                }

                return Results;
            }
            else
            {
                if (query.Category == Category.Audio)
                {
                    string uriString = "https://proxyfl.info/browse/100";
                    return logicHTTP.GetResults(uriString);
                }
                else if (query.Category == Category.Video)
                {
                    string uriString = "https://proxyfl.info/browse/200";
                    return logicHTTP.GetResults(uriString);
                }
                else if (query.Category == Category.Applications)
                {
                    string uriString = "https://proxyfl.info/browse/300";
                    return logicHTTP.GetResults(uriString);
                }
                else if (query.Category == Category.Games)
                {
                    string uriString = "https://proxyfl.info/browse/400";
                    return logicHTTP.GetResults(uriString);
                }
                else if (query.Category == Category.Porn)
                {
                    string uriString = "https://proxyfl.info/browse/500";
                    return logicHTTP.GetResults(uriString);
                }
                else if (query.Category == Category.Other)
                {
                    string uriString = "https://proxyfl.info/browse/600";
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
                        // if (element.Title.Contains(query.Title) && element.Category == query.Category)
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
                        // if (element.Title.Contains(query.Title))
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
    }
}

