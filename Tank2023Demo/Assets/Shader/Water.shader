Shader "Unlit/Water"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Amplitude("Amplitude", float) = 0
        _Frequency("Frequency", float) = 0
        _Speed("Speed", float) = 0
        _TilingAmplitude("TillingAmplitude",float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Amplitude;
            float _Frequency;
            float _Speed;
            float _TilingAmplitude;


            v2f vert (appdata v)
            {
                v2f o;
                float time = _Time.y * _Speed;

                // Calculate the displacement in the Y axis
                float wave = _Amplitude * sin(v.vertex.z * _Frequency + time);

                // Apply the displacement to the vertex position
                v.vertex.y += wave;

                // Calculate tiling offset
                float tilingOffset = _TilingAmplitude * sin(_Time.y * _Speed);

                // Apply tiling offset to UV coordinates
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv += float2(tilingOffset, tilingOffset);

                // Transform the position to clip space
                o.vertex = UnityObjectToClipPos(v.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
