using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TorrentCheck.Models.AccountViewModels;

namespace TorrentCheck.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        public ApplicationUser(RegisterViewModel viewModel)
        {
            Name = viewModel.Name;
            Description = viewModel.Description;
            TrustRating = 0;
            UseRecentFilter = false;
        }

        public ApplicationUser(string name, string description, int trustRating, bool useRecentFilter, string recentFilter, string customThemePath)
        {
            Name = name;
            Description = description;
            TrustRating = trustRating;
            UseRecentFilter = useRecentFilter;
            RecentFilter = recentFilter;
            CustomThemePath = customThemePath;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int TrustRating { get; set; }
        public bool UseRecentFilter { get; set; }
        public string RecentFilter { get; set; }
        public string CustomThemePath { get; set; }
    }
}
