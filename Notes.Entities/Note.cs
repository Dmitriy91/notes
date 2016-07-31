using System;

namespace Notes.Entities
{
    public class Note
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
