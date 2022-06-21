namespace NewsNowProject.Domain.Model
{
    public class SourceModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public EnumModel.SourceData Source { get; set; }
        public string SourceName { get; set; }
    }
}
