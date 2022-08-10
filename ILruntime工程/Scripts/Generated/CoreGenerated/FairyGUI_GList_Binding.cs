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
    unsafe class FairyGUI_GList_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(FairyGUI.GList);
            args = new Type[]{};
            method = type.GetMethod("SetVirtual", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetVirtual_0);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("set_numItems", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_numItems_1);
            args = new Type[]{};
            method = type.GetMethod("RemoveChildrenToPool", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, RemoveChildrenToPool_2);
            args = new Type[]{};
            method = type.GetMethod("AddItemFromPool", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, AddItemFromPool_3);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("set_columnGap", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_columnGap_4);
            args = new Type[]{};
            method = type.GetMethod("get_onClickItem", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_onClickItem_5);
            args = new Type[]{};
            method = type.GetMethod("get_numItems", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_numItems_6);
            args = new Type[]{};
            method = type.GetMethod("RefreshVirtualList", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, RefreshVirtualList_7);
            args = new Type[]{};
            method = type.GetMethod("ClearSelection", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ClearSelection_8);
            args = new Type[]{typeof(System.Int32), typeof(System.Boolean)};
            method = type.GetMethod("AddSelection", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, AddSelection_9);
            args = new Type[]{typeof(System.Int32), typeof(System.Boolean)};
            method = type.GetMethod("ScrollToView", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ScrollToView_10);
            args = new Type[]{typeof(FairyGUI.VertAlignType)};
            method = type.GetMethod("set_verticalAlign", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_verticalAlign_11);
            args = new Type[]{typeof(FairyGUI.AlignType)};
            method = type.GetMethod("set_align", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_align_12);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("ChildIndexToItemIndex", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ChildIndexToItemIndex_13);
            args = new Type[]{};
            method = type.GetMethod("SelectNone", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SelectNone_14);
            args = new Type[]{typeof(FairyGUI.ListLayoutType)};
            method = type.GetMethod("set_layout", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_layout_15);

            field = type.GetField("itemRenderer", flag);
            app.RegisterCLRFieldGetter(field, get_itemRenderer_0);
            app.RegisterCLRFieldSetter(field, set_itemRenderer_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_itemRenderer_0, AssignFromStack_itemRenderer_0);
            field = type.GetField("itemProvider", flag);
            app.RegisterCLRFieldGetter(field, get_itemProvider_1);
            app.RegisterCLRFieldSetter(field, set_itemProvider_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_itemProvider_1, AssignFromStack_itemProvider_1);
            field = type.GetField("foldInvisibleItems", flag);
            app.RegisterCLRFieldGetter(field, get_foldInvisibleItems_2);
            app.RegisterCLRFieldSetter(field, set_foldInvisibleItems_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_foldInvisibleItems_2, AssignFromStack_foldInvisibleItems_2);
            field = type.GetField("selectionMode", flag);
            app.RegisterCLRFieldGetter(field, get_selectionMode_3);
            app.RegisterCLRFieldSetter(field, set_selectionMode_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_selectionMode_3, AssignFromStack_selectionMode_3);


        }


        static StackObject* SetVirtual_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetVirtual();

            return __ret;
        }

        static StackObject* set_numItems_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @value = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.numItems = value;

            return __ret;
        }

        static StackObject* RemoveChildrenToPool_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.RemoveChildrenToPool();

            return __ret;
        }

        static StackObject* AddItemFromPool_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.AddItemFromPool();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_columnGap_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @value = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.columnGap = value;

            return __ret;
        }

        static StackObject* get_onClickItem_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.onClickItem;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_numItems_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.numItems;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* RefreshVirtualList_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.RefreshVirtualList();

            return __ret;
        }

        static StackObject* ClearSelection_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ClearSelection();

            return __ret;
        }

        static StackObject* AddSelection_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @scrollItToView = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Int32 @index = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.AddSelection(@index, @scrollItToView);

            return __ret;
        }

        static StackObject* ScrollToView_10(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @ani = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Int32 @index = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ScrollToView(@index, @ani);

            return __ret;
        }

        static StackObject* set_verticalAlign_11(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.VertAlignType @value = (FairyGUI.VertAlignType)typeof(FairyGUI.VertAlignType).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)20);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.verticalAlign = value;

            return __ret;
        }

        static StackObject* set_align_12(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.AlignType @value = (FairyGUI.AlignType)typeof(FairyGUI.AlignType).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)20);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.align = value;

            return __ret;
        }

        static StackObject* ChildIndexToItemIndex_13(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @index = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.ChildIndexToItemIndex(@index);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* SelectNone_14(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SelectNone();

            return __ret;
        }

        static StackObject* set_layout_15(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.ListLayoutType @value = (FairyGUI.ListLayoutType)typeof(FairyGUI.ListLayoutType).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)20);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GList instance_of_this_method = (FairyGUI.GList)typeof(FairyGUI.GList).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.layout = value;

            return __ret;
        }


        static object get_itemRenderer_0(ref object o)
        {
            return ((FairyGUI.GList)o).itemRenderer;
        }

        static StackObject* CopyToStack_itemRenderer_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.GList)o).itemRenderer;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_itemRenderer_0(ref object o, object v)
        {
            ((FairyGUI.GList)o).itemRenderer = (FairyGUI.ListItemRenderer)v;
        }

        static StackObject* AssignFromStack_itemRenderer_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            FairyGUI.ListItemRenderer @itemRenderer = (FairyGUI.ListItemRenderer)typeof(FairyGUI.ListItemRenderer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            ((FairyGUI.GList)o).itemRenderer = @itemRenderer;
            return ptr_of_this_method;
        }

        static object get_itemProvider_1(ref object o)
        {
            return ((FairyGUI.GList)o).itemProvider;
        }

        static StackObject* CopyToStack_itemProvider_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.GList)o).itemProvider;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_itemProvider_1(ref object o, object v)
        {
            ((FairyGUI.GList)o).itemProvider = (FairyGUI.ListItemProvider)v;
        }

        static StackObject* AssignFromStack_itemProvider_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            FairyGUI.ListItemProvider @itemProvider = (FairyGUI.ListItemProvider)typeof(FairyGUI.ListItemProvider).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            ((FairyGUI.GList)o).itemProvider = @itemProvider;
            return ptr_of_this_method;
        }

        static object get_foldInvisibleItems_2(ref object o)
        {
            return ((FairyGUI.GList)o).foldInvisibleItems;
        }

        static StackObject* CopyToStack_foldInvisibleItems_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.GList)o).foldInvisibleItems;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static void set_foldInvisibleItems_2(ref object o, object v)
        {
            ((FairyGUI.GList)o).foldInvisibleItems = (System.Boolean)v;
        }

        static StackObject* AssignFromStack_foldInvisibleItems_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Boolean @foldInvisibleItems = ptr_of_this_method->Value == 1;
            ((FairyGUI.GList)o).foldInvisibleItems = @foldInvisibleItems;
            return ptr_of_this_method;
        }

        static object get_selectionMode_3(ref object o)
        {
            return ((FairyGUI.GList)o).selectionMode;
        }

        static StackObject* CopyToStack_selectionMode_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.GList)o).selectionMode;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_selectionMode_3(ref object o, object v)
        {
            ((FairyGUI.GList)o).selectionMode = (FairyGUI.ListSelectionMode)v;
        }

        static StackObject* AssignFromStack_selectionMode_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            FairyGUI.ListSelectionMode @selectionMode = (FairyGUI.ListSelectionMode)typeof(FairyGUI.ListSelectionMode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)20);
            ((FairyGUI.GList)o).selectionMode = @selectionMode;
            return ptr_of_this_method;
        }



    }
}
