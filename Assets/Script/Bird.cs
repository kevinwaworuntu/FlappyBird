using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    //Menampung referensi shot yang ingin di buat
    [SerializeField]
    private Shot shotRef;
    //Menampung ground sebelumnya
    private Shot prevShot;

    //Global Variables
    [SerializeField] private int score;
    [SerializeField] private Text scoreText;
    
    [SerializeField] private float upForce = 100;
    [SerializeField] private bool isDead;
    [SerializeField] private UnityEvent OnJump, OnDead, OnAddPoint, OnShot;
    private Rigidbody2D rigidBody2d;
    private Animator animator;

    //init variable
    void Start()    
    {
        //Mendapatkan komponent ketika game baru berjalan
        rigidBody2d = GetComponent<Rigidbody2D>();
        //Mendapatkan komponen animator pada game object   
        animator = GetComponent<Animator>();
    }
    //Update setiap frame  
    void Update()
    {
        //Melakukan pengecekan jika belum mati dan klik kiri pada mouse
        if (!isDead && Input.GetMouseButtonDown(0))
        {
            //Burung meloncat
            Jump();
            

        }
        else if (!isDead && Input.GetButtonDown("Fire1"))
        {
            //Burung menembak
            SpawnShot();
        }
    }
    //Fungsi untuk mengecek sudah mati apa belum
    public bool IsDead()
    {
        return isDead;
    }

    //Membuat Burung Mati
    public void Dead()
    {
        //Pengecekan jika belum mati dan value OnDead tidak sama dengan Null
        if (!isDead && OnDead != null)
        {
            //Memanggil semua event pada OnDead
            OnDead.Invoke();
        }

        //Mengeset variable Dead menjadi True
        isDead = true;

    }
    //Method ini akan membuat Ground game object baru
    void SpawnShot()
    {
        //menduplikasi Groundref
        Shot newShot = Instantiate(shotRef);

        //mengaktifkan game object
        newShot.gameObject.SetActive(true);

        //menempatkan new shot dengan posisi nextshot dari prevshot agar posisinya sejajar dengan shot sebelumnya
        shotRef.SetNextShot(newShot.gameObject);
        OnShot.Invoke();


    }
        void Jump()
        {
            //Mengecek rigidbody null atau tidak
            if (rigidBody2d)
            {
                //menghentikan kecepatan burung ketika jatuh
                rigidBody2d.velocity = Vector2.zero;

                //Menambahkan gaya ke arah sumbu y agar burung meloncat
                rigidBody2d.AddForce(new Vector2(0, upForce));
            }

            //Pengecekan Null variable
            if (OnJump != null)
            {
                //Menjalankan semua event OnJump event
                OnJump.Invoke();
            }
        }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //menghentikan Animasi Burung ketika bersentukan dengan object lain
        animator.enabled = false;
    }
    public void AddScore(int value)
    {
        //Menambahkan Score value
        score += value;

        //Mengubah nilai text pada score text
        scoreText.text = score.ToString();

        //Pengecekan Null Value
        if (OnAddPoint != null)
        {
            //Memanggil semua event pada OnAddPoint
            OnAddPoint.Invoke();
        }
    }
}