using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public InventoryElement[] _calls;
    public BlockData[] _blockdatas;

    private void Start()
    {
        _calls[0].Image.sprite = _blockdatas[0].BlockSprite;
        _calls[0].Text.text = _blockdatas[0].count.ToString();

        _calls[1].Image.sprite = _blockdatas[1].BlockSprite;
        _calls[1].Text.text = _blockdatas[1].count.ToString();

        _calls[2].Image.sprite = _blockdatas[2].BlockSprite;
        _calls[2].Text.text = _blockdatas[2].count.ToString();

        _calls[3].Image.sprite = _blockdatas[3].BlockSprite;
        _calls[3].Text.text = _blockdatas[3].count.ToString();

        //_calls[1].sprite = _blockdatas[1].BlockSprite;


    }
}
