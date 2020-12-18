using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    private float _attackRange = 3f;
    private float _rayDistance = 3f;
    private float _stoppingDistance = 1.5f;
   
    private Vector3 testPosition;
    private Vector3 _destination;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private EnemyState _currentState;

    public GameObject enemybulletPrefab;
    
    
    public enum EnemyState
    {
        Wander,
        Chase,
        Attack
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            case EnemyState.Wander:
            {
                if(NeedDestination())
                {
                    GetDestination();
                }
                transform.rotation = _desiredRotation;
                transform.Translate(translation: Vector3.forward * Time.deltaTime);
                float theAngle = 150.0f;
                float segments = 10.0f;
                Vector3 startPos = transform.position; 
                Vector3 targetPos = Vector3.zero;

                int startAngle = Convert.ToInt32(-theAngle * 0.5f);
                int finishAngle = Convert.ToInt32(theAngle * 0.5f);
                int inc = Convert.ToInt32(theAngle / segments);

                RaycastHit hit;

                for (int i = startAngle; i < finishAngle; i += inc )
                {
                    targetPos = (Quaternion.Euler( 0, i, 0 ) * transform.forward).normalized * _rayDistance;

                    if ( Physics.Linecast( startPos, targetPos, out hit ) )
                    {
                         if(hit.collider.name == "Player")
                        {
                            Debug.Log("Shoot");
                            GameObject enemybulletObject = Instantiate(enemybulletPrefab);
                            enemybulletObject.transform.position = transform.position + (3*transform.forward/4);
                            enemybulletObject.transform.forward = transform.forward;
                        }
                    }  
                    Debug.DrawLine( startPos, targetPos, Color.green );       
                }
                /*Debug.DrawRay(transform.position, _direction * _rayDistance, Color.green);
                Ray ray = new Ray(transform.position, _direction);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, _rayDistance))
                {
                    if(hit.collider.name == "Player")
                    {
                        Debug.Log("Shoot");
                        GameObject enemybulletObject = Instantiate(enemybulletPrefab);
                        enemybulletObject.transform.position = transform.position + (3*transform.forward/4);
                        enemybulletObject.transform.forward = transform.forward;
                    }
                } */
            }
            break;
        }
        
    }

    private bool NeedDestination()
    {
        if(_destination == Vector3.zero)
        {
            return true;
        }
        var distance = Vector3.Distance(transform.position, _destination);
        if(distance < _stoppingDistance)
        {
            return true;
        }
        return false;
    }

    void GetDestination()
    {
        testPosition = (transform.position + (transform.forward * 0.25f)) +
                                new Vector3(UnityEngine.Random.Range(-0.1f,0.1f), 0f, 
                                UnityEngine.Random.Range(-0.1f,0.1f));
        
        _destination = new Vector3(testPosition.x, 0.25f, testPosition.z);

        _direction = Vector3.Normalize(_destination - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);

    }

   
    void OnTriggerEnter(Collider colliderInfo)
    {
        if(colliderInfo.GetComponent<Collider>().tag == "Boder")
        {
            Debug.Log("Turn");
            
            transform.Rotate(0f, 180f, 0f); 
            

        }
    } 
}
