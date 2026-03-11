using System;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Block[] blocks;
    
    public delegate void OnBlockClicked(int index);

    public OnBlockClicked onBlockClicked;
    
    // 0 ~ 8번까지의 블록 초기화
    public void InitBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].InitMarker(i, blockIndex =>
            {
                onBlockClicked?.Invoke(blockIndex);
            });
        }
    }
    
    
    public void PlaceMarker(int blockIndex, Constants.PlayerType playerType)
    {
        switch (playerType)
        {
            case Constants.PlayerType.Player1:
                blocks[blockIndex].SetMarker(Block.MarkerType.O);
                break;
            case Constants.PlayerType.Player2:
                blocks[blockIndex].SetMarker(Block.MarkerType.X);
                break;
        }
    }

    [ContextMenu("오목판 정렬")]
    public void SortBoard()
    {
        for (int i = 0; i < 225; i++)
        {
            int r = i / 15;
            int c = i % 15;
            
            float posX = -12.6f + (c * 1.8f);
            float posY = 12.6f - (r * 1.8f);
            blocks[i].transform.localPosition = new Vector3(posX, posY, 0);
        }
    }
}
