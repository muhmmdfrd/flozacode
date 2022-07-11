namespace Flozacode.Models.Paginations
{
    public class TableFilter
	{
		private string? _keyword { get; set; }
		private int _current { get; set; }
		private int _size { get; set; }
		private string? _sortName { get; set; }
		private string? _sortDir { get; set; }

		public string Keyword
		{
			get => _keyword ?? string.Empty;
			set => _keyword = value;
		}

		public int Current
		{
			get => Current == 0 ? 1 : Current;
			set => Current = value;
		}

		public int Size
		{
			get => _size == 0 ? 10 : _size;
			set => _size = value;
		}

		public int Skip => (Current - 1) * Size;

		public string SortName
		{
			get => _sortName ?? string.Empty;
			set => _sortName = value;
		}

		public string SortDir
		{
			get => _sortDir ?? string.Empty;
			set => _sortDir = value;
		}
	}
}
