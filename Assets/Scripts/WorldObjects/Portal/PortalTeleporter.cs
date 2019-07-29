using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour {

	public Transform player;
	public Transform reciever;

    public static bool cooldown;

	private bool playerIsOverlapping = false;

    private CameraController cam;

    void Start()
    {
        cam = GameObject.FindObjectOfType<CameraController>();
    }

	// Update is called once per frame
	void Update () {
		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			// If this is true: The player has moved across the portal
			if (dotProduct < 0f)
			{
                // Teleport him!
                cam.translationSmoothing = 0;
				float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
				rotationDiff += 180;
				player.Rotate(Vector3.up, rotationDiff);

				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
				player.position = reciever.position + positionOffset;

				playerIsOverlapping = false;
                cooldown = true;
                Invoke("Cooldown", 2);
			}
		}
	}

    void Cooldown()
    {
        cam.translationSmoothing = 10;
        cooldown = false;
    }

	void OnTriggerEnter (Collider other)
	{
        if (cooldown == false)
        {
            if (other.tag == "Player")
            {
                playerIsOverlapping = true;
            }
        }
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
		}
	}
}
