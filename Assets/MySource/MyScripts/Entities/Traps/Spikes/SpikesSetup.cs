using UnityEngine;

public class SpikesSetup : BaseMonoBehaviour
{
    public int spikeCount = 10;
    public float spacing = 0.58f;
    public GameObject SpikesModel;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpikeModel();
    }

    private void LoadSpikeModel()
    {
        if (SpikesModel != null) return;

        this.SpikesModel = transform.GetChild(0).gameObject;
        Debug.LogWarning("LoadSpikeModel", gameObject);
    }
}
