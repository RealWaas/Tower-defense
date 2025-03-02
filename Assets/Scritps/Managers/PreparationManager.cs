using System;
using UnityEngine;

public static class PreparationManager
{
    public static event Action OnPreparation;
    public static event Action OnItemChoice;

    public const int UNLOCK_WAVES_INDEX = 2;

    /// <summary>
    /// Set the preparation phase or a new item depending on the number of the wave.
    /// </summary>
    public static void TrySetPreparationPhase()
    {
        // TODO
        // Check if the item have already been picked up after a load

        // Give a new upgrade choice every X waves
        if (WaveManager.instance.currentWaveIndex % UNLOCK_WAVES_INDEX == 0)
        {
            if (InventoryManager.availableTurrets.Count <= WaveManager.instance.currentWaveIndex / UNLOCK_WAVES_INDEX && InventoryManager.availableTurrets.Count != 5)
            {
                OnItemChoice?.Invoke();
                return;
            }
        }
            OnPreparation?.Invoke();
    }

    /// <summary>
    /// Directly set the phase to preparation without trying to give a new item choice.
    /// </summary>
    public static void SetPreparationPhase() => OnPreparation?.Invoke();
}
