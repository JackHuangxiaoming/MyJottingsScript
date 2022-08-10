using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class FairyGUI_NTexture_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(FairyGUI.NTexture);
            args = new Type[]{};
            method = type.GetMethod("get_nativeTexture", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_nativeTexture_0);

            field = type.GetField("uvRect", flag);
            app.RegisterCLRFieldGetter(field, get_uvRect_0);
            app.RegisterCLRFieldSetter(field, set_uvRect_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_uvRect_0, AssignFromStack_uvRect_0);


        }


        static StackObject* get_nativeTexture_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.NTexture instance_of_this_method = (FairyGUI.NTexture)typeof(FairyGUI.NTexture).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.nativeTexture;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static object get_uvRect_0(ref object o)
        {
            return ((FairyGUI.NTexture)o).uvRect;
        }

        static StackObject* CopyToStack_uvRect_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.NTexture)o).uvRect;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_uvRect_0(ref object o, object v)
        {
            ((FairyGUI.NTexture)o).uvRect = (UnityEngine.Rect)v;
        }

        static StackObject* AssignFromStack_uvRect_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Rect @uvRect = (UnityEngine.Rect)typeof(UnityEngine.Rect).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)16);
            ((FairyGUI.NTexture)o).uvRect = @uvRect;
            return ptr_of_this_method;
        }



    }
}
