using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;

public class GrappleAbility : MonoBehaviour
{
    private PlayerInputProcessor playerInput;
    public float radius;
    public LayerMask grappleLayer;
    public bool grappling = false;
    Collider2D grappleObject = null;
    public Collider2D rotatorCollision;
    public GameObject grappleMarker;
    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInputProcessor>();
        playerInput.OnAbilityInvoke += GrappleAction;
    }
    private void OnDisable()
    {
        playerInput.OnAbilityInvoke -= GrappleAction;
    }

    private void Update()
    {
        rotatorCollision.transform.rotation = Quaternion.LookRotation(Vector3.forward, playerInput.movementAxis);
        if (grappling)
            return;
        GetPossibleObjects();
    }

    public void GrappleAction()
    {
        Debug.Log("Grappling");
        if (grappling)
        {
            Ungrapple();
        }
        else
        {
            
            Grapple();
        }
    }

    void Grapple()
    {
        
        grappleObject.GetComponent<GrappleObject>().Consume();
        SpringJoint2D joint = grappleObject.GetComponent<SpringJoint2D>();
        joint.connectedBody = GetComponent<Rigidbody2D>();
        grappling = true;
    }

    private bool GetPossibleObjects()
    {
        Collider2D[] grappleObjects = Physics2D.OverlapCircleAll(transform.position, radius, grappleLayer);
        if (grappleObjects.Length <= 0)
        {
            grappleMarker.GetComponent<SpriteRenderer>().color = Color.clear;
            return true;
        }

        float distance = 1000;
        for (int i = 0; i < grappleObjects.Length; i++)
        {
            var currentDistance = Vector2.Distance(transform.position, grappleObjects[i].transform.position);
            if (currentDistance < distance && !grappleObjects[i].GetComponent<GrappleObject>().consumed)
            {
                grappleMarker.GetComponent<SpriteRenderer>().color = Color.white;
                distance = currentDistance;
                grappleObject = grappleObjects[i];
                grappleMarker.transform.position = grappleObject.transform.position;

            }
        }

        return false;
    }

    void Ungrapple()
    {
        SpringJoint2D joint = grappleObject.GetComponent<SpringJoint2D>();
        joint.connectedBody = null;
        grappling = false;
    }

}
