using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    [SerializeField]
    private ObjectPooler goldParticles;

    public void SetPosition(GameObject _gameObject, Vector3 _position)
    {
        _gameObject.transform.position = _position;
        _gameObject.SetActive(true);
    }

    public void CreateGoldParticles(Vector3 _position)
    {
        GameObject particle = goldParticles.GetPooledObject();
        SetPosition(particle, _position);
    }

}
