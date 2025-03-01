using System.Collections.Generic;
using UnityEngine;

public class TurretVisual : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> turretRenderers = new List<MeshRenderer>();

    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material visualizerMaterial;

    public void SetBaseMaterial()
    {
        foreach (MeshRenderer renderer in turretRenderers)
        {
            renderer.material = baseMaterial;
        }
    }

    public void SetVisualizerMaterial()
    {
        foreach(MeshRenderer renderer in turretRenderers)
        {
            renderer.material = visualizerMaterial;
        }
    }

}
