using System.Collections.Generic;
using UnityEngine;

public class LightPosition : MonoBehaviour
{
    // Assign the target (e.g., your camera) in the Inspector.
    public Transform target;
    // Optional offset applied to the target's rotation.
    public Vector3 rotationOffset;
    // Delay (in seconds) before the light follows the target.
    public float delayAmount = 0.1f;

    // A simple container for storing a snapshot of the target's state.
    private class Snapshot
    {
        public float time;
        public Vector3 position;
        public Quaternion rotation;
    }

    // List of snapshots
    private List<Snapshot> snapshots = new List<Snapshot>();

    private void Update()
    {
        if (target == null)
            return;

        // Record the current state of the target.
        Snapshot snap = new Snapshot();
        snap.time = Time.time;
        snap.position = target.position;
        snap.rotation = target.rotation * Quaternion.Euler(rotationOffset);
        snapshots.Add(snap);

        // Remove old snapshots to avoid an unbounded list.
        float minTime = Time.time - delayAmount - 1f;
        while (snapshots.Count > 0 && snapshots[0].time < minTime)
        {
            snapshots.RemoveAt(0);
        }

        // Determine the desired timestamp (current time minus delay)
        float desiredTime = Time.time - delayAmount;

        // Interpolate between snapshots that surround the desired time.
        Snapshot older = null, newer = null;
        for (int i = 0; i < snapshots.Count - 1; i++)
        {
            if (snapshots[i].time <= desiredTime && snapshots[i + 1].time >= desiredTime)
            {
                older = snapshots[i];
                newer = snapshots[i + 1];
                break;
            }
        }

        if (older != null && newer != null)
        {
            // Calculate the interpolation factor between the two snapshots.
            float lerpT = (desiredTime - older.time) / (newer.time - older.time);
            Vector3 delayedPos = Vector3.Lerp(older.position, newer.position, lerpT);
            Quaternion delayedRot = Quaternion.Slerp(older.rotation, newer.rotation, lerpT);
            transform.position = delayedPos;
            transform.rotation = delayedRot;
        }
        else if (snapshots.Count > 0)
        {
            // Fallback: if no interpolation pair is found, use the oldest snapshot.
            transform.position = snapshots[0].position;
            transform.rotation = snapshots[0].rotation;
        }
    }
}

