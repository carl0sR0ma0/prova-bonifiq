namespace ProvaPub.Models
{
	public class ReturnList<T>
	{
		public List<T> List { get; set; }
		public int TotalCount { get; set; }
		public bool HasNext { get; set; }
	}
}
