using Google.Protobuf.Protocol;
using Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager
{
    Dictionary<ulong, GameObject> _objects = new Dictionary<ulong, GameObject>();   

    public void HandleSpawn(ObjectInfo objectInfo)
    {
        if (_objects.ContainsKey(objectInfo.ObjectId))
            return;

        GameObject go = Managers.Resource.Instantiate("Arissa");
        go.transform.position = new Vector3(objectInfo.PositionInfo.X, objectInfo.PositionInfo.Y, objectInfo.PositionInfo.Z);
        _objects.Add(objectInfo.ObjectId, go);
    }

    public void HandleSpawn(S_ENTER_GAME enterGamePacket)
    {
        HandleSpawn(enterGamePacket.Player);
    }

    public void HandleSpawn(S_SPAWN spawnPacket)
    {
        foreach(ObjectInfo obj in spawnPacket.Objects)
            HandleSpawn(obj);
    }

    public void HandleDespawn(S_DESPAWN despawnPacket)
    {
        foreach(ulong id in despawnPacket.ObjectIds)
            HandleDespawn(id);  
    }

    public void HandleDespawn(ulong objectId)
    {
        if (_objects.ContainsKey(objectId) == false)
            return;

        GameObject go = null;
        _objects.TryGetValue(objectId, out go);

        if (go == null)
            return;

        _objects.Remove(objectId);
        Managers.Resource.Destroy(go);
    }

    public void Clear()
    {
        foreach(GameObject obj in _objects.Values)
            Managers.Resource.Destroy(obj);

        _objects.Clear();
    }
}
