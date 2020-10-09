using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materializer : MonoBehaviour
{
    public Transform laserStart;
    public ParticleSystem impactEffect;
    public GameObject selectionCube;
    public Material energyMat;

    public GameObject energyLight;

    public float laserSpeed = 1;

    private Vector3 laserEnd;

    private LineRenderer lineRenderer;

    Ray ray;
    RaycastHit hit;

    private bool laserActive;
    private bool particleStarted = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    
    void Update()
    {
        

        if (Input.GetMouseButton(0))
        {
            laserActive = true;
            
        }

        if (lineRenderer.enabled)
        {
            if (!particleStarted)
            {
                impactEffect.Play();
                particleStarted = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            laserActive = false;
            lineRenderer.enabled = false;
        }

        if (laserActive)
        {
            AimRaycast();
            lineRenderer.material.SetTextureOffset("_MainTex", -Vector2.right * Time.time * laserSpeed);
            
        }
        else
        {
            impactEffect.Stop();
            particleStarted = false;
            selectionCube.SetActive(false);
            energyMat.DisableKeyword("_EMISSION");
            energyLight.SetActive(false);
        }

        
    }

    private void LateUpdate()
    {
        // LateUpdate to fix lag of linerenderer
        lineRenderer.SetPosition(0, laserStart.position);
        lineRenderer.SetPosition(1, laserEnd);
    }

    void AimRaycast()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        laserActive = false;
        lineRenderer.enabled = false;
        

        if (Physics.Raycast(ray, out hit, maxDistance: 100f))
        {
            if (hit.transform.tag == "Destructible")
            {
                laserEnd = hit.point;
                selectionCube.SetActive(true);
                selectionCube.transform.position = hit.transform.gameObject.transform.position;
                impactEffect.transform.position = hit.point;

                //Change particle system color to block color
                impactEffect.GetComponent<ParticleSystemRenderer>().material.color = hit.transform.GetComponent<Block>().particleColor;

                impactEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
                lineRenderer.enabled = true;
                energyMat.EnableKeyword("_EMISSION");
                energyLight.SetActive(true);

                hit.transform.GetComponent<Block>().destructionTimer -= Time.deltaTime;
                return;
            }
        }

        if (!laserActive)
        {
            impactEffect.Stop();
            particleStarted = false;
            selectionCube.SetActive(false);
            energyMat.DisableKeyword("_EMISSION");
            energyLight.SetActive(false);
        }
            
    }
}
