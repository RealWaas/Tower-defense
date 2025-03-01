using System;
using UnityEngine;

public static class PreparationManager
{
    public static event Action OnPreparation;
    public static event Action OnItemChoice;

    private const int UNLOCK_WAVES_INDEX = 5;

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
            OnItemChoice?.Invoke();
        }
        else
            OnPreparation?.Invoke();
    }

    /// <summary>
    /// Directly set the phase to preparation without trying to give a new item choice.
    /// </summary>
    public static void SetPreparationPhase() => OnPreparation?.Invoke();
}
