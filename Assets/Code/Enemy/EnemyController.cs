using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    PlayerMove playerS;
    public int minDamage;
    public int maxDamage;

    PlayerHealth health;
    private void Start(){
        health = GetComponent<PlayerHealth>();
    }
    public void TakeDamage(int damage){
        health.TakeDamage(damage);

    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            playerS = collision.GetComponent<PlayerMove>();
            InvokeRepeating("DamagePlayer", 0 ,0.1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            playerS = null;
            CancelInvoke("DamagePlayer");
        }
    }
    void DamagePlayer(){
        int damage = UnityEngine.Random.Range(minDamage, maxDamage);
        Debug.Log("Player take dama" + damage);
    }
}
