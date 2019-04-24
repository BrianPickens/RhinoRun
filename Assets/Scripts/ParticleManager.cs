using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType { Gold, Silver, Bronze, Diamond, Hurdle, Shield, RhinoSnax, UnlimitedCharge }
public class ParticleManager : MonoBehaviour
{

    private static ParticleManager instance = null;
    public static ParticleManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private ObjectPooler goldParticles = null;

    [SerializeField]
    private ObjectPooler silverParticles = null;

    [SerializeField]
    private ObjectPooler bronzeParticles = null;

    [SerializeField]
    private ObjectPooler diamondParticles = null;

    [SerializeField]
    private ObjectPooler hurdleParticles = null;

    [SerializeField]
    private ObjectPooler shieldParticles = null;

    [SerializeField]
    private ObjectPooler rhinoSnaxParticles = null;

    [SerializeField]
    private ObjectPooler chargeParticles = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetPosition(GameObject _gameObject, Vector3 _position)
    {
        _gameObject.transform.position = _position;
        _gameObject.SetActive(true);
    }

    public void CreateParticles(ParticleType _particleType, Vector3 _position)
    {
        GameObject newParticle = null;

        switch (_particleType)
        {
            case ParticleType.Bronze:
                newParticle = bronzeParticles.GetPooledObject();
                break;

            case ParticleType.Silver:
                newParticle = silverParticles.GetPooledObject();
                break;

            case ParticleType.Gold:
                newParticle = goldParticles.GetPooledObject();
                break;

            case ParticleType.Diamond:
                newParticle = diamondParticles.GetPooledObject();
                break;

            case ParticleType.Hurdle:
                newParticle = hurdleParticles.GetPooledObject();
                break;

            case ParticleType.RhinoSnax:
                newParticle = rhinoSnaxParticles.GetPooledObject();
                break;

            case ParticleType.Shield:
                newParticle = shieldParticles.GetPooledObject();
                break;

            case ParticleType.UnlimitedCharge:
                newParticle = chargeParticles.GetPooledObject();
                break;

            default:
                Debug.LogError("Invalid particle type in CreateParticles");
                break;
        }

        SetPosition(newParticle, _position);
    }

}
