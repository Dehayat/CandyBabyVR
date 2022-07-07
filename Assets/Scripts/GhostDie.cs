using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDie : MonoBehaviour
{
    [SerializeField]
    private Material deathMat;
    [SerializeField]
    private float dissolveSpeed = 1f;

    private SkinnedMeshRenderer skin;

    public void Die()
    {
        skin = GetComponentInChildren<SkinnedMeshRenderer>();
        skin.material = deathMat;
        StartCoroutine(Dissolve());
        //Destroy(gameObject);
    }
    IEnumerator Dissolve()
    {
        float level = 0;
        while (level < 1)
        {
            skin.material.SetFloat("_Level", level);
            level += Time.deltaTime * dissolveSpeed;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
