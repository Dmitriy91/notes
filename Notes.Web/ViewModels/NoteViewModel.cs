﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Notes.Web.ViewModels
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(32)]
        public string Name { get; set; }
        [Required, StringLength(264), UIHint("MultilineText")]
        public string Text { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}