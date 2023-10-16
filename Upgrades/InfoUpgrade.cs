using Godot;
using System;

public partial class InfoUpgrade
{
	private string name;
    private string description;
    private CompressedTexture2D image;
    private string imagePath;
	private string costImagePath;
	public static string defaultPath = "res://Icons/Potato.png";


    public void SetName(string name)
	{
		this.name = name;
	}

	public string GetName()
	{
		return this.name;
	}

	public void SetDescription(string description)
	{
		this.description = description;
	}

    public string GetDescription()
    {
        return this.description;
    }

    public void SetImagePath(string imagePath)
	{
		this.imagePath = imagePath;
		SetImage(imagePath);
	}

    //TODO rework the imgs
    public void SetImage(string imagePath)
    {
	    image = ResourceLoader.Load<CompressedTexture2D>(imagePath);
    }

    public CompressedTexture2D GetImage()
    {
	    return image;
    }

    public string GetImagePath()
    {
        return this.imagePath;
    }

    public void SetCostImagePath(string costImagePath)
    {
        this.costImagePath = costImagePath;
    }

    public string GetCostImagePath()
    {
        return this.costImagePath;
    }


}
