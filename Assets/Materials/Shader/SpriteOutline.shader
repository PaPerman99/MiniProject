Shader "Custom/SpriteOutline"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,1,1,1)
        _OutlineThickness ("Outline Thickness", Range(0,0.1)) = 0.03
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _OutlineColor;
            float _OutlineThickness;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);
                OUT.color = IN.color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float2 uv = IN.texcoord;

                // 采样原图四个方向的透明度
                float alpha = tex2D(_MainTex, uv).a;

                // 轮廓采样偏移
                float thickness = _OutlineThickness;

                float alphaUp = tex2D(_MainTex, uv + float2(0, thickness)).a;
                float alphaDown = tex2D(_MainTex, uv + float2(0, -thickness)).a;
                float alphaLeft = tex2D(_MainTex, uv + float2(-thickness, 0)).a;
                float alphaRight = tex2D(_MainTex, uv + float2(thickness, 0)).a;

                // 判断是否在轮廓范围：周围有透明，自己不透明
                bool outline = (alpha == 0) && (alphaUp > 0 || alphaDown > 0 || alphaLeft > 0 || alphaRight > 0);

                if (outline)
                    return _OutlineColor; // 返回描边颜色
                else
                    return tex2D(_MainTex, uv) * IN.color; // 返回原色
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}