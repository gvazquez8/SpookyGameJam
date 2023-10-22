using System.Collections.Generic;
using UnityEngine;
// Youtube: Sunny Valey Studio


public class Highlight : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> renderers;

    [SerializeField]
    private Color color = Color.white;

    // Used to cache all the materials of object
    private List<Material> materials;

    // Getting all materials from each renderer

    private void Awake()
    {
        materials = new List<Material>();
        foreach(var renderer in renderers)
        {
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool val)
    {
        if (val)
        {
            foreach(var material in materials)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color);
            }
        }
        else
        {
            foreach(var material in materials)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
