using Godot;

public class GamePopUpInfo
{
	public string Title => title;

	public string Description => description;

	public CompressedTexture2D Image => image;

	private string title;
	private string description;
	private CompressedTexture2D image = null;
	public GamePopUpInfo(string title, string desc, CompressedTexture2D image = null)
	{
		this.title = title;
		this.description = desc;
		this.image = image;
	}
}