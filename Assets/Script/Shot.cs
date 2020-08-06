using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    //Global variables
    [SerializeField] private Bird bird;
    [SerializeField] private float shotSpeed = 4;
    [SerializeField] private Transform nextPos;
    [SerializeField] public GameObject shotGameObject;


    // Update is called once per frame
    void Update()
    {   //Melakukan pengecekan jika burung belum mati
        if (!bird.IsDead())
        {
            transform.Translate(Vector3.right * shotSpeed * Time.deltaTime, Space.World);
        }
    }
    public void SetNextShot(GameObject shot)
    {
        //Pengecekan Null value
        if (shot != null)
        {
            //Menempatkan ground berikutnya pada posisi nextShot
            shot.transform.position = nextPos.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Memusnahkan object ketika bersentuhan
        Destroy(collision.gameObject);
        if (collision.gameObject.tag == "Pipe")
        {
            Destroy(shotGameObject);
        }
        
    }
}
