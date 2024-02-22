using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BrickLevel")]
public class BrickLevel : ScriptableObject
{
    public string levelName;
    public string[] levelData;

    // B - Brick 
    // I - InvBrick
}
