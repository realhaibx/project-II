using System;
using UnityEngine;

namespace StackMaker.Code.Script.Model.Entity
{
    public class Firework : MonoBehaviour
    {
        [SerializeField] private ParticleSystem fire;

        private string PLAYER_TAG = "Player";

        private void OnTriggerEnter(Collider other)
        {

            fire.Play();
        }
    }
}