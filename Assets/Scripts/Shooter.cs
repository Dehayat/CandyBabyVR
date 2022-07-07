using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject CandyBar;
    [SerializeField]
    private GameObject BadyBar;
    [SerializeField]
    private float shootForce = 10f;
    [SerializeReference]
    private Transform shootTransform;
    [SerializeField]
    private float shootDelay = 0.25f;
    [SerializeField]
    private float shootCoolDown = 0.25f;
    [SerializeField]
    private float sensitivity = 1f;
    [SerializeField]
    private float anglePerUnit = 1f;
    //[SerializeField]
    //private Transform gunTransform;
    [SerializeField]
    private float minYRotation = -50;
    [SerializeField]
    private float maxYRotation = 50;
    [SerializeField]
    private float minXRotation = -30;
    [SerializeField]
    private float maxXRotation = 30;
    [SerializeField]
    private GameObject shootSmoke;

    [SerializeField]
    private InputActionReference fireCandyAction;
    [SerializeField]
    private InputActionReference firePepperAction;

    private bool canShoot = true;
    private bool canAim = true;
    private Animator anim;

    public bool canControl = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 60;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!canControl) return;

        if (canShoot)
        {
            //if (Input.GetMouseButtonDown(0))
            if (fireCandyAction.action.WasPerformedThisFrame())
            {
                StartCoroutine(Shoot(CandyBar));
            }
            //else if (Input.GetMouseButtonDown(1))
            else if (firePepperAction.action.IsPressed())
            {
                StartCoroutine(Shoot(BadyBar));
            }
        }
        //if (canAim)
        //{
        //    Look(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        //}
    }

    //private void Look(float angleX, float angleY)
    //{
    //    angleX *= sensitivity * Time.deltaTime * anglePerUnit;
    //    angleY *= sensitivity * Time.deltaTime * anglePerUnit;
    //    var rot = gunTransform.localRotation.eulerAngles;
    //    if (rot.x > 180)
    //    {
    //        rot.x -= 360;
    //    }
    //    rot.x += angleX;
    //    rot.x = Mathf.Clamp(rot.x, minXRotation, maxXRotation);
    //    if (rot.y > 180)
    //    {
    //        rot.y -= 360;
    //    }
    //    rot.y += angleY;
    //    rot.y = Mathf.Clamp(rot.y, minYRotation, maxYRotation);
    //    gunTransform.localRotation = Quaternion.Euler(rot);
    //}

    IEnumerator Shoot(GameObject projectile)
    {
        canShoot = false;
        canAim = false;
        anim.SetTrigger("Shoot");
        //sound.Play();
        yield return new WaitForSeconds(shootDelay);
        Vector3 rotation = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
        var bar = Instantiate(projectile, shootTransform.position, Quaternion.Euler(rotation));
        bar.GetComponent<Rigidbody>().AddForce(shootTransform.forward * shootForce, ForceMode.Impulse);
        canAim = true;
        shootSmoke.SetActive(true);
        yield return new WaitForSeconds(shootCoolDown);
        canShoot = true;
    }
}
