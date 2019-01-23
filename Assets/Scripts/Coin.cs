using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{

    [SerializeField]
    private Renderer myRenderer;

    [SerializeField]
    private Texture goldTexture;

    [SerializeField]
    private Texture silverTexture;

    [SerializeField]
    private Texture bronzeTexture;

    public override void UpdateTexture(int _level)
    {
        Debug.Log("this got called");
        switch (_level)
        {
            case 0:
                myRenderer.material.mainTexture = bronzeTexture;
                break;

            case 1:
                myRenderer.material.mainTexture = silverTexture;
                break;

            case 2:
                myRenderer.material.mainTexture = goldTexture;
                break;

            default:
                Debug.LogError("invalid level in coin textures");
                myRenderer.material.mainTexture = bronzeTexture;
                break;
        }

    }

}
