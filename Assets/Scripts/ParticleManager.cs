using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    //may need to make this a static instance so everythign can easily call it and get particles?

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
