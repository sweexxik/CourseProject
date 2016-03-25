namespace CourseProject.Domain.LuceneEngine
{
    /// <summary>
    /// Basic interface where every document should inherit from
    /// </summary>
    public interface IDocument
    {
        int Id { get; set; }
    }
}
