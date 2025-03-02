using System.Collections.Generic;
using UnityEngine;

public class TurretVisual : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> turretRenderers = new List<MeshRenderer>();

    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material placementMaterial;
    [SerializeField] private Material selectionMaterial;

    public void SetBaseMaterial()
    {
        foreach (MeshRenderer renderer in turretRenderers)
        {
            renderer.material = baseMaterial;
        }
    }

    public void SetPlacementMaterial()
    {
        foreach(MeshRenderer renderer in turretRenderers)
        {
            renderer.material = placementMaterial;
        }
    }

    public void SetSelectionMaterial()
    {
        foreach (MeshRenderer renderer in turretRenderers)
        {
            renderer.material = selectionMaterial;
        }
    }
}
