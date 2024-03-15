using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXprefabPlayer : MonoBehaviour
{
    public GameObject VFX;

    public void PlayDeathVFX()
    {
        GameObject vfx = Instantiate(VFX, transform.position, Quaternion.identity);
        Destroy(vfx, vfx.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
    } 
}
