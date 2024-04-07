using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

  public Vector3 minRange;
  public Vector3 maxRange;
  
  public float speed = 1f;
  [SerializeField] float minDistance = 0.1f;
  [SerializeField] private Vector3 targetPos;

  void Start() {
    GenerateNewTarget();
  }

  void Update() {

    if(Vector3.Distance(transform.localPosition, targetPos) < minDistance ) {
        GenerateNewTarget(); 
    }

    // Moverse hacia targetPos 
    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * speed);

  }

  void GenerateNewTarget() {
    
    // Generar nueva posicion aleatoria 
    var x = Random.Range(Mathf.Min(minRange.x, maxRange.x), Mathf.Max(minRange.x, maxRange.x));
    var y = Random.Range(Mathf.Min(minRange.y, maxRange.y), Mathf.Max(minRange.y, maxRange.y));
    var z = Random.Range(Mathf.Min(minRange.z, maxRange.z), Mathf.Max(minRange.z, maxRange.z));
    
    targetPos = new Vector3(x, y, z);  
  }

}