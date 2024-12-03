Shader "Nextwave/nxtNoLightJersey" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		_PatternTex ("Pattern Texture (RGB)", 2D) = "white" {}
		_PatternColor ("Pattern Color", Vector) = (1,1,1,1)
		_BumpMap ("Normal map", 2D) = "bump" {}
		_GrayScale ("Gray Scale", Vector) = (0,1,0,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}