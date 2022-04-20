Shader "Custom/Portal1"
{
    Subshader{

        Zwrite Off
        ColorMask 0



        Stencil{
            Ref 1
            Comp always
            Pass replace
        }

        Pass{

        }
    }
}
