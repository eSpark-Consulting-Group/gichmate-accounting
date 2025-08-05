namespace Ashpro
{
	public class MenuDetails
	{
		public int? Id { get; set; }

		public string Name { get; set; }

		public string MenuCaption { get; set; }

		public int? ParentId { get; set; }

		public string AssemblyName { get; set; }

		public string ClassName { get; set; }

		public bool IsForm { get; set; }

		public byte[] Icon { get; set; }

		public int? FK_ModuleId { get; set; }

		public bool IsDialog { get; set; }

		public bool Status { get; set; }

		public int Position { get; set; }

		public bool IsCrudMenu { get; set; }

		public string MenuUrl { get; set; }

		public bool IsFavorite { get; set; }
	}
}
