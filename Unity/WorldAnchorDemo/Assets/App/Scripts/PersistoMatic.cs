using UnityEngine;
using UnityEngine.VR.WSA.Persistence;
using UnityEngine.VR.WSA;

public class PersistoMatic : MonoBehaviour
{

    public string ObjectAnchorStoreName;

    WorldAnchorStore anchorStore;

    bool Placing = false;
    // Use this for initialization
    void Start()
    {
        Debug.Log("WorldAnchorStore.GetAsync()");
        WorldAnchorStore.GetAsync(AnchorStoreReady);
    }

    void AnchorStoreReady(WorldAnchorStore store)
    {
        anchorStore = store;
        Placing = true;

        Debug.Log("looking for " + ObjectAnchorStoreName);
        string[] ids = anchorStore.GetAllIds();
        for (int index = 0; index < ids.Length; index++)
        {
            Debug.Log(ids[index]);
            if (ids[index] == ObjectAnchorStoreName)
            {
                WorldAnchor wa = anchorStore.Load(ids[index], gameObject);
                Placing = false;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Placing)
        {
            gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            OnSelect();
        }
    }

    void OnSelect()
    {
        if (anchorStore == null)
        {
            return;
        }

        if (Placing)
        {
            WorldAnchor attachingAnchor = gameObject.AddComponent<WorldAnchor>();
            if (attachingAnchor.isLocated)
            {
                Debug.Log("Saving persisted position immediately");
                bool saved = anchorStore.Save(ObjectAnchorStoreName, attachingAnchor);
                Debug.Log("saved: " + saved);
            }
            else
            {
                attachingAnchor.OnTrackingChanged += AttachingAnchor_OnTrackingChanged;
            }
        }
        else
        {
            WorldAnchor anchor = gameObject.GetComponent<WorldAnchor>();
            if (anchor != null)
            {
                DestroyImmediate(anchor);
            }

            string[] ids = anchorStore.GetAllIds();
            for (int index = 0; index < ids.Length; index++)
            {
                Debug.Log(ids[index]);
                if (ids[index] == ObjectAnchorStoreName)
                {
                    bool deleted = anchorStore.Delete(ids[index]);
                    Debug.Log("deleted: " + deleted);
                    break;
                }
            }
        }

        Placing = !Placing;
    }

    private void AttachingAnchor_OnTrackingChanged(WorldAnchor self, bool located)
    {
        if (located)
        {
            Debug.Log("Saving persisted position in callback");
            bool saved = anchorStore.Save(ObjectAnchorStoreName, self);
            Debug.Log("saved: " + saved);
            self.OnTrackingChanged -= AttachingAnchor_OnTrackingChanged;
        }
    }
}
