using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShot : MonoBehaviour
{
    [Header ("Charge Shot")]
    [SerializeField] private float midChargeTime;
    [SerializeField] private float fullChargeTime;
    [SerializeField]  GameObject NormalChargeBulletPrefab;
    [SerializeField]  GameObject MidChargeBulletPrefab;
    [SerializeField]  GameObject FullChargeBulletPrefab;

    private float chargeTimer;
    private bool timerRunning = false;

    public void StartTimer(){
        chargeTimer = 0;
        timerRunning = true;
    }

    void Update(){
        if (timerRunning)
        {
            chargeTimer += Time.deltaTime;
        }
    }
    
    public Bullet EndTimer(Transform SpawnPoint)
    {
        Bullet bullet;
        if (IsFullCharged())
        { 
            bullet = Instantiate(FullChargeBulletPrefab, SpawnPoint.position, Quaternion.identity).GetComponent<Bullet>();
        }
        else if (IsMidCharged()) 
        { 
            bullet = Instantiate(MidChargeBulletPrefab, SpawnPoint.position, Quaternion.identity).GetComponent<Bullet>(); 
        }
        else 
        {
            bullet = Instantiate(NormalChargeBulletPrefab, SpawnPoint.position, Quaternion.identity).GetComponent<Bullet>();
        }
        bullet.Init(transform.right);

        timerRunning = false;
        return bullet;
    }

    public bool IsMidCharged(){ 
        return (chargeTimer >= midChargeTime); 
    }

    public bool IsFullCharged(){
        return (chargeTimer >= fullChargeTime); 
    }
}
