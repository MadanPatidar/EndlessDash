Shader "Custom/CurvedWorld" 
{
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" { }
        _CurvatureX("Curvature X", Float) = 0.001
        _CurvatureY("Curvature Y", Float) = 0.001
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200


    CGPROGRAM
#pragma surface surf Lambert vertex:vert addshadow

        sampler2D _MainTex;
                float _CurvatureX;
                float _CurvatureY;


        struct Input
    {
        float2 uv_MainTex;
    };

    void vert(inout appdata_full v)
    {
        // Transform the vertex coordinates from model space into world space
        float4 worldPosition = mul(unity_ObjectToWorld, v.vertex);

        // Adjust the coordinates to be relative to the camera position
        worldPosition.xyz -= _WorldSpaceCameraPos.xyz;

        // Apply different curvature in X and Y axis
        float distanceSquared = worldPosition.z * worldPosition.z;
        worldPosition.x += distanceSquared * _CurvatureX;
        worldPosition.y += distanceSquared * _CurvatureY;

        // Apply the offset back to the vertices in model space
        v.vertex = mul(unity_WorldToObject, worldPosition);
    }

    void surf(Input IN, inout SurfaceOutput o)
    {
        half4 c = tex2D(_MainTex, IN.uv_MainTex);
        o.Albedo = c.rgb;
        o.Alpha = c.a;
    }
    ENDCG
        }
            FallBack "Diffuse"
}