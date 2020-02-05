using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : MonoBehaviour {
    public Player.Player player;
    public Vector3 moveDirection = Vector3.zero;
    public Rigidbody body;

    public Vector3 throwRotation = new Vector3(0, 90f, 0);
    public Vector3 rotRandomRanges = Vector3.zero;
    public float speedVariance = 2f;
    public bool disableMovement = false;

    public bool doDecay = true;
    public float decayTime = 5000f;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player.Player>();
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Throw(GameObject source, float force) {
        transform.parent = null;
        body.velocity = Vector3.zero;

        transform.position = source.transform.position + (source.transform.forward * 1.2f);
        transform.rotation = player.Cam.transform.rotation;
        transform.Rotate(new Vector3(
            Random.Range(rotRandomRanges.x * -1, rotRandomRanges.x) + throwRotation.x,
            Random.Range(rotRandomRanges.y * -1, rotRandomRanges.y) + throwRotation.y,
            Random.Range(rotRandomRanges.z * -1, rotRandomRanges.z) + throwRotation.z
        ));
        body.velocity = transform.right * (force + Random.Range(speedVariance*-1, speedVariance)) * -1;

        player.log("Melon speed: " + body.velocity.magnitude);

        body.isKinematic = disableMovement;
    }


}
