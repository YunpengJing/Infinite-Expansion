// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.21 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.21;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:2,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32701,y:32821,varname:node_1,prsc:2|custl-1494-OUT,voffset-1499-OUT;n:type:ShaderForge.SFN_ViewVector,id:1486,x:31161,y:33128,varname:node_1486,prsc:2;n:type:ShaderForge.SFN_Color,id:1488,x:31898,y:32427,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7418,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_NormalVector,id:1489,x:31161,y:32978,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:1490,x:31443,y:33059,varname:node_1490,prsc:2,dt:0|A-1489-OUT,B-1486-OUT;n:type:ShaderForge.SFN_Clamp01,id:1491,x:31684,y:33059,varname:node_1491,prsc:2|IN-1490-OUT;n:type:ShaderForge.SFN_Power,id:1492,x:31898,y:33046,cmnt:Glow,varname:node_1492,prsc:2|VAL-1491-OUT,EXP-1930-OUT;n:type:ShaderForge.SFN_Multiply,id:1494,x:32134,y:32730,cmnt:Final Glow,varname:node_1494,prsc:2|A-1488-RGB,B-1501-OUT,C-1492-OUT;n:type:ShaderForge.SFN_Vector1,id:1497,x:31657,y:33230,varname:node_1497,prsc:2,v1:15;n:type:ShaderForge.SFN_NormalVector,id:1498,x:32159,y:33145,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:1499,x:32410,y:33145,varname:node_1499,prsc:2|A-1498-OUT,B-1512-OUT;n:type:ShaderForge.SFN_Slider,id:1501,x:31532,y:32563,ptovrint:False,ptlb:Fall Off,ptin:_FallOff,varname:node_3664,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_RemapRange,id:1512,x:32172,y:33332,varname:node_1512,prsc:2,frmn:0,frmx:1,tomn:0.01,tomx:0.03|IN-1520-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1520,x:31933,y:33383,ptovrint:False,ptlb:Size,ptin:_Size,varname:node_8143,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.025;n:type:ShaderForge.SFN_Slider,id:1930,x:31467,y:33378,ptovrint:False,ptlb:Edge,ptin:_Edge,varname:node_1930,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:15;proporder:1488-1520-1501-1930;pass:END;sub:END;*/

Shader "TowerDefenseKit/CrystalHalo" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Size ("Size", Float ) = 0.025
        _FallOff ("Fall Off", Range(0, 2)) = 1
        _Edge ("Edge", Range(0, 15)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+2"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _FallOff;
            uniform float _Size;
            uniform float _Edge;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                v.vertex.xyz += (v.normal*(_Size*0.02+0.01));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
                float3 node_1494 = (_Color.rgb*_FallOff*pow(saturate(dot(i.normalDir,viewDirection)),_Edge)); // Final Glow
                float3 finalColor = node_1494;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            uniform float _Size;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                v.vertex.xyz += (v.normal*(_Size*0.02+0.01));
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
