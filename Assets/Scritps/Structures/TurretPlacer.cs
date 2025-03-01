using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    public Vector2Int placerPosition { get; private set; } = Vector2Int.zero;
    public bool hasTurret { get; private set; } = false;


    public void SetPosition(Vector2Int _position) => placerPosition = _position;
    public void SetTurret() => hasTurret = true;
}
