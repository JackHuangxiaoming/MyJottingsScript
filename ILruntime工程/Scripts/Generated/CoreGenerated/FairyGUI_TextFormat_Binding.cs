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
    unsafe class FairyGUI_TextFormat_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(FairyGUI.TextFormat);

            field = type.GetField("letterSpacing", flag);
            app.RegisterCLRFieldGetter(field, get_letterSpacing_0);
            app.RegisterCLRFieldSetter(field, set_letterSpacing_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_letterSpacing_0, AssignFromStack_letterSpacing_0);
            field = type.GetField("lineSpacing", flag);
            app.RegisterCLRFieldGetter(field, get_lineSpacing_1);
            app.RegisterCLRFieldSetter(field, set_lineSpacing_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_lineSpacing_1, AssignFromStack_lineSpacing_1);
            field = type.GetField("outline", flag);
            app.RegisterCLRFieldGetter(field, get_outline_2);
            app.RegisterCLRFieldSetter(field, set_outline_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_outline_2, AssignFromStack_outline_2);
            field = type.GetField("outlineColor", flag);
            app.RegisterCLRFieldGetter(field, get_outlineColor_3);
            app.RegisterCLRFieldSetter(field, set_outlineColor_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_outlineColor_3, AssignFromStack_outlineColor_3);


        }



        static object get_letterSpacing_0(ref object o)
        {
            return ((FairyGUI.TextFormat)o).letterSpacing;
        }

        static StackObject* CopyToStack_letterSpacing_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.TextFormat)o).letterSpacing;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_letterSpacing_0(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).letterSpacing = (System.Int32)v;
        }

        static StackObject* AssignFromStack_letterSpacing_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int32 @letterSpacing = ptr_of_this_method->Value;
            ((FairyGUI.TextFormat)o).letterSpacing = @letterSpacing;
            return ptr_of_this_method;
        }

        static object get_lineSpacing_1(ref object o)
        {
            return ((FairyGUI.TextFormat)o).lineSpacing;
        }

        static StackObject* CopyToStack_lineSpacing_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.TextFormat)o).lineSpacing;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_lineSpacing_1(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).lineSpacing = (System.Int32)v;
        }

        static StackObject* AssignFromStack_lineSpacing_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int32 @lineSpacing = ptr_of_this_method->Value;
            ((FairyGUI.TextFormat)o).lineSpacing = @lineSpacing;
            return ptr_of_this_method;
        }

        static object get_outline_2(ref object o)
        {
            return ((FairyGUI.TextFormat)o).outline;
        }

        static StackObject* CopyToStack_outline_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.TextFormat)o).outline;
            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_outline_2(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).outline = (System.Single)v;
        }

        static StackObject* AssignFromStack_outline_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Single @outline = *(float*)&ptr_of_this_method->Value;
            ((FairyGUI.TextFormat)o).outline = @outline;
            return ptr_of_this_method;
        }

        static object get_outlineColor_3(ref object o)
        {
            return ((FairyGUI.TextFormat)o).outlineColor;
        }

        static StackObject* CopyToStack_outlineColor_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.TextFormat)o).outlineColor;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_outlineColor_3(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).outlineColor = (UnityEngine.Color)v;
        }

        static StackObject* AssignFromStack_outlineColor_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Color @outlineColor = (UnityEngine.Color)typeof(UnityEngine.Color).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)16);
            ((FairyGUI.TextFormat)o).outlineColor = @outlineColor;
            return ptr_of_this_method;
        }



    }
}
