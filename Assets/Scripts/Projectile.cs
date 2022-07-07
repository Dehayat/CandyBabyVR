using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    GoodCandy,
    BadCandy
}
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private ProjectileType candyType;
    [SerializeField]
    private GameObject[] meshes;

    public ProjectileType GetCandyType()
    {
        return candyType;
    }

    private void Awake()
    {
        int chosenMesh = Random.Range(0, meshes.Length);
        for(int i = 0; i < meshes.Length; i++)
        {
            if (i == chosenMesh)
            {
                meshes[i].SetActive(true);
            }
            else
            {
                meshes[i].SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = 11;
            }
            gameObject.tag = "Floor";
            Destroy(this);
        }
    }
}
