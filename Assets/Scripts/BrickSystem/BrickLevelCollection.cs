using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BrickLevelCollection")]
public class BrickLevelCollection : ScriptableObject
{
    public List<BrickLevel> LvlCollection = new List<BrickLevel>();

    public string SelectedLvl;
}
