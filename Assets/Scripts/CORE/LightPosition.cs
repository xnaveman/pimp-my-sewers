using System.Collections.Generic;
using UnityEngine;

public class LightPosition : MonoBehaviour
{
    public Transform target;
    public Vector3 rotationOffset;
    public float delayAmount = 0.1f;

    private class Snapshot
    {
        public float time;
        public Vector3 position;
        public Quaternion rotation;
    }

    private List<Snapshot> snapshots = new List<Snapshot>();

    private void Update()
    {
        if (target == null)
            return;

        Snapshot snap = new Snapshot();
        snap.time = Time.time;
        snap.position = target.position;
        snap.rotation = target.rotation * Quaternion.Euler(rotationOffset);
        snapshots.Add(snap);

        float minTime = Time.time - delayAmount - 1f;
        while (snapshots.Count > 0 && snapshots[0].time < minTime)
        {
            snapshots.RemoveAt(0);
        }

        float desiredTime = Time.time - delayAmount;

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
            float lerpT = (desiredTime - older.time) / (newer.time - older.time);
            Vector3 delayedPos = Vector3.Lerp(older.position, newer.position, lerpT);
            Quaternion delayedRot = Quaternion.Slerp(older.rotation, newer.rotation, lerpT);
            transform.position = delayedPos;
            transform.rotation = delayedRot;
        }
        else if (snapshots.Count > 0)
        {
            transform.position = snapshots[0].position;
            transform.rotation = snapshots[0].rotation;
        }
    }
}

