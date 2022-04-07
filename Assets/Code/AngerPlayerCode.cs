using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngerPlayerCode : MonoBehaviour
{
    // Movement
    NavMeshAgent _navAgent;
    Camera mainCam;
    float speed = 5.0f;

    // Shooting
    public Transform spawnPoint;
    public Transform gun;
    public GameObject bulletPrefab;
    int bulletForce = 500;

    void Start() { 
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.speed = speed;
        mainCam = Camera.main;
    }

    private void Update() {

        // Shooting
        if(Input.GetMouseButtonDown(0)) {
            lookMouse();
            GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, transform.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(gun.forward * bulletForce);
        }

        // Movement
        if(Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            if(Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, 200)) {
                _navAgent.destination = hit.point;
            }
            print(_navAgent.speed);
        }
    }

    public void FixedUpdate() {
        lookMouse();
    }

    void lookMouse(){
        RaycastHit hit;
            if(Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition) ,out hit, 200 )){
                Vector3 target = hit.point;
                target.y = spawnPoint.position.y;
                gun.LookAt(target);
            }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Key")){
            PublicVars.keyNum++;
            Destroy(other.gameObject);
        }
    }
}