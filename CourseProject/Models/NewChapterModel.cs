namespace CourseProject.Models
{
    public class NewChapterModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public int CreativeId { get; set; }

        public string CreatedOn { get; set; }

        public bool Edit { get; set; }
    }
}