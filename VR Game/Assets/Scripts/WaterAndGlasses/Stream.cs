using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;

    private Vector3 targetPos = Vector3.zero;

    private ParticleSystem splashParticle = null;

    private Coroutine pourRoutine = null;


    public static Action<string> OnSaucerHitAction = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();
    }


    // Start is called before the first frame update
    void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Begin()
    {
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPour());
    }

    private IEnumerator BeginPour()
    {
        while(gameObject.activeSelf)
        {
            targetPos = FindEndPoint();
            
            MoveToPosition(0, transform.position);
            // MoveToPosition(1, targetPos);
            AnimateToPosition(1, targetPos);

            yield return null;
        }
    }

    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());
    }

    private IEnumerator EndPour()
    {
        while(!HasReachedPosition(0, targetPos))
        {
            AnimateToPosition(0, targetPos);
            AnimateToPosition(1, targetPos);
            
            yield return null;
        }

        Destroy(gameObject);
    }

    private Vector3 FindEndPoint()
    {
        // Debug.Log("Finding the endpoint");

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, 2f);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2f);

        // Debug.Log(endPoint);

        if(hit.collider.name == "Saucer1" || hit.collider.name == "Saucer2" || hit.collider.name == "Saucer3" || hit.collider.name == "Saucer4")
        {
            Debug.Log("Some saucer is hit");

            if(OnSaucerHitAction != null)
            {
                OnSaucerHitAction(hit.collider.name);
            }
        }

        return endPoint;
    }

    private void MoveToPosition(int index, Vector3 targetPos)
    {
        lineRenderer.SetPosition(index, targetPos);
    }

    private void AnimateToPosition(int index, Vector3 targetPos)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPos = Vector3.MoveTowards(currentPoint, targetPos, Time.deltaTime * 1.75f);

        lineRenderer.SetPosition(index, newPos);
    }

    private bool HasReachedPosition(int index, Vector3 targetPos)
    {
        Vector3 currentPos = lineRenderer.GetPosition(index);
        return currentPos == targetPos;
    }

    private IEnumerator UpdateParticle()
    {
        while(gameObject.activeSelf)
        {
            splashParticle.gameObject.transform.position = targetPos;

            bool isHitting = HasReachedPosition(1, targetPos);
            
            splashParticle.gameObject.SetActive(isHitting);
            
            yield return null;
        }
    }
}
