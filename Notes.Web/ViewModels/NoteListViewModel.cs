using Notes.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Notes.Web.ViewModels
{
    public class NoteListViewModel
    {
        public IEnumerable<NoteViewModel> NoteViewModels { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
