using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public List<GameObject> easyRoads;
    public List<GameObject> hardRoads;

    public bool useHardRoads = false; // toggle for testing

    private Transform lastExitPoint;

    void Start()
    {
        SpawnInitialRoad();
        SpawnNextRoad();
    }

    void SpawnInitialRoad()
    {
        GameObject firstRoad = Instantiate(GetRandomRoad(), Vector3.zero, Quaternion.identity);

        lastExitPoint = firstRoad.transform.Find("ExitPoint");
    }

    public void SpawnNextRoad()
    {
        GameObject newRoad = Instantiate(GetRandomRoad());

        Transform entry = newRoad.transform.Find("EntryPoint");
        Transform exit = newRoad.transform.Find("ExitPoint");

        if (entry == null || exit == null)
        {
            Debug.LogError("EntryPoint or ExitPoint missing on road prefab!");
            return;
        }

        // 🔥 Proper socket-to-socket alignment

        // 1️⃣ Match rotation
        newRoad.transform.rotation =
            lastExitPoint.rotation *
            Quaternion.Inverse(entry.localRotation);

        // 2️⃣ Match position
        newRoad.transform.position =
            lastExitPoint.position -
            (newRoad.transform.rotation * entry.localPosition);

        // Update last exit reference
        lastExitPoint = exit;
    }

    GameObject GetRandomRoad()
    {
        if (useHardRoads && hardRoads.Count > 0)
            return hardRoads[Random.Range(0, hardRoads.Count)];

        if (easyRoads.Count > 0)
            return easyRoads[Random.Range(0, easyRoads.Count)];

        Debug.LogError("No roads assigned!");
        return null;
    }
}