// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "lit/Kelp"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Strength("Strength", Float) = 1.0
        _Speed("Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        //LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            //Vertex input
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            //Vertex output
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float4 _MainTex_ST;
            float _Strength;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;

                v.vertex.x += (sin((( v.vertex.y * 2) + _Time.y * _Speed)) * _Strength); 
                //v.vertex.z += (sin((v.vertex.y + _Time.y * _Speed)) * _Strength); 

                o.pos = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag (v2f i, UNITY_VPOS_TYPE screenPos : SV_POSITION) : SV_Target
            {
                screenPos.xy = floor(screenPos.xy * 0.25) * 0.5;
                //float checker = -frac(screenPos.r + screenPos.g);
                //clip(checker); 

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col * _Color;
            }
            ENDCG
        }
    }
}
