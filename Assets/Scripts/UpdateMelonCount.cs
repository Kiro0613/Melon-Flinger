using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateMelonCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Player.Player player;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("player").GetComponent<Player.Player>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "X " + player.melonCount;
    }
}
