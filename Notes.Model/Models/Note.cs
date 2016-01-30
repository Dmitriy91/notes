using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Model
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required, StringLength(32)]
        public string Name { get; set; }
        [Required, StringLength(264)]
        public string Text { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}
