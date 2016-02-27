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
        public NoteFilter NoteFilter { get; set; }
    }

    public class NoteFilter
    {
        [StringLength(32)]
        public string Name { get; set; }
        [StringLength(32)]
        public string Text { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
    }
}
