using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlrUse : MonoBehaviour {
        public Player player;
        public GameObject inv;
        public GameObject melonBase;
        public Watermelon melon;
        public GameObject newMelon;

        [Range(0f, 10f)]
        public float pickupRange = 6f;
        public string pickupInput = "Pickup";
        bool pickupPressed;

        public string throwInput = "Throw";
        bool throwPressed;
        public float throwStrength = 20f;

        public bool infiniteMelons = false;

        private void Awake() {
            player = GetComponent<Player>();
        }

        // Start is called before the first frame update
        void Start() {
            inv = GameObject.FindGameObjectWithTag("Inv");
            melonBase = GameObject.Find("WatermelonProto");
        }

        // Update is called once per frame
        void Update() {
            pickupPressed = Input.GetButtonDown(pickupInput);
            throwPressed = Input.GetButtonDown(throwInput);

            if(pickupPressed) {
                if(Physics.Raycast(player.Cam.transform.position, player.Cam.transform.forward, out RaycastHit hit, pickupRange, 1 << 9)) {
                    player.melonCount++;
                    hit.rigidbody.isKinematic = true;
                    hit.transform.SetParent(inv.transform);
                    hit.transform.localPosition = Vector3.zero;
                }
            }

            if(throwPressed) {
                ThrowMelon();
            }
        }

        void ThrowMelon() {
            if(infiniteMelons) {
                newMelon = Instantiate(melonBase);
                newMelon.name = "Watermelon";
                melon = newMelon.GetComponent<Watermelon>();
            } else if(player.melonCount > 0) {
                melon = inv.GetComponentInChildren<Watermelon>();
                player.melonCount--;
            } else {
                Debug.LogError("No melons in inventory!");
                return;
            }

            melon.Throw(player.Cam.gameObject, throwStrength);
        }
    }
}
