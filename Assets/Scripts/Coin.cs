using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{

    [SerializeField]
    private Renderer myRenderer= null;

    [SerializeField]
    private Texture goldTexture = null;

    [SerializeField]
    private Texture silverTexture = null;

    [SerializeField]
    private Texture bronzeTexture = null;

    public override void UpdateTexture(int _level)
    {
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
