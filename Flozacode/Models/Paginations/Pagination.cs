namespace Flozacode.Models.Paginations
{
    public class Pagination<T>
	{
		public Pagination()
		{
			Total = 1;
			Size = 10;
			Data = null;
		}

		public Pagination(int total, int filtered, int size, IEnumerable<T>? data)
		{
			Total = total;
			Filtered = filtered;
			Size = size;
			Data = data;
		}

		public int Total { get; set; }
		public int Filtered { get; set; }
		public int Size { get; set; }
		public IEnumerable<T>? Data { get; set; }
	}
}
