namespace Uniqlo.ViewModel.Sliders
{
    public class SliderItemVM
    {
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public bool Isdeleted { get; internal set; }
    }
}
