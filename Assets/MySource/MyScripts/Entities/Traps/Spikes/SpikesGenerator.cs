using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesGenerator : BaseMonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private SpikesSetup spikesSetup;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchComponent<BoxCollider2D>(ref this.boxCollider2D, gameObject);
        UntilityHelper.AutoFetchComponent<SpikesSetup>(ref this.spikesSetup, gameObject);
        this.GenerateSpikesModel();
    }

    private void GenerateSpikesModel()
    {
        int generateCount = spikesSetup.spikeCount - transform.childCount;
        if (generateCount == 0) return;

        while (generateCount > 0)
        {
            GameObject newSpike = GameObject.Instantiate(spikesSetup.SpikesModel);

            Vector2 pos = spikesSetup.SpikesModel.transform.position;
            pos.x = pos.x + spikesSetup.spacing * (spikesSetup.spikeCount - generateCount);

            newSpike.transform.position = pos;
            newSpike.transform.parent = transform;
            newSpike.name = spikesSetup.SpikesModel.name;

            generateCount--;
        }

        while (generateCount < 0)
        {
            GameObject spikeObj = transform.GetChild(transform.childCount - 1).gameObject;
            DestroyImmediate(spikeObj);
            generateCount++;
        }

        this.AdjustCollider();
    }

    private void AdjustCollider()
    {
        this.boxCollider2D.size = new Vector2(spikesSetup.spacing * spikesSetup.spikeCount, this.boxCollider2D.size.y);
        this.boxCollider2D.offset = new Vector2((spikesSetup.spikeCount - 1) * (spikesSetup.spacing / 2), this.boxCollider2D.offset.y);
    }
}
