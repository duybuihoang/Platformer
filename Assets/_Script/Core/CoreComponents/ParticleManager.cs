using Unity.Mathematics;
using UnityEngine;

public class ParticleManager : CoreComponent
{
    private Transform particleContainer;

    protected override void Awake()
    {
        base.Awake();


        particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;

    }

    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
    {
        return  Instantiate(particlePrefab, position, rotation, particleContainer);

    }

    public GameObject StartParticles(GameObject particlePrefab)
    {
        return StartParticles(particlePrefab, transform.position, quaternion.identity);
    }

    public GameObject StartParticleWithRandomRotation(GameObject particlePrefab)
    {
        var randomRotation = quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0f, 360f));
        return StartParticles(particlePrefab, transform.position, randomRotation);
    }

}
