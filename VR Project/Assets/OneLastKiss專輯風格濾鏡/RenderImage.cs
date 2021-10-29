using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RenderImage : MonoBehaviour
{
    enum type
    {
        Default,
        EdgeDetect,
        LineCatch,
    }

    [SerializeField] Shader renderShader = null;

    [Header("Edge Detect")]
    [SerializeField] float colorWigth = 3f;
    [SerializeField] float normalWight = 8f;
    [SerializeField] float depthWight = 3f;
    [SerializeField] float power = 200f;

    [Header("Line Catch")]
    [SerializeField] float _plusValue = 27.7f;
    [SerializeField][Range(0, 5)] int _radius = 1;
    [SerializeField] float _scale = 20f;

    [Header("Common")]
    [SerializeField] type _rednerType;
    [SerializeField] Texture2D backGruondTexture = null;
    [SerializeField] Color colorStart = Color.red;
    [SerializeField] Color colorEnd = Color.blue;
    [SerializeField] [Range(0, 360)] float angle = 0;
    [SerializeField] float _attenuation = 1;

    Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }
    void Start()
    {
        camera.depthTextureMode = camera.depthTextureMode | DepthTextureMode.DepthNormals;
    }
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (renderShader == null) return;

        Material renderMat = new Material(renderShader);
        renderMat.SetFloat("_colorWigth", colorWigth);
        renderMat.SetFloat("_normalWight", normalWight);
        renderMat.SetFloat("_depthWight", depthWight);
        renderMat.SetFloat("_power", power);

        //
        renderMat.SetFloat("_plusValue", _plusValue);
        renderMat.SetInt("_radius", _radius);
        renderMat.SetFloat("_scale", _scale);

        //
        renderMat.SetInt("_rednerType", (int)_rednerType);
        renderMat.SetTexture("_backGruondTex", backGruondTexture);
        renderMat.SetColor("_colorStart", colorStart);
        renderMat.SetColor("_colorEnd", colorEnd);
        renderMat.SetFloat("_angle", angle);
        renderMat.SetFloat("_attenuation", _attenuation);

        Graphics.Blit(src, dst, renderMat);
    }

}
