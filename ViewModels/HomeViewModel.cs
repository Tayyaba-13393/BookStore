
using BookStoree.Models;
using System.Collections.Generic;

namespace BookStoree.ViewModels
{
    public class HomeViewModel
    {
        public List<BookCardViewModel> Books { get; set; }

        public List<Author> Authors { get; set; }
    }
}
