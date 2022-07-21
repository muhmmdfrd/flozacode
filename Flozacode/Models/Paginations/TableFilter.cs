namespace Flozacode.Models.Paginations
{
    public class TableFilter
	{
		public string Keyword { get; set; } = string.Empty;
		public int Current { get; set; } = 1;
		public int Size { get; set; } = 10;
		public int Skip => (Current - 1) * Size;
		public string SortName { get; set; } = string.Empty;
		public string SortDir { get; set; } = string.Empty;
	}
}
