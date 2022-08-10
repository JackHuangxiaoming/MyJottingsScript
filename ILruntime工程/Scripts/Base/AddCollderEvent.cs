using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTriggerDelegate(Collider other);
public delegate void OnCollisionDelegate(Collision other);
public delegate void OnMouseDelegate();



public class AddCollderEvent : MonoBehaviour
{

    public OnTriggerDelegate OnTriggerEnterHandler;
    public OnTriggerDelegate OnTriggerExitHandler;
    public OnTriggerDelegate OnTriggerStayHandler;
    public OnCollisionDelegate OnCollisionEnterHandler;
    public OnCollisionDelegate OnCollisionExitHandler;
    public OnCollisionDelegate OnCollisionStayHandler;
    public OnMouseDelegate OnMouseEnterHandler;
    public OnMouseDelegate OnMouseOverHandler;
    public OnMouseDelegate OnMouseExitHandler;
    public OnMouseDelegate OnMouseDownHandler;
    public OnMouseDelegate OnMouseUpHandler;

    public object Data { get; set; }
    private float _downTime;

    void OnMouseEnter()
    {
        if (OnMouseEnterHandler != null)
        {
            OnMouseEnterHandler();
        }
    }

    void OnMouseOver()
    {
        if (OnMouseOverHandler != null)
        {
            OnMouseOverHandler();
        }
    }

    void OnMouseExit()
    {
        if (OnMouseExitHandler != null)
        {
            OnMouseExitHandler();
        }
    }


    void OnMouseDown()
    {
        if (OnMouseDownHandler != null)
        {
            OnMouseDownHandler();
        }
    }


    void OnMouseUp()
    {
        if (OnMouseUpHandler != null)
        {
            OnMouseUpHandler();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (OnTriggerEnterHandler != null)
        {
            OnTriggerEnterHandler(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (OnTriggerExitHandler != null)
        {
            OnTriggerExitHandler(other);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (OnTriggerStayHandler != null)
        {
            OnTriggerStayHandler(other);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (OnCollisionEnterHandler != null)
        {
            OnCollisionEnterHandler(collisionInfo);
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (OnCollisionExitHandler != null)
        {
            OnCollisionExitHandler(collisionInfo);
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (OnCollisionStayHandler != null)
        {
            OnCollisionStayHandler(collisionInfo);
        }
    }



}
