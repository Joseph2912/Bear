using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalesMovement : MonoBehaviour
{
    [Header("NavMesh")]
    private NavMeshAgent _agent;
    public Transform[] waypoints;
    private int _waypointsIndex;
    private Vector3 _target;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDestination();
        if (Vector3.Distance(transform.position, _target) < 2)
        {
            IterateWaypointsIndex();
            UpdateDestination();
        }
        
    }

    public void UpdateDestination()
    {
        _target = waypoints[_waypointsIndex].position;
        _agent.SetDestination(_target);
    }

    public void IterateWaypointsIndex()
    {
        _waypointsIndex++;
        if( _waypointsIndex == waypoints.Length)
            _waypointsIndex = 0;
    }
}
