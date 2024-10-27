using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCollectableBehaviour : MonoBehaviour
{
    [SerializeField] private float _manaAmount;
    public void OnCollected(GameObject player){
            player.GetComponent<PlayerMana>().BuffMana(_manaAmount);
    }
}
