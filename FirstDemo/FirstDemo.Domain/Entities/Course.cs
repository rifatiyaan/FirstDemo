namespace FirstDemo.Domain.Entities
{
	public class Course : IEntity<Guid>
	{
		public Guid Id { get; set; }

		public string Title { get; set; }
		public string Description { get; set; }
		public uint Fees { get; set; }

	}
}
