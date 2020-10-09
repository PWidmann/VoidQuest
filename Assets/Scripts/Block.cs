using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockType { TestCrate, Netherblock}

    public BlockType blockType;
    public bool destructible;
    public Color particleColor;

    public float destructionTimer = 2f;

    private void Update()
    {
        if (destructionTimer <= 0)
            Destroy(gameObject);
    }
}
