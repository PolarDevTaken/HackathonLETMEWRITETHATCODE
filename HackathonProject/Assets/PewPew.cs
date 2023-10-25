using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class PewPew : MonoBehaviour
{
    public GameObject glock;
    public Animator gunAnim;
    public ParticleSystem ImpactParticleSystem;
    public ParticleSystem ShootingSystem;
    public Transform BulletSpawnPoint;
    public TrailRenderer BulletTrail;
    public float fireRate = 0.15f;
    public float damage = 25f;
    private float LastShootTime;
    public GameObject player;
    public Camera wepCamera;
    public AudioClip glockFireSound;
    public AudioClip glockReloadSound;
    public float maxAmmo = 15;
    public float currentAmmo = 15;
    public float reloadTime;
    public bool isReloading;
    // Start is called before the first frame update
    void Start()
    {
        gunAnim.updateMode = AnimatorUpdateMode.Normal;
        gunAnim.Play("IdleAnimation", -1, 0f);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading)
            {
                gunAnim.SetTrigger("reload");
                StartCoroutine(waiter());
            }

        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("MOSUEPRESSED");
            Shoot();
        }



        IEnumerator waiter()
        {
            isReloading = true;
            //AudioSource sourceaudio = GetComponent<AudioSource>();
            //sourceaudio.PlayOneShot(glockReloadSound);
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = maxAmmo;
            isReloading = false;
        }
    }
    public void Shoot()
    {
        if (LastShootTime + fireRate < Time.time && currentAmmo > 0 && !isReloading)
        {
            print("shotOnce");
            currentAmmo = currentAmmo - 1;
            //AudioSource sourceaudio = GetComponent<AudioSource>();
            //sourceaudio.PlayOneShot(glockFireSound);

            gunAnim.SetTrigger("fire");
            //  ShootingSystem.Play();
            Vector3 direction = GetDirection();
            Ray ray = wepCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            LastShootTime = Time.time;
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Debug.Log(objectHit);

                Debug.Log("postRaycast");
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit));
                if (objectHit.parent.GetComponent<enemyHP>())
                {
                    enemyHP hpScript = objectHit.parent.GetComponent<enemyHP>();
                    hpScript.TakeDamage(damage);
                }


            }


        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = wepCamera.transform.forward;
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        Trail.gameObject.SetActive(true);
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;
            yield return null;
        }
        Trail.transform.position = Hit.point;
        ParticleSystem spawned = Instantiate(ImpactParticleSystem, Hit.point, Quaternion.LookRotation(Hit.normal));
        spawned.transform.SetParent(Hit.transform);
        spawned.Play();



    }
}
