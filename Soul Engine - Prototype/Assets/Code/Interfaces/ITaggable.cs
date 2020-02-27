namespace SoulEngine
{
	public interface ITaggable
	{
		/// <summary>The various tags this object will look for to determine collision.</summary>
		string[] Tags { get; }
	}
}
