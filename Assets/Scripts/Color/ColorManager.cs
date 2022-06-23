using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;

public class ColorManager : Singleton<ColorManager>
{
    public List<Material> materials;
    public List<ColorSetup> colorSetups;

    public void ChangeColorByType(ArtManager.ArtType type)
    {
        var setup = colorSetups.Find(i => i.artType == type);

        for(int i = 0; i < materials.Count; i++)
        {
            materials[i].SetColor("_Color", setup.colors[Random.Range(0,setup.colors.Count)]);
        }
    }



}


[System.Serializable]
public class ColorSetup
{
    public ArtManager.ArtType artType;
    public List<Color> colors;
}