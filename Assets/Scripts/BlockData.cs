using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block Data", menuName = "ScriptableObjects/BlockData")]

public class BlockData : ScriptableObject
{
    public BlockType BlockType;
    public Sprite BlockSprite;
    public int count;

}
