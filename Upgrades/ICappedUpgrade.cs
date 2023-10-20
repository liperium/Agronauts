namespace WJA23Godot.Upgrades;

public interface ICappedUpgrade
{
    /// <summary>
    /// Gets the tier cap of the upgrade.
    /// </summary>
    /// <returns>The tier cap of the upgrade.</returns>
    public long GetTierCap();
}