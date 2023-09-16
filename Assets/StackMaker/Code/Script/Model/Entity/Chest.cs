using System;
using UnityEngine;

namespace StackMaker.Code.Script.Model.Entity
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private GameObject closeChest;
        [SerializeField] private GameObject openChest;

        private const string PLAYER_TAG = "Player";
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(PLAYER_TAG))
            {
                closeChest.SetActive(false);
                openChest.SetActive(true);
            }
        }
    }
}