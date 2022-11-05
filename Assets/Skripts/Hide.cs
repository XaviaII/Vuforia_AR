using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Hide : MonoBehaviour
{

    GameObject _cam;
    private GameObject _char;
    private Animator _mAnimator;

    private float _wait = 0;

    private ClickEvent click_event;

    // Start is called before the first frame update
    void Start()
    {
        _cam = GameObject.Find("ARCamera");
        _mAnimator = GetComponentInChildren<Animator>();
        _char = GameObject.Find("CharModel");
        click_event = GetComponent<ClickEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_cam.transform.position, transform.position);
        // Debug.Log("The Distance is: " + distance);

        if(_wait > 0)
        {
            _wait -= Time.deltaTime;
        }

        if (distance < 1f)
        {
            _char.SetActive(true);

            if (click_event._state == 1)
                _mAnimator.SetBool("Walk", true);


            if (_mAnimator != null && distance < 0.3f) 
            {
                if (_wait <= 0)
                {
                    _mAnimator.SetTrigger("Jump");
                    _wait = 3f;
                }
            }
        }
        else
        {
            _char.SetActive(false);
        }
    }
}
