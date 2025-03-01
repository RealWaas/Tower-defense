using System.Collections.Generic;

public static class InventoryManager
{
    private const int MAX_TURRET_COUNT = 5;
    public static List<TurretType> availableTurrets { get; private set; } = new List<TurretType>();

    public static void AddTurretToInventory(TurretType _turret)
    {
        if (availableTurrets.Count > MAX_TURRET_COUNT)
            return; // not enought space

        availableTurrets.Add(_turret);
    }

    public static void ResetInventory() => availableTurrets.Clear();
}
