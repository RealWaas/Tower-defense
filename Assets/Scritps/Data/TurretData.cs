using System;
using UnityEngine;

public struct TurretData
{
    public TurretData(TurretType _type, Vector2Int _pos, int _level)
    {
        turretType = _type;
        turretPos = _pos;
        turretLevel = _level;
    }

    public TurretData(TurretData _data)
    {
        turretType = _data.turretType;
        turretPos = _data.turretPos;
        turretLevel = _data.turretLevel;
    }

    public TurretType turretType;
    public Vector2Int turretPos;
    public int turretLevel;

    #region Operators
    public static bool operator ==(TurretData _turret1, TurretData _turret2)
    {
        return _turret1.turretPos == _turret2.turretPos;
    }
    public static bool operator !=(TurretData _turret1, TurretData _turret2)
    {
        return _turret1.turretPos != _turret2.turretPos;
    }

    public override bool Equals(object obj)
    {
        return obj is TurretData data &&
               turretPos.Equals(data.turretPos);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(turretPos);
    }
    #endregion
}

public enum TurretType
{
    None,
    Normal,
    Rapid,
    FlameThrower,
    Canon,
    SnowBlaster
}
