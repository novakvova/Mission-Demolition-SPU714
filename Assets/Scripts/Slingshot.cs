using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("References")]
    public GameObject ProjectilePrefab;
    [Space(20)]
    public float velocityMult = 8f;

    private GameObject launchPoint;
    private Vector3 launchPos;
    private GameObject projectile;
    private bool aimingMode;
    private Rigidbody projectileRigidbody;

    private void OnMouseEnter()
    {
        print("Enter");
        launchPoint.SetActive(true);
    }
    
    private void OnMouseExit()
    {
        print("Exit");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(ProjectilePrefab) as GameObject;
        projectile.transform.position = launchPos;
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    private void Update()
    {
        if (!aimingMode) return;
        
        Vector3 mousePos2D = Input.mousePosition; // с
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );
        Vector3 mouseDelta = mousePos3D-launchPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude) 
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if (Input.GetMouseButtonUp(0)) 
        { 
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
        }

    }
}
