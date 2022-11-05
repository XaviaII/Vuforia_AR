using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Vuforia;
using Debug = UnityEngine.Debug;

public class ClickEvent : MonoBehaviour
{
    private GameObject _char;
    private GameObject _ground;
    // [SerializeField] private float speed = 1.0f;
    // [SerializeField] private float distance = 0.5f;

    private string _name;
    private Animator _mAnimator;
    public int _state = 0;
    private float _wait = 0;

    private int _count = 0;

    // Start is called before the first frame update
    void Start()
    {
        _mAnimator = GetComponentInChildren<Animator>();
        _char = GameObject.Find("CharModel");
        // _ground = GameObject.Find("Cylinder");
        _ground = GameObject.Find("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Count: " + _count);

        if(_wait > 0)
        {
            _wait -= Time.frameCount;
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;


            if (Physics.Raycast(ray, out Hit))
            {
                _name = Hit.transform.name;

                switch(_name)
                {
                    case "CharModel":
                        // Walking State
                        if (_state == 0)
                        {
                            _mAnimator.SetBool("Walk", true);
                            _state = 1;
                        }
                        // Not Idle State
                        else
                        {
                            _mAnimator.SetBool("Walk", false);
                            _mAnimator.SetBool("TurnAround", false);
                            _state = 0;
                        }

                        // Debug.Log("Ray Hit: " + name);
                        break;
                    default:
                        // Debug.Log("Nothing");
                        break;
                }
            }
        }

        if (_state == 1)
        {
            var _velocity = Vector3.forward * 0.1f;
            var save_distance = 0.025;
            // Cylinder
            // var _endpos = (_ground.transform.localScale.z * 1 / 2) - save_distance;

            // Ground
            var _endpos_z = (_ground.transform.localScale.z * 10 / 2) - save_distance;
            var _endpos_x = (_ground.transform.localScale.x * 10 / 2) - save_distance;

            // var _center = _ground.transform.position;

            // Debug.Log("Center: " + _center);

            // float distance = Vector3.Distance(_char.transform.localPosition, Vector3.zero);

            if ((_char.transform.localPosition.z < _endpos_z && _char.transform.localPosition.z > -_endpos_z &&
                _char.transform.localPosition.x < _endpos_x && _char.transform.localPosition.x > -_endpos_x)  || _count > 850)
            // if(distance < _endpos || _count > 850)
            {
                if (_wait <= 0)
                {
                    if(_mAnimator.GetBool("TurnAround"))
                        _mAnimator.SetBool("TurnAround", false);
                    else
                        _char.transform.Translate(_velocity * Time.deltaTime);
                        _count = 0;

                    //if (_char.transform.localPosition.z > 0)
                    //    _char.transform.rotation = Quaternion.Euler(0, -180, 0);
                    // else
                    //    _char.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

            }
            else
            {

                _mAnimator.SetBool("TurnAround", true);
                _wait = 7.5f;
                _count += 1;
            }








                //if(_wait > 0) && _char.transform.localPosition.z > -_endpos
            //{
            //    _wait -= Time.deltaTime;
            //}  _char.transform.Translate(_velocity * Time.deltaTime);

            //if (_char.transform.localPosition.z < (_ground.transform.localScale.z * 10 / 2) - save_distance)
            //{
            //    if (_state2 == 0)
            //        
            //}
            //else
            //{
            //    if (_state2 == 0)
             //   {
            //        _mAnimator.SetTrigger("TurnAround");

            //        _wait = 10f;
            //        _state2 = 1;
             //   }

                
            //}

        }

    }
}
