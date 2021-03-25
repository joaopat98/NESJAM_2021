using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShot : MonoBehaviour
{
    [Header ("Charge Shot")]
    [SerializeField] private float midChargeTime;
    [SerializeField] private float fullChargeTime;
    private float chargeTimer;

    public void ResetTimer(){ chargeTimer = 0; }

    public void IncreaseTimer(float value = -1){
        if (value == -1){ chargeTimer += Time.deltaTime; }
        else{ chargeTimer += value; }
    }

    public bool GetMidChargedShot(){ return (chargeTimer >= midChargeTime); }

    public bool GetFullChargedShot(){ return (chargeTimer >= fullChargeTime); }
}
