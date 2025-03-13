using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSphere : MonoBehaviour
{
    public int dégâts = 20;
    private bool active = true;

    void OnTriggerEnter(Collider col){
        GameManager gm = GameManager.instance;
        HudManager hud = HudManager.instance;
        if(col.gameObject.tag == "Player" && active){
            gm.Coucou();
            
            active = false;
            hud.subPV(dégâts);
            Destroy(this.gameObject);
        }
    }
}
