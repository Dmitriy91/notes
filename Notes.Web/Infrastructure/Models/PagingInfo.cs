using System;

namespace Notes.Web.Infrastructure.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalPages
        {
            get
            {
                if (ItemsPerPage == 0)
                    return 0;

                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }

        public int CurrentPage { get; set; }
    }
}
