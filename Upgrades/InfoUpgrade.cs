using Godot;
using System;

public partial class InfoUpgrade
{
	private string name;
    private string description;
    private string imagePath;

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

    public string SetDescription()
    {
        return this.description;
    }

    public void SetImagePath(string imagePath)
	{
		this.imagePath = imagePath;
	}

    public string SetImagePath()
    {
        return this.imagePath;
    }
}
