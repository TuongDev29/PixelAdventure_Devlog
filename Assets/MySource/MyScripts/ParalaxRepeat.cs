using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParalaxRepeat : BaseMonoBehaviour
{
    [SerializeField] protected Transform cam;
    [SerializeField] protected Material mat;
    [Range(0.4f, 1f)][SerializeField] private float paralaxSpeed = 0.6f;
    [Range(0, 0.2f)][SerializeField] private float idleScrollSpeedX = 0.2f;
    [Range(0, 0.2f)][SerializeField] private float idleScrollSpeedY = 0;
    [SerializeField] private Vector2 accumulatedOffset = Vector2.zero;
    private Vector2 previousCamPos;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadMaterial();
        this.LoadMainCam();
    }

    private void LoadMaterial()
    {
        if (this.mat != null) return;
        this.mat = GetComponent<Renderer>().material;

        Debug.LogWarning($"{transform.name}: LoadMaterial", gameObject);
    }

    private void LoadMainCam()
    {
        if (this.cam != null) return;
        this.cam = Camera.main.transform;

        Debug.LogWarning($"{transform.name}: LoadMainCam", gameObject);
    }

    protected virtual void Start()
    {
        this.previousCamPos = cam.position;
    }

    protected virtual void Update()
    {
        // Tính khoảng cách di chuyển của camera
        float deltaX = cam.position.x - this.previousCamPos.x;
        float deltaY = cam.position.y - this.previousCamPos.y;

        // Tích lũy offset dựa trên di chuyển của camera và tốc độ idle
        accumulatedOffset.x += deltaX * (paralaxSpeed) + this.idleScrollSpeedX * Time.deltaTime;
        accumulatedOffset.y += deltaY * (paralaxSpeed) + this.idleScrollSpeedY * Time.deltaTime;

        // Giới hạn offset để lặp lại texture một cách mượt mà
        float offsetX = Mathf.Repeat(accumulatedOffset.x, 1f);
        float offsetY = Mathf.Repeat(accumulatedOffset.y, 1f);

        // Cập nhật texture offset
        mat.SetTextureOffset("_MainTex", accumulatedOffset);

        transform.position = new Vector2(cam.position.x, cam.position.y);

        this.previousCamPos = cam.position;
    }
}
