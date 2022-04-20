Shader "Custom/Portal3"
{
    Subshader{

        Zwrite Off
        ColorMask 0

        Stencil{
            Ref 3
            Comp always
            Pass replace
        }

        Pass{

        }
    }
}
