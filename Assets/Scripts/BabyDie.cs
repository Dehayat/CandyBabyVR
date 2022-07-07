using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyDie : MonoBehaviour
{
    [SerializeField]
    private float destroyDelay = 1f;
    [SerializeField]
    private GameObject candyExplosion;

    private SkinnedMeshRenderer skin;

    public void Die()
    {
        skin = GetComponentInChildren<SkinnedMeshRenderer>();
        skin.enabled = false;
        StartCoroutine(Explode());
        //Destroy(gameObject);
    }
    IEnumerator Explode()
    {
        Destroy(Instantiate(candyExplosion, transform.position, Quaternion.identity), destroyDelay+0.2f);

        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
