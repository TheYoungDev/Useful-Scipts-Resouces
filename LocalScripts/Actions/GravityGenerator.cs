﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGenerator : MonoBehaviour {

    public float Force = 20;
    private Rigidbody Rb;
    public GameObject CollidedObject;
    public GameObject CurrentObject;
    public Transform[] ShootLocation;
    public int Inverse = 1;
    public Transform MoveTo;
    public Vector3 pushDirection;
    public Vector3 FreezeDirection;
    public bool SinglePlayer = true;
    // Use this for initialization
    void Start () {
       // pushDirection = transform.up;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(ShootLocation[0].position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
        {
            //if()
            CurrentObject = hit.transform.gameObject;
            // if (CollidedObject != null)
            //Debug.Log(CurrentObject.name + " Current/Old " + CollidedObject.name);
            if (CollidedObject != null && (CurrentObject != CollidedObject || CurrentObject == null ))
            {
                if (CollidedObject.CompareTag("Player"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.constraints = RigidbodyConstraints.None;
                    Rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                    if (SinglePlayer)
                        CollidedObject.GetComponent <LocalPlayerMovement>().ExitGravityGenerator();
                    if (!SinglePlayer)
                        CollidedObject.GetComponent<playerNormalMovement>().ExitGravityGenerator();
                    //CollidedObject.GetComponent<playerNormalMovement>().Gravity = 50;
                }
                else if (CollidedObject.CompareTag("PickUp"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.useGravity = true;

                }
                else if (CollidedObject.tag.Contains("Character"))
                {

                }
                else if (CollidedObject.tag.Contains("Enemy"))
                {

                }
            }

            //Debug.DrawRay(ShootLocation[0].position, transform.TransformDirection(Vector3.up) * hit.distance, Color.red);
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                CollidedObject = hit.transform.gameObject;
                if(!SinglePlayer)
                    CollidedObject.GetComponent<playerNormalMovement>().GravityGenerator();
                else
                    CollidedObject.GetComponent<LocalPlayerMovement>().GravityGenerator();

                
                Rb = CollidedObject.GetComponent<Rigidbody>();
                if (FreezeDirection == new Vector3(0, 1, 0))
                    Rb.constraints =RigidbodyConstraints.FreezePositionY |RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation; 
                if (FreezeDirection == new Vector3(1, 0, 0))
                    Rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                if (FreezeDirection == new Vector3(0, 0, 1))
                    Rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                //if(Rb.velocity.magnitude<=0.5f)
                CollidedObject.transform.position = Vector3.MoveTowards(CollidedObject.transform.position, MoveTo.position, Time.deltaTime * Force / 20f);
                //CollidedObject.transform.Translate(pushDirection*Inverse* Time.deltaTime* Force/100f);
                // Rb.AddForce(transform.up * Force);
                //disable gravity add small force in transform .direction
            }
            else if (hit.transform.gameObject.tag.Contains("Character"))
            {

            }
            else if(hit.transform.gameObject.CompareTag("PickUp"))
            {
                CollidedObject = hit.transform.gameObject;
                Rb = CollidedObject.GetComponent<Rigidbody>();
                Rb.useGravity = false;
                if (Rb.velocity.magnitude <= 0.5f)
                    Rb.AddForce(transform.up * Force);

            }
            else if(hit.transform.gameObject.tag.Contains("Enemy"))
            {

            }




        }
        else if (Physics.Raycast(ShootLocation[1].position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
        {
            //if()
            CurrentObject = hit.transform.gameObject;
            // if (CollidedObject != null)
            //Debug.Log(CurrentObject.name + " Current/Old " + CollidedObject.name);
            if (CollidedObject != null && (CurrentObject != CollidedObject || CurrentObject == null))
            {
                if (CollidedObject.CompareTag("Player"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.constraints = RigidbodyConstraints.None;
                    Rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                    if (!SinglePlayer)
                        CollidedObject.GetComponent<playerNormalMovement>().ExitGravityGenerator();
                    else
                        CollidedObject.GetComponent<LocalPlayerMovement>().ExitGravityGenerator();
                    //CollidedObject.GetComponent<playerNormalMovement>().Gravity = 50;
                }
                else if (CollidedObject.CompareTag("PickUp"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.useGravity = true;

                }
                else if (CollidedObject.tag.Contains("Character"))
                {

                }
                else if (CollidedObject.tag.Contains("Enemy"))
                {

                }
            }

            Debug.DrawRay(ShootLocation[1].position, transform.TransformDirection(Vector3.up) * hit.distance, Color.red);
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                CollidedObject = hit.transform.gameObject;
                if(!SinglePlayer)
                    CollidedObject.GetComponent<playerNormalMovement>().GravityGenerator();
                else
                    CollidedObject.GetComponent<LocalPlayerMovement>().GravityGenerator();

                Rb = CollidedObject.GetComponent<Rigidbody>();
                if (FreezeDirection == new Vector3(0, 1, 0))
                    Rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                if (FreezeDirection == new Vector3(1, 0, 0))
                    Rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                if (FreezeDirection == new Vector3(0, 0, 1))
                    Rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                //if(Rb.velocity.magnitude<=0.5f)
                CollidedObject.transform.position = Vector3.MoveTowards(CollidedObject.transform.position, MoveTo.position, Time.deltaTime * Force / 20f);
                //CollidedObject.transform.Translate(pushDirection * Inverse * Time.deltaTime * Force / 100f);
                // Rb.AddForce(transform.up * Force);
                //disable gravity add small force in transform .direction
            }
            else if (hit.transform.gameObject.tag.Contains("Character"))
            {

            }
            else if (hit.transform.gameObject.CompareTag("PickUp"))
            {
                CollidedObject = hit.transform.gameObject;
                Rb = CollidedObject.GetComponent<Rigidbody>();
                Rb.useGravity = false;
                if (Rb.velocity.magnitude <= 0.5f)
                    Rb.AddForce(transform.up * Force);

            }
            else if (hit.transform.gameObject.tag.Contains("Enemy"))
            {

            }




        }
        else if (Physics.Raycast(ShootLocation[2].position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
        {
            //if()
            CurrentObject = hit.transform.gameObject;
            // if (CollidedObject != null)
            //Debug.Log(CurrentObject.name + " Current/Old " + CollidedObject.name);
            if (CollidedObject != null && (CurrentObject != CollidedObject || CurrentObject == null))
            {
                if (CollidedObject.CompareTag("Player"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.constraints = RigidbodyConstraints.None;
                    Rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                    if (!SinglePlayer)
                        CollidedObject.GetComponent<playerNormalMovement>().ExitGravityGenerator();
                    else
                        CollidedObject.GetComponent<LocalPlayerMovement>().ExitGravityGenerator();
                    //CollidedObject.GetComponent<playerNormalMovement>().Gravity = 50;
                }
                else if (CollidedObject.CompareTag("PickUp"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.useGravity = true;

                }
                else if (CollidedObject.tag.Contains("Character"))
                {

                }
                else if (CollidedObject.tag.Contains("Enemy"))
                {

                }
            }

            Debug.DrawRay(ShootLocation[2].position, transform.TransformDirection(Vector3.up) * hit.distance, Color.red);
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                CollidedObject = hit.transform.gameObject;
                if (!SinglePlayer)
                    CollidedObject.GetComponent<playerNormalMovement>().GravityGenerator();
                else
                    CollidedObject.GetComponent<LocalPlayerMovement>().GravityGenerator();
                Rb = CollidedObject.GetComponent<Rigidbody>();
                if (FreezeDirection == new Vector3(0, 1, 0))
                    Rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                if (FreezeDirection == new Vector3(1, 0, 0))
                    Rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                if (FreezeDirection == new Vector3(0, 0, 1))
                    Rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                //if(Rb.velocity.magnitude<=0.5f)
                CollidedObject.transform.position = Vector3.MoveTowards(CollidedObject.transform.position, MoveTo.position, Time.deltaTime * Force / 20f);
                //CollidedObject.transform.Translate(pushDirection * Inverse * Time.deltaTime * Force / 100f);
                // Rb.AddForce(transform.up * Force);
                //disable gravity add small force in transform .direction
            }
            else if (hit.transform.gameObject.tag.Contains("Character"))
            {

            }
            else if (hit.transform.gameObject.CompareTag("PickUp"))
            {
                CollidedObject = hit.transform.gameObject;
                Rb = CollidedObject.GetComponent<Rigidbody>();
                Rb.useGravity = false;
                if (Rb.velocity.magnitude <= 0.5f)
                    Rb.AddForce(transform.up * Force);

            }
            else if (hit.transform.gameObject.tag.Contains("Enemy"))
            {

            }




        }
        else if (Physics.Raycast(ShootLocation[3].position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
        {
            //if()
            CurrentObject = hit.transform.gameObject;
            // if (CollidedObject != null)
            //Debug.Log(CurrentObject.name + " Current/Old " + CollidedObject.name);
            if (CollidedObject != null && (CurrentObject != CollidedObject || CurrentObject == null))
            {
                if (CollidedObject.CompareTag("Player"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.constraints = RigidbodyConstraints.None;
                    Rb.constraints = RigidbodyConstraints.FreezeRotation| RigidbodyConstraints.FreezeRotation;
                    /*Rb.constraints = RigidbodyConstraints.FreezeRotationY;
                    Rb.constraints = RigidbodyConstraints.FreezeRotationZ;*/
                    if (!SinglePlayer)
                        CollidedObject.GetComponent<playerNormalMovement>().ExitGravityGenerator();
                    else
                        CollidedObject.GetComponent<LocalPlayerMovement>().ExitGravityGenerator();
                    //CollidedObject.GetComponent<playerNormalMovement>().Gravity = 50;
                }
                else if (CollidedObject.CompareTag("PickUp"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.useGravity = true;

                }
                else if (CollidedObject.tag.Contains("Character"))
                {

                }
                else if (CollidedObject.tag.Contains("Enemy"))
                {

                }
            }

            Debug.DrawRay(ShootLocation[3].position, transform.TransformDirection(Vector3.up) * hit.distance, Color.red);
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                CollidedObject = hit.transform.gameObject;
                if (!SinglePlayer)
                    CollidedObject.GetComponent<playerNormalMovement>().GravityGenerator();
                else
                    CollidedObject.GetComponent<LocalPlayerMovement>().GravityGenerator();
                Rb = CollidedObject.GetComponent<Rigidbody>();
                if (FreezeDirection == new Vector3(0, 1, 0))
                    Rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                if (FreezeDirection == new Vector3(1, 0, 0))
                    Rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                if (FreezeDirection == new Vector3(0, 0, 1))
                    Rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                //if(Rb.velocity.magnitude<=0.5f)
               // CollidedObject.transform.Translate(pushDirection * Inverse* Time.deltaTime * Force / 100f);
                CollidedObject.transform.position = Vector3.MoveTowards(CollidedObject.transform.position, MoveTo.position, Time.deltaTime * Force / 20f);
                // Rb.AddForce(transform.up * Force);
                //disable gravity add small force in transform .direction
            }
            else if (hit.transform.gameObject.tag.Contains("Character"))
            {

            }
            else if (hit.transform.gameObject.CompareTag("PickUp"))
            {
                CollidedObject = hit.transform.gameObject;
                Rb = CollidedObject.GetComponent<Rigidbody>();
                Rb.useGravity = false;
                if (Rb.velocity.magnitude <= 0.5f)
                    Rb.AddForce(transform.up * Force);

            }
            else if (hit.transform.gameObject.tag.Contains("Enemy"))
            {

            }




        }
        else if (Physics.Raycast(ShootLocation[4].position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
        {
            //if()
            CurrentObject = hit.transform.gameObject;
            // if (CollidedObject != null)
            //Debug.Log(CurrentObject.name + " Current/Old " + CollidedObject.name);
            if (CollidedObject != null && (CurrentObject != CollidedObject || CurrentObject == null))
            {
                if (CollidedObject.CompareTag("Player"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.constraints = RigidbodyConstraints.None;
                    Rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                    if (!SinglePlayer)
                        CollidedObject.GetComponent<playerNormalMovement>().ExitGravityGenerator();
                    else
                        CollidedObject.GetComponent<LocalPlayerMovement>().ExitGravityGenerator();
                    //CollidedObject.GetComponent<playerNormalMovement>().Gravity = 50;
                }
                else if (CollidedObject.CompareTag("PickUp"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.useGravity = true;

                }
                else if (CollidedObject.tag.Contains("Character"))
                {

                }
                else if (CollidedObject.tag.Contains("Enemy"))
                {

                }
            }

            Debug.DrawRay(ShootLocation[4].position, transform.TransformDirection(Vector3.up) * hit.distance, Color.red);
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                CollidedObject = hit.transform.gameObject;
                if (!SinglePlayer)
                    CollidedObject.GetComponent<playerNormalMovement>().GravityGenerator();
                else
                    CollidedObject.GetComponent<LocalPlayerMovement>().GravityGenerator();
                Rb = CollidedObject.GetComponent<Rigidbody>();
                if (FreezeDirection == new Vector3(0, 1, 0))
                    Rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                if (FreezeDirection == new Vector3(1, 0, 0))
                    Rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                if (FreezeDirection == new Vector3(0, 0, 1))
                    Rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                //if(Rb.velocity.magnitude<=0.5f)
                CollidedObject.transform.position = Vector3.MoveTowards(CollidedObject.transform.position, MoveTo.position, Time.deltaTime * Force /20f);
                //CollidedObject.transform.Translate(pushDirection * Inverse*Time.deltaTime * Force / 100f);
                // Rb.AddForce(transform.up * Force);
                //disable gravity add small force in transform .direction
            }
            else if (hit.transform.gameObject.tag.Contains("Character"))
            {

            }
            else if (hit.transform.gameObject.CompareTag("PickUp"))
            {
                CollidedObject = hit.transform.gameObject;
                Rb = CollidedObject.GetComponent<Rigidbody>();
                Rb.useGravity = false;
                if (Rb.velocity.magnitude <= 0.5f)
                    Rb.AddForce(transform.up * Force);

            }
            else if (hit.transform.gameObject.tag.Contains("Enemy"))
            {

            }




        }
        else
        {
            if (CollidedObject != null)
            {
                if (CollidedObject.CompareTag("Player"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.constraints = RigidbodyConstraints.None;
                    Rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotation;
                    if (!SinglePlayer)
                        CollidedObject.GetComponent<playerNormalMovement>().ExitGravityGenerator();
                    else
                        CollidedObject.GetComponent<LocalPlayerMovement>().ExitGravityGenerator();
                    //CollidedObject.GetComponent<playerNormalMovement>().Gravity = 50;
                }
                else if (CollidedObject.CompareTag("PickUp"))
                {
                    Rb = CollidedObject.GetComponent<Rigidbody>();
                    Rb.useGravity = true;

                }
                else if (CollidedObject.tag.Contains("Character"))
                {

                }
                else if (CollidedObject.tag.Contains("Enemy"))
                {

                }
            }
        }
    }
}
